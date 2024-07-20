// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65575)]
public class ManFst001 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_255, 3 entries

  //ACTOR0 = 1001148
  //ACTOR1 = 1001140
  //CUTEVENT = 29
  //EOBJECT0 = 2001659
  //EOBJECT1 = 2001660
  //EOBJECT7 = 2616477
  //EVENTACTIONSEARCH = 1
  //OPENINGEVENTHANDLER = 1245186
  //SEQ2ACTOR1 = 2

  private const uint EVENT_ON_TALK = 0;
  private const uint EVENT_ON_EMOTE = 1;
  private const uint EVENT_ON_BNPC_KILL = 2;
  private const uint EVENT_ON_WITHIN_RANGE = 3;
  private const uint EVENT_ON_ENTER_TERRITORY = 4;
  private const uint EVENT_ON_EVENT_ITEM = 5;
  private const uint EVENT_ON_EOBJ_HIT = 6;
  private const uint EVENT_ON_SAY = 7;
  private const uint OPENING_EVENT_HANDLER = 1245186;

  void onProgress(uint type, ulong param1, ulong param2, ulong param3 )
  {
    switch( quest.Sequence )
    {
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer), id=unknown
        // +Callback Scene00001: Normal(Talk, FadeIn, TargetCanMove), id=BERTENNANT
        // +Callback Scene00002: Normal(QuestAccept, SystemTalk), id=unknown
        break;
      }
      case 255:
      {
        if( param1 == 1001140 ) // ACTOR1 = unknown
        {
          Scene00003(); // Scene00003: Normal(None), id=unknown
          // +Callback Scene00004: Normal(CutScene), id=unknown
          // +Callback Scene00005: Normal(QuestReward, QuestComplete, SystemTalk), id=unknown
          break;
        }
        if( param1 == 2001659 ) // EOBJECT0 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001660 ) // EOBJECT1 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
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
    onProgress(EVENT_ON_WITHIN_RANGE, 0, 0, 0 );
  }

  public override void OnEventTerritory()
  {
    onProgress(EVENT_ON_ENTER_TERRITORY, 0, 0, 0 );
  }
  void checkProgressSeq0()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00000: Normal(QuestOffer), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.Character.OpeningSequence = 2;
        Scene00001();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00001() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00001: Normal(Talk, FadeIn, TargetCanMove), id=BERTENNANT" );
    var callback = (SceneResult result) =>
    {
      Scene00002();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR | SceneFlags.SET_BASE, Callback: callback );
  }
private void Scene00002() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00002: Normal(QuestAccept, SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
      this.owner.Event.NewEvent(OPENING_EVENT_HANDLER, EventType.Nest, this.Event.ActorId);
      this.owner.Event.NewScene(OPENING_EVENT_HANDLER, 0x1E, SceneFlags.HIDE_HOTBAR | SceneFlags.NO_DEFAULT_CAMERA);
    };
    owner.Event.NewScene( Id, 2, 0, Callback: callback );
  }

private void Scene00003() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00003: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00004();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00004: Normal(CutScene), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00005();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
private void Scene00005() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00005: Normal(QuestReward, QuestComplete, SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst001:65575 calling Scene00009: Empty(None), id=unknown" );
  }
};
}
