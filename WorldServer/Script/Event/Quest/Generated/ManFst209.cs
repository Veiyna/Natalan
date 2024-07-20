// FFXIVTheMovie.ParserV3.11
// param used:
//SCENE_7 REMOVED!!
//WARP_ADALA = 146|-53.45|-24.09|-567.49|-0.55|false
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65879)]
public class ManFst209 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_2, 6 entries
  //SEQ_3, 13 entries
  //SEQ_4, 13 entries
  //SEQ_5, 11 entries
  //SEQ_255, 11 entries

  //ACTOR0 = 1005116
  //ACTOR1 = 1004584
  //ACTOR10 = 1004610
  //ACTOR11 = 1004611
  //ACTOR12 = 1005140
  //ACTOR13 = 1005141
  //ACTOR14 = 1005142
  //ACTOR15 = 1005143
  //ACTOR16 = 1005144
  //ACTOR17 = 1005145
  //ACTOR18 = 1005018
  //ACTOR2 = 1004585
  //ACTOR20 = 1005156
  //ACTOR21 = 1005146
  //ACTOR22 = 1005147
  //ACTOR23 = 1005148
  //ACTOR24 = 1005149
  //ACTOR25 = 1005150
  //ACTOR26 = 1005151
  //ACTOR27 = 1005152
  //ACTOR28 = 1005153
  //ACTOR29 = 1005154
  //ACTOR3 = 1004586
  //ACTOR30 = 1005155
  //ACTOR31 = 1005012
  //ACTOR4 = 1004587
  //ACTOR5 = 1005161
  //ACTOR6 = 1005017
  //ACTOR7 = 1004605
  //ACTOR8 = 1004606
  //ACTOR9 = 1004608
  //CUTMANFST20950 = 88
  //EOBJECT0 = 2001592
  //EVENTACTIONWAITINGSHOR = 11
  //INSTANCEDUNGEON0 = 20001
  //POPRANGE0 = 4165046
  //POPRANGE1 = 4156164
  //POPRANGE2 = 4148347
  //QUESTBATTLE0 = 45
  //TERRITORYTYPE0 = 275
  //TERRITORYTYPE1 = 146
  //UNLOCKADDNEWCONTENTTOCF = 3702
  //UNLOCKIMAGEDUNGEON = 77

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MINFILIA
        break;
      }
      case 1:
      {
        if( param1 == 1004584 ) // ACTOR1 = FLAMESGT
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=FLAMESGT
          }
          break;
        }
        if( param1 == 1004585 ) // ACTOR2 = unknown
        {
          Scene00003(); // Scene00003: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004586 ) // ACTOR3 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004587 ) // ACTOR4 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005161 ) // ACTOR5 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 2001592 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00008(); // Scene00008: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 1004584 ) // ACTOR1 = FLAMESGT
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=FLAMESGT
          break;
        }
        if( param1 == 1004585 ) // ACTOR2 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004586 ) // ACTOR3 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004587 ) // ACTOR4 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005161 ) // ACTOR5 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1005017 ) // ACTOR6 = FLAMESGT
        {
          if( quest.UI8AL != 1 )
          {
            Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=FLAMESGT
          }
          break;
        }
        if( param1 == 1004605 ) // ACTOR7 = FLAMEPRIVATE2
        {
          Scene00015(); // Scene00015: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE2
          break;
        }
        if( param1 == 1004606 ) // ACTOR8 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004608 ) // ACTOR9 = FLAMEPRIVATE
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE
          break;
        }
        if( param1 == 1004610 ) // ACTOR10 = AMALJA
        {
          Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=AMALJA
          break;
        }
        if( param1 == 1004611 ) // ACTOR11 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005140 ) // ACTOR12 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005141 ) // ACTOR13 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005142 ) // ACTOR14 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005143 ) // ACTOR15 = unknown
        {
          Scene00023(); // Scene00023: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005144 ) // ACTOR16 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005145 ) // ACTOR17 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005018 ) // ACTOR18 = ADALA
        {
          Scene00026(); // Scene00026: Normal(Talk, YesNo, TargetCanMove), id=ADALA
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1004605 ) // ACTOR7 = FLAMEPRIVATE2
        {
          Scene00027(); // Scene00027: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE2
          break;
        }
        if( param1 == 1004606 ) // ACTOR8 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004608 ) // ACTOR9 = FLAMEPRIVATE
        {
          Scene00029(); // Scene00029: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE
          break;
        }
        if( param1 == 1004610 ) // ACTOR10 = AMALJA
        {
          Scene00030(); // Scene00030: Normal(Talk, TargetCanMove), id=AMALJA
          break;
        }
        if( param1 == 1004611 ) // ACTOR11 = unknown
        {
          Scene00031(); // Scene00031: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005017 ) // ACTOR6 = FLAMESGT
        {
          Scene00032(); // Scene00032: Normal(Talk, TargetCanMove), id=FLAMESGT
          break;
        }
        if( param1 == 1005140 ) // ACTOR12 = unknown
        {
          Scene00033(); // Scene00033: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005141 ) // ACTOR13 = unknown
        {
          Scene00034(); // Scene00034: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005142 ) // ACTOR14 = unknown
        {
          Scene00035(); // Scene00035: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005143 ) // ACTOR15 = unknown
        {
          Scene00036(); // Scene00036: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005144 ) // ACTOR16 = unknown
        {
          Scene00037(); // Scene00037: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005145 ) // ACTOR17 = unknown
        {
          Scene00038(); // Scene00038: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005018 ) // ACTOR18 = ADALA
        {
          Scene00039(); // Scene00039: Normal(Talk, YesNo, TargetCanMove), id=ADALA
          break;
        }
        break;
      }
      case 5:
      {
        if( param1 == 1005156 ) // ACTOR20 = THANCRED
        {
          if( quest.UI8AL != 1 )
          {
            Scene00040(); // Scene00040: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED
          }
          break;
        }
        if( param1 == 1005146 ) // ACTOR21 = unknown
        {
          Scene00041(); // Scene00041: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005147 ) // ACTOR22 = unknown
        {
          Scene00042(); // Scene00042: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005148 ) // ACTOR23 = unknown
        {
          Scene00043(); // Scene00043: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005149 ) // ACTOR24 = unknown
        {
          Scene00044(); // Scene00044: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005150 ) // ACTOR25 = unknown
        {
          Scene00045(); // Scene00045: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005151 ) // ACTOR26 = unknown
        {
          Scene00046(); // Scene00046: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005152 ) // ACTOR27 = unknown
        {
          Scene00047(); // Scene00047: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005153 ) // ACTOR28 = unknown
        {
          Scene00048(); // Scene00048: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005154 ) // ACTOR29 = unknown
        {
          Scene00049(); // Scene00049: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005155 ) // ACTOR30 = unknown
        {
          Scene00050(); // Scene00050: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1005012 ) // ACTOR31 = GUIDE
        {
          Scene00051(); // Scene00051: Normal(Talk, QuestReward, TargetCanMove), id=GUIDE
          // +Callback Scene00052: Normal(CutScene, QuestComplete, AutoFadeIn), id=unknown
          break;
        }
        if( param1 == 1005146 ) // ACTOR21 = unknown
        {
          Scene00053(); // Scene00053: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005147 ) // ACTOR22 = unknown
        {
          Scene00054(); // Scene00054: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005148 ) // ACTOR23 = unknown
        {
          Scene00055(); // Scene00055: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005149 ) // ACTOR24 = unknown
        {
          Scene00056(); // Scene00056: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005150 ) // ACTOR25 = unknown
        {
          Scene00057(); // Scene00057: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005151 ) // ACTOR26 = unknown
        {
          Scene00058(); // Scene00058: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005152 ) // ACTOR27 = unknown
        {
          Scene00059(); // Scene00059: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005153 ) // ACTOR28 = unknown
        {
          Scene00060(); // Scene00060: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005154 ) // ACTOR29 = unknown
        {
          Scene00061(); // Scene00061: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005155 ) // ACTOR30 = unknown
        {
          Scene00062(); // Scene00062: Empty(None), id=unknown
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
    }
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag16( 1, false );
      quest.Sequence = 4;
    }
  }
  void checkProgressSeq4()
  {
    quest.Sequence = 5;
  }
  void checkProgressSeq5()
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
    player.sendDebug("ManFst209:65879 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManFst209:65879 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MINFILIA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("ManFst209:65879 calling Scene00002: Normal(Talk, TargetCanMove), id=FLAMESGT" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00003: Empty(None), id=unknown" );
  }

private void Scene00004() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00004: Empty(None), id=unknown" );
  }

private void Scene00005() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00005: Empty(None), id=unknown" );
  }

private void Scene00006() //SEQ_1: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00006: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_2: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("ManFst209:65879 calling Scene00008: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 45 );
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00009: Normal(Talk, TargetCanMove), id=FLAMESGT" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00011() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00011: Empty(None), id=unknown" );
  }

private void Scene00012() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00013: Empty(None), id=unknown" );
  }

private void Scene00014() //SEQ_3: ACTOR6, UI8AL = 1, Flag16(1)=True(Todo:2)
  {
    player.sendDebug("ManFst209:65879 calling Scene00014: Normal(Talk, TargetCanMove), id=FLAMESGT" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      player.SetDutyUnlock(20001);
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_3: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00015: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE2" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_3: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00017: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_3: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00018: Normal(Talk, TargetCanMove), id=AMALJA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_3: ACTOR11, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00019: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_3: ACTOR12, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00020: Empty(None), id=unknown" );
  }

private void Scene00021() //SEQ_3: ACTOR13, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00021: Empty(None), id=unknown" );
  }

private void Scene00022() //SEQ_3: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00023() //SEQ_3: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00023: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_3: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00024: Empty(None), id=unknown" );
  }

private void Scene00025() //SEQ_3: ACTOR17, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00025: Empty(None), id=unknown" );
  }

private void Scene00026() //SEQ_3: ACTOR18, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00026: Normal(Talk, YesNo, TargetCanMove), id=ADALA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.Event.StopEvent(Id);
        player.TeleportTo(new WorldPosition(146,new Vector3(-53.45f, -24.09f, -567.49f), -0.55f));
      }
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00027() //SEQ_4: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00027: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE2" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 27, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00028() //SEQ_4: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00028: Empty(None), id=unknown" );
  }

private void Scene00029() //SEQ_4: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00029: Normal(Talk, TargetCanMove), id=FLAMEPRIVATE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 29, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00030() //SEQ_4: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00030: Normal(Talk, TargetCanMove), id=AMALJA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 30, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00031() //SEQ_4: ACTOR11, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00031: Empty(None), id=unknown" );
  }

private void Scene00032() //SEQ_4: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00032: Normal(Talk, TargetCanMove), id=FLAMESGT" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 32, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00033() //SEQ_4: ACTOR12, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00033: Empty(None), id=unknown" );
  }

private void Scene00034() //SEQ_4: ACTOR13, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00034: Empty(None), id=unknown" );
  }

private void Scene00035() //SEQ_4: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00035: Empty(None), id=unknown" );
  }

private void Scene00036() //SEQ_4: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00036: Empty(None), id=unknown" );
  }

private void Scene00037() //SEQ_4: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00037: Empty(None), id=unknown" );
  }

private void Scene00038() //SEQ_4: ACTOR17, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00038: Empty(None), id=unknown" );
  }

private void Scene00039() //SEQ_4: ACTOR18, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00039: Normal(Talk, YesNo, TargetCanMove), id=ADALA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.Event.StopEvent(Id);
        player.TeleportTo(new WorldPosition(146,new Vector3(-53.45f, -24.09f, -567.49f), -0.55f));
      }
    };
    owner.Event.NewScene( Id, 39, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00040() //SEQ_5: ACTOR20, UI8AL = 1, Flag16(1)=True(Todo:4)
  {
    player.sendDebug("ManFst209:65879 calling Scene00040: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 40, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00041() //SEQ_5: ACTOR21, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00041: Empty(None), id=unknown" );
  }

private void Scene00042() //SEQ_5: ACTOR22, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00042: Empty(None), id=unknown" );
  }

private void Scene00043() //SEQ_5: ACTOR23, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00043: Empty(None), id=unknown" );
  }

private void Scene00044() //SEQ_5: ACTOR24, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00044: Empty(None), id=unknown" );
  }

private void Scene00045() //SEQ_5: ACTOR25, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00045: Empty(None), id=unknown" );
  }

private void Scene00046() //SEQ_5: ACTOR26, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00046: Empty(None), id=unknown" );
  }

private void Scene00047() //SEQ_5: ACTOR27, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00047: Empty(None), id=unknown" );
  }

