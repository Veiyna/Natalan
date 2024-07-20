// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66353)]
public class GaiUsb010 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 7 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1006283
  //ACTOR1 = 1006284
  //ACTOR2 = 1006285
  //ACTOR3 = 1007646
  //ENEMY0 = 4292844
  //ENEMY1 = 4292847
  //EOBJECT0 = 2002289
  //EVENTRANGE0 = 4292885
  //EVENTACTIONRESCUEUNDERMIDDLE = 35
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000639

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=DETOHMOSHROCA
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( param1 == 4292885 ) // EVENTRANGE0 = unknown
        {
          Scene00002(); // Scene00002: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 1006284 ) // ACTOR1 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00003(); // Scene00003: Normal(Message), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to ACTOR1
        if( param1 == 4292844 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4292847 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 1006285 ) // ACTOR2 = MERCHANT
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=MERCHANT
          break;
        }
        if( param1 == 1007646 ) // ACTOR3 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002289 ) // EOBJECT0 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1006283 ) // ACTOR0 = DETOHMOSHROCA
        {
          Scene00009(); // Scene00009: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=DETOHMOSHROCA
          break;
        }
        if( param1 == 1006284 ) // ACTOR1 = MERCHANT
        {
          Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=MERCHANT
          break;
        }
        if( param1 == 1006285 ) // ACTOR2 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007646 ) // ACTOR3 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
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
      quest.UI8BH = 1;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb010:66353 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=DETOHMOSHROCA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00002: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 2, Flag8(1)=True
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00003: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 2);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }



private void Scene00004() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00004: Normal(Talk, TargetCanMove), id=MERCHANT" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00005: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00008: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00009: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00010();
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00010() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=DETOHMOSHROCA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00011: Normal(Talk, TargetCanMove), id=MERCHANT" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb010:66353 calling Scene00013: Empty(None), id=unknown" );
  }
};
}
