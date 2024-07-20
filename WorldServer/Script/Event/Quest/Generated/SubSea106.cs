// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65939)]
public class SubSea106 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 2 entries
  //SEQ_4, 2 entries
  //SEQ_5, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003245
  //ACTOR1 = 1003246
  //ACTOR2 = 1001000
  //ENEMY0 = 3929855
  //EOBJECT0 = 2001260
  //EOBJECT1 = 2001261
  //EVENTACTION = 32
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000343
  //ITEM1 = 2000344
  //QSTACCEPTCHECK = 65940

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=FRAELOEF
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=KHANSWYS
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack ?
      case 2:
      {
        if( type == EVENT_ON_TALK ) Scene00002(); // Scene00002: Normal(Inventory), id=unknown
        if( type == EVENT_ON_EVENT_ITEM ) Scene00004(); // Scene00004: Empty(None), id=unknown
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack ?
      //seq 3 event item ITEM1 = UI8BL max stack 1
      case 3:
      {
        if( param1 == 2001261 ) // EOBJECT1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00006(); // Scene00006: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT1
        if( param1 == 3929855 ) // ENEMY0 = unknown
        {
          Scene00009(); // Scene00009: Normal(Message, PopBNpc), id=unknown
          break;
        }
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack ?
      //seq 4 event item ITEM1 = UI8BL max stack 1
      case 4:
      {
        if( param1 == 1003246 ) // ACTOR1 = KHANSWYS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00011(); // Scene00011: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00012: Normal(Talk, TargetCanMove), id=KHANSWYS
          }
          break;
        }
        if( param1 == 2001261 ) // EOBJECT1 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 5 event item ITEM0 = UI8BH max stack ?
      //seq 5 event item ITEM1 = UI8BL max stack 1
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=FRAELOEF
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack ?
      //seq 255 event item ITEM1 = UI8BL max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00018(); // Scene00018: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HNAANZA
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
    quest.UI8BH = 1;
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
      quest.UI8BH = 1;
      quest.UI8BL = 1;
    }
  }
  void checkProgressSeq4()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 5;
    }
  }
  void checkProgressSeq5()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea106:65939 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=FRAELOEF" );
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
    player.sendDebug("SubSea106:65939 calling Scene00001: Normal(Talk, TargetCanMove), id=KHANSWYS" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubSea106:65939 calling Scene00002: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00004() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubSea106:65939 calling Scene00004: Empty(None), id=unknown" );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00006() //SEQ_3: EOBJECT1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubSea106:65939 calling Scene00006: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_3: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea106:65939 calling Scene00009: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_4: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubSea106:65939 calling Scene00011: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00012();
      }
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00012() //SEQ_4: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubSea106:65939 calling Scene00012: Normal(Talk, TargetCanMove), id=KHANSWYS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_4: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea106:65939 calling Scene00014: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00017() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubSea106:65939 calling Scene00017: Normal(Talk, TargetCanMove), id=FRAELOEF" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea106:65939 calling Scene00018: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HNAANZA" );
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
