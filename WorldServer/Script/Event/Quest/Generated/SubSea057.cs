// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66005)]
public class SubSea057 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 7 entries
  //SEQ_3, 6 entries
  //SEQ_4, 6 entries
  //SEQ_5, 1 entries
  //SEQ_6, 2 entries
  //SEQ_7, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002626
  //ACTOR1 = 1002633
  //ACTOR10 = 1000972
  //ACTOR2 = 1002645
  //ACTOR3 = 1002644
  //ACTOR4 = 1002646
  //ACTOR5 = 1002647
  //ACTOR6 = 1002648
  //ACTOR7 = 1002649
  //ACTOR8 = 1002650
  //ACTOR9 = 1003349
  //CUTSCENE02 = 204
  //CUTSCENE03 = 205
  //CUTSCENE04 = 206
  //EOBJECT0 = 2001734
  //EVENTACTION = 35
  //EVENTACTIONSEARCH = 1
  //LOCACTOR1 = 1003289
  //LOCACTOR2 = 1000970
  //LOCACTOR3 = 1000974
  //LOCPOSACTOR2 = 4155969
  //LOCPOSACTOR3 = 4155970
  //LOGMESSAGERECOMMENDLISTUNLOCK = 3701
  //QUESTBATTLE0 = 29
  //REWARD0 = 9
  //SEQ0ACTOR0LQ = 90
  //TERRITORYTYPE0 = 272
  //TERRITORYTYPE1 = 134
  //UNLOCKIMAGEINN = 18
  //UNLOCKIMAGELEVE = 20

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=STAELWYRN
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=STAELWYRN
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=GRYNEWYDA
        break;
      }
      case 2:
      {
        if( param1 == 1002645 ) // ACTOR2 = AYLMER
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=AYLMER
          }
          break;
        }
        if( param1 == 1002644 ) // ACTOR3 = SEVRIN
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=SEVRIN
          break;
        }
        if( param1 == 1002646 ) // ACTOR4 = EYRIMHUS
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=EYRIMHUS
          break;
        }
        if( param1 == 1002647 ) // ACTOR5 = SOZAIRARZAI
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=SOZAIRARZAI
          break;
        }
        if( param1 == 1002648 ) // ACTOR6 = IREZUMI
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=IREZUMI
          break;
        }
        if( param1 == 1002649 ) // ACTOR7 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1002650 ) // ACTOR8 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1002646 ) // ACTOR4 = EYRIMHUS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00011(); // Scene00011: Normal(Talk, NpcDespawn, TargetCanMove), id=EYRIMHUS
          }
          break;
        }
        if( param1 == 1002644 ) // ACTOR3 = SEVRIN
        {
          Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=SEVRIN
          break;
        }
        if( param1 == 1002648 ) // ACTOR6 = IREZUMI
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=IREZUMI
          break;
        }
        if( param1 == 1002649 ) // ACTOR7 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 1002650 ) // ACTOR8 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 1002647 ) // ACTOR5 = SOZAIRARZAI
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=SOZAIRARZAI
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1002647 ) // ACTOR5 = SOZAIRARZAI
        {
          if( quest.UI8AL != 1 )
          {
            Scene00019(); // Scene00019: Normal(Talk, QuestBattle, YesNo, TargetCanMove), id=SOZAIRARZAI
          }
          break;
        }
        if( param1 == 1002648 ) // ACTOR6 = IREZUMI
        {
          Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=IREZUMI
          break;
        }
        if( param1 == 1002649 ) // ACTOR7 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 1002650 ) // ACTOR8 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 1002644 ) // ACTOR3 = SEVRIN
        {
          Scene00023(); // Scene00023: Normal(Talk, TargetCanMove), id=SEVRIN
          break;
        }
        if( param1 == 2001734 ) // EOBJECT0 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00026(); // Scene00026: Normal(CutScene, AutoFadeIn), id=unknown
        break;
      }
      case 6:
      {
        if( param1 == 1002626 ) // ACTOR0 = STAELWYRN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00027(); // Scene00027: Normal(Talk, FadeIn, TargetCanMove), id=STAELWYRN
          }
          break;
        }
        if( param1 == 1003349 ) // ACTOR9 = SEVRIN
        {
          Scene00028(); // Scene00028: Normal(Talk, TargetCanMove), id=SEVRIN
          break;
        }
        break;
      }
      case 7:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00029(); // Scene00029: Normal(Talk, TargetCanMove), id=STAELWYRN
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00030(); // Scene00030: Normal(Talk, Message, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=BADERON
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
    }
  }
  void checkProgressSeq5()
  {
    quest.Sequence = 6;
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
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=STAELWYRN" );
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
    player.sendDebug("SubSea057:66005 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubSea057:66005 calling Scene00002: Normal(Talk, TargetCanMove), id=GRYNEWYDA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubSea057:66005 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=AYLMER" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00004: Normal(Talk, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00005: Normal(Talk, TargetCanMove), id=EYRIMHUS" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00006: Normal(Talk, TargetCanMove), id=SOZAIRARZAI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00007: Normal(Talk, TargetCanMove), id=IREZUMI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_2: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00011() //SEQ_3: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubSea057:66005 calling Scene00011: Normal(Talk, NpcDespawn, TargetCanMove), id=EYRIMHUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00013: Normal(Talk, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_3: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00014: Normal(Talk, TargetCanMove), id=IREZUMI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_3: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00016() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_3: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00017: Normal(Talk, TargetCanMove), id=SOZAIRARZAI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_4: ACTOR5, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubSea057:66005 calling Scene00019: Normal(Talk, QuestBattle, YesNo, TargetCanMove), id=SOZAIRARZAI" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 29 );
      }
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_4: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00020: Normal(Talk, TargetCanMove), id=IREZUMI" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_4: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00021: Empty(None), id=unknown" );
  }

private void Scene00022() //SEQ_4: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00023() //SEQ_4: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00023: Normal(Talk, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00025() //SEQ_4: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00025: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00026() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubSea057:66005 calling Scene00026: Normal(CutScene, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 26, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00027() //SEQ_6: ACTOR0, UI8AL = 1, Flag8(1)=True(Todo:5)
  {
    player.sendDebug("SubSea057:66005 calling Scene00027: Normal(Talk, FadeIn, TargetCanMove), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 5, 0, 0, 0 );
      checkProgressSeq6();
    };
    owner.Event.NewScene( Id, 27, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00028() //SEQ_6: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00028: Normal(Talk, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 28, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00029() //SEQ_7: , <No Var>, <No Flag>(Todo:6)
  {
    player.sendDebug("SubSea057:66005 calling Scene00029: Normal(Talk, TargetCanMove), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 6, 0, 0, 0 );
      checkProgressSeq7();
    };
    owner.Event.NewScene( Id, 29, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00030() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea057:66005 calling Scene00030: Normal(Talk, Message, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=BADERON" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 30, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
