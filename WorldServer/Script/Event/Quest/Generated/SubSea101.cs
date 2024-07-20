// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65934)]
public class SubSea101 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003239
  //ACTOR1 = 1003240
  //ACTOR2 = 1003241
  //ITEM0 = 2000338
  //ITEM1 = 2000339

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
      //seq 0 event item ITEM0 = UI8BH max stack 1
      //seq 0 event item ITEM1 = UI8BL max stack 1
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=WYRKRHIT
        break;
      }
      //seq 1 event item ITEM0 = UI8BL max stack 1
      //seq 1 event item ITEM1 = UI8CH max stack 1
      case 1:
      {
        if( param1 == 1003240 ) // ACTOR1 = SYNGITHUV
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: NpcTrade(Talk, TargetCanMove), id=SYNGITHUV
            // +Callback Scene00100: Normal(Talk, TargetCanMove), id=SYNGITHUV
          }
          break;
        }
        if( param1 == 1003241 ) // ACTOR2 = JESSAMINE
        {
          if( quest.UI8BH != 1 )
          {
            Scene00002(); // Scene00002: NpcTrade(Talk, TargetCanMove), id=JESSAMINE
            // +Callback Scene00098: Normal(Talk, TargetCanMove), id=JESSAMINE
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      //seq 255 event item ITEM1 = UI8BL max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=WYRKRHIT
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
  void checkProgressSeq0()
  {
    quest.Sequence = 1;
    quest.UI8BL = 1;
    quest.UI8CH = 1;
  }
  void checkProgressSeq1()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
      {
        quest.UI8AL = 0 ;
        quest.UI8BH = 0 ;
        quest.setBitFlag8( 1, false );
        quest.setBitFlag8( 2, false );
        quest.UI8BL = 0;
        quest.UI8CH = 0;
        quest.Sequence = 255;
      }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea101:65934 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=WYRKRHIT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubSea101:65934 calling Scene00001: NpcTrade(Talk, TargetCanMove), id=SYNGITHUV" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00100();
      }
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00100() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubSea101:65934 calling Scene00100: Normal(Talk, TargetCanMove), id=SYNGITHUV" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 100, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubSea101:65934 calling Scene00002: NpcTrade(Talk, TargetCanMove), id=JESSAMINE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00098();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00098() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubSea101:65934 calling Scene00098: Normal(Talk, TargetCanMove), id=JESSAMINE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 98, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea101:65934 calling Scene00003: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=WYRKRHIT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
