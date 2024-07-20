// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66463)]
public class GaiUsb904 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 3 entries
  //SEQ_2, 1 entries
  //SEQ_3, 3 entries
  //SEQ_4, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006448
  //ACTOR1 = 1006449
  //ACTOR2 = 1006450
  //ACTOR3 = 1006451
  //ACTOR4 = 1007567
  //ENEMY0 = 4293214
  //ENEMY1 = 2196
  //EOBJECT0 = 2002158
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000961
  //ITEM1 = 2000724
  //ITEM2 = 2000725
  //ITEM3 = 2000984
  //QSTACCEPTCHECK01 = 66461
  //QSTACCEPTCHECK02 = 66462

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HAUSTEFORT
        break;
      }
      //seq 1 event item ITEM1 = UI8BH max stack 1
      case 1:
      {
        if( param1 == 2002158 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4293214 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 2196 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        break;
      }
      //seq 2 event item ITEM1 = UI8BH max stack 1
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00005: Normal(Talk, TargetCanMove), id=HAUSTEFORT
        break;
      }
      //seq 3 event item ITEM2 = UI8CH max stack 1
      case 3:
      {
        if( param1 == 1006449 ) // ACTOR1 = OMBELINE
        {
          if( quest.UI8AL != 1 )
          {
            Scene00006(); // Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00007: Normal(Talk, TargetCanMove), id=OMBELINE
          }
          break;
        }
        if( param1 == 1006450 ) // ACTOR2 = THEOPHILAIN
        {
          if( quest.UI8BH != 1 )
          {
            Scene00008(); // Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00009: Normal(Talk, TargetCanMove), id=THEOPHILAIN
          }
          break;
        }
        if( param1 == 1006451 ) // ACTOR3 = IGNEMORTEL
        {
          if( quest.UI8BL != 1 )
          {
            Scene00010(); // Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00011: Normal(Talk, TargetCanMove), id=IGNEMORTEL
          }
          break;
        }
        break;
      }
      //seq 4 event item ITEM3 = UI8BH max stack 1
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=HAUSTEFORT
        break;
      }
      //seq 255 event item ITEM3 = UI8BH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00013(); // Scene00013: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=CENOTA
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
      quest.UI8BH = 1;
    }
  }
  void checkProgressSeq2()
  {
    quest.UI8BH = 0;
    quest.Sequence = 3;
    quest.UI8CH = 1;
  }
  void checkProgressSeq3()
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
          quest.Sequence = 4;
        }
  }
  void checkProgressSeq4()
  {
    quest.Sequence = 255;
    quest.UI8BH = 1;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("GaiUsb904:66463 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HAUSTEFORT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00003: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }



private void Scene00004() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00005();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00005: Normal(Talk, TargetCanMove), id=HAUSTEFORT" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00007();
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00007: Normal(Talk, TargetCanMove), id=OMBELINE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00009();
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_3: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00009: Normal(Talk, TargetCanMove), id=THEOPHILAIN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00011();
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00011: Normal(Talk, TargetCanMove), id=IGNEMORTEL" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00012: Normal(Talk, TargetCanMove), id=HAUSTEFORT" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00013: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00014();
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00014() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb904:66463 calling Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=CENOTA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
