// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66170)]
public class SubWil124 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003939
  //EOBJECT0 = 2001420
  //EOBJECT1 = 2001599
  //EOBJECT2 = 2001600
  //EOBJECT3 = 2001601
  //EOBJECT4 = 2001602
  //EVENTACTIONGATHERSHORT = 6
  //ITEM0 = 2000394

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=BENEGER
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 5
      case 1:
      {
        if( param1 == 2001420 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001599 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001600 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00006(); // Scene00006: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001601 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00008(); // Scene00008: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001602 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00010(); // Scene00010: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 5
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00011(); // Scene00011: NpcTrade(Talk, TargetCanMove), id=BENEGER
        // +Callback Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BENEGER
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
    if( quest.UI8AL == 5 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.setBitFlag8( 5, false );
      quest.Sequence = 255;
      quest.UI8BH = 5;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil124:66170 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=BENEGER" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EOBJECT0, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil124:66170 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: EOBJECT1, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil124:66170 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT2, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil124:66170 calling Scene00006: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: EOBJECT3, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubWil124:66170 calling Scene00008: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_1: EOBJECT4, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("SubWil124:66170 calling Scene00010: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 5, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil124:66170 calling Scene00011: NpcTrade(Talk, TargetCanMove), id=BENEGER" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00012();
      }
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil124:66170 calling Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BENEGER" );
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
