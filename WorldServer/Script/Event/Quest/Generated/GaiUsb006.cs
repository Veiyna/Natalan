// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66349)]
public class GaiUsb006 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 6 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006273
  //ACTOR1 = 1006274
  //ACTOR2 = 1006275
  //ACTOR3 = 1006276
  //ACTOR4 = 1006277
  //ACTOR5 = 1006278
  //EOBJECT0 = 2002036
  //EVENTACTIONSEARCH = 1

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=GEGERUJU
        break;
      }
      case 1:
      {
        if( param1 == 1006274 ) // ACTOR1 = BASHA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=BASHA
          }
          break;
        }
        if( param1 == 2002036 ) // EOBJECT0 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1006275 ) // ACTOR2 = GALA
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00005(); // Scene00005: Normal(Talk, NpcDespawn, TargetCanMove), id=GALA
          }
          break;
        }
        if( param1 == 1006276 ) // ACTOR3 = GALB
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00006(); // Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=GALB
          }
          break;
        }
        if( param1 == 1006277 ) // ACTOR4 = GALC
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00007(); // Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=GALC
          }
          break;
        }
        if( param1 == 1006278 ) // ACTOR5 = GALD
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00008(); // Scene00008: Normal(Talk, NpcDespawn, TargetCanMove), id=GALD
          }
          break;
        }
        if( param1 == 1006274 ) // ACTOR1 = BASHA
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=BASHA
          break;
        }
        if( param1 == 2002036 ) // EOBJECT0 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GEGERUJU
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
    if( quest.UI8AL == 4 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb006:66349 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=GEGERUJU" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00002: Normal(Talk, TargetCanMove), id=BASHA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_2: ACTOR2, UI8AL = 4, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00005: Normal(Talk, NpcDespawn, TargetCanMove), id=GALA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR3, UI8AL = 4, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=GALB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR4, UI8AL = 4, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=GALC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_2: ACTOR5, UI8AL = 4, Flag8(4)=True(Todo:1)
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00008: Normal(Talk, NpcDespawn, TargetCanMove), id=GALD" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 4, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 4 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00009: Normal(Talk, TargetCanMove), id=BASHA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb006:66349 calling Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=GEGERUJU" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
