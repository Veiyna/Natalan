// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66448)]
public class GaiUsb803 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 13 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006384
  //ACTOR1 = 1006415
  //ACTOR10 = 1006424
  //ACTOR11 = 1006425
  //ACTOR2 = 1006416
  //ACTOR3 = 1006417
  //ACTOR4 = 1006418
  //ACTOR5 = 1006419
  //ACTOR6 = 1006420
  //ACTOR7 = 1006421
  //ACTOR8 = 1006422
  //ACTOR9 = 1006423
  //EOBJECT0 = 2002588
  //EOBJECT1 = 2002615
  //EVENTRANGE0 = 4333660
  //EVENTACTIONSEARCH = 1
  //QUESTBATTLE0 = 60
  //TERRITORYTYPE0 = 301

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=BRIGIE
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=HAURCHEFANT
        break;
      }
      case 3:
      {
        if( param1 == 1006416 ) // ACTOR2 = HOURLINET
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, QuestBattle, YesNo, TargetCanMove), id=HOURLINET
          }
          break;
        }
        if( param1 == 2002588 ) // EOBJECT0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002615 ) // EOBJECT1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006417 ) // ACTOR3 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006418 ) // ACTOR4 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006419 ) // ACTOR5 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006420 ) // ACTOR6 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006421 ) // ACTOR7 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006422 ) // ACTOR8 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006423 ) // ACTOR9 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006424 ) // ACTOR10 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006425 ) // ACTOR11 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 4333660 ) // EVENTRANGE0 = unknown
        {
          Scene00018(); // Scene00018: Normal(QuestBattle, YesNo), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00019(); // Scene00019: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HAURCHEFANT
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
      quest.setBitFlag16( 1, false );
      quest.setBitFlag16( 13, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb803:66448 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HAURCHEFANT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00002: Normal(Talk, TargetCanMove), id=BRIGIE" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00003: Normal(Talk, TargetCanMove), id=HAURCHEFANT" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_3: ACTOR2, UI8AL = 1, Flag16(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00004: Normal(Talk, QuestBattle, YesNo, TargetCanMove), id=HOURLINET" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 60 );
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00008() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00008: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00009() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_3: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00011() //SEQ_3: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00011: Empty(None), id=unknown" );
  }

private void Scene00012() //SEQ_3: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_3: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00013: Empty(None), id=unknown" );
  }

private void Scene00014() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00014: Empty(None), id=unknown" );
  }

private void Scene00015() //SEQ_3: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00016() //SEQ_3: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_3: ACTOR11, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00017: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_3: EVENTRANGE0, <No Var>, Flag16(13)=True
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00018: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 60 );
      }
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb803:66448 calling Scene00019: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HAURCHEFANT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
