// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65913)]
public class SubFst056 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000435
  //ACTOR1 = 1000436
  //EOBJECT0 = 2000748
  //EOBJECT1 = 2000749
  //EOBJECT2 = 2000750
  //EOBJECT3 = 2000751
  //EVENTACTIONSEARCH = 1
  //SEQ0ACTOR0 = 0
  //SEQ1EOBJECT0 = 1
  //SEQ1EOBJECT0EVENTACTIONNO = 99
  //SEQ1EOBJECT0EVENTACTIONOK = 100
  //SEQ1EOBJECT1 = 2
  //SEQ1EOBJECT1EVENTACTIONNO = 97
  //SEQ1EOBJECT1EVENTACTIONOK = 98
  //SEQ1EOBJECT2 = 3
  //SEQ1EOBJECT2EVENTACTIONNO = 95
  //SEQ1EOBJECT2EVENTACTIONOK = 96
  //SEQ1EOBJECT3 = 4
  //SEQ1EOBJECT3EVENTACTIONNO = 93
  //SEQ1EOBJECT3EVENTACTIONOK = 94
  //SEQ2ACTOR1 = 5
  //SEQ2ACTOR1NPCTRADENO = 91
  //SEQ2ACTOR1NPCTRADEOK = 92

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=OSHAJAAB
        break;
      }
      case 1:
      {
        if( param1 == 2000748 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00099(); // Scene00099: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000749 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00097(); // Scene00097: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000750 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00095(); // Scene00095: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000751 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00093(); // Scene00093: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00005(); // Scene00005: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=THEODORE
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
    if( quest.UI8AL == 4 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst056:65913 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=OSHAJAAB" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00099() //SEQ_1: EOBJECT0, UI8AL = 4, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubFst056:65913 calling Scene00099: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00097() //SEQ_1: EOBJECT1, UI8AL = 4, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubFst056:65913 calling Scene00097: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00095() //SEQ_1: EOBJECT2, UI8AL = 4, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubFst056:65913 calling Scene00095: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00093() //SEQ_1: EOBJECT3, UI8AL = 4, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubFst056:65913 calling Scene00093: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst056:65913 calling Scene00005: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=THEODORE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
