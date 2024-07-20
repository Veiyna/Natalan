// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66053)]
public class ManFst313 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 5 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006343
  //ACTOR1 = 1003281
  //ACTOR2 = 1007686
  //ACTOR3 = 1007687
  //ACTOR4 = 1007688
  //ACTOR5 = 1007689
  //ACTOR6 = 1007467
  //ACTOR7 = 1006355
  //CUTMANFST31310 = 246
  //CUTMANFST31320 = 98
  //CUTMANFST31330 = 247
  //CUTMANFST31340 = 248
  //EVENTACTIONSEARCH = 1
  //LOCACTION1 = 1002
  //LOCACTOR0 = 1003783
  //LOCACTOR1 = 1002387
  //LOCACTOR2 = 1002388
  //LOCACTOR3 = 1003247
  //LOCACTOR4 = 1002389
  //LOCFACE0 = 604
  //LOCFACE1 = 605
  //LOCPOSACTOR1 = 4333952
  //LOCPOSACTOR2 = 4333953
  //LOCPOSACTOR3 = 4333954
  //LOCPOSACTOR4 = 4333955
  //LOCSE1 = 42
  //LOCTALKSHAPE1 = 6
  //TERRITORYTYPE0 = 212

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
        // +Callback Scene00001: Normal(Talk, NpcDespawn, QuestAccept, TargetCanMove), id=YSHTOLA
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, FadeIn, TargetCanMove), id=RASHAHT
        break;
      }
      case 2:
      {
        if( type == EVENT_ON_ENTER_TERRITORY ) // BASE_ID_TERRITORY_TYPE = unknown
        {
          Scene00003(); // Scene00003: Normal(FadeIn), id=unknown
          break;
        }
        if( param1 == 1007686 ) // ACTOR2 = NPCA
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 1007687 ) // ACTOR3 = NPCB
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=NPCB
          break;
        }
        if( param1 == 1007688 ) // ACTOR4 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007689 ) // ACTOR5 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: Normal(CutScene), id=unknown
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00009(); // Scene00009: Normal(Talk, QuestReward, TargetCanMove, Menu), id=ILIUD
        // +Callback Scene00010: Normal(CutScene, QuestComplete), id=unknown
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
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst313:66053 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManFst313:66053 calling Scene00001: Normal(Talk, NpcDespawn, QuestAccept, TargetCanMove), id=YSHTOLA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("ManFst313:66053 calling Scene00002: Normal(Talk, FadeIn, TargetCanMove), id=RASHAHT" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00003() //SEQ_2: BASE_ID_TERRITORY_TYPE, <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("ManFst313:66053 calling Scene00003: Normal(FadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst313:66053 calling Scene00004: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst313:66053 calling Scene00005: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst313:66053 calling Scene00006: Empty(None), id=unknown" );
  }

private void Scene00007() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst313:66053 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("ManFst313:66053 calling Scene00008: Normal(CutScene), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00009() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst313:66053 calling Scene00009: Normal(Talk, QuestReward, TargetCanMove, Menu), id=ILIUD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00010();
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00010() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManFst313:66053 calling Scene00010: Normal(CutScene, QuestComplete), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
    };
    owner.Event.NewScene( Id, 10, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
