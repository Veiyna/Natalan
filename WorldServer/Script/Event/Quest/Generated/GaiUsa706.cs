// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66315)]
public class GaiUsa706 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 11 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1006226
  //ACTOR1 = 1007710
  //ENEMY0 = 4284061
  //ENEMY1 = 4284063
  //ENEMY2 = 4284064
  //ENEMY3 = 4284065
  //ENEMY4 = 4284066
  //ENEMY5 = 4284067
  //EOBJECT0 = 2002460
  //EOBJECT1 = 2002640
  //EOBJECT2 = 2002641
  //EVENTRANGE0 = 4283978
  //EVENTACTIONSEARCH = 1

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=HREMFING
        break;
      }
      case 1:
      {
        if( param1 == 4283978 ) // EVENTRANGE0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EVENTRANGE0
        if( param1 == 4284061 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4284063 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4284064 ) // ENEMY2 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4284065 ) // ENEMY3 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4284066 ) // ENEMY4 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4284067 ) // ENEMY5 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 2002460 ) // EOBJECT0 = unknown
        {
          Scene00003(); // Scene00003: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002640 ) // EOBJECT1 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002641 ) // EOBJECT2 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007710 ) // ACTOR1 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006226 ) // ACTOR0 = HREMFING
        {
          Scene00007(); // Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HREMFING
          break;
        }
        if( param1 == 2002640 ) // EOBJECT1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002641 ) // EOBJECT2 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007710 ) // ACTOR1 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
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
    if( quest.UI8AL == 6 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=HREMFING" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EVENTRANGE0, UI8AL = 6, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00002: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 6);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 6 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }







private void Scene00003() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00003: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00005: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00006: Empty(None), id=unknown" );
  }

private void Scene00007() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HREMFING" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa706:66315 calling Scene00010: Empty(None), id=unknown" );
  }
};
}
