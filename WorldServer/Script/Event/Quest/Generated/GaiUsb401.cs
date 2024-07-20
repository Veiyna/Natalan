// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66393)]
public class GaiUsb401 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006341
  //ACTOR2 = 1007711
  //ACTOR3 = 1007712
  //ACTOR4 = 1006343
  //CUTSCENE01 = 243
  //CUTSCENE02 = 371
  //EOBJECT0 = 2002086
  //EVENTACTIONPROCESSUPPERMIDDLE = 32
  //INSTANCEDUNGEON0 = 20002
  //POPRANGE0 = 4332843
  //SCREENIMAGEUNLOCK = 111
  //UNLOCKADDNEWCONTENTTOCF = 3702
  //UNLOCKIMAGEVSTITAN = 77

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer), id=unknown
        // +Callback Scene00001: Normal(CutScene, QuestAccept), id=unknown
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Empty(None), id=unknown
        break;
      }
      case 2:
      {
        if( param1 == 1007711 ) // ACTOR2 = unknown
        {
          Scene00003(); // Scene00003: Normal(CutScene, FadeIn), id=unknown
          break;
        }
        if( param1 == 1007712 ) // ACTOR3 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=YSHTOLA
        // +Callback Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YSHTOLA
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
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb401:66393 calling Scene00000: Normal(QuestOffer), id=unknown" );
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
    player.sendDebug("GaiUsb401:66393 calling Scene00001: Normal(CutScene, QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb401:66393 calling Scene00002: Empty(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb401:66393 calling Scene00003: Normal(CutScene, FadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SetDutyUnlock(20002);
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb401:66393 calling Scene00004: Empty(None), id=unknown" );
  }

private void Scene00005() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb401:66393 calling Scene00005: Normal(Talk, TargetCanMove), id=YSHTOLA" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb401:66393 calling Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YSHTOLA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
