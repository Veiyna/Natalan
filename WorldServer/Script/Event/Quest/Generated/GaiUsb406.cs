// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66398)]
public class GaiUsb406 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 12 entries
  //SEQ_255, 7 entries

  //ACTOR0 = 1006331
  //ACTOR1 = 1007542
  //ACTOR2 = 1007543
  //ACTOR3 = 1007544
  //ACTOR4 = 1007545
  //ACTOR5 = 1007546
  //ACTOR6 = 1007547
  //EOBJECT0 = 2002341
  //EOBJECT1 = 2002342
  //EOBJECT2 = 2002343
  //EOBJECT3 = 2002338
  //EOBJECT4 = 2002339
  //EOBJECT5 = 2002340
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000669
  //ITEM1 = 2000929

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SWYGRAEL
        break;
      }
      //seq 1 event item ITEM0 = UI8BL max stack 3
      //seq 1 event item ITEM1 = UI8CH max stack 3
      case 1:
      {
        if( param1 == 2002341 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag16( 1 ) )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002342 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag16( 2 ) )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002343 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag16( 3 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1007542 ) // ACTOR1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007543 ) // ACTOR2 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007544 ) // ACTOR3 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007545 ) // ACTOR4 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007546 ) // ACTOR5 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007547 ) // ACTOR6 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002338 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag16( 10 ) )
          {
            Scene00015(); // Scene00015: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002339 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag16( 11 ) )
          {
            Scene00017(); // Scene00017: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002340 ) // EOBJECT5 = unknown
        {
          if( !quest.getBitFlag16( 12 ) )
          {
            Scene00019(); // Scene00019: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 3
      //seq 255 event item ITEM1 = UI8BL max stack 3
      case 255:
      {
        if( param1 == 1006331 ) // ACTOR0 = SWYGRAEL
        {
          Scene00020(); // Scene00020: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00021: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SWYGRAEL
          break;
        }
        if( param1 == 1007542 ) // ACTOR1 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007543 ) // ACTOR2 = unknown
        {
          Scene00023(); // Scene00023: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007544 ) // ACTOR3 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007545 ) // ACTOR4 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007546 ) // ACTOR5 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007547 ) // ACTOR6 = unknown
        {
          Scene00027(); // Scene00027: Empty(None), id=unknown
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
    if( quest.UI8AL == 3 )
      if( quest.UI8BH == 3 )
      {
        quest.UI8AL = 0 ;
        quest.UI8BH = 0 ;
        quest.setBitFlag16( 1, false );
        quest.setBitFlag16( 2, false );
        quest.setBitFlag16( 3, false );
        quest.setBitFlag16( 10, false );
        quest.setBitFlag16( 11, false );
        quest.setBitFlag16( 12, false );
        quest.UI8BL = 0;
        quest.UI8CH = 0;
        quest.Sequence = 255;
        quest.UI8BH = 3;
        quest.UI8BL = 3;
      }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb406:66398 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SWYGRAEL" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 3, Flag16(1)=True
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag16( 1, true );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT1, UI8AL = 3, Flag16(2)=True
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag16( 2, true );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, UI8AL = 3, Flag16(3)=True
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag16( 3, true );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00011() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00011: Empty(None), id=unknown" );
  }

private void Scene00012() //SEQ_1: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_1: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00013: Empty(None), id=unknown" );
  }

private void Scene00015() //SEQ_1: EOBJECT3, UI8BH = 3, Flag16(10)=True
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00015: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    quest.setBitFlag16( 10, true );
    checkProgressSeq1();
  }

private void Scene00017() //SEQ_1: EOBJECT4, UI8BH = 3, Flag16(11)=True
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00017: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    quest.setBitFlag16( 11, true );
    checkProgressSeq1();
  }

private void Scene00019() //SEQ_1: EOBJECT5, UI8BH = 3, Flag16(12)=True
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00019: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    quest.setBitFlag16( 12, true );
    checkProgressSeq1();
  }

private void Scene00020() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00020: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00021();
      }
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00021() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00021: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SWYGRAEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00023() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00023: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00024: Empty(None), id=unknown" );
  }

private void Scene00025() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00025: Empty(None), id=unknown" );
  }

private void Scene00026() //SEQ_255: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00026: Empty(None), id=unknown" );
  }

private void Scene00027() //SEQ_255: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb406:66398 calling Scene00027: Empty(None), id=unknown" );
  }
};
}
