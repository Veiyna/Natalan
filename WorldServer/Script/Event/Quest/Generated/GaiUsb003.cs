// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66346)]
public class GaiUsb003 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 7 entries
  //SEQ_255, 6 entries

  //ACTOR0 = 1006264
  //ENEMY0 = 4291318
  //EOBJECT0 = 2002034
  //EOBJECT1 = 2002633
  //EOBJECT2 = 2002634
  //EOBJECT3 = 2002635
  //EOBJECT4 = 2002636
  //EOBJECT5 = 2002637
  //EVENTACTIONLOOKOUTLONG = 41
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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=TRACHTOUM
        break;
      }
      case 1:
      {
        if( param1 == 2002034 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4291318 ) // ENEMY0 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002633 ) // EOBJECT1 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002634 ) // EOBJECT2 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002635 ) // EOBJECT3 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002636 ) // EOBJECT4 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002637 ) // EOBJECT5 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006264 ) // ACTOR0 = TRACHTOUM
        {
          Scene00014(); // Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TRACHTOUM
          break;
        }
        if( param1 == 2002633 ) // EOBJECT1 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002634 ) // EOBJECT2 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002635 ) // EOBJECT3 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002636 ) // EOBJECT4 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002637 ) // EOBJECT5 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
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
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb003:66346 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=TRACHTOUM" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00003: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00008: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_1: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00012() //SEQ_1: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00012: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_1: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00013: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00014() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TRACHTOUM" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00018: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00020: Empty(None), id=unknown" );
  }

private void Scene00022() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_255: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb003:66346 calling Scene00024: Empty(None), id=unknown" );
  }
};
}
