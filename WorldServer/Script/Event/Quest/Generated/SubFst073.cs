// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65923)]
public class SubFst073 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 9 entries
  //SEQ_255, 5 entries

  //ACTOR0 = 1000503
  //ACTOR1 = 1000470
  //ENEMY0 = 3842567
  //EOBJECT0 = 2001016
  //EOBJECT1 = 2001018
  //EOBJECT2 = 2001017
  //EOBJECT3 = 2001086
  //EOBJECT4 = 2001087
  //EOBJECT5 = 2001088
  //EOBJECT6 = 2001845
  //EVENTRANGE0 = 3842570
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000237
  //QSTACCEPTCHECK = 65695

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=ARMELLE
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 2
      case 1:
      {
        if( param1 == 2001016 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001018 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001017 ) // EOBJECT2 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001086 ) // EOBJECT3 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001087 ) // EOBJECT4 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001088 ) // EOBJECT5 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001845 ) // EOBJECT6 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 3842570 ) // EVENTRANGE0 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 3842567 ) // ENEMY0 = unknown
        {
          Scene00022(); // Scene00022: Normal(Message, PopBNpc), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 2
      case 255:
      {
        if( param1 == 1000470 ) // ACTOR1 = KEITHA
        {
          Scene00023(); // Scene00023: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KEITHA
          break;
        }
        if( param1 == 2001017 ) // EOBJECT2 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001086 ) // EOBJECT3 = unknown
        {
          Scene00031(); // Scene00031: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001087 ) // EOBJECT4 = unknown
        {
          Scene00034(); // Scene00034: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001088 ) // EOBJECT5 = unknown
        {
          Scene00037(); // Scene00037: Empty(None), id=unknown
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
      quest.setBitFlag8( 2, false );
      quest.Sequence = 255;
      quest.UI8BH = 2;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("SubFst073:65923 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=ARMELLE" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 2, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubFst073:65923 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 2 );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT1, UI8AL = 2, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubFst073:65923 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 2 );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00014() //SEQ_1: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00014: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00017() //SEQ_1: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00017: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00020() //SEQ_1: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00020: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00022() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00022: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00023() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00023: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00024();
      }
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00024() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KEITHA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00028() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00028: Empty(None), id=unknown" );
  }

private void Scene00031() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00031: Empty(None), id=unknown" );
  }

private void Scene00034() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00034: Empty(None), id=unknown" );
  }

private void Scene00037() //SEQ_255: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst073:65923 calling Scene00037: Empty(None), id=unknown" );
  }
};
}
