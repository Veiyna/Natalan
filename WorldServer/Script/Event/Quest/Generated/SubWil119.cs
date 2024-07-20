// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66165)]
public class SubWil119 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_2, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003937
  //ACTOR1 = 1003946
  //ENEMY0 = 3982707
  //ENEMY1 = 3982708
  //EOBJECT0 = 2001418
  //EOBJECT1 = 2001867
  //EVENTRANGE0 = 4094797
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000391

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=EADUUARD
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( param1 == 4094797 ) // EVENTRANGE0 = unknown
        {
          Scene00002(); // Scene00002: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2001418 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00003(); // Scene00003: Normal(Message), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 3982707 ) // ENEMY0 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 3982708 ) // ENEMY1 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001867 ) // EOBJECT1 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00008: Normal(Talk, TargetCanMove), id=ESMOUR
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00009(); // Scene00009: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=EADUUARD
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
    if( quest.UI8AL == 2 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 2;
      quest.UI8BH = 1;
    }
  }
  void checkProgressSeq2()
  {
    quest.UI8BH = 0;
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil119:66165 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=EADUUARD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil119:66165 calling Scene00002: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 2, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil119:66165 calling Scene00003: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 2);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 2 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil119:66165 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil119:66165 calling Scene00005: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil119:66165 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubWil119:66165 calling Scene00007: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00008();
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubWil119:66165 calling Scene00008: Normal(Talk, TargetCanMove), id=ESMOUR" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil119:66165 calling Scene00009: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=EADUUARD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
