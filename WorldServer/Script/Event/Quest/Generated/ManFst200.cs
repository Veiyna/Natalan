// FFXIVTheMovie.ParserV3.11
// param used:
//WARP_SCENE8 = 128|-11.86|92|17|-1.95|true
//WARP_SCENE9 = 132|22.55|-19|114.2|0|false
//WARP_SCENE12 = 128|-13.95|91.5|-5.88|2.89|false
//WARP_SCENE15 = 128|-11.86|92|17|-1.95|true
//WARP_SCENE24 = 128|-24.3|92|2.47|-0.37|false
//WARP_SCENE30 = 130|-44.14|84|-0.47|1.3|true
//WARP_SCENE31 = 128|-24.3|92|2.47|-0.37|false
//WARP_SCENE36 = 130|-19.45|83.2|3.85|1.8|false
//WARP_SCENE41 = 130|-44.14|84|-0.47|1.3|true
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66043)]
public class ManFst200 : QuestScript
{

  //SEQ_0, 2 entries
  //SEQ_1, 1 entries
  //SEQ_2, 2 entries
  //SEQ_3, 3 entries
  //SEQ_4, 4 entries
  //SEQ_5, 4 entries
  //SEQ_6, 3 entries
  //SEQ_7, 4 entries
  //SEQ_8, 5 entries
  //SEQ_255, 5 entries

