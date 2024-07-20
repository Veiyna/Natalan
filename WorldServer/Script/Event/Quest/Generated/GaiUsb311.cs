// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66388)]
public class GaiUsb311 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 8 entries
  //SEQ_2, 12 entries
  //SEQ_3, 12 entries
  //SEQ_255, 13 entries

  //ACTOR0 = 1006273
  //ACTOR1 = 1006320
  //ACTOR10 = 1007554
  //ACTOR11 = 1007555
  //ACTOR12 = 1007556
  //ACTOR13 = 1007557
  //ACTOR14 = 1007558
  //ACTOR2 = 1006321
  //ACTOR3 = 1006322
  //ACTOR4 = 1007549
  //ACTOR5 = 1007550
  //ACTOR6 = 1007551
  //ACTOR7 = 1007552
  //ACTOR8 = 1007553
  //ACTOR9 = 1006323
  //EOBJECT0 = 2002064
  //EOBJECT1 = 2002065
  //EOBJECT2 = 2002066
  //EOBJECT3 = 2002677
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000661

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=GEGERUJU
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( param1 == 1006320 ) // ACTOR1 = POACHER
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=POACHER
          }
          break;
        }
        if( param1 == 1006321 ) // ACTOR2 = unknown
        {
          Scene00003(); // Scene00003: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006322 ) // ACTOR3 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007549 ) // ACTOR4 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007550 ) // ACTOR5 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007551 ) // ACTOR6 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007552 ) // ACTOR7 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007553 ) // ACTOR8 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8CL max stack ?
      case 2:
      {
        if( param1 == 2002064 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            if( type == EVENT_ON_TALK ) Scene00010(); // Scene00010: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002065 ) // EOBJECT1 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00012(); // Scene00012: Normal(Inventory), id=unknown
            // +Callback Scene00013: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002066 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00014(); // Scene00014: Normal(Inventory), id=unknown
            // +Callback Scene00015: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1006320 ) // ACTOR1 = POACHER
        {
          Scene00016(); // Scene00016: Normal(Talk, TargetCanMove), id=POACHER
          break;
        }
        if( param1 == 1006321 ) // ACTOR2 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006322 ) // ACTOR3 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007549 ) // ACTOR4 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007550 ) // ACTOR5 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007551 ) // ACTOR6 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007552 ) // ACTOR7 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007553 ) // ACTOR8 = unknown
        {
          Scene00023(); // Scene00023: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002677 ) // EOBJECT3 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00024(); // Scene00024: Normal(Inventory), id=unknown
            // +Callback Scene00025: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1006320 ) // ACTOR1 = POACHER
        {
          if( quest.UI8AL != 1 )
          {
            Scene00026(); // Scene00026: Normal(Talk, TargetCanMove), id=POACHER
          }
          break;
        }
        if( param1 == 1006322 ) // ACTOR3 = unknown
        {
          Scene00027(); // Scene00027: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006323 ) // ACTOR9 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007554 ) // ACTOR10 = unknown
        {
          Scene00029(); // Scene00029: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007555 ) // ACTOR11 = unknown
        {
          Scene00030(); // Scene00030: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007556 ) // ACTOR12 = unknown
        {
          Scene00031(); // Scene00031: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007557 ) // ACTOR13 = unknown
        {
          Scene00032(); // Scene00032: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007558 ) // ACTOR14 = unknown
        {
          Scene00033(); // Scene00033: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002064 ) // EOBJECT0 = unknown
        {
          Scene00035(); // Scene00035: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002065 ) // EOBJECT1 = unknown
        {
          Scene00037(); // Scene00037: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002066 ) // EOBJECT2 = unknown
        {
          Scene00039(); // Scene00039: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002677 ) // EOBJECT3 = unknown
        {
          Scene00041(); // Scene00041: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006273 ) // ACTOR0 = GEGERUJU
        {
          Scene00042(); // Scene00042: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GEGERUJU
          break;
        }
        if( param1 == 1006320 ) // ACTOR1 = POACHER
        {
          Scene00043(); // Scene00043: Normal(Talk, TargetCanMove), id=POACHER
          break;
        }
        if( param1 == 1006322 ) // ACTOR3 = unknown
        {
          Scene00044(); // Scene00044: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006323 ) // ACTOR9 = unknown
        {
          Scene00045(); // Scene00045: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007554 ) // ACTOR10 = unknown
        {
          Scene00046(); // Scene00046: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007555 ) // ACTOR11 = unknown
        {
          Scene00047(); // Scene00047: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007556 ) // ACTOR12 = unknown
        {
          Scene00048(); // Scene00048: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007557 ) // ACTOR13 = unknown
        {
          Scene00049(); // Scene00049: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007558 ) // ACTOR14 = unknown
        {
          Scene00050(); // Scene00050: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002064 ) // EOBJECT0 = unknown
        {
          Scene00052(); // Scene00052: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002065 ) // EOBJECT1 = unknown
        {
          Scene00054(); // Scene00054: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002066 ) // EOBJECT2 = unknown
        {
          Scene00056(); // Scene00056: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002677 ) // EOBJECT3 = unknown
        {
          Scene00058(); // Scene00058: Empty(None), id=unknown
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
      quest.UI8BH = 0;
      quest.Sequence = 2;
      quest.UI8CL = 1;
    }
  }
  void checkProgressSeq2()
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
            quest.setBitFlag16( 3, false );
            quest.setBitFlag16( 12, false );
            quest.UI8CL = 0;
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
    player.sendDebug("GaiUsb311:66388 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb311:66388 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=GEGERUJU" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00002: Normal(Talk, TargetCanMove), id=POACHER" );
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
    player.sendDebug("GaiUsb311:66388 calling Scene00003: Empty(None), id=unknown" );
  }

private void Scene00004() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00004: Empty(None), id=unknown" );
  }

private void Scene00005() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00005: Empty(None), id=unknown" );
  }

private void Scene00006() //SEQ_1: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00006: Empty(None), id=unknown" );
  }

private void Scene00007() //SEQ_1: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_1: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_1: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_2: EOBJECT0, UI8AL = 1, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00010: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00011() //SEQ_2: EOBJECT0, UI8AL = 1, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag16( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_2: EOBJECT1, UI8BH = 1, Flag16(2)=True
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00012: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00013();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_2: EOBJECT1, UI8BH = 1, Flag16(2)=True
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag16( 2, true );
    checkProgressSeq2();
  }

private void Scene00014() //SEQ_2: EOBJECT2, UI8BL = 1, Flag16(3)=True
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00014: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00015();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_2: EOBJECT2, UI8BL = 1, Flag16(3)=True
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00015: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag16( 3, true );
    checkProgressSeq2();
  }

private void Scene00016() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00016: Normal(Talk, TargetCanMove), id=POACHER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00017: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00018: Empty(None), id=unknown" );
  }

private void Scene00019() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00019: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00020: Empty(None), id=unknown" );
  }

private void Scene00021() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00021: Empty(None), id=unknown" );
  }

private void Scene00022() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00023() //SEQ_2: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00023: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_2: EOBJECT3, UI8CH = 1, Flag16(12)=True
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00024: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00025();
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00025() //SEQ_2: EOBJECT3, UI8CH = 1, Flag16(12)=True
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00025: Empty(None), id=unknown" );
    quest.UI8CH =  (byte)( 1);
    quest.setBitFlag16( 12, true );
    checkProgressSeq2();
  }

private void Scene00026() //SEQ_3: ACTOR1, UI8AL = 1, Flag16(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00026: Normal(Talk, TargetCanMove), id=POACHER" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00027() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00027: Empty(None), id=unknown" );
  }

private void Scene00028() //SEQ_3: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00028: Empty(None), id=unknown" );
  }

private void Scene00029() //SEQ_3: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00029: Empty(None), id=unknown" );
  }

private void Scene00030() //SEQ_3: ACTOR11, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00030: Empty(None), id=unknown" );
  }

private void Scene00031() //SEQ_3: ACTOR12, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00031: Empty(None), id=unknown" );
  }

private void Scene00032() //SEQ_3: ACTOR13, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00032: Empty(None), id=unknown" );
  }

private void Scene00033() //SEQ_3: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00033: Empty(None), id=unknown" );
  }

private void Scene00035() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00035: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00037() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00037: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00039() //SEQ_3: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00039: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00041() //SEQ_3: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00041: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00042() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00042: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GEGERUJU" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 42, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00043() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00043: Normal(Talk, TargetCanMove), id=POACHER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 43, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00044() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00044: Empty(None), id=unknown" );
  }

private void Scene00045() //SEQ_255: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00045: Empty(None), id=unknown" );
  }

private void Scene00046() //SEQ_255: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00046: Empty(None), id=unknown" );
  }

private void Scene00047() //SEQ_255: ACTOR11, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00047: Empty(None), id=unknown" );
  }

private void Scene00048() //SEQ_255: ACTOR12, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00048: Empty(None), id=unknown" );
  }

private void Scene00049() //SEQ_255: ACTOR13, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00049: Empty(None), id=unknown" );
  }

private void Scene00050() //SEQ_255: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00050: Empty(None), id=unknown" );
  }

private void Scene00052() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00052: Empty(None), id=unknown" );
  }

private void Scene00054() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00054: Empty(None), id=unknown" );
  }

private void Scene00056() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00056: Empty(None), id=unknown" );
  }

private void Scene00058() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb311:66388 calling Scene00058: Empty(None), id=unknown" );
  }
};
}
