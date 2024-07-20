using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DotRecast.Detour.Crowd;
using Shared;
using Shared.SqPack;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Game.Entity;

public abstract class Character : Actor
{
    public ulong LastTick;
    public ulong targetId = 0xE0000000u;

    public Stance Stance = Stance.Passive;

    public float Radius = 1;
        
    public ActorStatus State { get; set; }
    public uint StateParam { get; set; }

    public InvincibilityType InvincibilityType = InvincibilityType.InvincibilityNone;

    public byte ClassJobId => IsPlayer ? ToPlayer.ClassJobId : _classjobid;
    public byte Level => IsPlayer ? (byte)ToPlayer.Level : _level;


    protected byte _level = 90;
    private byte _classjobid = 0;


    public Action.Action CurrentAction;

    public const byte MAX_STATUS_EFFECTS = 30;

    public readonly List<StatusEffect.StatusEffect> StatusEffects = new();
    
    public ushort VisualEffect;

    public uint LastComboActionId = 0;

    public DtCrowdAgent CrowdAgent;
    public uint MaxHP => GetBaseStat(BaseParam.HP);
    public uint MaxMP => GetBaseStat(BaseParam.MP);

    public uint HP;
    public uint MP;
    protected readonly Dictionary<BaseParam, Stat> Stats = new();

    protected Character(uint id, ActorType type) : base(id, type)
    {
    }

    public virtual void Update(double lastTick)
    {
        if (this.CurrentAction?.Update() == true)
        {
            this.CurrentAction = null;
        }
        UpdateStatusEffects(lastTick);
    }


    public void UpdateStatusEffects(double lastTick)
    {
        var time = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var isTick = time - this.LastTick > 3000 || this.LastTick == 0;
        if(isTick)
            this.LastTick = time;
        foreach (var statuseffect in this.StatusEffects.ToList())
        {
            if (isTick)
            {
                statuseffect.OnTick();
                    
            }
            if (statuseffect.Duration > 0 && time - statuseffect.StartTime > statuseffect.Duration*1000)
            {
                RemoveStatusEffect(statuseffect);  
            }
        }
            
            
    }
    public void SetTarget( ulong newTargetId)
    {
        this.targetId = newTargetId;
        SendMessageToVisible(new ServerActorActionTarget
        {
            Action = ActorActionServer.SetTarget,
            Parameter6 = newTargetId,
        });
    }
        