  //ACTOR0 = 1003027
  //ACTOR1 = 1000460
  //ACTOR10 = 1001821
  //ACTOR2 = 1000100
  //ACTOR3 = 1000106
  //ACTOR4 = 1002693
  //ACTOR5 = 1002703
  //ACTOR6 = 1001029
  //ACTOR7 = 1002695
  //ACTOR8 = 1004336
  //ACTOR9 = 1004434
  //EOBJECT0 = 2001674
  //EOBJECT1 = 2001666
  //EOBJECT2 = 2001677
  //EOBJECT3 = 2001669
  //EOBJECT4 = 2001687
  //EVENTACTIONWAITINGSHOR = 11
  //ITEM0 = 2000456
  //ITEM1 = 2000457
  //NCUTEVENT001 = 64
  //NCUTEVENT002 = 65
  //NCUTEVENT003 = 66
  //NCUTEVENT004 = 154
  //NCUTEVENT005 = 67
  //NCUTEVENT006 = 74
  //NCUTEVENT007 = 155
  //NCUTEVENT008 = 75
  //NCUTEVENT009 = 158
  //NCUTEVENT010 = 230
  //NCUTEVENT011 = 229
  //NCUTEVENT012 = 228
  //NCUTEVENT013 = 227
  //NCUTEVENT014 = 231
  //NCUTEVENT015 = 229
  //POPRANGE0 = 3877982
  //POPRANGE1 = 4103392
  //POPRANGE2 = 4102790
  //POPRANGE3 = 4103399
  //POPRANGE4 = 4103402
  //POPRANGE5 = 4102780
  //POPRANGE6 = 4103339
  //TERRITORYTYPE0 = 205
  //TERRITORYTYPE1 = 132
  //TERRITORYTYPE2 = 128
  //TERRITORYTYPE3 = 130
  //UNLOCKIMAGEAIRSHIP = 68

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
      //seq 0 event item ITEM0 = UI8BH max stack 1
      //seq 0 event item ITEM1 = UI8BL max stack 1
      case 0:
      {
        if( param1 == 1003027 ) // ACTOR0 = KANESENNA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=KANESENNA
            // +Callback Scene00001: Normal(CutScene, QuestAccept), id=unknown
          }
          break;
        }
        if( param1 == 1000460 ) // ACTOR1 = unknown
        {
          Scene00002(); // Scene00002: Normal(YesNo, TargetCanMove), id=unknown
          break;
        }
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      //seq 1 event item ITEM1 = UI8BL max stack 1
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(CutScene), id=unknown
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      //seq 2 event item ITEM1 = UI8BL max stack 1
      case 2:
      {
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          }
          break;
        }
        if( param1 == 2001674 ) // EOBJECT0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      //seq 3 event item ITEM1 = UI8BL max stack 1
      case 3:
      {
        if( param1 == 2001666 ) // EOBJECT1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00007(); // Scene00007: Normal(YesNo, CanCancel), id=unknown
            // +Callback Scene00008: Normal(CutScene, AutoFadeIn), id=unknown
          }
          break;
        }
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          Scene00009(); // Scene00009: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          break;
        }
        if( param1 == 2001674 ) // EOBJECT0 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack 1
      //seq 4 event item ITEM1 = UI8BL max stack 1
      case 4:
      {
        if( param1 == 1002693 ) // ACTOR4 = STORMA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=STORMA
          }
          break;
        }
        if( param1 == 1002703 ) // ACTOR5 = CABINCREW
        {
          Scene00012(); // Scene00012: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=CABINCREW
          break;
        }
        if( param1 == 2001677 ) // EOBJECT2 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          Scene00014(); // Scene00014: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          // +Callback Scene00015: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
          break;
        }
        break;
      }
      //seq 5 event item ITEM0 = UI8BH max stack 1
      //seq 5 event item ITEM1 = UI8BL max stack 1
      case 5:
      {
        if( param1 == 1001029 ) // ACTOR6 = ZANTHAEL
        {
          if( quest.UI8AL != 1 )
          {
            Scene00016(); // Scene00016: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00017: Normal(Talk, TargetCanMove), id=ZANTHAEL
            // +Callback Scene00018: Normal(CutScene), id=unknown
          }
          break;
        }
        if( param1 == 1002693 ) // ACTOR4 = STORMA
        {
          Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=STORMA
          break;
        }
        if( param1 == 2001677 ) // EOBJECT2 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          Scene00022(); // Scene00022: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          // +Callback Scene00023: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
          break;
        }
        break;
      }
      //seq 6 event item ITEM1 = UI8BH max stack 1
      case 6:
      {
        if( param1 == 1002695 ) // ACTOR7 = LNOPHLO
        {
          if( quest.UI8AL != 1 )
          {
            Scene00024(); // Scene00024: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO
          }
          break;
        }
        if( param1 == 2001677 ) // EOBJECT2 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          Scene00026(); // Scene00026: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          // +Callback Scene00027: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
          break;
        }
        break;
      }
      //seq 7 event item ITEM1 = UI8BH max stack 1
      case 7:
      {
        if( param1 == 2001669 ) // EOBJECT3 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00029(); // Scene00029: Normal(YesNo, CanCancel), id=unknown
            // +Callback Scene00030: Normal(CutScene, AutoFadeIn), id=unknown
          }
          break;
        }
        if( param1 == 1002695 ) // ACTOR7 = LNOPHLO
        {
          Scene00031(); // Scene00031: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO
          break;
        }
        if( param1 == 2001677 ) // EOBJECT2 = unknown
        {
          Scene00032(); // Scene00032: Empty(None), id=unknown
          break;
        }
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          Scene00033(); // Scene00033: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          // +Callback Scene00034: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
          break;
        }
        break;
      }
      //seq 8 event item ITEM1 = UI8BH max stack 1
      case 8:
      {
        if( param1 == 1004336 ) // ACTOR8 = FLAMEA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00035(); // Scene00035: Normal(Talk, TargetCanMove), id=FLAMEA
          }
          break;
        }
        if( param1 == 1004434 ) // ACTOR9 = CABINCREW
        {
          Scene00036(); // Scene00036: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=CABINCREW
          break;
        }
        if( param1 == 2001687 ) // EOBJECT4 = unknown
        {
          Scene00037(); // Scene00037: Empty(None), id=unknown
          break;
        }
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          Scene00038(); // Scene00038: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          // +Callback Scene00039: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
          break;
        }
        if( param1 == 1002695 ) // ACTOR7 = LNOPHLO
        {
          Scene00040(); // Scene00040: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO
          // +Callback Scene00041: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM1 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1001821 ) // ACTOR10 = BARTHOLOMEW
        {
          Scene00042(); // Scene00042: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00043: Normal(Talk, TargetCanMove), id=BARTHOLOMEW
          // +Callback Scene00044: Normal(CutScene), id=unknown
          // +Callback Scene00045: Normal(QuestReward, QuestComplete, SystemTalk), id=unknown
          break;
        }
        if( param1 == 1004336 ) // ACTOR8 = FLAMEA
        {
          Scene00047(); // Scene00047: Normal(Talk, TargetCanMove), id=FLAMEA
          break;
        }
        if( param1 == 2001687 ) // EOBJECT4 = unknown
        {
          Scene00048(); // Scene00048: Empty(None), id=unknown
          break;
        }
        if( param1 == 1000106 ) // ACTOR3 = LIONNELLAIS
        {
          Scene00049(); // Scene00049: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS
          // +Callback Scene00050: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
          break;
        }
        if( param1 == 1002695 ) // ACTOR7 = LNOPHLO
        {
          Scene00051(); // Scene00051: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO
          // +Callback Scene00052: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown
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
    quest.Sequence = 2;
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
      quest.setBitFlag8( 1, false );
      quest.Sequence = 4;
    }
  }
  void checkProgressSeq4()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 5;
      quest.UI8BH = 1;
      quest.UI8BL = 1;
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
      quest.Sequence = 6;
    }
  }
  void checkProgressSeq6()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 7;
    }
  }
  void checkProgressSeq7()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 8;
    }
  }
  void checkProgressSeq8()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
      quest.UI8BH = 1;
    }
  }

