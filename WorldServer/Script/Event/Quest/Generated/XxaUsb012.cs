// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(69401)]
public class XxaUsb012 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 3 entries
  //SEQ_2, 2 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006279
  //ACTOR1 = 1004917
  //ENEMY0 = 4292977
  //EOBJECT0 = 2002646
  //EOBJECT1 = 2002043
  //EOBJECT2 = 2002574
  //EVENTACTION0 = 15
  //EVENTACTION1 = 1
  //ITEM0 = 2003010
  //LOCACTOR0 = 1006283
  //LOCPOSACTOR0 = 4256141
  //LOCPOSACTOR10 = 8402456
  //LOCPOSACTOR11 = 8402455
  //QSTACCEPTCHECK01 = 66351
  //QSTACCEPTCHECK02 = 66352
  //QSTACCEPTCHECK03 = 66355

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=LANDENEL
        break;
      }
      case 1:
      {
        if( param1 == 2002646 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4292977 ) // ENEMY0 = unknown
        {
          Scene00005(); // Scene00005: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2002043 ) // EOBJECT1 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( param1 == 2002574 ) // EOBJECT2 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00008(); // Scene00008: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002043 ) // EOBJECT1 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00010(); // Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00011: Normal(Talk, TargetCanMove), id=LANDENEL
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=UODHNUN
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
      quest.Sequence = 3;
      quest.UI8BH = 1;
    }
  }
  void checkProgressSeq3()
  {
    quest.UI8BH = 0;
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00001();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00001() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00003: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00005: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_2: EOBJECT2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00008: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00010() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00011();
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00011: Normal(Talk, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb012:69401 calling Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
