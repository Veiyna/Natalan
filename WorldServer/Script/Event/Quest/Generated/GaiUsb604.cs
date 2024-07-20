// FFXIVTheMovie.ParserV3.11
// param used:
//IGNORE_BNPCHACK_EVENTRANGE0 SET!!
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66422)]
public class GaiUsb604 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_2, 1 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006372
  //ACTOR1 = 1006731
  //ACTOR2 = 1006376
  //ACTOR3 = 1006377
  //ENEMY0 = 4289901
  //ENEMY1 = 4289905
  //EOBJECT0 = 2002503
  //EVENTRANGE0 = 4289908
  //EVENTACTIONRESCUEUNDERMIDDLE = 35
  //EVENTACTIONSEARCH = 1
  //LOCACTOR0 = 1006902

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=LUDOVOIX
        break;
      }
      case 1:
      {
        if( param1 == 4289908 ) // EVENTRANGE0 = unknown
        {
          Scene00002(); // Scene00002: Normal(Message, PopBNpc), id=unknown
          // +Callback Scene00003: Normal(Message), id=unknown
          break;
        }
        if( param1 == 1006731 ) // ACTOR1 = NPC
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00004(); // Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=NPC
          }
          break;
        }
        // BNpcHack credit moved to ACTOR1
        if( param1 == 4289901 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4289905 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 2002503 ) // EOBJECT0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=LUDOVOIX
        break;
      }
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=EDMELLE
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00009(); // Scene00009: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=FORLEMORT
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
    if( quest.UI8AL == 2 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 3;
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb604:66422 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=LUDOVOIX" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00002: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00003: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR1, UI8AL = 2, Flag8(1)=True
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=NPC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 2);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }



private void Scene00006() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_2: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00007: Normal(Talk, TargetCanMove), id=LUDOVOIX" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00008: Normal(Talk, TargetCanMove), id=EDMELLE" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb604:66422 calling Scene00009: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=FORLEMORT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
