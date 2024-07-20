// FFXIVTheMovie.ParserV3.11
// fake IsAnnounce table
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66964)]
public class SubFst122 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 1 entries
  //SEQ_4, 1 entries
  //SEQ_5, 1 entries
  //SEQ_6, 1 entries
  //SEQ_7, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000373
  //ACTOR1 = 1005576
  //COMPLETION0 = 203
  //COMPLETION1 = 206
  //EVENTACTIONSEARCH = 1
  //EVENTSURPRISED = 762
  //ITEM0 = 2001082

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SPINNINGBLADE
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(SystemTalk), id=unknown
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack 1
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00005(); // Scene00005: Normal(SystemTalk), id=unknown
        // +Callback Scene00006: Normal(Talk, TargetCanMove), id=RAIMONDAUX
        break;
      }
      //seq 5 event item ITEM0 = UI8BH max stack 1
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX
        // +Callback Scene00008: Normal(SystemTalk), id=unknown
        // +Callback Scene00009: Normal(Talk, TargetCanMove), id=RAIMONDAUX
        break;
      }
      //seq 6 event item ITEM0 = UI8BH max stack 1
      case 6:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00010(); // Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00011: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX
        break;
      }
      case 7:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(SystemTalk), id=unknown
        // +Callback Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=RAIMONDAUX
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00014(); // Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SPINNINGBLADE
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
    quest.Sequence = 6;
    quest.UI8BH = 1;
  }
  void checkProgressSeq6()
  {
    quest.UI8BH = 0;
    quest.Sequence = 7;
  }
  void checkProgressSeq7()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst122:66964 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("SubFst122:66964 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SPINNINGBLADE" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubFst122:66964 calling Scene00002: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubFst122:66964 calling Scene00003: Normal(SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("SubFst122:66964 calling Scene00004: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubFst122:66964 calling Scene00005: Normal(SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubFst122:66964 calling Scene00006: Normal(Talk, TargetCanMove), id=RAIMONDAUX" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubFst122:66964 calling Scene00007: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX" );
    var callback = (SceneResult result) =>
    {
      Scene00008();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubFst122:66964 calling Scene00008: Normal(SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubFst122:66964 calling Scene00009: Normal(Talk, TargetCanMove), id=RAIMONDAUX" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_6: , <No Var>, <No Flag>(Todo:5)
  {
    player.sendDebug("SubFst122:66964 calling Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00011();
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_6: , <No Var>, <No Flag>(Todo:5)
  {
    player.sendDebug("SubFst122:66964 calling Scene00011: Normal(Talk, TargetCanMove, SystemTalk), id=RAIMONDAUX" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 5, 0, 0, 0 );
      checkProgressSeq6();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_7: , <No Var>, <No Flag>(Todo:6)
  {
    player.sendDebug("SubFst122:66964 calling Scene00012: Normal(SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00013();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_7: , <No Var>, <No Flag>(Todo:6)
  {
    player.sendDebug("SubFst122:66964 calling Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=RAIMONDAUX" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 6, 0, 0, 0 );
      checkProgressSeq7();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst122:66964 calling Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SPINNINGBLADE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
