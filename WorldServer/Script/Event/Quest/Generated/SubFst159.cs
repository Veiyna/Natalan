// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66961)]
public class SubFst159 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 2 entries
  //SEQ_3, 4 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1000375
  //ACTOR1 = 1007792
  //ACTOR2 = 1000153
  //ACTOR3 = 1007793
  //ACTOR4 = 1007794
  //ACTOR5 = 1007795
  //ACTOR6 = 1007969
  //ACTOR7 = 1007970
  //ACTOR8 = 1007971
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2001249
  //LOCACTOR0 = 1007990
  //LOCACTOR1 = 1007991
  //LOCACTOR2 = 1007992
  //LOCACTOR3 = 1000153
  //LOCBGM = 88
  //LOCBGM2 = 87
  //LOCFACE0 = 604
  //LOCFACE1 = 605
  //LOCFACE2 = 614
  //LOCMOTION1 = 571
  //LOCMOTION2 = 572
  //LOCPOSACTOR0 = 4666289
  //LOCPOSACTOR1 = 4666740
  //QUEST0 = 65741

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=ERAL
        // +Callback Scene00001: Normal(QuestAccept), id=unknown
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=YEADYTHE
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( param1 == 1000153 ) // ACTOR2 = BEATINE
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=BEATINE
          }
          break;
        }
        if( param1 == 1007792 ) // ACTOR1 = YEADYTHE
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=YEADYTHE
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      case 3:
      {
        if( param1 == 1007793 ) // ACTOR3 = BOY01425
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00005(); // Scene00005: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=BOY01425
          }
          break;
        }
        if( param1 == 1007794 ) // ACTOR4 = GIRL01425
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00008(); // Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00009: Normal(Talk, NpcDespawn, TargetCanMove), id=GIRL01425
          }
          break;
        }
        if( param1 == 1007795 ) // ACTOR5 = BOY201425
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00011(); // Scene00011: NpcTrade(Talk), id=unknown
            // +Callback Scene00012: Normal(Talk, NpcDespawn, TargetCanMove), id=BOY201425
          }
          break;
        }
        if( param1 == 1007792 ) // ACTOR1 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1007792 ) // ACTOR1 = YEADYTHE
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=YEADYTHE
          // +Callback Scene00015: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=YEADYTHE
          break;
        }
        if( param1 == 1007969 ) // ACTOR6 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007970 ) // ACTOR7 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007971 ) // ACTOR8 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
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
      quest.UI8BH = 1;
    }
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.UI8BH = 0;
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=ERAL" );
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
    player.sendDebug("SubFst159:66961 calling Scene00001: Normal(QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubFst159:66961 calling Scene00002: Normal(Talk, TargetCanMove), id=YEADYTHE" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubFst159:66961 calling Scene00003: Normal(Talk, TargetCanMove), id=BEATINE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00004: Normal(Talk, TargetCanMove), id=YEADYTHE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_3: ACTOR3, UI8AL = 3, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubFst159:66961 calling Scene00005: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00006();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_3: ACTOR3, UI8AL = 3, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubFst159:66961 calling Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=BOY01425" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 3 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: ACTOR4, UI8AL = 3, Flag8(2)=True(Todo:2)
  {
    player.sendDebug("SubFst159:66961 calling Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00009();
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_3: ACTOR4, UI8AL = 3, Flag8(2)=True(Todo:2)
  {
    player.sendDebug("SubFst159:66961 calling Scene00009: Normal(Talk, NpcDespawn, TargetCanMove), id=GIRL01425" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 3 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_3: ACTOR5, UI8AL = 3, Flag8(3)=True(Todo:2)
  {
    player.sendDebug("SubFst159:66961 calling Scene00011: NpcTrade(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00012();
      }
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00012() //SEQ_3: ACTOR5, UI8AL = 3, Flag8(3)=True(Todo:2)
  {
    player.sendDebug("SubFst159:66961 calling Scene00012: Normal(Talk, NpcDespawn, TargetCanMove), id=BOY201425" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 3 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_3: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00013: Empty(None), id=unknown" );
  }

private void Scene00014() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00014: Normal(Talk, TargetCanMove), id=YEADYTHE" );
    var callback = (SceneResult result) =>
    {
      Scene00015();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00015: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=YEADYTHE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 15, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00016() //SEQ_255: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_255: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00017: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_255: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst159:66961 calling Scene00018: Empty(None), id=unknown" );
  }
};
}
