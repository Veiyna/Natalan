// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66963)]
public class SubWil159 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 4 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1007799
  //ACTOR1 = 1007800
  //ACTOR2 = 1001697
  //ACTOR3 = 1007801
  //ACTOR4 = 1007798
  //EVENTACTIONSEARCH = 1
  //LOCACTION0 = 573
  //LOCACTION1 = 574
  //LOCACTOR0 = 1007993
  //LOCBGM1 = 88
  //LOCBGM2 = 82
  //LOCFACE0 = 605
  //LOCFACE1 = 612
  //LOCFACE2 = 615
  //LOCFACE3 = 606
  //LOCPOSACTOR0 = 4672004
  //LOCPOSACTOR1 = 4672622
  //LOCPOSACTOR2 = 4672825

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=PMOLMINN
        // +Callback Scene00001: Normal(QuestAccept), id=unknown
        break;
      }
      case 1:
      {
        if( param1 == 1007800 ) // ACTOR1 = WOMENA01427
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=WOMENA01427
            // +Callback Scene00003: Normal(Talk, TargetCanMove), id=WOMENA01427
          }
          break;
        }
        if( param1 == 1001697 ) // ACTOR2 = SOSOTTA
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=SOSOTTA
            // +Callback Scene00005: Normal(Talk, TargetCanMove), id=SOSOTTA
          }
          break;
        }
        if( param1 == 1007801 ) // ACTOR3 = WOMENB01427
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=WOMENB01427
            // +Callback Scene00007: Normal(Talk, TargetCanMove), id=WOMENB01427
          }
          break;
        }
        if( param1 == 1007799 ) // ACTOR0 = PMOLMINN
        {
          Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=PMOLMINN
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1007798 ) // ACTOR4 = GUILLAUNAUX
        {
          if( quest.UI8AL != 1 )
          {
            Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=GUILLAUNAUX
          }
          break;
        }
        if( param1 == 1007799 ) // ACTOR0 = PMOLMINN
        {
          Scene00010(); // Scene00010: Normal(Talk, TargetCanMove), id=PMOLMINN
          break;
        }
        if( param1 == 1007800 ) // ACTOR1 = WOMENA01427
        {
          Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=WOMENA01427
          break;
        }
        if( param1 == 1007801 ) // ACTOR3 = WOMENB01427
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=WOMENB01427
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1007798 ) // ACTOR4 = GUILLAUNAUX
        {
          Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=GUILLAUNAUX
          // +Callback Scene00014: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=GUILLAUNAUX
          // +Callback Scene00015: Normal(Talk, TargetCanMove), id=GUILLAUNAUX
          break;
        }
        if( param1 == 1007799 ) // ACTOR0 = PMOLMINN
        {
          Scene00016(); // Scene00016: Normal(Talk, TargetCanMove), id=PMOLMINN
          break;
        }
        if( param1 == 1007800 ) // ACTOR1 = WOMENA01427
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=WOMENA01427
          break;
        }
        if( param1 == 1007801 ) // ACTOR3 = WOMENB01427
        {
          Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=WOMENB01427
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
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=PMOLMINN" );
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
    player.sendDebug("SubWil159:66963 calling Scene00001: Normal(QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 3, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil159:66963 calling Scene00002: Normal(Talk, TargetCanMove), id=WOMENA01427" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 3, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil159:66963 calling Scene00003: Normal(Talk, TargetCanMove), id=WOMENA01427" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR2, UI8AL = 3, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil159:66963 calling Scene00004: Normal(Talk, TargetCanMove), id=SOSOTTA" );
    var callback = (SceneResult result) =>
    {
      Scene00005();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_1: ACTOR2, UI8AL = 3, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil159:66963 calling Scene00005: Normal(Talk, TargetCanMove), id=SOSOTTA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_1: ACTOR3, UI8AL = 3, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil159:66963 calling Scene00006: Normal(Talk, TargetCanMove), id=WOMENB01427" );
    var callback = (SceneResult result) =>
    {
      Scene00007();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_1: ACTOR3, UI8AL = 3, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil159:66963 calling Scene00007: Normal(Talk, TargetCanMove), id=WOMENB01427" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00008: Normal(Talk, TargetCanMove), id=PMOLMINN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubWil159:66963 calling Scene00009: Normal(Talk, TargetCanMove), id=GUILLAUNAUX" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_2: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00010: Normal(Talk, TargetCanMove), id=PMOLMINN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00011: Normal(Talk, TargetCanMove), id=WOMENA01427" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00012: Normal(Talk, TargetCanMove), id=WOMENB01427" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00013: Normal(Talk, TargetCanMove), id=GUILLAUNAUX" );
    var callback = (SceneResult result) =>
    {
      Scene00014();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00014() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00014: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=GUILLAUNAUX" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00015();
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
private void Scene00015() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00015: Normal(Talk, TargetCanMove), id=GUILLAUNAUX" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00016: Normal(Talk, TargetCanMove), id=PMOLMINN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00017: Normal(Talk, TargetCanMove), id=WOMENA01427" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil159:66963 calling Scene00018: Normal(Talk, TargetCanMove), id=WOMENB01427" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
