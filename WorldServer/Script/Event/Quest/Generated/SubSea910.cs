// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;
using WorldServer.Network.Message;

namespace WorldServer.Script.Quest
{
[EventScript(66237)]
public class SubSea910 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003281
  //ACTOR1 = 1001027
  //ACTOR2 = 1006002
  //NCUT0 = 239
  //RITEM0 = 6017

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=unknown
        break;
      }
      case 1:
      {
        if( param1 == 1001027 ) // ACTOR1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00002: Normal(CutScene), id=unknown
          }
          break;
        }
        if( param1 == 1003281 ) // ACTOR0 = unknown
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1006002 ) // ACTOR2 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, NpcDespawn, TargetCanMove, CanCancel), id=unknown
          }
          break;
        }
        if( param1 == 1001027 ) // ACTOR1 = unknown
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00006(); // Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=unknown
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
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea910:66237 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubSea910:66237 calling Scene00001: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00002();
      }
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubSea910:66237 calling Scene00002: Normal(CutScene), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea910:66237 calling Scene00003: Normal(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubSea910:66237 calling Scene00004: Normal(Talk, NpcDespawn, TargetCanMove, CanCancel), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults == 1 || ( result.errorCode == 0 && result.numOfResults == 2 ) )
      {
        quest.UI8AL =  (byte)( 1);
        quest.setBitFlag8( 1, true );
        player.SendQuestMessage(Id, 1, 0, 0, 0 );
        checkProgressSeq2();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea910:66237 calling Scene00005: Normal(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea910:66237 calling Scene00006: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.SetMasterUnlock((ushort)UnlockEntry.CompanionMenu, true);
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

  public override void OnYield(byte yieldId, string data)
  {
    this.owner.SetCompanionName(data);
    this.owner.Event.ResumeEvent(this.Id,yieldId, 1);
  }
};
}
