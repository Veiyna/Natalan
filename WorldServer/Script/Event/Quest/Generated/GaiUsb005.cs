// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66348)]
public class GaiUsb005 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 3 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1006266
  //ENEMY0 = 4292674
  //EOBJECT0 = 2002035
  //EOBJECT1 = 2002639
  //EOBJECT2 = 2002638
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONWAITING2MIDDLE = 12
  //ITEM0 = 2000636
  //ITEM1 = 2000637

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
      //seq 0 event item ITEM0 = UI8BH max stack ?
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=WHEISKAET
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( type == EVENT_ON_TALK ) Scene00002(); // Scene00002: Normal(Inventory), id=unknown
        if( type == EVENT_ON_EVENT_ITEM ) Scene00003(); // Scene00003: Empty(None), id=unknown
        break;
      }
      //seq 2 event item ITEM1 = UI8BH max stack 1
      case 2:
      {
        if( param1 == 2002639 ) // EOBJECT1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT1
        if( param1 == 4292674 ) // ENEMY0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002638 ) // EOBJECT2 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM1 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1006266 ) // ACTOR0 = WHEISKAET
        {
          Scene00008(); // Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00009: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=WHEISKAET
          break;
        }
        if( param1 == 2002638 ) // EOBJECT2 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
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
      quest.Sequence = 255;
      quest.UI8BH = 1;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb005:66348 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=WHEISKAET" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00002: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00003: Empty(None), id=unknown" );
    player.SendQuestMessage(Id, 0, 0, 0, 0 );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_2: EOBJECT1, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00005: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_2: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00008() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00009();
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00009: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=WHEISKAET" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb005:66348 calling Scene00011: Empty(None), id=unknown" );
  }
};
}
