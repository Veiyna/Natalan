// FFXIVTheMovie.ParserV3.11
// param used:
//ACTOR17 = NPCB
//_ACTOR17 = S
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66412)]
public class GaiUsb507 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 9 entries
  //SEQ_2, 12 entries
  //SEQ_3, 8 entries
  //SEQ_4, 12 entries
  //SEQ_5, 8 entries
  //SEQ_255, 13 entries

  //ACTOR0 = 1003958
  //ACTOR1 = 1006361
  //ACTOR10 = 1006345
  //ACTOR11 = 1006346
  //ACTOR12 = 1006347
  //ACTOR13 = 1006348
  //ACTOR14 = 1007587
  //ACTOR15 = 1007588
  //ACTOR16 = 1007589
  //ACTOR17 = 1006362
  //ACTOR18 = 1006349
  //ACTOR19 = 1006344
  //ACTOR2 = 1006353
  //ACTOR20 = 1007591
  //ACTOR21 = 1007592
  //ACTOR22 = 1007593
  //ACTOR23 = 1007594
  //ACTOR24 = 1006350
  //ACTOR25 = 1006351
  //ACTOR26 = 1007590
  //ACTOR27 = 1007595
  //ACTOR28 = 1007596
  //ACTOR29 = 1007597
  //ACTOR3 = 1006354
  //ACTOR4 = 1007586
  //ACTOR5 = 1007599
  //ACTOR6 = 1007600
  //ACTOR7 = 1007601
  //ACTOR8 = 1007618
  //ACTOR9 = 1007619
  //EVENTACTIONPROCESSLONG = 17
  //EVENTACTIONPROCESSMIDDLE = 16
  //EVENTACTIONPROCESSSHOR = 15
  //ITEM0 = 2000685
  //ITEM1 = 2001067
  //ITEM2 = 2000686
  //QSTACCEPTCHECK = 66417

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=ELUNED
        // +Callback Scene00001: Normal(QuestAccept), id=unknown
        break;
      }
      case 1:
      {
        if( param1 == 1006361 ) // ACTOR1 = NPCA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCA
          }
          break;
        }
        if( param1 == 1006353 ) // ACTOR2 = unknown
        {
          Scene00003(); // Scene00003: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006354 ) // ACTOR3 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007586 ) // ACTOR4 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007599 ) // ACTOR5 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007600 ) // ACTOR6 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007601 ) // ACTOR7 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007618 ) // ACTOR8 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007619 ) // ACTOR9 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 4
      case 2:
      {
        if( param1 == 1006345 ) // ACTOR10 = unknown
        {
          if( !quest.getBitFlag16( 1 ) )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1006346 ) // ACTOR11 = unknown
        {
          if( !quest.getBitFlag16( 2 ) )
          {
            Scene00012(); // Scene00012: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1006347 ) // ACTOR12 = unknown
        {
          if( !quest.getBitFlag16( 3 ) )
          {
            Scene00013(); // Scene00013: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1006348 ) // ACTOR13 = unknown
        {
          if( !quest.getBitFlag16( 4 ) )
          {
            Scene00014(); // Scene00014: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1006353 ) // ACTOR2 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007601 ) // ACTOR7 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007618 ) // ACTOR8 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007619 ) // ACTOR9 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007587 ) // ACTOR14 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007588 ) // ACTOR15 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007589 ) // ACTOR16 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006362 ) // ACTOR17 = NPCB
        {
          Scene00026(); // Scene00026: Normal(Talk, TargetCanMove), id=NPCB
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 4
      case 3:
      {
        if( param1 == 1006362 ) // ACTOR17 = NPCB
        {
          if( quest.UI8AL != 1 )
          {
            Scene00027(); // Scene00027: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00028: Normal(Talk, TargetCanMove), id=NPCB
          }
          break;
        }
        if( param1 == 1006353 ) // ACTOR2 = unknown
        {
          Scene00029(); // Scene00029: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007601 ) // ACTOR7 = unknown
        {
          Scene00030(); // Scene00030: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007618 ) // ACTOR8 = unknown
        {
          Scene00031(); // Scene00031: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007619 ) // ACTOR9 = unknown
        {
          Scene00032(); // Scene00032: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007587 ) // ACTOR14 = unknown
        {
          Scene00033(); // Scene00033: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007588 ) // ACTOR15 = unknown
        {
          Scene00034(); // Scene00034: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007589 ) // ACTOR16 = unknown
        {
          Scene00035(); // Scene00035: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 4 event item ITEM2 = UI8CL max stack 1
      //seq 4 event item ITEM1 = UI8DH max stack 3
      case 4:
      {
        if( param1 == 1006349 ) // ACTOR18 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00036(); // Scene00036: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1006344 ) // ACTOR19 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00037(); // Scene00037: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1007591 ) // ACTOR20 = unknown
        {
          Scene00038(); // Scene00038: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007592 ) // ACTOR21 = unknown
        {
          Scene00039(); // Scene00039: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007593 ) // ACTOR22 = unknown
        {
          Scene00040(); // Scene00040: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007594 ) // ACTOR23 = unknown
        {
          Scene00041(); // Scene00041: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007587 ) // ACTOR14 = unknown
        {
          Scene00042(); // Scene00042: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007588 ) // ACTOR15 = unknown
        {
          Scene00043(); // Scene00043: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007589 ) // ACTOR16 = unknown
        {
          Scene00046(); // Scene00046: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006362 ) // ACTOR17 = NPCB
        {
          Scene00047(); // Scene00047: Normal(Talk, TargetCanMove), id=NPCB
          break;
        }
        if( param1 == 1006350 ) // ACTOR24 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00048(); // Scene00048: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1006351 ) // ACTOR25 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00051(); // Scene00051: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 5 event item ITEM2 = UI8BH max stack 1
      //seq 5 event item ITEM1 = UI8BL max stack 3
      case 5:
      {
        if( param1 == 1006362 ) // ACTOR17 = NPCB
        {
          if( quest.UI8AL != 1 )
          {
            Scene00052(); // Scene00052: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00053: Normal(Talk, TargetCanMove), id=NPCB
          }
          break;
        }
        if( param1 == 1007591 ) // ACTOR20 = unknown
        {
          Scene00054(); // Scene00054: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007592 ) // ACTOR21 = unknown
        {
          Scene00055(); // Scene00055: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007593 ) // ACTOR22 = unknown
        {
          Scene00056(); // Scene00056: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007594 ) // ACTOR23 = unknown
        {
          Scene00057(); // Scene00057: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007587 ) // ACTOR14 = unknown
        {
          Scene00058(); // Scene00058: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007588 ) // ACTOR15 = unknown
        {
          Scene00059(); // Scene00059: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007589 ) // ACTOR16 = unknown
        {
          Scene00060(); // Scene00060: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1003958 ) // ACTOR0 = ELUNED
        {
          Scene00061(); // Scene00061: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ELUNED
          break;
        }
        if( param1 == 1007590 ) // ACTOR26 = unknown
        {
          Scene00062(); // Scene00062: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007591 ) // ACTOR20 = unknown
        {
          Scene00063(); // Scene00063: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007592 ) // ACTOR21 = unknown
        {
          Scene00064(); // Scene00064: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007593 ) // ACTOR22 = unknown
        {
          Scene00065(); // Scene00065: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007594 ) // ACTOR23 = unknown
        {
          Scene00066(); // Scene00066: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007595 ) // ACTOR27 = unknown
        {
          Scene00067(); // Scene00067: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007596 ) // ACTOR28 = unknown
        {
          Scene00068(); // Scene00068: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007597 ) // ACTOR29 = unknown
        {
          Scene00069(); // Scene00069: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007587 ) // ACTOR14 = unknown
        {
          Scene00070(); // Scene00070: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007588 ) // ACTOR15 = unknown
        {
          Scene00071(); // Scene00071: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007589 ) // ACTOR16 = unknown
        {
          Scene00072(); // Scene00072: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006362 ) // ACTOR17 = NPCB
        {
          Scene00073(); // Scene00073: Normal(Talk, TargetCanMove), id=NPCB
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
    if( quest.UI8AL == 4 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag16( 1, false );
      quest.setBitFlag16( 2, false );
      quest.setBitFlag16( 3, false );
      quest.setBitFlag16( 4, false );
      quest.Sequence = 3;
      quest.UI8BH = 4;
    }
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.UI8BH = 0;
      quest.Sequence = 4;
    }
  }
  void checkProgressSeq4()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
          if( quest.UI8CH == 1 )
          {
            quest.UI8AL = 0 ;
            quest.UI8BH = 0 ;
            quest.UI8BL = 0 ;
            quest.UI8CH = 0 ;
            quest.setBitFlag16( 1, false );
            quest.setBitFlag16( 2, false );
            quest.setBitFlag16( 11, false );
            quest.setBitFlag16( 12, false );
            quest.UI8CL = 0;
            quest.UI8DH = 0;
            quest.Sequence = 5;
            quest.UI8BH = 1;
            quest.UI8BL = 3;
          }
  }
  void checkProgressSeq5()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.UI8BH = 0;
      quest.UI8BL = 0;
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=ELUNED" );
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
    player.sendDebug("GaiUsb507:66412 calling Scene00001: Normal(QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag16(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00003: Empty(None), id=unknown" );
  }

private void Scene00004() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00004: Empty(None), id=unknown" );
  }

private void Scene00005() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00005: Empty(None), id=unknown" );
  }

private void Scene00006() //SEQ_1: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00006: Empty(None), id=unknown" );
  }

private void Scene00007() //SEQ_1: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_1: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_1: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_1: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00011() //SEQ_2: ACTOR10, UI8AL = 4, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag16( 1, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_2: ACTOR11, UI8AL = 4, Flag16(2)=True(Todo:1)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00012: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag16( 2, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
    checkProgressSeq2();
  }

private void Scene00013() //SEQ_2: ACTOR12, UI8AL = 4, Flag16(3)=True(Todo:1)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag16( 3, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
    checkProgressSeq2();
  }

private void Scene00014() //SEQ_2: ACTOR13, UI8AL = 4, Flag16(4)=True(Todo:1)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00014: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag16( 4, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
    checkProgressSeq2();
  }

private void Scene00015() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00016() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_2: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00017: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_2: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00018: Empty(None), id=unknown" );
  }

private void Scene00019() //SEQ_2: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00019: Empty(None), id=unknown" );
  }

private void Scene00022() //SEQ_2: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00025() //SEQ_2: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00025: Empty(None), id=unknown" );
  }

private void Scene00026() //SEQ_2: ACTOR17, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00026: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00027() //SEQ_3: ACTOR17, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00027: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00028();
      }
    };
    owner.Event.NewScene( Id, 27, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00028() //SEQ_3: ACTOR17, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00028: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 28, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00029() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00029: Empty(None), id=unknown" );
  }

private void Scene00030() //SEQ_3: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00030: Empty(None), id=unknown" );
  }

private void Scene00031() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00031: Empty(None), id=unknown" );
  }

private void Scene00032() //SEQ_3: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00032: Empty(None), id=unknown" );
  }

private void Scene00033() //SEQ_3: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00033: Empty(None), id=unknown" );
  }

private void Scene00034() //SEQ_3: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00034: Empty(None), id=unknown" );
  }

private void Scene00035() //SEQ_3: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00035: Empty(None), id=unknown" );
  }

private void Scene00036() //SEQ_4: ACTOR18, UI8AL = 1, Flag16(1)=True(Todo:3)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00036: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag16( 1, true );
    player.SendQuestMessage(Id, 3, 0, 0, 0 );
    checkProgressSeq4();
  }

private void Scene00037() //SEQ_4: ACTOR19, UI8BH = 1, Flag16(2)=True
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00037: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag16( 2, true );
    checkProgressSeq4();
  }

private void Scene00038() //SEQ_4: ACTOR20, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00038: Empty(None), id=unknown" );
  }

private void Scene00039() //SEQ_4: ACTOR21, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00039: Empty(None), id=unknown" );
  }

private void Scene00040() //SEQ_4: ACTOR22, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00040: Empty(None), id=unknown" );
  }

private void Scene00041() //SEQ_4: ACTOR23, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00041: Empty(None), id=unknown" );
  }

private void Scene00042() //SEQ_4: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00042: Empty(None), id=unknown" );
  }

private void Scene00043() //SEQ_4: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00043: Empty(None), id=unknown" );
  }

private void Scene00046() //SEQ_4: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00046: Empty(None), id=unknown" );
  }

private void Scene00047() //SEQ_4: ACTOR17, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00047: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 47, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00048() //SEQ_4: ACTOR24, UI8BL = 1, Flag16(11)=True
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00048: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag16( 11, true );
    checkProgressSeq4();
  }

private void Scene00051() //SEQ_4: ACTOR25, UI8CH = 1, Flag16(12)=True
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00051: Empty(None), id=unknown" );
    quest.UI8CH =  (byte)( 1);
    quest.setBitFlag16( 12, true );
    checkProgressSeq4();
  }

private void Scene00052() //SEQ_5: ACTOR17, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00052: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00053();
      }
    };
    owner.Event.NewScene( Id, 52, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00053() //SEQ_5: ACTOR17, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00053: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 53, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00054() //SEQ_5: ACTOR20, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00054: Empty(None), id=unknown" );
  }

private void Scene00055() //SEQ_5: ACTOR21, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00055: Empty(None), id=unknown" );
  }

private void Scene00056() //SEQ_5: ACTOR22, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00056: Empty(None), id=unknown" );
  }

private void Scene00057() //SEQ_5: ACTOR23, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00057: Empty(None), id=unknown" );
  }

private void Scene00058() //SEQ_5: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00058: Empty(None), id=unknown" );
  }

private void Scene00059() //SEQ_5: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00059: Empty(None), id=unknown" );
  }

private void Scene00060() //SEQ_5: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00060: Empty(None), id=unknown" );
  }

private void Scene00061() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00061: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ELUNED" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 61, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00062() //SEQ_255: ACTOR26, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00062: Empty(None), id=unknown" );
  }

private void Scene00063() //SEQ_255: ACTOR20, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00063: Empty(None), id=unknown" );
  }

private void Scene00064() //SEQ_255: ACTOR21, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00064: Empty(None), id=unknown" );
  }

private void Scene00065() //SEQ_255: ACTOR22, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00065: Empty(None), id=unknown" );
  }

private void Scene00066() //SEQ_255: ACTOR23, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00066: Empty(None), id=unknown" );
  }

private void Scene00067() //SEQ_255: ACTOR27, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00067: Empty(None), id=unknown" );
  }

private void Scene00068() //SEQ_255: ACTOR28, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00068: Empty(None), id=unknown" );
  }

private void Scene00069() //SEQ_255: ACTOR29, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00069: Empty(None), id=unknown" );
  }

private void Scene00070() //SEQ_255: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00070: Empty(None), id=unknown" );
  }

private void Scene00071() //SEQ_255: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00071: Empty(None), id=unknown" );
  }

private void Scene00072() //SEQ_255: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00072: Empty(None), id=unknown" );
  }

private void Scene00073() //SEQ_255: ACTOR17, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb507:66412 calling Scene00073: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 73, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
