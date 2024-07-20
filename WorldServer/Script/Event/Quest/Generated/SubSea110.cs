// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65943)]
public class SubSea110 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 11 entries
  //SEQ_2, 11 entries
  //SEQ_3, 11 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002231
  //ACTOR1 = 1002447
  //ACTOR10 = 1003496
  //ACTOR2 = 1002448
  //ACTOR3 = 1002449
  //ACTOR4 = 1002450
  //ACTOR5 = 1002451
  //ACTOR6 = 1002452
  //ACTOR7 = 1003493
  //ACTOR8 = 1003494
  //ACTOR9 = 1003495
  //EOBJECT0 = 2000756
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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=DOESRAEL
        break;
      }
      case 1:
      {
        if( param1 == 1002447 ) // ACTOR1 = WYRSTMAGA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=WYRSTMAGA
          }
          break;
        }
        if( param1 == 1002448 ) // ACTOR2 = OROGUARD
        {
          Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=OROGUARD
          break;
        }
        if( param1 == 1002449 ) // ACTOR3 = IRATECOACHMANS
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=IRATECOACHMANS
          break;
        }
        if( param1 == 1002450 ) // ACTOR4 = IRATECOACHMANN
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=IRATECOACHMANN
          break;
        }
        if( param1 == 1002451 ) // ACTOR5 = COACHMANN
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=COACHMANN
          break;
        }
        if( param1 == 1002452 ) // ACTOR6 = COACHMANS
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=COACHMANS
          break;
        }
        if( param1 == 1003493 ) // ACTOR7 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003494 ) // ACTOR8 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003495 ) // ACTOR9 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003496 ) // ACTOR10 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000756 ) // EOBJECT0 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1002449 ) // ACTOR3 = IRATECOACHMANS
        {
          if( !quest.getBitFlag16( 1 ) )
          {
            Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=IRATECOACHMANS
            // +Callback Scene00015: Normal(Talk, TargetCanMove), id=IRATECOACHMANS
            // +Callback Scene00017: Normal(Talk, TargetCanMove), id=IRATECOACHMANS
          }
          break;
        }
        if( param1 == 1002450 ) // ACTOR4 = IRATECOACHMANN
        {
          if( !quest.getBitFlag16( 2 ) )
          {
            Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=IRATECOACHMANN
            // +Callback Scene00019: Normal(Talk, TargetCanMove), id=IRATECOACHMANN
            // +Callback Scene00021: Normal(Talk, TargetCanMove), id=IRATECOACHMANN
          }
          break;
        }
        if( param1 == 1002447 ) // ACTOR1 = WYRSTMAGA
        {
          Scene00022(); // Scene00022: Normal(Talk, TargetCanMove), id=WYRSTMAGA
          break;
        }
        if( param1 == 1002448 ) // ACTOR2 = OROGUARD
        {
          Scene00023(); // Scene00023: Normal(Talk, TargetCanMove), id=OROGUARD
          break;
        }
        if( param1 == 1002451 ) // ACTOR5 = COACHMANN
        {
          Scene00024(); // Scene00024: Normal(Talk, TargetCanMove), id=COACHMANN
          break;
        }
        if( param1 == 1002452 ) // ACTOR6 = COACHMANS
        {
          Scene00025(); // Scene00025: Normal(Talk, TargetCanMove), id=COACHMANS
          break;
        }
        if( param1 == 1003493 ) // ACTOR7 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003494 ) // ACTOR8 = unknown
        {
          Scene00027(); // Scene00027: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003495 ) // ACTOR9 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003496 ) // ACTOR10 = unknown
        {
          Scene00029(); // Scene00029: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000756 ) // EOBJECT0 = unknown
        {
          Scene00031(); // Scene00031: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1002447 ) // ACTOR1 = WYRSTMAGA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00033(); // Scene00033: Normal(Talk, NpcDespawn, TargetCanMove), id=WYRSTMAGA
          }
          break;
        }
        if( param1 == 1002448 ) // ACTOR2 = OROGUARD
        {
          Scene00034(); // Scene00034: Normal(Talk, TargetCanMove), id=OROGUARD
          break;
        }
        if( param1 == 1002449 ) // ACTOR3 = IRATECOACHMANS
        {
          Scene00035(); // Scene00035: Normal(Talk, TargetCanMove), id=IRATECOACHMANS
          break;
        }
        if( param1 == 1002450 ) // ACTOR4 = IRATECOACHMANN
        {
          Scene00036(); // Scene00036: Normal(Talk, TargetCanMove), id=IRATECOACHMANN
          break;
        }
        if( param1 == 1002451 ) // ACTOR5 = COACHMANN
        {
          Scene00037(); // Scene00037: Normal(Talk, TargetCanMove), id=COACHMANN
          break;
        }
        if( param1 == 1002452 ) // ACTOR6 = COACHMANS
        {
          Scene00038(); // Scene00038: Normal(Talk, TargetCanMove), id=COACHMANS
          break;
        }
        if( param1 == 1003493 ) // ACTOR7 = unknown
        {
          Scene00039(); // Scene00039: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003494 ) // ACTOR8 = unknown
        {
          Scene00040(); // Scene00040: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003495 ) // ACTOR9 = unknown
        {
          Scene00041(); // Scene00041: Empty(None), id=unknown
          break;
        }
        if( param1 == 1003496 ) // ACTOR10 = unknown
        {
          Scene00042(); // Scene00042: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000756 ) // EOBJECT0 = unknown
        {
          Scene00044(); // Scene00044: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00046(); // Scene00046: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=DOESRAEL
        break;
      }
      default:
      {
        player.sendUrgent("Sequence {} not defined. quest.Sequence ");
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

  public override void OnAreaTrigger(ulong actorId, WorldPosition position)
  {
    onProgress(EVENT_ON_WITHIN_RANGE, actorId, 0, 0 );
  }

  public override void OnEventTerritory()
  {
    onProgress(EVENT_ON_ENTER_TERRITORY, 0, 0, 0 );
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
      quest.setBitFlag16( 1, false );
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 2 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag16( 1, false );
      quest.setBitFlag16( 2, false );
      quest.Sequence = 3;
    }
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag16( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=DOESRAEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag16(1)=True(Todo:0)
  {
    player.sendDebug("SubSea110:65943 calling Scene00001: Normal(Talk, TargetCanMove), id=WYRSTMAGA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00002: Normal(Talk, TargetCanMove), id=OROGUARD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00003: Normal(Talk, TargetCanMove), id=IRATECOACHMANS" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00004: Normal(Talk, TargetCanMove), id=IRATECOACHMANN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00005: Normal(Talk, TargetCanMove), id=COACHMANN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_1: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00006: Normal(Talk, TargetCanMove), id=COACHMANS" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_1: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_1: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_1: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00013: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00014() //SEQ_2: ACTOR3, UI8AL = 2, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("SubSea110:65943 calling Scene00014: Normal(Talk, TargetCanMove), id=IRATECOACHMANS" );
    var callback = (SceneResult result) =>
    {
      Scene00015();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_2: ACTOR3, UI8AL = 2, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("SubSea110:65943 calling Scene00015: Normal(Talk, TargetCanMove), id=IRATECOACHMANS" );
    var callback = (SceneResult result) =>
    {
      Scene00017();
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00017() //SEQ_2: ACTOR3, UI8AL = 2, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("SubSea110:65943 calling Scene00017: Normal(Talk, TargetCanMove), id=IRATECOACHMANS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 2 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_2: ACTOR4, UI8AL = 2, Flag16(2)=True(Todo:1)
  {
    player.sendDebug("SubSea110:65943 calling Scene00018: Normal(Talk, TargetCanMove), id=IRATECOACHMANN" );
    var callback = (SceneResult result) =>
    {
      Scene00019();
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00019() //SEQ_2: ACTOR4, UI8AL = 2, Flag16(2)=True(Todo:1)
  {
    player.sendDebug("SubSea110:65943 calling Scene00019: Normal(Talk, TargetCanMove), id=IRATECOACHMANN" );
    var callback = (SceneResult result) =>
    {
      Scene00021();
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00021() //SEQ_2: ACTOR4, UI8AL = 2, Flag16(2)=True(Todo:1)
  {
    player.sendDebug("SubSea110:65943 calling Scene00021: Normal(Talk, TargetCanMove), id=IRATECOACHMANN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag16( 2, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 2 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00022: Normal(Talk, TargetCanMove), id=WYRSTMAGA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00023() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00023: Normal(Talk, TargetCanMove), id=OROGUARD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00024() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00024: Normal(Talk, TargetCanMove), id=COACHMANN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00025() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00025: Normal(Talk, TargetCanMove), id=COACHMANS" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 25, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00026() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00026: Empty(None), id=unknown" );
  }

private void Scene00027() //SEQ_2: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00027: Empty(None), id=unknown" );
  }

private void Scene00028() //SEQ_2: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00028: Empty(None), id=unknown" );
  }

private void Scene00029() //SEQ_2: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00029: Empty(None), id=unknown" );
  }

private void Scene00031() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00031: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00033() //SEQ_3: ACTOR1, UI8AL = 1, Flag16(1)=True(Todo:2)
  {
    player.sendDebug("SubSea110:65943 calling Scene00033: Normal(Talk, NpcDespawn, TargetCanMove), id=WYRSTMAGA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 33, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00034() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00034: Normal(Talk, TargetCanMove), id=OROGUARD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 34, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00035() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00035: Normal(Talk, TargetCanMove), id=IRATECOACHMANS" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 35, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00036() //SEQ_3: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00036: Normal(Talk, TargetCanMove), id=IRATECOACHMANN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 36, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00037() //SEQ_3: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00037: Normal(Talk, TargetCanMove), id=COACHMANN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 37, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00038() //SEQ_3: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00038: Normal(Talk, TargetCanMove), id=COACHMANS" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 38, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00039() //SEQ_3: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00039: Empty(None), id=unknown" );
  }

private void Scene00040() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00040: Empty(None), id=unknown" );
  }

private void Scene00041() //SEQ_3: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00041: Empty(None), id=unknown" );
  }

private void Scene00042() //SEQ_3: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00042: Empty(None), id=unknown" );
  }

private void Scene00044() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00044: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00046() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea110:65943 calling Scene00046: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=DOESRAEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 46, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