private void Scene00048() //SEQ_5: ACTOR28, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00048: Empty(None), id=unknown" );
  }

private void Scene00049() //SEQ_5: ACTOR29, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00049: Empty(None), id=unknown" );
  }

private void Scene00050() //SEQ_5: ACTOR30, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00050: Empty(None), id=unknown" );
  }

private void Scene00051() //SEQ_255: ACTOR31, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00051: Normal(Talk, QuestReward, TargetCanMove), id=GUIDE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00052();
      }
    };
    owner.Event.NewScene( Id, 51, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00052() //SEQ_255: ACTOR31, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00052: Normal(CutScene, QuestComplete, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      owner.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 52, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00053() //SEQ_255: ACTOR21, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00053: Empty(None), id=unknown" );
  }

private void Scene00054() //SEQ_255: ACTOR22, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00054: Empty(None), id=unknown" );
  }

private void Scene00055() //SEQ_255: ACTOR23, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00055: Empty(None), id=unknown" );
  }

private void Scene00056() //SEQ_255: ACTOR24, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00056: Empty(None), id=unknown" );
  }

private void Scene00057() //SEQ_255: ACTOR25, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00057: Empty(None), id=unknown" );
  }

private void Scene00058() //SEQ_255: ACTOR26, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00058: Empty(None), id=unknown" );
  }

private void Scene00059() //SEQ_255: ACTOR27, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00059: Empty(None), id=unknown" );
  }

private void Scene00060() //SEQ_255: ACTOR28, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00060: Empty(None), id=unknown" );
  }

private void Scene00061() //SEQ_255: ACTOR29, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00061: Empty(None), id=unknown" );
  }

private void Scene00062() //SEQ_255: ACTOR30, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst209:65879 calling Scene00062: Empty(None), id=unknown" );
  }
};
}
