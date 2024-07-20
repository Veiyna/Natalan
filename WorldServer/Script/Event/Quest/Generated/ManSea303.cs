// FFXIVTheMovie.ParserV3.11
// fake IsAnnounce table
// param used:
//SCENE_2 REMOVED!!
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;
using WorldServer.Network.Message;

namespace WorldServer.Script.Quest
{
[EventScript(66220)]
public class ManSea303 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002388
  //ACTOR1 = 1005012
  //GCRANK01 = 1
  //LOCACTION1 = 1002
  //LOCACTOR0 = 1003783
  //LOCSE1 = 42
  //LOCTALKSHAPE1 = 6
  //LOGMESSAGEMONSTERNOTEPAGEUNLOCK = 1018
  //NCUT0 = 221
  //NCUT1 = 391
  //ORDEROFMAELSTROM = 1
  //POPRANGE0 = 4148347
  //REWARD0 = 22
  //SCREENIMAGE0 = 32
  //SCREENIMAGE1 = 69

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
        // +Callback Scene00001: Normal(Talk, CutScene, FadeIn, QuestAccept, TargetCanMove, SystemTalk, CreateCharacterTalk), id=STORMPERSONNEL
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk, QuestReward, TargetCanMove), id=GUIDE
        // +Callback Scene00004: Normal(CutScene, QuestComplete, AutoFadeIn), id=unknown
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
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea303:66220 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManSea303:66220 calling Scene00001: Normal(Talk, CutScene, FadeIn, QuestAccept, TargetCanMove, SystemTalk, CreateCharacterTalk), id=STORMPERSONNEL" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
      Scene00002();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI | (SceneFlags)4164955899, Callback: callback );
  }

  private void Scene00002() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea303:66220 calling Scene00001: Normal(Talk, CutScene, FadeIn, QuestAccept, TargetCanMove, SystemTalk, CreateCharacterTalk), id=STORMPERSONNEL" );
    var callback = (SceneResult result) =>
    {
    };

    owner.Event.NewScene(Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback);
  }

private void Scene00003() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea303:66220 calling Scene00003: Normal(Talk, QuestReward, TargetCanMove), id=GUIDE" );
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
    player.sendDebug("ManSea303:66220 calling Scene00004: Normal(CutScene, QuestComplete, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      owner.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 4, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

  public override void OnYield(byte yieldId, uint[] data)
  {
    this.owner.SetGC(1);
    base.OnYield(yieldId, data);
  }

};
}
