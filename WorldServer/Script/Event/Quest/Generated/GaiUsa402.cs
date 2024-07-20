// FFXIVTheMovie.ParserV3.11
// param used:
//ID_ACTOR1 = 2097153
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66280)]
public class GaiUsa402 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1000580
  //ACTOR1 = 5010000
  //ACTOR2 = 1007651
  //ACTOR3 = 1007652
  //ACTOR4 = 1007653
  //ACTOR5 = 1000168
  //CUTEVENT = 242
  //EOBJECT0 = 2001957
  //EVENTRANGE0 = 4323294
  //ITEM0 = 2000596
  //LOCPOSCAM1 = 4334898

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
      //seq 0 event item ITEM0 = UI8BH max stack 1
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(CutScene, QuestAccept), id=unknown
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( param1 == 5010000 || param1 == 2097153 || (param1 == 74403447 && param3 == 22) ) // ACTOR1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(FadeIn, SystemTalk), id=unknown
          }
          break;
        }
        if( param1 == 1007651) // ACTOR2 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007652) // ACTOR3 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007653 ) // ACTOR4 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1000168 ) // ACTOR5 = VORSAILE
        {
          Scene00007(); // Scene00007: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=VORSAILE
          break;
        }
        if( param1 == 1007651 ) // ACTOR2 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007652 ) // ACTOR3 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007653 ) // ACTOR4 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
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
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
      quest.UI8BH = 1;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa402:66280 calling Scene00001: Normal(CutScene, QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00003: Normal(FadeIn, SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00004: Empty(None), id=unknown" );
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR);
  }

private void Scene00005() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00005: Empty(None), id=unknown" );
  }

private void Scene00006() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00006: Empty(None), id=unknown" );
  }

private void Scene00007() //SEQ_255: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00007: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00008();
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_255: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=VORSAILE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00011() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa402:66280 calling Scene00011: Empty(None), id=unknown" );
  }
};
}
