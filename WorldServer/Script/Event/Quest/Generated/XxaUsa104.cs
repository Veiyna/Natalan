// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(69394)]
public class XxaUsa104 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 5 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006675
  //ACTOR1 = 1000584
  //ACTOR2 = 1000580
  //ENEMY0 = 179
  //ENEMY1 = 7
  //EOBJECT0 = 2001913
  //EOBJECT1 = 2001914
  //EOBJECT2 = 2001915
  //EVENTACTION0 = 15
  //ITEM0 = 2003005
  //QSTACCEPTCHECK = 66254

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PAPALYMO
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=IMEDIA
        break;
      }
      //seq 2 event item ITEM0 = UI8CH max stack 3
      case 2:
      {
        if( param2 == 179 ) // ENEMY0 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param2 == 7 ) // ENEMY1 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001913 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00006(); // Scene00006: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001914 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001915 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00008(); // Scene00008: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 3
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00009(); // Scene00009: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO
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
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8BL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8AL == 3 )
        {
          quest.UI8BL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8AL = 0 ;
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
    player.sendDebug("XxaUsa104:69394 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("XxaUsa104:69394 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PAPALYMO" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00002: Normal(Talk, TargetCanMove), id=IMEDIA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ENEMY0, UI8BL = 1, <No Flag>
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    checkProgressSeq2();
  }

private void Scene00004() //SEQ_2: ENEMY1, UI8BH = 1, <No Flag>
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    checkProgressSeq2();
  }

private void Scene00006() //SEQ_2: EOBJECT0, UI8AL = 3, Flag8(1)=True
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00006: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_2: EOBJECT1, UI8AL = 3, Flag8(2)=True
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    checkProgressSeq2();
  }

private void Scene00008() //SEQ_2: EOBJECT2, UI8AL = 3, Flag8(3)=True
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00008: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00009: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00010();
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00010() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa104:69394 calling Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
