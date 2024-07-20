// FFXIVTheMovie.ParserV3.11
// param used:
//_AGGRESSIVE_BNPC_HACK SET!!
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66446)]
public class GaiUsb801 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_2, 4 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006384
  //ACTOR1 = 1006407
  //ACTOR2 = 1006408
  //ACTOR3 = 1006409
  //ACTOR4 = 1006410
  //ENEMY0 = 4293113
  //EVENTACTIONRESCUEUNDERMIDDLE = 35
  //EVENTACTIONRESCUEUNDERSHORT = 34

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HAURCHEFANT
        break;
      }
      case 1:
      {
        if( param1 == 1006407 ) // ACTOR1 = FRANCEL
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=FRANCEL
          }
          break;
        }
        // BNpcHack credit moved to ACTOR1
        if( param1 == 4293113 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 1006408 ) // ACTOR2 = NPCA
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 1006409 ) // ACTOR3 = NPCB
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=NPCB
          break;
        }
        if( param1 == 1006410 ) // ACTOR4 = NPCC
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=NPCC
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1006408 ) // ACTOR2 = NPCA
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00007(); // Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCA
          }
          break;
        }
        if( param1 == 1006409 ) // ACTOR3 = NPCB
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00009(); // Scene00009: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCB
          }
          break;
        }
        if( param1 == 1006410 ) // ACTOR4 = NPCC
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00011(); // Scene00011: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCC
          }
          break;
        }
        if( param1 == 1006407 ) // ACTOR1 = FRANCEL
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=FRANCEL
          break;
        }
        break;
      }
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00014(); // Scene00014: Normal(Talk, NpcDespawn, TargetCanMove), id=FRANCEL
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00015(); // Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HAURCHEFANT
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
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.Sequence = 3;
    }
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb801:66446 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HAURCHEFANT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00002: Normal(Talk, TargetCanMove), id=FRANCEL" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }


private void Scene00003() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00003: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00004: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00005: Normal(Talk, TargetCanMove), id=NPCC" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR2, UI8AL = 3, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: ACTOR3, UI8AL = 3, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00009: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_2: ACTOR4, UI8AL = 3, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00011: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00012: Normal(Talk, TargetCanMove), id=FRANCEL" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00014: Normal(Talk, NpcDespawn, TargetCanMove), id=FRANCEL" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb801:66446 calling Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HAURCHEFANT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