private void Scene00000() //SEQ_0: ACTOR0, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("ManFst200:66043 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=KANESENNA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00001();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00001() //SEQ_0: ACTOR0, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("ManFst200:66043 calling Scene00001: Normal(CutScene, QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_0: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00002: Normal(YesNo, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("ManFst200:66043 calling Scene00003: Normal(CutScene), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("ManFst200:66043 calling Scene00004: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        quest.UI8AL =  (byte)( 1);
        quest.setBitFlag8( 1, true );
        player.SendQuestMessage(Id, 1, 0, 0, 0 );
        checkProgressSeq2();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_3: EOBJECT1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("ManFst200:66043 calling Scene00007: Normal(YesNo, CanCancel), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00008();
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_3: EOBJECT1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("ManFst200:66043 calling Scene00008: Normal(CutScene, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
      player.Event.StopEvent(Id);
      //player.TeleportTo(new WorldPosition(128,new Vector3(-11.86f, 92f, 17f), -1.95f));
      //warpMgr not supporting show zone text, fallback to old api
      player.TeleportTo(new WorldPosition( 128, new Vector3(-11.86f, 92.0f, 17.0f), -1.95f));
    };
    owner.Event.NewScene( Id, 8, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00009() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00009: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        player.Event.StopEvent(Id);
        player.TeleportTo(new WorldPosition(132,new Vector3(22.55f, -19f, 114.2f), 0f));
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00011() //SEQ_4: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("ManFst200:66043 calling Scene00011: Normal(Talk, TargetCanMove), id=STORMA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_4: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00012: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=CABINCREW" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        player.Event.StopEvent(Id);
        player.TeleportTo(new WorldPosition(128,new Vector3(-13.95f, 91.5f, -5.88f), 2.89f));
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_4: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00013: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00014() //SEQ_4: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00014: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00015();
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_4: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00015: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.Event.StopEvent(Id);
      //player.TeleportTo(new WorldPosition(128,new Vector3(-11.86f, 92f, 17f), -1.95f));
      //warpMgr not supporting show zone text, fallback to old api
      player.TeleportTo(new WorldPosition( 128, new Vector3(-11.86f, 92.0f, 17.0f), -1.95f));
    };
    owner.Event.NewScene( Id, 15, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00016() //SEQ_5: ACTOR6, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("ManFst200:66043 calling Scene00016: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00017();
      }
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00017() //SEQ_5: ACTOR6, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("ManFst200:66043 calling Scene00017: Normal(Talk, TargetCanMove), id=ZANTHAEL" );
    var callback = (SceneResult result) =>
    {
      Scene00018();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00018() //SEQ_5: ACTOR6, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("ManFst200:66043 calling Scene00018: Normal(CutScene), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 18, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00020() //SEQ_5: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00020: Normal(Talk, TargetCanMove), id=STORMA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_5: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00021: Empty(None), id=unknown" );
    checkProgressSeq5();
  }

private void Scene00022() //SEQ_5: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00022: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00023();
      }
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00023() //SEQ_5: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00023: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 23, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00024() //SEQ_6: ACTOR7, UI8AL = 1, Flag8(1)=True(Todo:5)
  {
    player.sendDebug("ManFst200:66043 calling Scene00024: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        quest.UI8AL =  (byte)( 1);
        quest.setBitFlag8( 1, true );
        player.SendQuestMessage(Id, 5, 0, 0, 0 );
        checkProgressSeq6();
        player.Event.StopEvent(Id);
        player.TeleportTo(new WorldPosition(128,new Vector3(-24.3f, 92f, 2.47f), -0.37f));
      }
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00025() //SEQ_6: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00025: Empty(None), id=unknown" );
    checkProgressSeq6();
  }

