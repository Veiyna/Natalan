// FFXIVTheMovie.ParserV3.11
// param used:
//ID_ACTOR1 = 4298942495
//ID_ACTOR2 = 4298942496
//ID_ACTOR3 = 4298942497
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66154)]
public class SubWil110 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 10 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1003929
  //ACTOR1 = 1003942
  //ACTOR2 = 1003943
  //ACTOR3 = 1003944
  //ENEMY0 = 4100865
  //ENEMY1 = 4100866
  //EOBJECT0 = 2001663
  //EOBJECT1 = 2001664
  //EOBJECT2 = 2001665
  //EOBJECT3 = 2001866
  //EVENTRANGE0 = 4100864
  //EVENTACTIONGATHERSHORT = 6
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000399

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=ISEMBARD
        // +Callback Scene00001: Normal(QuestAccept), id=unknown
        break;
      }
      //seq 1 event item ITEM0 = UI8CH max stack 3
      case 1:
      {
        if( param1 == 1003942 || param1 == 4298942495 ) // ACTOR1 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1003943 || param1 == 4298942496 ) // ACTOR2 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1003944 || param1 == 4298942497 ) // ACTOR3 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 4100864 ) // EVENTRANGE0 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 4100865 ) // ENEMY0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 4100866 ) // ENEMY1 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001663 ) // EOBJECT0 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001664 ) // EOBJECT1 = unknown
        {
          Scene00010(); // Scene00010: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2001665 ) // EOBJECT2 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001866 ) // EOBJECT3 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 3
      case 255:
      {
        if( param1 == 1003929 ) // ACTOR0 = ISEMBARD
        {
          Scene00019(); // Scene00019: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00020: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISEMBARD
          break;
        }
        if( param1 == 2001663 ) // EOBJECT0 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001664 ) // EOBJECT1 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001665 ) // EOBJECT2 = unknown
        {
          Scene00027(); // Scene00027: Empty(None), id=unknown
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
    if( quest.UI8BL == 1 )
      if( quest.UI8AL == 1 )
        if( quest.UI8BH == 1 )
        {
          quest.UI8BL = 0 ;
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.UI8CH = 0;
          quest.Sequence = 255;
          quest.UI8BH = 3;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=ISEMBARD" );
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
    player.sendDebug("SubWil110:66154 calling Scene00001: Normal(QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8BL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil110:66154 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 0, 0, 0 );
    checkProgressSeq1();
  }

private void Scene00003() //SEQ_1: ACTOR2, UI8AL = 1, Flag8(2)=True
  {
    player.sendDebug("SubWil110:66154 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 2, true );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: ACTOR3, UI8BH = 1, Flag8(3)=True
  {
    player.sendDebug("SubWil110:66154 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00005: Empty(None), id=unknown" );
    this.owner.Map.CreateBNpcFromLayoutId(4100865, this.owner.Id);
    this.owner.Map.CreateBNpcFromLayoutId(4100866, this.owner.Id);
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00010: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00015: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00018() //SEQ_1: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00018: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00019() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00019: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00020();
      }
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00020() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00020: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00024: Empty(None), id=unknown" );
  }

private void Scene00027() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil110:66154 calling Scene00027: Empty(None), id=unknown" );
  }
};
}
