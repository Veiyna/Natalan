using System.Collections.Generic;
using WorldServer.Game.Entity;
using WorldServer.Game.Event.Director.Enum;
using WorldServer.Game.Map;
using WorldServer.Network;
using WorldServer.Network.Message;

namespace WorldServer.Game.Event.Director;

public class Director
{
    private DirectorType Type { get; set; }
    public uint DirectorId { get; set; }
    private ushort ContentId { get; set; }
    public ushort ContentFinderConditionId { get; set; }
    private byte Sequence { get; set; } = 1;
    private byte Branch { get; set; }

    private byte[] DirectorVars { get; set; } = new byte[10];

    private Territory Owner;

    public Dictionary<uint, ulong> CustomVariables { get; set; } = new();
    public Director(Territory owner,DirectorType directorType, ushort contentId, ushort contentFinderConditionId)
    {
        Type = directorType;
        ContentId = contentId;
        ContentFinderConditionId = contentFinderConditionId;
        DirectorId = ((uint)Type << 16) | contentId;
        this.Owner = owner;
    }

    public void SendDirectorInit(Player player)
    {
        player.Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.DirectorInit,
            Parameter1 = DirectorId,
            Parameter2 = ContentId
            
        });
    }


    public void OnDirectorSync(Player player)
    {
        player.Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.DirectorUpdate,
            Parameter1 = 0x00110001,
            Parameter2 = 0x80000000,
            Parameter3 = 1
        });
    }

    public void SendDirectorClear(Player player)
    {
        player.Session.Send(new ServerActorActionSelf
        {
            Action = ActorActionServer.DirectorClear,
            Parameter1 = DirectorId
        });
    }

    public void SetVar(byte index, byte value)
    {
        if (index < DirectorVars.Length - 1)
        {
            DirectorVars[index] = value;
        }

        this.Owner.Players.ForEach(SendDirectorVars);
    }

    public byte GetVar(byte index)
    {
        return DirectorVars[index];
    }
    public void SendDirectorVars(Player player)
    {
        player.Session.Send(new ServerDirectorVars
        {
            DirectorId = this.DirectorId,
            Sequence = this.Sequence,
            Branch = this.Branch,
            Data = this.DirectorVars
        });
    }
    
    

    public void SetCustomVar(uint id, ulong value)
    {
        CustomVariables[id] = value;
    }
    
    public ulong GetCustomVar(uint id)
    {
        return CustomVariables.GetValueOrDefault(id);
    }
}