    public void SetStance( Stance newStance)
    {
        this.Stance = newStance;
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.ToggleWeapon,
            Parameter1 = (byte)newStance,
                
        });
    }

    public uint GetBaseStat(BaseParam stat)
    {
        if (this.Stats.TryGetValue(stat, out Stat statValue))
            return (uint)statValue.Value;
        return 0;
    }
    public void SetBaseStat(BaseParam stat, dynamic value)
    {
        if (this.Stats.TryGetValue(stat, out Stat statValue))
            statValue.Value = value;
        else
        {
            statValue = new Stat
            {
                BaseParam = GameTableManager.BaseParam.GetRow((uint)stat),
                Value = (uint)value,
            };
            this.Stats.Add(stat, statValue);
        }
    }
    
    public void SendHPUpdate()
    {
        SendMessageToVisible(new ServerUpdateHpMpTp
        {
            Character = this,
        }, true);
    }

    public virtual void Die()
    {

        if (State != ActorStatus.Dead)
        {
            this.HP = 0;
            this.MP = 0;
            SendHPUpdate();
            SetState(ActorStatus.Dead);

            SendMessageToVisible(new ServerActorAction
            {
                Action = ActorActionServer.DeathAnimation,
                Parameter4 = 0x20
            }, true);
                
        }
    }
    public virtual void OnActionHostile(Character source)
    {
            
    }
    public void Raise()
    {
        if (State != ActorStatus.Dead) return;
        RemoveStatusEffect(148); // raise status
        RemoveStatusEffect(1140); // duel raise status
        this.HP = MaxHP;
        this.MP = MaxMP;
        SendHPUpdate();
        SendMessageToVisible(new ServerActorActionSelf
        {
            Action = ActorActionServer.ZoneIn,
            Parameter2 = 1,
        },true);
        SetState(ActorStatus.Idle);
            
    }
        
    public void SetState(ActorStatus state, byte param = 0)
    {
        State = state;
        StateParam = param;
            
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.SetStatus,
            Parameter1 = (uint)state,
            Parameter2 = param
        }, true);
    }
    public void TakeDamage(uint damage)
    {
        if (State != ActorStatus.Dead)
        {
            if (damage >= this.HP || this.HP == 0)
            {
                switch (this.InvincibilityType)
                {
                    case InvincibilityType.InvincibilityNone:
                    {
                        HP = 0;
                        Die();
                        break;
                    }
                    case InvincibilityType.InvincibilityRefill:
                    {
                        this.HP = MaxHP;
                        break;
                    }
                }

            }
            else
            {
                HP -= damage;
            }
        }
        SendHPUpdate();
    }
    public void Heal(uint heal)
    {
        HP += Math.Min(MaxHP - HP, heal);

        SendHPUpdate();
    }

    public void DeductMP(ushort value)
    {
        MP -= value;
        SendHPUpdate();
    }

    public void RestoreMP(ushort value)
    {
        MP += value;
        SendHPUpdate();
    }
        
    public void SetMP(ushort value)
    {
        MP = value;
        SendHPUpdate();
    }


    public void AddStatusEffect(StatusEffect.StatusEffect effect)
    {
        SendMessageToVisible(new ServerEffectResult
        {
            effect = effect
        }, true);
        effect.OnExecute();
        this.StatusEffects.Add(effect);
    }
        
    public void AddStatusEffect(uint id, Character source , ushort param = 0, float duration = 0)
    {
        var effect = new StatusEffect.StatusEffect(id, source, this, duration, param)
        {
            StartTime = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds()
        };

        SendMessageToVisible(new ServerEffectResult
        {
            index = (byte)this.StatusEffects.Count,
            effect = effect
        }, true);
        effect.OnExecute();
        this.StatusEffects.Add(effect);
    }

    public void RemoveStatusEffect(StatusEffect.StatusEffect statusEffect, bool sendUpdate = true)
    {
        if (statusEffect is null)
            return;
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.StatusEffectLose,
            Parameter1 = statusEffect.StatusId
        }, true);
        statusEffect.OnRemove();
        this.StatusEffects.Remove(statusEffect);
        if(sendUpdate)
            SendStatusEffects();
    }

    public void ClearStatusEffects()
    {
        foreach (var status in this.StatusEffects)
        {
            status.OnRemove();
        }
        this.StatusEffects.Clear();
            
        SendStatusEffects();
    }
        
    public void RemoveStatusEffect(uint id, bool sendUpdate = true)
    {
        var statusEffect = this.StatusEffects.FirstOrDefault(status => status.StatusId == id);
        if (statusEffect is null)
            return;
        SendMessageToVisible(new ServerActorAction
        {
            Action = ActorActionServer.StatusEffectLose,
            Parameter1 = statusEffect.StatusId
        }, true);
        statusEffect.OnRemove();
        this.StatusEffects.Remove(statusEffect);
        if(sendUpdate)
            SendStatusEffects();
    }

    public void SendStatusEffects()
    {
        SendMessageToVisible(new ServerStatusEffectList
        {
            Character = this
        }, true);
    }

    public void SetVisualEffect(ushort effect)
    {
        this.VisualEffect = effect;
        SendMessageToVisible(new ServerCharaVisualEffect
        {
            Effect = effect
        }, true);
    }

    public bool HasStatusEffect(uint id)
    {
        return this.StatusEffects.Any(s => s.StatusId == id);
    }
    
    public bool Face(Vector3 p)
    {
        var position = Position.Offset;
        float oldRot = Position.Orientation;
        float rot = Utilities.CalcAngFrom(position.X, position.Z, p.X, p.Z);
        float newRot = (float)Math.PI - rot + (float)Math.PI / 2;
            
        Position.Relocate(newRot);
            

        return Math.Abs(oldRot - newRot) <= float.Epsilon * Math.Max(Math.Abs(oldRot), Math.Abs(newRot));
    }

}