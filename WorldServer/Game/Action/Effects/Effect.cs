using WorldServer.Game.Action.Enums;
using WorldServer.Game.Entity;

namespace WorldServer.Game.Action.Effects
{
    public sealed class Effect
    {
        public ActionEffectType Type = ActionEffectType.Nothing;
        public ActionEffectResultFlag Flags = ActionEffectResultFlag.None;
        
        public ulong ExecuteDelay = 100;

        public Action Action;

        public Character Source;
        public Character Target;
        
        public byte Parameter1;
        public byte Parameter2;
        public ushort Parameter3;

        public uint Value;
        public float StatusDuration;
    }
}