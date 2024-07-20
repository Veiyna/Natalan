// FFXIVTheMovie.ParserV3.11
// fake IsAnnounce table
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66235)]
public class SubSea921 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1004990
  //LOCGENERALACTION = 15
  //RITEM0 = 4745
  //UNLOCKIMAGEDYEING = 249

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=SWYRGEIM
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00002: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SWYRGEIM
        // +Callback Scene00003: Normal(Talk, TargetCanMove), id=SWYRGEIM
        break;
      }
      default:
      {
        player.sendUrgent("Sequence {} not defined. quest.Sequence ");
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
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea921:66235 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=SWYRGEIM" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea921:66235 calling Scene00001: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00002();
      }
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00002() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea921:66235 calling Scene00002: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SWYRGEIM" );
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
    player.sendDebug("SubSea921:66235 calling Scene00003: Normal(Talk, TargetCanMove), id=SWYRGEIM" );
    var callback = (SceneResult result) =>
    {
      player.SetMasterUnlock((ushort)UnlockEntry.Dye, true);
      player.FinishQuest( Id, result.GetResult( 1 ) );
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
