// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65782)]
public class SubSea923 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 4 entries
  //SEQ_4, 5 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1000856
  //ACTOR1 = 1003585
  //ACTOR2 = 1000857
  //ACTOR3 = 1008882
  //ACTOR4 = 1005414
  //LCUTACTORASCELYN01 = 1008946
  //LCUTACTORCHEER01 = 4165351
  //LCUTACTORFISHERMAN01 = 1010698
  //LCUTACTORFISHER01 = 5049369
  //LCUTACTORNEPTDRAGON01 = 1010699
  //LCUTACTORSISIPU = 1673578
  //LCUTACTORWAWARAGO = 1673577
  //LCUTBGMDISQUIET01 = 78
  //LCUTBGMJOY01 = 87
  //LCUTBGMRISEINARMS01 = 86
  //LCUTBGMTHEMEFACEWAR01 = 83
  //LCUTBGMTHEMEREST0201 = 98
  //LCUTMOTIONBASELIE01 = 981
  //LCUTMOTIONEVENTARMS = 1041
  //LCUTPOSASCELYN01 = 4632830
  //LCUTPOSPC01 = 4653199
  //POPRANGE0 = 4165755
  //RITEM0 = 8756
  //RITEM1 = 8775
  //RITEM2 = 8768
  //RITEM3 = 8772
  //RITEM4 = 8763
  //RITEM5 = 8754

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer), id=unknown
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=WAWALAGO
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00003: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=WAWALAGO
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00005: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=WAWALAGO
        break;
      }
      case 3:
      {
        if( param1 == 1003585 ) // ACTOR1 = STEERSMAN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=STEERSMAN
          }
          break;
        }
        if( param1 == 1000857 ) // ACTOR2 = SISIPU
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=SISIPU
          break;
        }
        if( param1 == 1000856 ) // ACTOR0 = WAWALAGO
        {
          Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=WAWALAGO
          break;
        }
        if( param1 == 1008882 ) // ACTOR3 = ASCELYN
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=ASCELYN
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1005414 ) // ACTOR4 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00010(); // Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00011: Normal(Talk, FadeIn, TargetCanMove), id=unknown
          }
          break;
        }
        if( param1 == 1003585 ) // ACTOR1 = STEERSMAN
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=STEERSMAN
          break;
        }
        if( param1 == 1000857 ) // ACTOR2 = SISIPU
        {
          Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=SISIPU
          break;
        }
        if( param1 == 1000856 ) // ACTOR0 = WAWALAGO
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=WAWALAGO
          break;
        }
        if( param1 == 1008882 ) // ACTOR3 = ASCELYN
        {
          Scene00015(); // Scene00015: Normal(Talk, TargetCanMove), id=ASCELYN
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1000856 ) // ACTOR0 = WAWALAGO
        {
          Scene00016(); // Scene00016: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, ENpcBind), id=WAWALAGO
          break;
        }
        if( param1 == 1000857 ) // ACTOR2 = SISIPU
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=SISIPU
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
    quest.Sequence = 3;
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
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00000: Normal(QuestOffer), id=unknown" );
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
    player.sendDebug("SubSea923:65782 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=WAWALAGO" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubSea923:65782 calling Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00003();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubSea923:65782 calling Scene00003: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=WAWALAGO" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubSea923:65782 calling Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00005();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubSea923:65782 calling Scene00005: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=WAWALAGO" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00006() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubSea923:65782 calling Scene00006: Normal(Talk, TargetCanMove), id=STEERSMAN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00007: Normal(Talk, TargetCanMove), id=SISIPU" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00008: Normal(Talk, TargetCanMove), id=WAWALAGO" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00009: Normal(Talk, TargetCanMove), id=ASCELYN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_4: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubSea923:65782 calling Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00011();
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_4: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubSea923:65782 calling Scene00011: Normal(Talk, FadeIn, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00012() //SEQ_4: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00012: Normal(Talk, TargetCanMove), id=STEERSMAN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_4: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00013: Normal(Talk, TargetCanMove), id=SISIPU" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_4: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00014: Normal(Talk, TargetCanMove), id=WAWALAGO" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_4: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00015: Normal(Talk, TargetCanMove), id=ASCELYN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00016: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, ENpcBind), id=WAWALAGO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 16, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00017() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea923:65782 calling Scene00017: Normal(Talk, TargetCanMove), id=SISIPU" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
