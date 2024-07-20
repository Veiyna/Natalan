// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65697)]
public class SubFst055 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 3 entries
  //SEQ_2, 1 entries
  //SEQ_3, 3 entries
  //SEQ_4, 2 entries
  //SEQ_5, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000471
  //ACTOR1 = 1000476
  //ACTOR2 = 1000483
  //ACTOR3 = 1000484
  //ACTOR4 = 1003002
  //EOBJECT0 = 2000053
  //EOBJECT1 = 2001837
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000096
  //LOCACTION1 = 81
  //LOCACTION2 = 82
  //LOCACTOR = 1000470
  //LOCFACE1 = 611
  //LOCFACE2 = 604
  //LOCPOSCAM1 = 1991469
  //QSTACCEPTCHECK = 65692
  //QUESTBATTLE0 = 14
  //TERRITORYTYPE0 = 227

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=LUQUELOT
        break;
      }
      case 1:
      {
        if( param1 == 1000476 ) // ACTOR1 = LAODAIRE
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=LAODAIRE
          }
          break;
        }
        if( param1 == 1000483 ) // ACTOR2 = BERNARD
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=BERNARD
          }
          break;
        }
        if( param1 == 1000484 ) // ACTOR3 = EWMOND
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=EWMOND
          }
          break;
        }
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: Normal(Talk, FadeIn, TargetCanMove, CreateCharacterTalk), id=LUQUELOT
        break;
      }
      case 3:
      {
        if( param1 == 1003002 ) // ACTOR4 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 2000053 ) // EOBJECT0 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001837 ) // EOBJECT1 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack 1
      case 4:
      {
        if( param1 == 1003002 ) // ACTOR4 = KUPULUKOPO
        {
          if( quest.UI8AL != 1 )
          {
            Scene00011(); // Scene00011: Normal(Talk, FadeIn, TargetCanMove), id=KUPULUKOPO
          }
          break;
        }
        if( param1 == 2000053 ) // EOBJECT0 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 5 event item ITEM0 = UI8BH max stack 1
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00015(); // Scene00015: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00016: Normal(Talk, FadeIn, TargetCanMove, CreateCharacterTalk), id=LUQUELOT
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00018(); // Scene00018: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LUQUELOT
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
      quest.Sequence = 5;
      quest.UI8BH = 1;
    }
  }
  void checkProgressSeq5()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst055:65697 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=LUQUELOT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 3, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubFst055:65697 calling Scene00001: Normal(Talk, TargetCanMove), id=LAODAIRE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, UI8AL = 3, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubFst055:65697 calling Scene00002: Normal(Talk, TargetCanMove), id=BERNARD" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR3, UI8AL = 3, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubFst055:65697 calling Scene00003: Normal(Talk, TargetCanMove), id=EWMOND" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubFst055:65697 calling Scene00004: Normal(Talk, FadeIn, TargetCanMove, CreateCharacterTalk), id=LUQUELOT" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00005() //SEQ_3: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubFst055:65697 calling Scene00005: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 14 );
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst055:65697 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00009() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst055:65697 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00011() //SEQ_4: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubFst055:65697 calling Scene00011: Normal(Talk, FadeIn, TargetCanMove), id=KUPULUKOPO" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00014() //SEQ_4: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst055:65697 calling Scene00014: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00015() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubFst055:65697 calling Scene00015: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00016();
      }
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00016() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubFst055:65697 calling Scene00016: Normal(Talk, FadeIn, TargetCanMove, CreateCharacterTalk), id=LUQUELOT" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00018() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst055:65697 calling Scene00018: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LUQUELOT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
