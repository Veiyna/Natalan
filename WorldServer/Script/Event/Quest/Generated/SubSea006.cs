// FFXIVTheMovie.ParserV3.11
// fake IsAnnounce table
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65652)]
public class SubSea006 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000959
  //ACTOR1 = 1003603
  //ENEMY0 = 338
  //ITEM0 = 2000450

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=HLAHONO
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Empty(None), id=unknown
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 4
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=ABYLFARR
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 4
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00004: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HLAHONO
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
    onProgress(EVENT_ON_WITHIN_RANGE, 0, 0, 0 );
  }

  public override void OnEventTerritory()
  {
    onProgress(EVENT_ON_ENTER_TERRITORY, 0, 0, 0 );
  }
  void checkProgressSeq0()
  {
    quest.Sequence = 1;
  }
  void checkProgressSeq1()
  {
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 255;
    quest.UI8BH = 4;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea006:65652 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=HLAHONO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubSea006:65652 calling Scene00001: Empty(None), id=unknown" );
    player.SendQuestMessage(Id, 0, 0, 0, 0 );
    checkProgressSeq1();
  }

private void Scene00002() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubSea006:65652 calling Scene00002: Normal(Talk, TargetCanMove), id=ABYLFARR" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea006:65652 calling Scene00003: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00004();
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea006:65652 calling Scene00004: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HLAHONO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
