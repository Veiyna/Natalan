// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66213)]
public class ManFst204 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 6 entries
  //SEQ_2, 6 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000100
  //ACTOR1 = 1004612
  //ACTOR2 = 1004619
  //ACTOR3 = 1004620
  //ACTOR4 = 1004613
  //ACTOR5 = 1004614
  //ACTOR6 = 1004615
  //INSTANCEDUNGEON0 = 2
  //LOCACTOR0 = 1003061
  //LOCACTOR1 = 1004616
  //LOCACTOR2 = 1004617
  //LOCACTOR3 = 1004618
  //LOCFACE0 = 604
  //LOCFACE1 = 617
  //LOCFACE2 = 612
  //LOCFACE3 = 605
  //LOCPOSACTOR3 = 4091288
  //SEQ0ACTOR0LQ = 90
  //UNLOCKADDNEWCONTENTTOCF = 3702
  //UNLOCKIMAGEDUNGEONTAMTARA = 76

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
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=MIOUNNE
        break;
      }
      case 1:
      {
        if( param1 == 1004612 ) // ACTOR1 = GODSQUIVERBOW
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=GODSQUIVERBOW
          }
          break;
        }
        if( param1 == 1004619 ) // ACTOR2 = ISILDAURE
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=ISILDAURE
          break;
        }
        if( param1 == 1004620 ) // ACTOR3 = ALIANNE
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=ALIANNE
          break;
        }
        if( param1 == 1004613 ) // ACTOR4 = DOLOROUSBEAR
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=DOLOROUSBEAR
          break;
        }
        if( param1 == 1004614 ) // ACTOR5 = EMANAFA
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=EMANAFA
          break;
        }
        if( param1 == 1004615 ) // ACTOR6 = KIKINA
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=KIKINA
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1004619 ) // ACTOR2 = ISILDAURE
        {
          Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=ISILDAURE
          break;
        }
        if( param1 == 1004620 ) // ACTOR3 = ALIANNE
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=ALIANNE
          break;
        }
        if( param1 == 1004613 ) // ACTOR4 = DOLOROUSBEAR
        {
          Scene00010(); // Scene00010: Normal(Talk, TargetCanMove), id=DOLOROUSBEAR
          break;
        }
        if( param1 == 1004614 ) // ACTOR5 = EMANAFA
        {
          Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=EMANAFA
          break;
        }
        if( param1 == 1004615 ) // ACTOR6 = KIKINA
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=KIKINA
          break;
        }
        if( param1 == 1004612 ) // ACTOR1 = GODSQUIVERBOW
        {
          Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=GODSQUIVERBOW
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00014(); // Scene00014: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=MIOUNNE
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
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManFst204:66213 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=MIOUNNE" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("ManFst204:66213 calling Scene00002: Normal(Talk, TargetCanMove), id=GODSQUIVERBOW" );
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
    player.sendDebug("ManFst204:66213 calling Scene00003: Normal(Talk, TargetCanMove), id=ISILDAURE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00004: Normal(Talk, TargetCanMove), id=ALIANNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00005: Normal(Talk, TargetCanMove), id=DOLOROUSBEAR" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_1: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00006: Normal(Talk, TargetCanMove), id=EMANAFA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00007: Normal(Talk, TargetCanMove), id=KIKINA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00008: Normal(Talk, TargetCanMove), id=ISILDAURE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00009: Normal(Talk, TargetCanMove), id=ALIANNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00010: Normal(Talk, TargetCanMove), id=DOLOROUSBEAR" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00011: Normal(Talk, TargetCanMove), id=EMANAFA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00012: Normal(Talk, TargetCanMove), id=KIKINA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00013: Normal(Talk, TargetCanMove), id=GODSQUIVERBOW" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst204:66213 calling Scene00014: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=MIOUNNE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
