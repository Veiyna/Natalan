// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66196)]
public class ManFst205 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 4 entries
  //SEQ_3, 3 entries
  //SEQ_4, 2 entries
  //SEQ_5, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1001353
  //ACTOR1 = 1004621
  //ACTOR2 = 1002285
  //ACTOR3 = 1004623
  //ACTOR4 = 1004624
  //CUTSCENE04FST = 79
  //CUTSCENE04SEA = 215
  //CUTSCENE04WIL = 216
  //EOBJECT0 = 2001854
  //INSTANCEDUNGEON0 = 3
  //LOCACTOR0 = 1003995
  //LOCACTOR1 = 1004618
  //LOCACTOR2 = 1005137
  //QUESTBATTLE0 = 44
  //SEQ0ACTOR0LQ = 90
  //TERRITORYTYPE0 = 274
  //TERRITORYTYPE1 = 130
  //UNLOCKADDNEWCONTENTTOCF = 3702
  //UNLOCKIMAGEDUNGEONCOPPERBELL = 75

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
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=MOMODI
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=PAINTEDMESA
        break;
      }
      case 2:
      {
        if( param1 == 1002285 ) // ACTOR2 = STONETORCH
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=STONETORCH
          }
          break;
        }
        if( param1 == 1004623 ) // ACTOR3 = ISILDAURE
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=ISILDAURE
          break;
        }
        if( param1 == 1004624 ) // ACTOR4 = ALIANNE
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=ALIANNE
          break;
        }
        if( param1 == 1004621 ) // ACTOR1 = PAINTEDMESA
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=PAINTEDMESA
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1004623 ) // ACTOR3 = ISILDAURE
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=ISILDAURE
          break;
        }
        if( param1 == 1004624 ) // ACTOR4 = ALIANNE
        {
          Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=ALIANNE
          break;
        }
        if( param1 == 1002285 ) // ACTOR2 = STONETORCH
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=STONETORCH
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1004621 ) // ACTOR1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00010(); // Scene00010: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 2001854 ) // EOBJECT0 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(CutScene, AutoFadeIn), id=unknown
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00013(); // Scene00013: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=MOMODI
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
    quest.Sequence = 4;
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
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManFst205:66196 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=MOMODI" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("ManFst205:66196 calling Scene00002: Normal(Talk, TargetCanMove), id=PAINTEDMESA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("ManFst205:66196 calling Scene00003: Normal(Talk, TargetCanMove), id=STONETORCH" );
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
    player.sendDebug("ManFst205:66196 calling Scene00004: Normal(Talk, TargetCanMove), id=ISILDAURE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00005: Normal(Talk, TargetCanMove), id=ALIANNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00006: Normal(Talk, TargetCanMove), id=PAINTEDMESA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00007: Normal(Talk, TargetCanMove), id=ISILDAURE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00008: Normal(Talk, TargetCanMove), id=ALIANNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00009: Normal(Talk, TargetCanMove), id=STONETORCH" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_4: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("ManFst205:66196 calling Scene00010: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 44 );
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_4: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00012() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("ManFst205:66196 calling Scene00012: Normal(CutScene, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 12, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00013() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst205:66196 calling Scene00013: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=MOMODI" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
