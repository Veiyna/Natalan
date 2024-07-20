// FFXIVTheMovie.ParserV3.11
// simple method used
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66003)]
public class SubSea055 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002627
  //ACTOR1 = 1002629
  //ACTOR2 = 1002631
  //EOBJECT0 = 2001237
  //EOBJECT1 = 2001238
  //EOBJECT2 = 2001239
  //EVENTACTIONPROCESSMIDDLE = 16
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000347
  //QSTACCEPTCHECK = 66000

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=GURCANT
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=RHOTWYDA
        break;
      }
      //seq 2 event item ITEM0 = UI8CH max stack 3
      case 2:
      {
        if( param1 == 2001237 || param1 == 4298896562 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001238 || param1 == 4298896563 ) // EOBJECT1 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001239 || param1 == 4298896564 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 3
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00005(); // Scene00005: NpcTrade(Talk, TargetCanMove), id=PFREWAHL
        // +Callback Scene00094: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=PFREWAHL
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
  }
  void checkProgressSeq1()
  {
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
        {
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.UI8CH = 0;
          quest.Sequence = 255;
          quest.UI8BH = 3;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea055:66003 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=GURCANT" );
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
    player.sendDebug("SubSea055:66003 calling Scene00001: Normal(Talk, TargetCanMove), id=RHOTWYDA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_2: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubSea055:66003 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00003() //SEQ_2: EOBJECT1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubSea055:66003 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag8( 2, true );
    checkProgressSeq2();
  }

private void Scene00004() //SEQ_2: EOBJECT2, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubSea055:66003 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq2();
  }

private void Scene00005() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea055:66003 calling Scene00005: NpcTrade(Talk, TargetCanMove), id=PFREWAHL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00094();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00094() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea055:66003 calling Scene00094: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=PFREWAHL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 94, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
