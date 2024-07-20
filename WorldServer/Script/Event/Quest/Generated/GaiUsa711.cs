// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66320)]
public class GaiUsa711 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 2 entries
  //SEQ_4, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006688
  //ACTOR1 = 1007417
  //ACTOR2 = 1006240
  //ACTOR3 = 1006241
  //LOCACTION1 = 788
  //LOCACTOR1 = 1008113
  //LOCACTOR2 = 1008114
  //LOCACTOR3 = 1006241
  //LOCACTOR4 = 1006242
  //LOCFACE0 = 604
  //LOCFACE1 = 620
  //LOCPOSACTOR1 = 4256813
  //LOCPOSACTOR2 = 4256814
  //RITEM0 = 4674

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MINFILIA
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=NORAXIA
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=MEDROD
        break;
      }
      case 3:
      {
        if( param1 == 1006241 ) // ACTOR3 = AIDEEN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=AIDEEN
          }
          break;
        }
        if( param1 == 1006240 ) // ACTOR2 = MEDROD
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=MEDROD
          break;
        }
        break;
      }
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00006(); // Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00007: Normal(Talk, TargetCanMove), id=MEDROD
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=MEDROD
        // +Callback Scene00009: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=MEDROD
        // +Callback Scene00010: Normal(Talk, TargetCanMove), id=MEDROD
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
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 4;
    }
  }
  void checkProgressSeq4()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa711:66320 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MINFILIA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00002: Normal(Talk, TargetCanMove), id=NORAXIA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00003: Normal(Talk, TargetCanMove), id=MEDROD" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_3: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00004: Normal(Talk, TargetCanMove), id=AIDEEN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00005: Normal(Talk, TargetCanMove), id=MEDROD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00007();
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00007: Normal(Talk, TargetCanMove), id=MEDROD" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00008: Normal(Talk, TargetCanMove), id=MEDROD" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00009: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=MEDROD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00010();
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
private void Scene00010() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa711:66320 calling Scene00010: Normal(Talk, TargetCanMove), id=MEDROD" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
