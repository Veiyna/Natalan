// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66155)]
public class SubWil111 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 4 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003929
  //ACTOR1 = 1003964
  //ACTOR2 = 1003965
  //ACTOR3 = 1003966
  //ACTOR4 = 1003967
  //ITEM0 = 2000515

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
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ISEMBARD
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: NpcTrade(Talk, TargetCanMove), id=UNGUST
        // +Callback Scene00002: Normal(Talk, TargetCanMove), id=UNGUST
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( param1 == 1003965 ) // ACTOR2 = POVERTY1
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00003(); // Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=POVERTY1
          }
          break;
        }
        if( param1 == 1003966 ) // ACTOR3 = POVERTY2
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00004(); // Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=POVERTY2
          }
          break;
        }
        if( param1 == 1003967 ) // ACTOR4 = POVERTY3
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00005(); // Scene00005: Normal(Talk, NpcDespawn, TargetCanMove), id=POVERTY3
          }
          break;
        }
        if( param1 == 1003964 ) // ACTOR1 = UNGUST
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=UNGUST
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=UNGUST
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISEMBARD
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
    quest.UI8BH = 1;
  }
  void checkProgressSeq1()
  {
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.Sequence = 3;
    }
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil111:66155 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubWil111:66155 calling Scene00001: NpcTrade(Talk, TargetCanMove), id=UNGUST" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00002();
      }
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubWil111:66155 calling Scene00002: Normal(Talk, TargetCanMove), id=UNGUST" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 3, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubWil111:66155 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=POVERTY1" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR3, UI8AL = 3, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("SubWil111:66155 calling Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=POVERTY2" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR4, UI8AL = 3, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubWil111:66155 calling Scene00005: Normal(Talk, NpcDespawn, TargetCanMove), id=POVERTY3" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil111:66155 calling Scene00006: Normal(Talk, TargetCanMove), id=UNGUST" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("SubWil111:66155 calling Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=UNGUST" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil111:66155 calling Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
