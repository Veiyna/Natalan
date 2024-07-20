// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(69393)]
public class XxaUsa103 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006674
  //ACTOR1 = 1000563
  //ACTOR2 = 1000576
  //ACTOR3 = 1000587
  //QSTACCEPTCHECK = 66253

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=YDA
        break;
      }
      case 1:
      {
        if( param1 == 1000563 ) // ACTOR1 = AMEEXIA
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=AMEEXIA
            // +Callback Scene00003: Normal(Talk, TargetCanMove), id=AMEEXIA
            // +Callback Scene00004: Normal(Talk, TargetCanMove), id=AMEEXIA
          }
          break;
        }
        if( param1 == 1000576 ) // ACTOR2 = KNOLEXIA
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=KNOLEXIA
            // +Callback Scene00006: Normal(Talk, TargetCanMove), id=KNOLEXIA
            // +Callback Scene00007: Normal(Talk, TargetCanMove), id=KNOLEXIA
          }
          break;
        }
        if( param1 == 1000587 ) // ACTOR3 = DELLEXIA
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=DELLEXIA
            // +Callback Scene00009: Normal(Talk, TargetCanMove), id=DELLEXIA
            // +Callback Scene00010: Normal(Talk, TargetCanMove), id=DELLEXIA
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00011(); // Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YDA
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
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("XxaUsa103:69393 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=YDA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 3, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00002: Normal(Talk, TargetCanMove), id=AMEEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 3, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00003: Normal(Talk, TargetCanMove), id=AMEEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00004();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_1: ACTOR1, UI8AL = 3, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00004: Normal(Talk, TargetCanMove), id=AMEEXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR2, UI8AL = 3, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00005: Normal(Talk, TargetCanMove), id=KNOLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_1: ACTOR2, UI8AL = 3, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00006: Normal(Talk, TargetCanMove), id=KNOLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00007();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_1: ACTOR2, UI8AL = 3, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00007: Normal(Talk, TargetCanMove), id=KNOLEXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: ACTOR3, UI8AL = 3, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00008: Normal(Talk, TargetCanMove), id=DELLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_1: ACTOR3, UI8AL = 3, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00009: Normal(Talk, TargetCanMove), id=DELLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00010();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00010() //SEQ_1: ACTOR3, UI8AL = 3, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00010: Normal(Talk, TargetCanMove), id=DELLEXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa103:69393 calling Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YDA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
