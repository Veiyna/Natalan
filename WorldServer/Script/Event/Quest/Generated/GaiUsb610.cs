// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66428)]
public class GaiUsb610 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 8 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006388
  //ACTOR1 = 1006386
  //EOBJECT0 = 2002113
  //EOBJECT1 = 2002114
  //EOBJECT2 = 2002115
  //EOBJECT3 = 2002116
  //EOBJECT4 = 2002117
  //EOBJECT5 = 2002118
  //EOBJECT6 = 2002119
  //EOBJECT7 = 2002120
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000698

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PRAIRILLOT
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 8
      case 1:
      {
        if( param1 == 2002113 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002114 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002115 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002116 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002117 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002118 ) // EOBJECT5 = unknown
        {
          if( !quest.getBitFlag8( 6 ) )
          {
            Scene00013(); // Scene00013: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002119 ) // EOBJECT6 = unknown
        {
          if( !quest.getBitFlag8( 7 ) )
          {
            Scene00015(); // Scene00015: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002120 ) // EOBJECT7 = unknown
        {
          if( !quest.getBitFlag8( 8 ) )
          {
            Scene00017(); // Scene00017: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 8
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00018(); // Scene00018: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00019: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STEPHANNOT
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
    if( quest.UI8AL == 8 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.setBitFlag8( 5, false );
      quest.setBitFlag8( 6, false );
      quest.setBitFlag8( 7, false );
      quest.setBitFlag8( 8, false );
      quest.Sequence = 255;
      quest.UI8BH = 8;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb610:66428 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PRAIRILLOT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 8, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT1, UI8AL = 8, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, UI8AL = 8, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT3, UI8AL = 8, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: EOBJECT4, UI8AL = 8, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 5, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_1: EOBJECT5, UI8AL = 8, Flag8(6)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 6, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00015() //SEQ_1: EOBJECT6, UI8AL = 8, Flag8(7)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00015: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 7, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00017() //SEQ_1: EOBJECT7, UI8AL = 8, Flag8(8)=True(Todo:0)
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00017: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 8, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 8 );
    checkProgressSeq1();
  }

private void Scene00018() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00018: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00019();
      }
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00019() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb610:66428 calling Scene00019: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STEPHANNOT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
