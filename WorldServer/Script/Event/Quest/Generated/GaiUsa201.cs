// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66260)]
public class GaiUsa201 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 4 entries
  //SEQ_255, 1 entries

  //ACTIONTIMELINEEVENTTROUBLE = 944
  //ACTOR0 = 1000580
  //ACTOR1 = 1002937
  //ACTOR2 = 1002998
  //EOBJECT0 = 2002270
  //EOBJECT1 = 2000723
  //EOBJECT2 = 2000983
  //EVENTACTIONSEARCH = 1
  //LOCACTION0 = 951
  //LOCACTION1 = 945
  //LOCACTION2 = 953
  //LOCACTION3 = 954
  //QUESTBATTLE0 = 57
  //SEQ0ACTOR0LQ = 90
  //TERRITORYTYPE0 = 303

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
        // +Callback Scene00090: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=KOMUXIO
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=CLAXIO
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=KOMUXIO
        break;
      }
      case 3:
      {
        if( param1 == 1002998 ) // ACTOR2 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 2002270 ) // EOBJECT0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000723 ) // EOBJECT1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000983 ) // EOBJECT2 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00011(); // Scene00011: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO
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
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00090();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00090() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00090: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=KOMUXIO" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 90, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=CLAXIO" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00003: Normal(Talk, TargetCanMove), id=KOMUXIO" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_3: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00004: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 57 );
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00008() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00008: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00010() //SEQ_3: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00011() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa201:66260 calling Scene00011: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 11, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
