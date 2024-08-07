// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66351)]
public class GaiUsb008 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006279
  //ENEMY0 = 4292783
  //ENEMY1 = 2194
  //EOBJECT0 = 2002038
  //EOBJECT1 = 2002037
  //EOBJECT2 = 2002039
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONSEARCHMIDDLE = 3
  //ITEM0 = 2000959
  //ITEM1 = 2000638
  //LOCACTOR0 = 1006283
  //LOCPOSACTOR0 = 4256141

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
      //seq 0 event item ITEM0 = UI8BH max stack ?
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=LANDENEL
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( param1 == 2002038 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4292783 ) // ENEMY0 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 2194 ) // ENEMY1 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002037 ) // EOBJECT1 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 2 event item ITEM1 = UI8BH max stack 1
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: Empty(None), id=unknown
        break;
      }
      //seq 255 event item ITEM1 = UI8BH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00009: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LANDENEL
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
      quest.UI8BH = 0;
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 255;
    quest.UI8BH = 1;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb008:66351 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00003: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00005: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00007: Empty(None), id=unknown" );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00008() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00009();
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb008:66351 calling Scene00009: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
