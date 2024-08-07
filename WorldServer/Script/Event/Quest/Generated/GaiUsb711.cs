// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66442)]
public class GaiUsb711 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1006404
  //ACTOR1 = 1007654
  //ENEMY0 = 4293068
  //ENEMY1 = 4293071
  //EOBJECT0 = 2002133
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONWAITING2MIDDLE = 12

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=ISAUDOREL
        break;
      }
      case 1:
      {
        if( param1 == 2002133 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4293068 ) // ENEMY0 = unknown
        {
          Scene00003(); // Scene00003: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 4293071 ) // ENEMY1 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007654 ) // ACTOR1 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006404 ) // ACTOR0 = ISAUDOREL
        {
          Scene00006(); // Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISAUDOREL
          break;
        }
        if( param1 == 1007654 ) // ACTOR1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
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
    if( quest.UI8AL == 2 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb711:66442 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb711:66442 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=ISAUDOREL" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EOBJECT0, UI8AL = 2, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb711:66442 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 2);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 2 );
    checkProgressSeq1();
  }

private void Scene00003() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb711:66442 calling Scene00003: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb711:66442 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb711:66442 calling Scene00005: Empty(None), id=unknown" );
  }

private void Scene00006() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb711:66442 calling Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISAUDOREL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb711:66442 calling Scene00008: Empty(None), id=unknown" );
  }
};
}