private void Scene00026() //SEQ_6: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00026: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00027();
      }
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00027() //SEQ_6: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00027: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 27, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00029() //SEQ_7: EOBJECT3, UI8AL = 1, Flag8(1)=True(Todo:6)
  {
    player.sendDebug("ManFst200:66043 calling Scene00029: Normal(YesNo, CanCancel), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00030();
      }
    };
    owner.Event.NewScene( Id, 29, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00030() //SEQ_7: EOBJECT3, UI8AL = 1, Flag8(1)=True(Todo:6)
  {
    player.sendDebug("ManFst200:66043 calling Scene00030: Normal(CutScene, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 6, 0, 0, 0 );
      checkProgressSeq7();
      player.Event.StopEvent(Id);
      //player.TeleportTo(new WorldPosition(130,new Vector3(-44.14f, 84f, -0.47f), 1.3f));
      //warpMgr not supporting show zone text, fallback to old api
      player.TeleportTo(new WorldPosition( 130, new Vector3(-44.14f, 84.0f, -0.47f), 1.3f));
    };
    owner.Event.NewScene( Id, 30, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00031() //SEQ_7: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00031: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        player.Event.StopEvent(Id);
        player.TeleportTo(new WorldPosition(128,new Vector3(-24.3f, 92f, 2.47f), -0.37f));
      }
    };
    owner.Event.NewScene( Id, 31, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00032() //SEQ_7: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00032: Empty(None), id=unknown" );
    checkProgressSeq7();
  }

private void Scene00033() //SEQ_7: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00033: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00034();
      }
    };
    owner.Event.NewScene( Id, 33, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00034() //SEQ_7: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00034: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 34, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00035() //SEQ_8: ACTOR8, UI8AL = 1, Flag8(1)=True(Todo:7)
  {
    player.sendDebug("ManFst200:66043 calling Scene00035: Normal(Talk, TargetCanMove), id=FLAMEA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 7, 0, 0, 0 );
      checkProgressSeq8();
    };
    owner.Event.NewScene( Id, 35, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00036() //SEQ_8: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00036: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=CABINCREW" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        player.Event.StopEvent(Id);
        player.TeleportTo(new WorldPosition(130,new Vector3(-19.45f, 83.2f, 3.85f), 1.8f));
      }
    };
    owner.Event.NewScene( Id, 36, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00037() //SEQ_8: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00037: Empty(None), id=unknown" );
    checkProgressSeq8();
  }

private void Scene00038() //SEQ_8: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00038: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00039();
      }
    };
    owner.Event.NewScene( Id, 38, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00039() //SEQ_8: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00039: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 39, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00040() //SEQ_8: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00040: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00041();
      }
    };
    owner.Event.NewScene( Id, 40, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00041() //SEQ_8: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00041: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.Event.StopEvent(Id);
      //player.TeleportTo(new WorldPosition(130,new Vector3(-44.14f, 84f, -0.47f), 1.3f));
      //warpMgr not supporting show zone text, fallback to old api
      player.TeleportTo(new WorldPosition( 130, new Vector3(-44.14f, 84.0f, -0.47f), 1.3f));
    };
    owner.Event.NewScene( Id, 41, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00042() //SEQ_255: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00042: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00043();
      }
    };
    owner.Event.NewScene( Id, 42, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00043() //SEQ_255: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00043: Normal(Talk, TargetCanMove), id=BARTHOLOMEW" );
    var callback = (SceneResult result) =>
    {
      Scene00044();
    };
    owner.Event.NewScene( Id, 43, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00044() //SEQ_255: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00044: Normal(CutScene), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00045();
    };
    owner.Event.NewScene( Id, 44, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
private void Scene00045() //SEQ_255: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00045: Normal(QuestReward, QuestComplete, SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 45, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00047() //SEQ_255: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00047: Normal(Talk, TargetCanMove), id=FLAMEA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 47, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00048() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00048: Empty(None), id=unknown" );
  }

private void Scene00049() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00049: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LIONNELLAIS" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00050();
      }
    };
    owner.Event.NewScene( Id, 49, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00050() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00050: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 50, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00051() //SEQ_255: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00051: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=LNOPHLO" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00052();
      }
    };
    owner.Event.NewScene( Id, 51, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00052() //SEQ_255: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst200:66043 calling Scene00052: Normal(CutScene, AutoFadeIn, ReturnTrue), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 52, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
