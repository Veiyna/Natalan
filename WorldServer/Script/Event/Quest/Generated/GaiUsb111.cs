// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66366)]
public class GaiUsb111 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 2 entries
  //SEQ_4, 1 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1006288
  //ACTOR1 = 1006287
  //ACTOR2 = 1006737
  //ENEMY0 = 2268
  //ITEM0 = 2000647
  //ITEM1 = 2000648

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=UKHUBATIA
        break;
      }
      case 1:
      {
        // empty entry
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack ?
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=UTYKHATIA
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack ?
      //seq 3 event item ITEM1 = UI8BL max stack 3
      case 3:
      {
        if( param1 == 2268 ) // ENEMY0 = unknown
        {
          if( quest.UI8BL != 3 )
          {
            player.SendQuestMessage(Id, 2, 2, quest.UI8BL, 3 );
            quest.UI8BL =  (byte)( quest.UI8BL + 1);
            checkProgressSeq3();
          }
          break;
        }
        if( param1 == 1006737 ) // ACTOR2 = UKAHZUNA
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=UKAHZUNA
          // +Callback Scene00004: Normal(Talk, TargetCanMove), id=UKAHZUNA
          break;
        }
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack ?
      //seq 4 event item ITEM1 = UI8BL max stack 3
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00005(); // Scene00005: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00006: Normal(Talk, TargetCanMove), id=UKAHZUNA
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack ?
      case 255:
      {
        if( param1 == 1006287 ) // ACTOR1 = UTYKHATIA
        {
          Scene00007(); // Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=UTYKHATIA
          break;
        }
        if( param1 == 1006737 ) // ACTOR2 = UKAHZUNA
        {
          Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=UKAHZUNA
          break;
        }
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
    quest.Sequence = 2;
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
    if( quest.UI8BL == 3 )
    {
      quest.Sequence = 4;
      quest.UI8BH = 1;
      quest.UI8BL = 3;
    }
  }
  void checkProgressSeq4()
  {
    quest.UI8BL = 0;
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb111:66366 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=UKHUBATIA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }


private void Scene00002() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00002: Normal(Talk, TargetCanMove), id=UTYKHATIA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }


private void Scene00003() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00003: Normal(Talk, TargetCanMove), id=UKAHZUNA" );
    var callback = (SceneResult result) =>
    {
      Scene00004();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00004: Normal(Talk, TargetCanMove), id=UKAHZUNA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00005: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00006();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00006: Normal(Talk, TargetCanMove), id=UKAHZUNA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=UTYKHATIA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb111:66366 calling Scene00008: Normal(Talk, TargetCanMove), id=UKAHZUNA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
