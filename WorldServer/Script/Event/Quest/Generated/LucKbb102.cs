// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(68798)]
public class LucKbb102 : QuestScript
{

  //SEQ_0, 2 entries
  //SEQ_1, 2 entries
  //SEQ_2, 3 entries
  //SEQ_3, 6 entries
  //SEQ_4, 6 entries
  //SEQ_5, 4 entries
  //SEQ_6, 1 entries
  //SEQ_7, 3 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1029033
  //ACTOR1 = 1029034
  //ACTOR10 = 1029040
  //ACTOR11 = 1029041
  //ACTOR12 = 1029042
  //ACTOR13 = 1029089
  //ACTOR14 = 1029043
  //ACTOR15 = 1029130
  //ACTOR16 = 1029131
  //ACTOR17 = 1029044
  //ACTOR18 = 1029045
  //ACTOR2 = 1029035
  //ACTOR3 = 1029036
  //ACTOR4 = 1029037
  //ACTOR5 = 1029038
  //ACTOR6 = 1029039
  //ACTOR7 = 1003063
  //ACTOR8 = 1000286
  //ACTOR9 = 1000288
  //EOBJECT0 = 2010239
  //EVENTACTION0 = 1
  //LEVELENPCID0 = 7953149
  //LEVELENPCID1 = 7953031
  //LEVELENPCID2 = 7953193
  //LEVELENPCID3 = 7953194
  //LEVELENPCID4 = 7953206
  //LEVELENPCID5 = 7953208
  //LEVELENPCID6 = 7953263
  //LEVELENPCID7 = 7953264
  //LEVELENPCID8 = 7954350
  //LOCACTOR0 = 1029026
  //LOCACTOR1 = 1029027
  //LOCACTOR2 = 1029046
  //LOCACTOR3 = 1029047
  //LOCACTOR4 = 1029048
  //LOCACTOR5 = 1029049
  //LOCACTOR6 = 1029050
  //LOCACTOR7 = 1029090
  //LOCBGM0 = 70
  //LOCMOTION0 = 6271
  //LOCMOTION1 = 4303
  //LOCMOTION2 = 951
  //LOCSE0 = 77
  //LOCSE1 = 39
  //LOCVFX0 = 300
  //LOCWEATHER0 = 1
  //QUESTBATTLE0 = 193
  //TERRITORYTYPE0 = 865
  //TERRITORYTYPE1 = 133

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
        if( param1 == 1029033 ) // ACTOR0 = RADOVAN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
            // +Callback Scene00001: Normal(Talk, NpcDespawn, QuestAccept, TargetCanMove, ENpcBind), id=RADOVAN
          }
          break;
        }
        if( param1 == 1029034 ) // ACTOR1 = SOPHIE
        {
          Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        break;
      }
      case 1:
      {
        if( param1 == 1029035 ) // ACTOR2 = RADOVAN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, NpcDespawn, TargetCanMove, ENpcBind), id=RADOVAN
          }
          break;
        }
        if( param1 == 1029036 ) // ACTOR3 = SOPHIE
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1029037 ) // ACTOR4 = LASSENCHOU
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Normal(Talk, NpcDespawn, TargetCanMove, ENpcBind), id=LASSENCHOU
          }
          break;
        }
        if( param1 == 1029038 ) // ACTOR5 = RADOVAN
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=RADOVAN
          break;
        }
        if( param1 == 1029039 ) // ACTOR6 = SOPHIE
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1003063 ) // ACTOR7 = SIMPKIN
        {
          if( quest.UI8BL != 1 )
          {
            Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=SIMPKIN
            // +Callback Scene00009: Normal(Talk, TargetCanMove), id=SIMPKIN
          }
          break;
        }
        if( param1 == 1000286 ) // ACTOR8 = ESTAINE
        {
          if( quest.UI8AL != 1 )
          {
            Scene00010(); // Scene00010: Normal(Talk, TargetCanMove), id=ESTAINE
            // +Callback Scene00011: Normal(Talk, TargetCanMove), id=ESTAINE
          }
          break;
        }
        if( param1 == 1000288 ) // ACTOR9 = FRANCQUET
        {
          if( quest.UI8BH != 1 )
          {
            Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=FRANCQUET
            // +Callback Scene00013: Normal(Talk, TargetCanMove), id=FRANCQUET
          }
          break;
        }
        if( param1 == 1029040 ) // ACTOR10 = ONDINE
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=ONDINE
          break;
        }
        if( param1 == 1029041 ) // ACTOR11 = RADOVAN
        {
          Scene00015(); // Scene00015: Normal(Talk, TargetCanMove), id=RADOVAN
          break;
        }
        if( param1 == 1029042 ) // ACTOR12 = SOPHIE
        {
          Scene00016(); // Scene00016: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1029041 ) // ACTOR11 = RADOVAN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00017(); // Scene00017: Normal(Talk, NpcDespawn, TargetCanMove, SystemTalk, ENpcBind), id=RADOVAN
          }
          break;
        }
        if( param1 == 1029042 ) // ACTOR12 = SOPHIE
        {
          Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        if( param1 == 1029040 ) // ACTOR10 = ONDINE
        {
          Scene00019(); // Scene00019: Normal(Talk, TargetCanMove), id=ONDINE
          break;
        }
        if( param1 == 1003063 ) // ACTOR7 = SIMPKIN
        {
          Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=SIMPKIN
          break;
        }
        if( param1 == 1000286 ) // ACTOR8 = ESTAINE
        {
          Scene00021(); // Scene00021: Normal(Talk, TargetCanMove), id=ESTAINE
          break;
        }
        if( param1 == 1000288 ) // ACTOR9 = FRANCQUET
        {
          Scene00022(); // Scene00022: Normal(Talk, TargetCanMove), id=FRANCQUET
          break;
        }
        break;
      }
      case 5:
      {
        if( param1 == 1029089 ) // ACTOR13 = LASSENCHOU
        {
          if( quest.UI8AL != 1 )
          {
            Scene00023(); // Scene00023: Normal(Talk, QuestBattle, YesNo, TargetCanMove, CanCancel), id=LASSENCHOU
            // +Callback Scene00024: Normal(Talk, FadeIn, TargetCanMove, AutoFadeIn, ENpcBind, ReturnTrue), id=LASSENCHOU
          }
          break;
        }
        if( param1 == 1029038 ) // ACTOR5 = RADOVAN
        {
          Scene00025(); // Scene00025: Normal(Talk, TargetCanMove), id=RADOVAN
          break;
        }
        if( param1 == 1029039 ) // ACTOR6 = SOPHIE
        {
          Scene00026(); // Scene00026: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        if( param1 == 2010239 ) // EOBJECT0 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 6:
      {
        if( type == EVENT_ON_ENTER_TERRITORY ) // BASE_ID_TERRITORY_TYPE = unknown
        {
          Scene00029(); // Scene00029: Normal(Talk, FadeIn, Dismount), id=unknown
          break;
        }
        break;
      }
      case 7:
      {
        if( param1 == 1029043 ) // ACTOR14 = EDITHA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00030(); // Scene00030: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=EDITHA
          }
          break;
        }
        if( param1 == 1029130 ) // ACTOR15 = RADOVAN
        {
          Scene00031(); // Scene00031: Normal(Talk, TargetCanMove), id=RADOVAN
          break;
        }
        if( param1 == 1029131 ) // ACTOR16 = SOPHIE
        {
          Scene00032(); // Scene00032: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1029044 ) // ACTOR17 = RADOVAN
        {
          Scene00033(); // Scene00033: Normal(Talk, QuestReward, QuestComplete, TargetCanMove, ENpcBind), id=RADOVAN
          break;
        }
        if( param1 == 1029045 ) // ACTOR18 = SOPHIE
        {
          Scene00034(); // Scene00034: Normal(Talk, TargetCanMove), id=SOPHIE
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
    if( quest.UI8BL == 1 )
      if( quest.UI8AL == 1 )
        if( quest.UI8BH == 1 )
        {
          quest.UI8BL = 0 ;
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
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
    }
  }
  void checkProgressSeq5()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 6;
    }
  }
  void checkProgressSeq6()
  {
    quest.Sequence = 7;
  }
  void checkProgressSeq7()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: ACTOR0, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("LucKbb102:68798 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("LucKbb102:68798 calling Scene00001: Normal(Talk, NpcDespawn, QuestAccept, TargetCanMove, ENpcBind), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_0: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00002: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove, ENpcBind), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00004: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00005: Normal(Talk, NpcDespawn, TargetCanMove, ENpcBind), id=LASSENCHOU" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00006: Normal(Talk, TargetCanMove), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00007: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: ACTOR7, UI8BL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00008: Normal(Talk, TargetCanMove), id=SIMPKIN" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_3: ACTOR7, UI8BL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00009: Normal(Talk, TargetCanMove), id=SIMPKIN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_3: ACTOR8, UI8AL = 1, Flag8(2)=True
  {
    player.sendDebug("LucKbb102:68798 calling Scene00010: Normal(Talk, TargetCanMove), id=ESTAINE" );
    var callback = (SceneResult result) =>
    {
      Scene00011();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_3: ACTOR8, UI8AL = 1, Flag8(2)=True
  {
    player.sendDebug("LucKbb102:68798 calling Scene00011: Normal(Talk, TargetCanMove), id=ESTAINE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_3: ACTOR9, UI8BH = 1, Flag8(3)=True
  {
    player.sendDebug("LucKbb102:68798 calling Scene00012: Normal(Talk, TargetCanMove), id=FRANCQUET" );
    var callback = (SceneResult result) =>
    {
      Scene00013();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_3: ACTOR9, UI8BH = 1, Flag8(3)=True
  {
    player.sendDebug("LucKbb102:68798 calling Scene00013: Normal(Talk, TargetCanMove), id=FRANCQUET" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_3: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00014: Normal(Talk, TargetCanMove), id=ONDINE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_3: ACTOR11, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00015: Normal(Talk, TargetCanMove), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_3: ACTOR12, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00016: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_4: ACTOR11, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00017: Normal(Talk, NpcDespawn, TargetCanMove, SystemTalk, ENpcBind), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_4: ACTOR12, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00018: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_4: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00019: Normal(Talk, TargetCanMove), id=ONDINE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_4: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00020: Normal(Talk, TargetCanMove), id=SIMPKIN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_4: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00021: Normal(Talk, TargetCanMove), id=ESTAINE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_4: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00022: Normal(Talk, TargetCanMove), id=FRANCQUET" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00023() //SEQ_5: ACTOR13, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00023: Normal(Talk, QuestBattle, YesNo, TargetCanMove, CanCancel), id=LASSENCHOU" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00024();
      }
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00024() //SEQ_5: ACTOR13, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00024: Normal(Talk, FadeIn, TargetCanMove, AutoFadeIn, ENpcBind, ReturnTrue), id=LASSENCHOU" );
    var callback = (SceneResult result) =>
    {
      //quest battle
      owner.Event.StopEvent(Id);
      player.createAndJoinQuestBattle( 193 );
    };
    owner.Event.NewScene( Id, 24, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00025() //SEQ_5: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00025: Normal(Talk, TargetCanMove), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 25, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00026() //SEQ_5: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00026: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00028() //SEQ_5: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00028: Empty(None), id=unknown" );
    checkProgressSeq5();
  }

private void Scene00029() //SEQ_6: BASE_ID_TERRITORY_TYPE, <No Var>, <No Flag>(Todo:5)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00029: Normal(Talk, FadeIn, Dismount), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 5, 0, 0, 0 );
      checkProgressSeq6();
    };
    owner.Event.NewScene( Id, 29, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00030() //SEQ_7: ACTOR14, UI8AL = 1, Flag8(1)=True(Todo:6)
  {
    player.sendDebug("LucKbb102:68798 calling Scene00030: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=EDITHA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 6, 0, 0, 0 );
      checkProgressSeq7();
    };
    owner.Event.NewScene( Id, 30, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00031() //SEQ_7: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00031: Normal(Talk, TargetCanMove), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 31, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00032() //SEQ_7: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00032: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 32, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00033() //SEQ_255: ACTOR17, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00033: Normal(Talk, QuestReward, QuestComplete, TargetCanMove, ENpcBind), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 33, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00034() //SEQ_255: ACTOR18, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb102:68798 calling Scene00034: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 34, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
