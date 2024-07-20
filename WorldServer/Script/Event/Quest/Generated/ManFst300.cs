// FFXIVTheMovie.ParserV3.11
// param used:
//EOBJECT0 = dummy1
//EOBJECT1 = dummy2
//ACTOR5 = ZANTHAEL
//SCENE_7 = dummy1
//SCENE_9 = dummy2
//SCENE_11 = ZANTHAEL
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66047)]
public class ManFst300 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 24 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1005116
  //ACTOR1 = 1003785
  //ACTOR10 = 1004875
  //ACTOR11 = 1004876
  //ACTOR12 = 1006179
  //ACTOR13 = 1004880
  //ACTOR14 = 1004881
  //ACTOR15 = 1004882
  //ACTOR16 = 1006173
  //ACTOR17 = 1006174
  //ACTOR18 = 1006175
  //ACTOR19 = 1006181
  //ACTOR2 = 1004883
  //ACTOR20 = 1006183
  //ACTOR21 = 1006184
  //ACTOR22 = 1006177
  //ACTOR23 = 1006178
  //ACTOR24 = 1005122
  //ACTOR3 = 1004884
  //ACTOR4 = 1004885
  //ACTOR5 = 1001029
  //ACTOR6 = 1004870
  //ACTOR7 = 1004871
  //ACTOR8 = 1004872
  //ACTOR9 = 1004874
  //EOBJECT0 = 2001690
  //EOBJECT1 = 2001691
  //EVENTACTIONWAITING = 10
  //LOCACTION1 = 1002
  //LOCACTOR0 = 1003783
  //LOCSE1 = 42
  //LOCTALKSHAPE1 = 6
  //NCUT0 = 89
  //NCUT1 = 90
  //NCUT2 = 151
  //NCUT3 = 91
  //NCUT4 = 152
  //NCUT5 = 92
  //NCUT6 = 153
  //NCUT7 = 93

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
        // +Callback Scene00001: Normal(CutScene, QuestAccept, AutoFadeIn), id=unknown
        break;
      }
      case 1:
      {
        if( param1 == 1003785 ) // ACTOR1 = TATARU
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=TATARU
          }
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR3 = SERPENTOFFICER
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR4 = STORMOFFICER
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=STORMOFFICER
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 2001690 ) // EOBJECT0 = dummy1
        {
          if( quest.UI8BH != 1 )
          {
            Scene00007(); // Scene00007: Normal(Talk, CutScene, FadeIn, CreateCharacterTalk), id=dummy1
          }
          break;
        }
        if( param1 == 2001691 ) // EOBJECT1 = dummy2
        {
          if( quest.UI8BL != 1 )
          {
            Scene00009(); // Scene00009: Normal(Talk, CutScene, FadeIn, CreateCharacterTalk), id=dummy2
          }
          break;
        }
        if( param1 == 1001029 ) // ACTOR5 = ZANTHAEL
        {
          if( quest.UI8AL != 1 )
          {
            Scene00010(); // Scene00010: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=ZANTHAEL
            // +Callback Scene00011: Normal(Talk, CutScene, FadeIn, CreateCharacterTalk), id=ZANTHAEL
          }
          break;
        }
        if( param1 == 1004870 ) // ACTOR6 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004871 ) // ACTOR7 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004872 ) // ACTOR8 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004874 ) // ACTOR9 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004875 ) // ACTOR10 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004876 ) // ACTOR11 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006179 ) // ACTOR12 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004880 ) // ACTOR13 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004881 ) // ACTOR14 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004882 ) // ACTOR15 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006173 ) // ACTOR16 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006174 ) // ACTOR17 = unknown
        {
          Scene00023(); // Scene00023: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006175 ) // ACTOR18 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006181 ) // ACTOR19 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006183 ) // ACTOR20 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006184 ) // ACTOR21 = unknown
        {
          Scene00027(); // Scene00027: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006177 ) // ACTOR22 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006178 ) // ACTOR23 = unknown
        {
          Scene00029(); // Scene00029: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00030(); // Scene00030: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR3 = SERPENTOFFICER
        {
          Scene00031(); // Scene00031: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR4 = STORMOFFICER
        {
          Scene00032(); // Scene00032: Normal(Talk, TargetCanMove), id=STORMOFFICER
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1005122 ) // ACTOR24 = MINFILIA
        {
          Scene00033(); // Scene00033: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=MINFILIA
          // +Callback Scene00034: Normal(CutScene), id=unknown
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00035(); // Scene00035: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR3 = SERPENTOFFICER
        {
          Scene00036(); // Scene00036: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR4 = STORMOFFICER
        {
          Scene00037(); // Scene00037: Normal(Talk, TargetCanMove), id=STORMOFFICER
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
    if( quest.UI8BH == 1 )
      if( quest.UI8BL == 1 )
        if( quest.UI8AL == 1 )
        {
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
          quest.UI8AL = 0 ;
          quest.setBitFlag24( 1, false );
          quest.setBitFlag24( 2, false );
          quest.setBitFlag24( 3, false );
          quest.Sequence = 255;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManFst300:66047 calling Scene00001: Normal(CutScene, QuestAccept, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("ManFst300:66047 calling Scene00002: Normal(Talk, TargetCanMove), id=TATARU" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00003: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00004: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00005: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: EOBJECT0, UI8BH = 1, Flag24(1)=True
  {
    player.sendDebug("ManFst300:66047 calling Scene00007: Normal(Talk, CutScene, FadeIn, CreateCharacterTalk), id=dummy1" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag24( 1, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00009() //SEQ_2: EOBJECT1, UI8BL = 1, Flag24(2)=True
  {
    player.sendDebug("ManFst300:66047 calling Scene00009: Normal(Talk, CutScene, FadeIn, CreateCharacterTalk), id=dummy2" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag24( 2, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00010() //SEQ_2: ACTOR5, UI8AL = 1, Flag24(3)=True
  {
    player.sendDebug("ManFst300:66047 calling Scene00010: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=ZANTHAEL" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00011();
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_2: ACTOR5, UI8AL = 1, Flag24(3)=True
  {
    player.sendDebug("ManFst300:66047 calling Scene00011: Normal(Talk, CutScene, FadeIn, CreateCharacterTalk), id=ZANTHAEL" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag24( 3, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00013: Empty(None), id=unknown" );
  }

private void Scene00014() //SEQ_2: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00014: Empty(None), id=unknown" );
  }

private void Scene00015() //SEQ_2: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00016() //SEQ_2: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_2: ACTOR11, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00017: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_2: ACTOR12, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00018: Empty(None), id=unknown" );
  }

private void Scene00019() //SEQ_2: ACTOR13, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00019: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_2: ACTOR14, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00020: Empty(None), id=unknown" );
  }

private void Scene00021() //SEQ_2: ACTOR15, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00021: Empty(None), id=unknown" );
  }

private void Scene00022() //SEQ_2: ACTOR16, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00023() //SEQ_2: ACTOR17, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00023: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_2: ACTOR18, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00024: Empty(None), id=unknown" );
  }

private void Scene00025() //SEQ_2: ACTOR19, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00025: Empty(None), id=unknown" );
  }

private void Scene00026() //SEQ_2: ACTOR20, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00026: Empty(None), id=unknown" );
  }

private void Scene00027() //SEQ_2: ACTOR21, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00027: Empty(None), id=unknown" );
  }

private void Scene00028() //SEQ_2: ACTOR22, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00028: Empty(None), id=unknown" );
  }

private void Scene00029() //SEQ_2: ACTOR23, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00029: Empty(None), id=unknown" );
  }

private void Scene00030() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00030: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 30, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00031() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00031: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 31, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00032() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00032: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 32, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00033() //SEQ_255: ACTOR24, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00033: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=MINFILIA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00034();
      }
    };
    owner.Event.NewScene( Id, 33, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00034() //SEQ_255: ACTOR24, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00034: Normal(CutScene), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
    };
    owner.Event.NewScene( Id, 34, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00035() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00035: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 35, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00036() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00036: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 36, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00037() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst300:66047 calling Scene00037: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 37, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
