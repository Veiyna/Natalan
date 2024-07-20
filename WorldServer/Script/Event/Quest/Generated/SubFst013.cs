// FFXIVTheMovie.ParserV3.11
// fake IsAnnounce table
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65576)]
public class SubFst013 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 1 entries
  //SEQ_4, 1 entries
  //SEQ_5, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000162
  //ACTOR1 = 1000161
  //FIRSTQUEST = 65575
  //SEQ0ACTOR0 = 0
  //SEQ1ACTOR1 = 1
  //SEQ1ACTOR1EMOTENO = 99
  //SEQ1ACTOR1EMOTEOK = 100
  //SEQ2ACTOR0 = 2
  //SEQ3ACTOR1 = 3
  //SEQ3ACTOR1EMOTENO = 97
  //SEQ3ACTOR1EMOTEOK = 98
  //SEQ4ACTOR0 = 4
  //SEQ5ACTOR1 = 5
  //SEQ5ACTOR1EMOTENO = 95
  //SEQ5ACTOR1EMOTEOK = 96
  //SEQ6ACTOR0 = 6

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=TALK
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk), id=unknown
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=TALK
        break;
      }
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk), id=unknown
        break;
      }
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=TALK
        // +Callback Scene00005: Normal(Talk), id=unknown
        // +Callback Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TALK
        break;
      }
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00096(); // Scene00096: Normal(Talk, TargetCanMove), id=TALK
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00098(); // Scene00098: Normal(Talk, TargetCanMove), id=TALK
        // +Callback Scene00100: Normal(Talk, TargetCanMove), id=TALK
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
    quest.Sequence = 3;
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 4;
  }
  void checkProgressSeq4()
  {
    quest.Sequence = 5;
  }
  void checkProgressSeq5()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst013:65576 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=TALK" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubFst013:65576 calling Scene00001: Normal(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubFst013:65576 calling Scene00002: Normal(Talk, TargetCanMove), id=TALK" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("SubFst013:65576 calling Scene00003: Normal(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubFst013:65576 calling Scene00004: Normal(Talk, TargetCanMove), id=TALK" );
    var callback = (SceneResult result) =>
    {
      Scene00005();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubFst013:65576 calling Scene00005: Normal(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubFst013:65576 calling Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TALK" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00096() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubFst013:65576 calling Scene00096: Normal(Talk, TargetCanMove), id=TALK" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 96, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00098() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst013:65576 calling Scene00098: Normal(Talk, TargetCanMove), id=TALK" );
    var callback = (SceneResult result) =>
    {
      Scene00100();
    };
    owner.Event.NewScene( Id, 98, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00100() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst013:65576 calling Scene00100: Normal(Talk, TargetCanMove), id=TALK" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 100, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
