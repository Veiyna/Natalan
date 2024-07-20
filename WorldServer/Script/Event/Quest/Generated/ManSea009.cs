// FFXIVTheMovie.ParserV3.11
// param used:
//_BRANCH SET!!
//PRIVATE_SCENE8 = 198
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66081)]
public class ManSea009 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1000972
  //ACTOR1 = 1001029
  //ACTOR2 = 1003355
  //ACTOR3 = 1002694
  //CUTSCENE01 = 137
  //CUTSCENE02 = 63
  //CUTSCENE03 = 223
  //POPRANGE0 = 4146938
  //POPRANGE1 = 4146949
  //RITEM0 = 3760
  //TERRITORYTYPE0 = 198

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=BADERON
        // +Callback Scene00001: Normal(None), id=unknown
        break;
      }
      case 1:
      {
        if( param1 == 1001029 ) // ACTOR1 = ZANTHAEL, CB=1
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=ZANTHAEL
            // +Callback Scene00003: Normal(Talk, TargetCanMove), id=ZANTHAEL
            // +Callback Scene00004: Normal(CutScene, AutoFadeIn), id=unknown
          }
          else
          {
            Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=ZANTHAEL
          }
          break;
        }
        if( param1 == 1003355 ) // ACTOR2 = JNASSHYM
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=JNASSHYM
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1002694 ) // ACTOR3 = MERLWYB
        {
          Scene00007(); // Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=MERLWYB
          break;
        }
        if( param1 == 1001029 ) // ACTOR1 = ZANTHAEL
        {
          Scene00008(); // Scene00008: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=ZANTHAEL
          break;
        }
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
    player.sendDebug("ManSea009:66081 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=BADERON" );
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
    player.sendDebug("ManSea009:66081 calling Scene00001: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True, Branch(Todo:0)
  {
    player.sendDebug("ManSea009:66081 calling Scene00002: Normal(Talk, TargetCanMove), id=ZANTHAEL" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True, Branch(Todo:0)
  {
    player.sendDebug("ManSea009:66081 calling Scene00003: Normal(Talk, TargetCanMove), id=ZANTHAEL" );
    var callback = (SceneResult result) =>
    {
      Scene00004();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True, Branch(Todo:0)
  {
    player.sendDebug("ManSea009:66081 calling Scene00004: Normal(CutScene, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(player.Position);
    };
    owner.Event.NewScene( Id, 4, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
private void Scene00005() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True, Branch(Todo:0)
  {
    player.sendDebug("ManSea009:66081 calling Scene00005: Normal(Talk, TargetCanMove), id=ZANTHAEL" );
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR);
  }

private void Scene00006() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea009:66081 calling Scene00006: Normal(Talk, TargetCanMove), id=JNASSHYM" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea009:66081 calling Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=MERLWYB" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea009:66081 calling Scene00008: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=ZANTHAEL" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        player.Event.StopEvent(Id);
        player.enterPredefinedPrivateInstance( 198 );
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
