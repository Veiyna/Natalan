// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(69403)]
public class XxaUsb503 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006672
  //ACTOR1 = 1033920
  //ACTOR2 = 1003958
  //ITEM0 = 2003014
  //ITEM1 = 2003012
  //ITEM2 = 2003013
  //ITEM3 = 2003015
  //LOCBGM1 = 92
  //QSTACCEPTCHECK01 = 66407
  //QSTACCEPTCHECK02 = 66408

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
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MARQUEZ
        break;
      }
      //seq 1 event item ITEM1 = UI8BH max stack 1
      //seq 1 event item ITEM2 = UI8BL max stack 1
      //seq 1 event item ITEM0 = UI8CH max stack 1
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00003: Normal(Talk, TargetCanMove), id=GOLGSMITHXXXXX
        break;
      }
      //seq 2 event item ITEM1 = UI8BH max stack 1
      //seq 2 event item ITEM2 = UI8BL max stack 1
      //seq 2 event item ITEM3 = UI8CH max stack 1
      case 2:
      {
        if( param1 == 1006672 ) // ACTOR0 = MARQUEZ
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00005: Normal(Talk, FadeIn, TargetCanMove), id=MARQUEZ
          }
          break;
        }
        if( param1 == 1033920 ) // ACTOR1 = GOLGSMITHXXXXX
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=GOLGSMITHXXXXX
          break;
        }
        break;
      }
      //seq 255 event item ITEM3 = UI8BH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ELUNED
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
    quest.UI8BH = 1;
    quest.UI8BL = 1;
    quest.UI8CH = 1;
  }
  void checkProgressSeq1()
  {
    quest.UI8CH = 0;
    quest.Sequence = 2;
    quest.UI8CH = 1;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.UI8BH = 0;
      quest.UI8BL = 0;
      quest.UI8CH = 0;
      quest.Sequence = 255;
      quest.UI8BH = 1;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("XxaUsb503:69403 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MARQUEZ" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00003();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00003: Normal(Talk, TargetCanMove), id=GOLGSMITHXXXXX" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00005();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_2: ACTOR0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00005: Normal(Talk, FadeIn, TargetCanMove), id=MARQUEZ" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00006: Normal(Talk, TargetCanMove), id=GOLGSMITHXXXXX" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00007: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00008();
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsb503:69403 calling Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ELUNED" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
