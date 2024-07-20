// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66080)]
public class ManSea007 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 5 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000972
  //ACTOR1 = 1003282
  //ACTOR2 = 1003283
  //ACTOR3 = 1003284
  //ACTOR4 = 1003285
  //ACTOR5 = 1003286
  //CUTEVENT0 = 207
  //CUTEVENT1 = 211
  //CUTEVENT2 = 136
  //CUTSCENE03 = 210
  //EOBJECT0 = 2001739
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000519
  //LOCACTOR0 = 1005031
  //LOCFACE0 = 604
  //LOCFACE1 = 617
  //QUESTBATTLE0 = 36
  //TERRITORYTYPE0 = 280
  //TERRITORYTYPE1 = 138

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=BADERON
        // +Callback Scene00001: Normal(CutScene, QuestAccept), id=unknown
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00003: Normal(Talk, FadeIn, TargetCanMove), id=REYNER
        break;
      }
      case 2:
      {
        if( param1 == 1003283 ) // ACTOR2 = RYSSFLOH
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, QuestBattle, YesNo, TargetCanMove), id=RYSSFLOH
          }
          break;
        }
        if( param1 == 1003284 ) // ACTOR3 = GLAZRAEL
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=GLAZRAEL
          break;
        }
        if( param1 == 1003285 ) // ACTOR4 = YELLOWJACKETA
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=YELLOWJACKETA
          break;
        }
        if( param1 == 1003286 ) // ACTOR5 = YELLOWJACKETB
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=YELLOWJACKETB
          break;
        }
        if( param1 == 2001739 ) // EOBJECT0 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00010(); // Scene00010: Normal(CutScene, AutoFadeIn), id=unknown
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=REYNER
        // +Callback Scene00012: Normal(CutScene, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=unknown
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
    quest.UI8BH = 1;
  }
  void checkProgressSeq1()
  {
    quest.UI8BH = 0;
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
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea007:66080 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=BADERON" );
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
    player.sendDebug("ManSea007:66080 calling Scene00001: Normal(CutScene, QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("ManSea007:66080 calling Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManSea007:66080 calling Scene00003: Normal(Talk, FadeIn, TargetCanMove), id=REYNER" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("ManSea007:66080 calling Scene00004: Normal(Talk, QuestBattle, YesNo, TargetCanMove), id=RYSSFLOH" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 36 );
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea007:66080 calling Scene00005: Normal(Talk, TargetCanMove), id=GLAZRAEL" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea007:66080 calling Scene00006: Normal(Talk, TargetCanMove), id=YELLOWJACKETA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea007:66080 calling Scene00007: Normal(Talk, TargetCanMove), id=YELLOWJACKETB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea007:66080 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00010() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("ManSea007:66080 calling Scene00010: Normal(CutScene, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 10, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00011() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea007:66080 calling Scene00011: Normal(Talk, TargetCanMove), id=REYNER" );
    var callback = (SceneResult result) =>
    {
      Scene00012();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea007:66080 calling Scene00012: Normal(CutScene, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
