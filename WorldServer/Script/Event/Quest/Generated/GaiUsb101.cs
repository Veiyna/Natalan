// FFXIVTheMovie.ParserV3.11
// fake IsAnnounce table
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66356)]
public class GaiUsb101 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1004917
  //ENEMY0 = 190
  //ITEM0 = 2000642

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
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=UODHNUN
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 7
      case 1:
      {
        // empty entry
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 7
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00003: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=UODHNUN
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
    quest.Sequence = 255;
    quest.UI8BH = 7;
  }
  void checkProgressSeq1()
  {
    quest.Sequence = 255;
    quest.UI8BH = 7;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb101:66356 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb101:66356 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }


private void Scene00002() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb101:66356 calling Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00003();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb101:66356 calling Scene00003: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=UODHNUN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
