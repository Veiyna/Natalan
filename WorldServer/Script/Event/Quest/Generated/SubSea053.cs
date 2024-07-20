// FFXIVTheMovie.ParserV3.11
// param used:
//ID_ACTOR1 = 4298896548
//ID_ACTOR2 = 4298896549
//ID_ACTOR3 = 4298896550
//ID_ACTOR4 = 4298896551
//ID_ACTOR5 = 4298896552
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66001)]
public class SubSea053 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002626
  //ACTOR1 = 1002635
  //ACTOR2 = 1002636
  //ACTOR3 = 1002637
  //ACTOR4 = 1002638
  //ACTOR5 = 1002639
  //QSTACCEPTCHECK = 66000

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=STAELWYRN
        break;
      }
      case 1:
      {
        if( param1 == 1002635 || param1 == 4298896548 ) // ACTOR1 = SEVRIN
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00001(); // Scene00001: Normal(Talk, NpcDespawn, TargetCanMove), id=SEVRIN
          }
          break;
        }
        if( param1 == 1002636 || param1 == 4298896549 ) // ACTOR2 = AYLMER
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00002(); // Scene00002: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=AYLMER
          }
          break;
        }
        if( param1 == 1002637 || param1 == 4298896550 ) // ACTOR3 = EYRIMHUS
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00003(); // Scene00003: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=EYRIMHUS
          }
          break;
        }
        if( param1 == 1002638 || param1 == 4298896551 ) // ACTOR4 = SOZAIRARZAI
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00004(); // Scene00004: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=SOZAIRARZAI
          }
          break;
        }
        if( param1 == 1002639 || param1 == 4298896552 ) // ACTOR5 = WAUTER
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00005(); // Scene00005: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=WAUTER
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00006(); // Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STAELWYRN
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
    onProgress(EVENT_ON_WITHIN_RANGE, 0, 0, 0 );
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
    if( quest.UI8AL == 5 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.setBitFlag8( 5, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea053:66001 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubSea053:66001 calling Scene00001: Normal(Talk, NpcDespawn, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubSea053:66001 calling Scene00002: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=AYLMER" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults == 1 || ( result.errorCode == 0 && result.numOfResults == 2 ) )
      {
        quest.UI8AL =  (byte)( quest.UI8AL + 1);
        quest.setBitFlag8( 2, true );
        player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
        checkProgressSeq1();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR3, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubSea053:66001 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=EYRIMHUS" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults == 1 || ( result.errorCode == 0 && result.numOfResults == 2 ) )
      {
        quest.UI8AL =  (byte)( quest.UI8AL + 1);
        quest.setBitFlag8( 3, true );
        player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
        checkProgressSeq1();
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR4, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubSea053:66001 calling Scene00004: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=SOZAIRARZAI" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults == 1 || ( result.errorCode == 0 && result.numOfResults == 2 ) )
      {
        quest.UI8AL =  (byte)( quest.UI8AL + 1);
        quest.setBitFlag8( 4, true );
        player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
        checkProgressSeq1();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR5, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("SubSea053:66001 calling Scene00005: Normal(Talk, NpcDespawn, TargetCanMove, Menu), id=WAUTER" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults == 1 || ( result.errorCode == 0 && result.numOfResults == 2 ) )
      {
        quest.UI8AL =  (byte)( quest.UI8AL + 1);
        quest.setBitFlag8( 5, true );
        player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
        checkProgressSeq1();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea053:66001 calling Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
