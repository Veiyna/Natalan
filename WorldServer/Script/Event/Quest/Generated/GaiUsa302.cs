// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66270)]
public class GaiUsa302 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006193
  //ENEMY0 = 28
  //EOBJECT0 = 2001945
  //ITEM0 = 2000588
  //ITEM1 = 2000589
  //ITEM2 = 2000590

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=TETEROON
        break;
      }
      //seq 1 event item ITEM0 = UI8BL max stack ?
      //seq 1 event item ITEM1 = UI8CH max stack 1
      //seq 1 event item ITEM2 = UI8CL max stack 5
      case 1:
      {
        if( param1 == 2001945 ) // EOBJECT0 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00001(); // Scene00001: Normal(Inventory), id=unknown
          }
          break;
        }
        if( param1 == 28 ) // ENEMY0 = unknown
        {
          if( quest.UI8AL != 5 )
          {
            Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack ?
      //seq 255 event item ITEM1 = UI8BL max stack 1
      //seq 255 event item ITEM2 = UI8CH max stack 5
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00004: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TETEROON
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
    quest.UI8CH = 1;
    quest.UI8CL = 5;
  }
  void checkProgressSeq1()
  {
    if( quest.UI8BH == 1 )
      if( quest.UI8AL == 5 )
      {
        quest.UI8BH = 0 ;
        quest.UI8AL = 0 ;
        quest.setBitFlag8( 1, false );
        quest.UI8BL = 0;
        quest.UI8CH = 0;
        quest.UI8CL = 0;
        quest.Sequence = 255;
        quest.UI8BH = 1;
        quest.UI8BL = 1;
        quest.UI8CH = 5;
      }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa302:66270 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=TETEROON" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: EOBJECT0, UI8BH = 1, Flag8(1)=True
  {
    player.sendDebug("GaiUsa302:66270 calling Scene00001: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ENEMY0, UI8AL = 5, <No Flag>
  {
    player.sendDebug("GaiUsa302:66270 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    checkProgressSeq1();
  }

private void Scene00003() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa302:66270 calling Scene00003: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00004();
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa302:66270 calling Scene00004: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TETEROON" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
