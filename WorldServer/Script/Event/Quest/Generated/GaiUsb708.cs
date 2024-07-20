// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66439)]
public class GaiUsb708 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 9 entries
  //SEQ_255, 6 entries

  //ACTOR0 = 1006403
  //ACTOR1 = 1007701
  //ACTOR2 = 1007702
  //ENEMY0 = 4292758
  //ENEMY1 = 4293100
  //EOBJECT0 = 2002131
  //EOBJECT1 = 2002509
  //EOBJECT2 = 2002626
  //EOBJECT3 = 2002627
  //EVENTRANGE0 = 4292757
  //EVENTACTIONSEARCH = 1

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BELMONT
        break;
      }
      case 1:
      {
        if( param1 == 4292757 ) // EVENTRANGE0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EVENTRANGE0
        if( param1 == 4292758 ) // ENEMY0 = unknown
        {
          Scene00003(); // Scene00003: Empty(None), id=unknown
          break;
        }
        if( param1 == 4293100 ) // ENEMY1 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002131 ) // EOBJECT0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002509 ) // EOBJECT1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002626 ) // EOBJECT2 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002627 ) // EOBJECT3 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007701 ) // ACTOR1 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007702 ) // ACTOR2 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006403 ) // ACTOR0 = BELMONT
        {
          Scene00013(); // Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BELMONT
          break;
        }
        if( param1 == 2002131 ) // EOBJECT0 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002626 ) // EOBJECT2 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002627 ) // EOBJECT3 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007701 ) // ACTOR1 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007702 ) // ACTOR2 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
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
    if( quest.UI8AL == 2 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb708:66439 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BELMONT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EVENTRANGE0, UI8AL = 2, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00002: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 2);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 2 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00003: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00008: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_1: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00011: Empty(None), id=unknown" );
  }

private void Scene00012() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BELMONT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00017: Empty(None), id=unknown" );
  }

private void Scene00019() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00019: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00020: Empty(None), id=unknown" );
  }

private void Scene00021() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb708:66439 calling Scene00021: Empty(None), id=unknown" );
  }
};
}
