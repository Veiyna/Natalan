// FFXIVTheMovie.ParserV3.11
// param used:
//SCENE_6 REMOVED!!
//SCENE_92 REMOVED!!
//SCENE_91 REMOVED!!
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65915)]
public class SubFst058 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 2 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1000436
  //ACTOR1 = 1000748
  //EOBJECT0 = 2000688
  //EOBJECT1 = 2000689
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000191
  //SEQ0ACTOR0 = 0
  //SEQ1ACTOR1 = 1
  //SEQ1EOBJECT0 = 2
  //SEQ1EOBJECT0EVENTACTIONNO = 99
  //SEQ1EOBJECT0EVENTACTIONOK = 100
  //SEQ2EOBJECT0 = 4
  //SEQ2EOBJECT0EVENTACTIONNO = 95
  //SEQ2EOBJECT0EVENTACTIONOK = 96
  //SEQ2EOBJECT1 = 3
  //SEQ2EOBJECT1EVENTACTIONNO = 97
  //SEQ2EOBJECT1EVENTACTIONOK = 98
  //SEQ3ACTOR1 = 5
  //SEQ3ACTOR1NPCTRADENO = 93
  //SEQ3ACTOR1NPCTRADEOK = 94
  //SEQ3EOBJECT0 = 6
  //SEQ3EOBJECT0EVENTACTIONNO = 91
  //SEQ3EOBJECT0EVENTACTIONOK = 92

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=THEODORE
        break;
      }
      case 1:
      {
        if( param1 == 1000748 ) // ACTOR1 = ROSELINE
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=ROSELINE
          }
          break;
        }
        if( param1 == 2000688 ) // EOBJECT0 = unknown
        {
          Scene00099(); // Scene00099: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( param1 == 2000689 ) // EOBJECT1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00097(); // Scene00097: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000688 ) // EOBJECT0 = unknown
        {
          Scene00095(); // Scene00095: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1000748 ) // ACTOR1 = ROSELINE
        {
          Scene00005(); // Scene00005: NpcTrade(Talk, TargetCanMove), id=ROSELINE
          // +Callback Scene00094: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ROSELINE
          break;
        }
        if( param1 == 2000688 ) // EOBJECT0 = unknown
        {
          Scene00093(); // Scene00093: Empty(None), id=unknown
          break;
        }
        break;
      }
      default:
      {
        player.sendUrgent($"Sequence {quest.Sequence} not defined.  ");
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

  public override void OnBNpcKill(BNpc bNpc)
  {
    onProgress(EVENT_ON_BNPC_KILL, bNpc.InstanceId, bNpc.BNpcNameId, 0);
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
    quest.Sequence = 1;
  }
  void checkProgressSeq1()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
      quest.UI8BH = 1;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst058:65915 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=THEODORE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubFst058:65915 calling Scene00001: Normal(Talk, TargetCanMove), id=ROSELINE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00099() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst058:65915 calling Scene00099: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00097() //SEQ_2: EOBJECT1, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubFst058:65915 calling Scene00097: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00095() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst058:65915 calling Scene00095: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00005() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst058:65915 calling Scene00005: NpcTrade(Talk, TargetCanMove), id=ROSELINE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00094();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00094() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst058:65915 calling Scene00094: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ROSELINE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 94, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00093() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst058:65915 calling Scene00093: Empty(None), id=unknown" );
  }
};
}
