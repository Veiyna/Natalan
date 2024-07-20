using Lumina.Excel.GeneratedSheets;
using Shared.Game;
using Shared.SqPack;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;

namespace WorldServer.Game;

public class EventItemAction : Action.Action
{
    public EventItemAction(Character caster, uint actionId, ushort sequence, ulong targetId, WorldPosition sourcePosition, SkillType skillType, uint additionalId) : base(caster, actionId, sequence, targetId, sourcePosition, skillType, additionalId)
    {
        this.EventItemEntry = GameTableManager.EventItem.GetRow(additionalId);
        this.CastTimeMs = (uint)(this.EventItemEntry.CastTime * 1000);
        this.RecastTimeMs = (uint)(ActionData.Recast100ms * 100);
    }

    private EventItem EventItemEntry;
    public override void Execute()
    {
        base.Execute();
        var player = this.Source.ToPlayer;
        player.Event.OnEventItem(this.EventItemEntry.Quest.Row, this.AdditionalId );
    }
}