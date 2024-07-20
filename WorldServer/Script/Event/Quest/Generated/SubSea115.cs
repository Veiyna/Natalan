// FFXIVTheMovie.ParserV3.11
// fake IsAnnounce table
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65948)]
public class SubSea115 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002238
  //ACTOR1 = 1002274
  //SEQ0ACTOR0 = 0
  //SEQ1ACTOR1 = 1
  //SEQ1ACTOR1EMOTENO = 99
  //SEQ1ACTOR1EMOTEOK = 100

  private const uint EVENT_ON_TALK = 0;
  private const uint EVENT_ON_EMOTE = 1;
  private const uint EVENT_ON_BNPC_KILL = 2;
  private const uint EVENT_ON_WITHIN_RANGE = 3;
  private const uint EVENT_ON_ENTER_TERRITORY = 4;
  private const uint EVENT_ON_EVENT_ITEM = 5;
  private const uint EVENT_ON_EOBJ_HIT = 6;
  private const uint EVENT_ON_SAY = 7;

  void onProgress(uint type, ulong param1, ulong param2, ulong param3 )
  {
    switch( quest.Sequence )
    {
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=AHTBYRM
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=unknown
        // +Callback Scene00100: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HALDBRODA
        break;
      }
      default:
      {
        player.sendUrgent("Sequence {} not defined. quest.Sequence ");
        break;
      }
    }
  }

  public override void OnGossip(ulong actorId)
  {
    onProgress(EVENT_ON_TALK, actorId, 0, 0 );
  }

  public override void OnEmote(ulong actorId, ushort emoteId)
  {
    player.sendDebug($"emote: {emoteId}");
    onProgress(EVENT_ON_EMOTE, actorId, 0, emoteId );
  }

  public override void OnAreaTrigger(ulong actorId, WorldPosition position)
  {
    onProgress(EVENT_ON_WITHIN_RANGE, actorId, 0, 0 );
  }

  public override void OnEventTerritory()
  {
    onProgress(EVENT_ON_ENTER_TERRITORY, 0, 0, 0 );
  }
  public override void OnEventItem(ulong actorId)
  {
    onProgress(EVENT_ON_EVENT_ITEM, actorId, 0, 0 );
  }
  void checkProgressSeq0()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea115:65948 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=AHTBYRM" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea115:65948 calling Scene00001: Normal(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00100();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00100() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea115:65948 calling Scene00100: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HALDBRODA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 100, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
