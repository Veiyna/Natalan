// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65949)]
public class SubSea116 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 5 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002238
  //ACTOR1 = 1002237
  //ACTOR2 = 1002240
  //ACTOR3 = 1002463
  //ACTOR4 = 1002464
  //ACTOR5 = 1002465
  //EOBJECT0 = 2000766
  //EOBJECT1 = 2000767
  //EVENTACTIONSEARCH = 1
  //LOCACTOR1 = 1002534
  //LOCACTOR2 = 1002535
  //LOCEOBJ1 = 2000768

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=AHTBYRM
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=GHIMTHOTA
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=CNANGHO
        break;
      }
      case 3:
      {
        if( param1 == 1002463 ) // ACTOR3 = CORSAIR
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, FadeIn, TargetCanMove), id=CORSAIR
          }
          break;
        }
        if( param1 == 1002464 ) // ACTOR4 = CORSAIRFOLLOWA
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=CORSAIRFOLLOWA
          break;
        }
        if( param1 == 1002465 ) // ACTOR5 = CORSAIRFOLLOWB
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=CORSAIRFOLLOWB
          break;
        }
        if( param1 == 2000766 ) // EOBJECT0 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000767 ) // EOBJECT1 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GHIMTHOTA
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
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea116:65949 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=AHTBYRM" );
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
    player.sendDebug("SubSea116:65949 calling Scene00001: Normal(Talk, TargetCanMove), id=GHIMTHOTA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubSea116:65949 calling Scene00002: Normal(Talk, TargetCanMove), id=CNANGHO" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_3: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubSea116:65949 calling Scene00003: Normal(Talk, FadeIn, TargetCanMove), id=CORSAIR" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_3: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea116:65949 calling Scene00004: Normal(Talk, TargetCanMove), id=CORSAIRFOLLOWA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_3: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea116:65949 calling Scene00005: Normal(Talk, TargetCanMove), id=CORSAIRFOLLOWB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea116:65949 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00009() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea116:65949 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea116:65949 calling Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GHIMTHOTA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
