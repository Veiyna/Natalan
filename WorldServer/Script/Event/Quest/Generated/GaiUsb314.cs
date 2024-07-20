// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66391)]
public class GaiUsb314 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 15 entries
  //SEQ_2, 16 entries
  //SEQ_3, 13 entries
  //SEQ_255, 13 entries

  //ACTOR0 = 1006676
  //ACTOR1 = 1006314
  //ACTOR2 = 1006313
  //ACTOR3 = 1006315
  //ACTOR4 = 1006316
  //ACTOR5 = 1006317
  //ACTOR6 = 1006318
  //ACTOR7 = 1006319
  //ACTOR8 = 1006266
  //BGMLVUP = 23
  //CUTSCENE01 = 369
  //EOBJECT0 = 2002075
  //EOBJECT1 = 2002076
  //EOBJECT2 = 2002077
  //EOBJECT3 = 2002078
  //EOBJECT4 = 2002079
  //EOBJECT5 = 2002080
  //EOBJECT6 = 2002081
  //EOBJECT7 = 2002082
  //EOBJECT8 = 2002598
  //EVENTACTIONSEARCH = 1
  //LOCACTION0 = 788
  //LOCACTION1 = 1016
  //LOCACTION2 = 683
  //LOCACTOR1 = 1006272
  //LOCACTOR100 = 1007014
  //LOCBINDEOBJ100 = 4293459
  //LOCBINDEOBJ101 = 4293460
  //LOCBINDEOBJ102 = 4293461
  //LOCEOBJ1 = 2002687
  //LOCPOSACTOR0 = 4374246
  //LOCPOSACTOR1 = 4258358
  //LOCPOSCAM1 = 4374251
  //LOCPOSEOBJ1 = 4373897
  //QSTACCEPTCHECK = 66390

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
        // +Callback Scene00001: Normal(Talk, TargetCanMove), id=YSHTOLA
        // +Callback Scene00002: Normal(CutScene, QuestAccept), id=unknown
        break;
      }
      case 1:
      {
        if( param1 == 1006314 ) // ACTOR1 = NPC
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=NPC
          }
          break;
        }
        if( param1 == 1006313 ) // ACTOR2 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006315 ) // ACTOR3 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006316 ) // ACTOR4 = UODHNUN
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=UODHNUN
          break;
        }
        if( param1 == 1006317 ) // ACTOR5 = LANDENEL
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=LANDENEL
          break;
        }
        if( param1 == 1006318 ) // ACTOR6 = BRAYFLOX
        {
          Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=BRAYFLOX
          break;
        }
        if( param1 == 1006319 ) // ACTOR7 = SHAMANILOHMANI
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI
          break;
        }
        if( param1 == 2002075 ) // EOBJECT0 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002076 ) // EOBJECT1 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002077 ) // EOBJECT2 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002078 ) // EOBJECT3 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002079 ) // EOBJECT4 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002080 ) // EOBJECT5 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002081 ) // EOBJECT6 = unknown
        {
          Scene00023(); // Scene00023: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002082 ) // EOBJECT7 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 2002598 ) // EOBJECT8 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00026(); // Scene00026: Normal(None), id=unknown
            // +Callback Scene00027: Normal(FadeIn, SystemTalk), id=unknown
          }
          break;
        }
        if( param1 == 1006313 ) // ACTOR2 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006314 ) // ACTOR1 = NPC
        {
          Scene00029(); // Scene00029: Normal(Talk, TargetCanMove), id=NPC
          break;
        }
        if( param1 == 1006315 ) // ACTOR3 = unknown
        {
          Scene00030(); // Scene00030: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006316 ) // ACTOR4 = UODHNUN
        {
          Scene00031(); // Scene00031: Normal(Talk, TargetCanMove), id=UODHNUN
          break;
        }
        if( param1 == 1006317 ) // ACTOR5 = LANDENEL
        {
          Scene00032(); // Scene00032: Normal(Talk, TargetCanMove), id=LANDENEL
          break;
        }
        if( param1 == 1006318 ) // ACTOR6 = BRAYFLOX
        {
          Scene00033(); // Scene00033: Normal(Talk, TargetCanMove), id=BRAYFLOX
          break;
        }
        if( param1 == 1006319 ) // ACTOR7 = SHAMANILOHMANI
        {
          Scene00034(); // Scene00034: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI
          break;
        }
        if( param1 == 2002075 ) // EOBJECT0 = unknown
        {
          Scene00036(); // Scene00036: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002076 ) // EOBJECT1 = unknown
        {
          Scene00038(); // Scene00038: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002077 ) // EOBJECT2 = unknown
        {
          Scene00040(); // Scene00040: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002078 ) // EOBJECT3 = unknown
        {
          Scene00042(); // Scene00042: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002079 ) // EOBJECT4 = unknown
        {
          Scene00044(); // Scene00044: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002080 ) // EOBJECT5 = unknown
        {
          Scene00046(); // Scene00046: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002081 ) // EOBJECT6 = unknown
        {
          Scene00048(); // Scene00048: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002082 ) // EOBJECT7 = unknown
        {
          Scene00050(); // Scene00050: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1006266 ) // ACTOR8 = WHEISKAET
        {
          if( quest.UI8AL != 1 )
          {
            Scene00051(); // Scene00051: Normal(Talk, TargetCanMove), id=WHEISKAET
          }
          break;
        }
        if( param1 == 1006317 ) // ACTOR5 = LANDENEL
        {
          if( quest.UI8BH != 1 )
          {
            Scene00053(); // Scene00053: Normal(Talk, TargetCanMove), id=LANDENEL
            // +Callback Scene00054: Normal(Talk, TargetCanMove), id=LANDENEL
          }
          break;
        }
        if( param1 == 1006316 ) // ACTOR4 = UODHNUN
        {
          if( quest.UI8BL != 1 )
          {
            Scene00055(); // Scene00055: Normal(Talk, TargetCanMove), id=UODHNUN
            // +Callback Scene00056: Normal(Talk, TargetCanMove), id=UODHNUN
          }
          break;
        }
        if( param1 == 1006318 ) // ACTOR6 = BRAYFLOX
        {
          if( quest.UI8CH != 1 )
          {
            Scene00057(); // Scene00057: Normal(Talk, TargetCanMove), id=BRAYFLOX
            // +Callback Scene00058: Normal(Talk, TargetCanMove), id=BRAYFLOX
          }
          break;
        }
        if( param1 == 1006319 ) // ACTOR7 = SHAMANILOHMANI
        {
          if( quest.UI8CL != 1 )
          {
            Scene00059(); // Scene00059: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI
          }
          break;
        }
        if( param1 == 1006313 ) // ACTOR2 = SHAMANILOHMANI
        {
          Scene00061(); // Scene00061: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI
          break;
        }
        if( param1 == 1006314 ) // ACTOR1 = unknown
        {
          Scene00062(); // Scene00062: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006315 ) // ACTOR3 = NPC
        {
          Scene00063(); // Scene00063: Normal(Talk, TargetCanMove), id=NPC
          break;
        }
        if( param1 == 2002078 ) // EOBJECT3 = unknown
        {
          Scene00065(); // Scene00065: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002079 ) // EOBJECT4 = unknown
        {
          Scene00067(); // Scene00067: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002080 ) // EOBJECT5 = unknown
        {
          Scene00069(); // Scene00069: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002081 ) // EOBJECT6 = unknown
        {
          Scene00071(); // Scene00071: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002082 ) // EOBJECT7 = unknown
        {
          Scene00073(); // Scene00073: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006676 ) // ACTOR0 = YSHTOLA
        {
          Scene00075(); // Scene00075: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=YSHTOLA
          break;
        }
        if( param1 == 1006313 ) // ACTOR2 = unknown
        {
          Scene00076(); // Scene00076: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006314 ) // ACTOR1 = NPC
        {
          Scene00077(); // Scene00077: Normal(Talk, TargetCanMove), id=NPC
          break;
        }
        if( param1 == 1006315 ) // ACTOR3 = unknown
        {
          Scene00078(); // Scene00078: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006316 ) // ACTOR4 = UODHNUN
        {
          Scene00079(); // Scene00079: Normal(Talk, TargetCanMove), id=UODHNUN
          break;
        }
        if( param1 == 1006317 ) // ACTOR5 = LANDENEL
        {
          Scene00080(); // Scene00080: Normal(Talk, TargetCanMove), id=LANDENEL
          break;
        }
        if( param1 == 1006318 ) // ACTOR6 = BRAYFLOX
        {
          Scene00081(); // Scene00081: Normal(Talk, TargetCanMove), id=BRAYFLOX
          break;
        }
        if( param1 == 1006319 ) // ACTOR7 = SHAMANILOHMANI
        {
          Scene00082(); // Scene00082: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI
          break;
        }
        if( param1 == 2002078 ) // EOBJECT3 = unknown
        {
          Scene00084(); // Scene00084: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002079 ) // EOBJECT4 = unknown
        {
          Scene00086(); // Scene00086: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002080 ) // EOBJECT5 = unknown
        {
          Scene00088(); // Scene00088: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002081 ) // EOBJECT6 = unknown
        {
          Scene00090(); // Scene00090: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002082 ) // EOBJECT7 = unknown
        {
          Scene00092(); // Scene00092: Empty(None), id=unknown
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
      quest.setBitFlag16( 1, false );
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag16( 1, false );
      quest.Sequence = 3;
    }
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
          if( quest.UI8CH == 1 )
            if( quest.UI8CL == 1 )
            {
              quest.UI8AL = 0 ;
              quest.UI8BH = 0 ;
              quest.UI8BL = 0 ;
              quest.UI8CH = 0 ;
              quest.UI8CL = 0 ;
              quest.setBitFlag16( 1, false );
              quest.setBitFlag16( 2, false );
              quest.setBitFlag16( 3, false );
              quest.setBitFlag16( 4, false );
              quest.setBitFlag16( 5, false );
              quest.Sequence = 255;
            }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("GaiUsb314:66391 calling Scene00001: Normal(Talk, TargetCanMove), id=YSHTOLA" );
    var callback = (SceneResult result) =>
    {
      Scene00002();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00002() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00002: Normal(CutScene, QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 1, Flag16(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00003: Normal(Talk, TargetCanMove), id=NPC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00004: Empty(None), id=unknown" );
  }

private void Scene00005() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00005: Empty(None), id=unknown" );
  }

private void Scene00006() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00006: Normal(Talk, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00007: Normal(Talk, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00008: Normal(Talk, TargetCanMove), id=BRAYFLOX" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_1: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00009: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00013: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00015() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00015: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00017() //SEQ_1: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00017: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00019() //SEQ_1: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00019: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00021() //SEQ_1: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00021: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00023() //SEQ_1: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00023: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00025() //SEQ_1: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00025: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00026() //SEQ_2: EOBJECT8, UI8AL = 1, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00026: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00027();
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00027() //SEQ_2: EOBJECT8, UI8AL = 1, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00027: Normal(FadeIn, SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 27, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00028() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00028: Empty(None), id=unknown" );
  }

private void Scene00029() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00029: Normal(Talk, TargetCanMove), id=NPC" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 29, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00030() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00030: Empty(None), id=unknown" );
  }

private void Scene00031() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00031: Normal(Talk, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 31, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00032() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00032: Normal(Talk, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 32, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00033() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00033: Normal(Talk, TargetCanMove), id=BRAYFLOX" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 33, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00034() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00034: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 34, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00036() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00036: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00038() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00038: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00040() //SEQ_2: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00040: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00042() //SEQ_2: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00042: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00044() //SEQ_2: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00044: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00046() //SEQ_2: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00046: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00048() //SEQ_2: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00048: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00050() //SEQ_2: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00050: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00051() //SEQ_3: ACTOR8, UI8AL = 1, Flag16(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00051: Normal(Talk, TargetCanMove), id=WHEISKAET" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 51, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00053() //SEQ_3: ACTOR5, UI8BH = 1, Flag16(2)=True
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00053: Normal(Talk, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
      Scene00054();
    };
    owner.Event.NewScene( Id, 53, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00054() //SEQ_3: ACTOR5, UI8BH = 1, Flag16(2)=True
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00054: Normal(Talk, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag16( 2, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 54, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00055() //SEQ_3: ACTOR4, UI8BL = 1, Flag16(3)=True
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00055: Normal(Talk, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
      Scene00056();
    };
    owner.Event.NewScene( Id, 55, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00056() //SEQ_3: ACTOR4, UI8BL = 1, Flag16(3)=True
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00056: Normal(Talk, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag16( 3, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 56, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00057() //SEQ_3: ACTOR6, UI8CH = 1, Flag16(4)=True
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00057: Normal(Talk, TargetCanMove), id=BRAYFLOX" );
    var callback = (SceneResult result) =>
    {
      Scene00058();
    };
    owner.Event.NewScene( Id, 57, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00058() //SEQ_3: ACTOR6, UI8CH = 1, Flag16(4)=True
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00058: Normal(Talk, TargetCanMove), id=BRAYFLOX" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag16( 4, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 58, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00059() //SEQ_3: ACTOR7, UI8CL = 1, Flag16(5)=True
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00059: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CL =  (byte)( 1);
      quest.setBitFlag16( 5, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 59, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00061() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00061: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 61, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00062() //SEQ_3: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00062: Empty(None), id=unknown" );
  }

private void Scene00063() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00063: Normal(Talk, TargetCanMove), id=NPC" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 63, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00065() //SEQ_3: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00065: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00067() //SEQ_3: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00067: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00069() //SEQ_3: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00069: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00071() //SEQ_3: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00071: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00073() //SEQ_3: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00073: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00075() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00075: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=YSHTOLA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 75, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00076() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00076: Empty(None), id=unknown" );
  }

private void Scene00077() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00077: Normal(Talk, TargetCanMove), id=NPC" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 77, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00078() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00078: Empty(None), id=unknown" );
  }

private void Scene00079() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00079: Normal(Talk, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 79, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00080() //SEQ_255: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00080: Normal(Talk, TargetCanMove), id=LANDENEL" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 80, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00081() //SEQ_255: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00081: Normal(Talk, TargetCanMove), id=BRAYFLOX" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 81, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00082() //SEQ_255: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00082: Normal(Talk, TargetCanMove), id=SHAMANILOHMANI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 82, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00084() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00084: Empty(None), id=unknown" );
  }

private void Scene00086() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00086: Empty(None), id=unknown" );
  }

private void Scene00088() //SEQ_255: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00088: Empty(None), id=unknown" );
  }

private void Scene00090() //SEQ_255: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00090: Empty(None), id=unknown" );
  }

private void Scene00092() //SEQ_255: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb314:66391 calling Scene00092: Empty(None), id=unknown" );
  }
};
}
