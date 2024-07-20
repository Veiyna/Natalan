// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65950)]
public class SubSea117 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 1 entries
  //SEQ_3, 2 entries
  //SEQ_4, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002237
  //ACTOR1 = 1002229
  //ACTOR2 = 1002240
  //ACTOR3 = 1002651
  //ACTOR4 = 1002238
  //EOBJECT0 = 2001730
  //EVENTACTIONSEARCH = 1
  //LOCACTION1 = 961
  //LOCACTOR1 = 1002672
  //QUESTBATTLE0 = 24
  //TERRITORYTYPE0 = 249

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=GHIMTHOTA
        break;
      }
      case 1:
      {
        if( param1 == 1002229 ) // ACTOR1 = URSWYRST
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=URSWYRST
          }
          break;
        }
        if( param1 == 1002240 ) // ACTOR2 = CNANGHO
        {
          if( quest.UI8BH != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=CNANGHO
          }
          break;
        }
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=CORSAIRFOLLOWA
        break;
      }
      case 3:
      {
        if( param1 == 1002237 ) // ACTOR0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 2001730 ) // EOBJECT0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=AHTBYRM
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GHIMTHOTA
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
      if( quest.UI8BH == 1 )
      {
        quest.UI8AL = 0 ;
        quest.UI8BH = 0 ;
        quest.setBitFlag8( 1, false );
        quest.setBitFlag8( 2, false );
        quest.Sequence = 2;
      }
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
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea117:65950 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=GHIMTHOTA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubSea117:65950 calling Scene00001: Normal(Talk, TargetCanMove), id=URSWYRST" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubSea117:65950 calling Scene00002: Normal(Talk, TargetCanMove), id=CNANGHO" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubSea117:65950 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=CORSAIRFOLLOWA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_3: ACTOR0, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubSea117:65950 calling Scene00004: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 24 );
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea117:65950 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00007() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubSea117:65950 calling Scene00007: Normal(Talk, TargetCanMove), id=AHTBYRM" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea117:65950 calling Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GHIMTHOTA" );
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
