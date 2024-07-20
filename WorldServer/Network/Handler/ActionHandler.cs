using Shared.Network;
using WorldServer.Game.Action;
using WorldServer.Game.Entity.Enums;
using WorldServer.Network.Message;

namespace WorldServer.Network.Handler
{
    public static class ActionHandler
    {
        [SubPacketHandler(SubPacketClientHandlerId.ClientSkill, SubPacketHandlerFlags.RequiresWorld)]
        public static void HandleClientSkill(WorldSession session, ClientSkill skill)
        {
            var skilltype = (SkillType)skill.Type;
            ActionManager.HandleAction(session.Player, skill.ActionId, skill.TargetId, skill.Sequence,skill.ItemContainer, skill.ItemSlot, skilltype);

        }
    }
}