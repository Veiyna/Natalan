// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65695)]
public class SubFst053 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000503
  //ENEMY0 = 1943223
  //ENEMY1 = 1943224
  //ENEMY2 = 1943225
  //EOBJECT0 = 2000042
  //ITEM0 = 2000095
  //SEQ0ACTOR0 = 0
  //SEQ1EOBJECT0 = 1
  //SEQ1EOBJECT0USEITEMNO = 99
  //SEQ1EOBJECT0USEITEMOK = 100
  //SEQ2ACTOR0 = 2

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ARMELLE
        break;
      }
      //seq 1 event item ITEM0 = UI8BL max stack ?
      case 1:
      {
        if( param1 == 2000042 ) // EOBJECT0 = unknown
        {
          Scene00001(); // Scene00001: Normal(Inventory), id=unknown
          break;
        }
        if( param1 == 1943223 ) // ENEMY0 = unknown
        {
          if( quest.UI8AL != 3 )
          {
            player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
            quest.UI8AL =  (byte)( quest.UI8AL + 1);
            checkProgressSeq1();
          }
          break;
        }
        if( param1 == 1943224 ) // ENEMY1 = unknown
        {
          if( quest.UI8AL != 3 )
          {
            player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
            quest.UI8AL =  (byte)( quest.UI8AL + 1);
            checkProgressSeq1();
          }
          break;
        }
        if( param1 == 1943225 ) // ENEMY2 = unknown
        {
          if( quest.UI8AL != 3 )
          {
            player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
            quest.UI8AL =  (byte)( quest.UI8AL + 1);
            checkProgressSeq1();
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack ?
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ARMELLE
        // +Callback Scene00100: Normal(Message, QuestGimmickReaction, SystemTalk), id=unknown
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
    quest.UI8BL = 1;
  }
  void checkProgressSeq1()
  {
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.UI8BL = 0;
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst053:65695 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ARMELLE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: EOBJECT0, <No Var>, Flag8(1)=True
  {
    player.sendDebug("SubFst053:65695 calling Scene00001: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }




private void Scene00002() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst053:65695 calling Scene00002: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ARMELLE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00100();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00100() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst053:65695 calling Scene00100: Normal(Message, QuestGimmickReaction, SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
    };
    owner.Event.NewScene( Id, 100, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
