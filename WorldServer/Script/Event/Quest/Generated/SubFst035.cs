// FFXIVTheMovie.ParserV3.11
// param used:
//SCENE_19 = CONJURERC
//SCENE_23 = dummyt
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65665)]
public class SubFst035 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 6 entries
  //SEQ_3, 2 entries
  //SEQ_4, 1 entries
  //SEQ_5, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000421
  //ACTOR1 = 1000465
  //ACTOR10 = 1000101
  //ACTOR2 = 1000512
  //ACTOR3 = 1000513
  //ACTOR4 = 1000514
  //ACTOR5 = 1000515
  //ACTOR6 = 1000516
  //ACTOR7 = 1000740
  //ACTOR8 = 1000100
  //ACTOR9 = 1000102
  //CUTSCENE02 = 70
  //CUTSCENE03 = 71
  //CUTSCENE04 = 72
  //EOBJECT0 = 2001232
  //EOBJECT10 = 1140471
  //EOBJECT6 = 2654267
  //EOBJECT7 = 2654268
  //EOBJECT8 = 2654272
  //EOBJECT9 = 1140501
  //EVENTACTION = 26
  //EVENTACTIONSEARCH = 1
  //LOCACTOR0 = 1003064
  //LOGMESSAGERECOMMENDLISTUNLOCK = 3701
  //MOTION3 = 708
  //QUESTBATTLE0 = 15
  //REWARD0 = 9
  //REWARDLEVE = 5
  //SEQ0ACTOR0LQ = 50
  //TERRITORYTYPE0 = 225
  //TERRITORYTYPE1 = 148
  //UNLOCKCHECKINNREWARD = 2
  //UNLOCKIMAGEGEARSET = 29
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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove, CanCancel), id=GALFRID
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=GALFRID
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=ALESTAN
        break;
      }
      case 2:
      {
        if( param1 == 1000512 ) // ACTOR2 = KIKOKUA
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00004(); // Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=KIKOKUA
          }
          break;
        }
        if( param1 == 1000513 ) // ACTOR3 = KIKOKUB
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00007(); // Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=KIKOKUB
          }
          break;
        }
        if( param1 == 1000514 ) // ACTOR4 = KIKOKUC
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00010(); // Scene00010: Normal(Talk, NpcDespawn, TargetCanMove), id=KIKOKUC
          }
          break;
        }
        if( param1 == 1000515 ) // ACTOR5 = CONJURERA
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00013(); // Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=CONJURERA
          }
          break;
        }
        if( param1 == 1000516 ) // ACTOR6 = CONJURERB
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00016(); // Scene00016: Normal(Talk, NpcDespawn, TargetCanMove), id=CONJURERB
          }
          break;
        }
        if( param1 == 1000740 ) // ACTOR7 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1000740 ) // ACTOR7 = CONJURERC
        {
          if( quest.UI8AL != 1 )
          {
            Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=CONJURERC
            // +Callback Scene00019: Normal(QuestBattle, YesNo), id=CONJURERC
          }
          break;
        }
        if( param1 == 2001232 ) // EOBJECT0 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00023(); // Scene00023: Normal(CutScene, AutoFadeIn), id=dummyt
        break;
      }
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00024(); // Scene00024: Normal(Talk, TargetCanMove), id=ALESTAN
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00025(); // Scene00025: Normal(Talk, Message, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=MIOUNNE
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
    if( quest.UI8AL == 5 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.setBitFlag8( 5, false );
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
    quest.Sequence = 5;
  }
  void checkProgressSeq5()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst035:65665 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove, CanCancel), id=GALFRID" );
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
    player.sendDebug("SubFst035:65665 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=GALFRID" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubFst035:65665 calling Scene00002: Normal(Talk, TargetCanMove), id=ALESTAN" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR2, UI8AL = 5, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubFst035:65665 calling Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=KIKOKUA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 5 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR3, UI8AL = 5, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("SubFst035:65665 calling Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=KIKOKUB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 5 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_2: ACTOR4, UI8AL = 5, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubFst035:65665 calling Scene00010: Normal(Talk, NpcDespawn, TargetCanMove), id=KIKOKUC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 5 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_2: ACTOR5, UI8AL = 5, Flag8(4)=True(Todo:1)
  {
    player.sendDebug("SubFst035:65665 calling Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=CONJURERA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 4, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 5 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_2: ACTOR6, UI8AL = 5, Flag8(5)=True(Todo:1)
  {
    player.sendDebug("SubFst035:65665 calling Scene00016: Normal(Talk, NpcDespawn, TargetCanMove), id=CONJURERB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 5, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 5 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst035:65665 calling Scene00017: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_3: ACTOR7, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubFst035:65665 calling Scene00018: Normal(Talk, TargetCanMove), id=CONJURERC" );
    var callback = (SceneResult result) =>
    {
      Scene00019();
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00019() //SEQ_3: ACTOR7, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubFst035:65665 calling Scene00019: Normal(QuestBattle, YesNo), id=CONJURERC" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 15 );
      }
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst035:65665 calling Scene00021: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00023() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubFst035:65665 calling Scene00023: Normal(CutScene, AutoFadeIn), id=dummyt" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 23, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00024() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubFst035:65665 calling Scene00024: Normal(Talk, TargetCanMove), id=ALESTAN" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00025() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst035:65665 calling Scene00025: Normal(Talk, Message, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=MIOUNNE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 25, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
