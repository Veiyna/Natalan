using Shared.Database.Datacentre.Models;

namespace WorldServer.Game.Event;

public class QuestScript : EventScript
{
    protected QuestModel quest => this.owner.GetQuest(Id);
}