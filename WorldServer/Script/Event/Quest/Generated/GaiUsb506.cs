// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66411)]
public class GaiUsb506 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 1 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006360
  //ACTOR1 = 1003946
  //ACTOR2 = 1004575
  //ACTOR3 = 1004506
  //ACTOR4 = 1006355
  //ITEM0 = 2000682
  //ITEM1 = 2000683
  //ITEM2 = 2000684

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BARRYN
        break;
      }
      //seq 1 event item ITEM0 = UI8BL max stack 1
      case 1:
      {
        if( param1 == 1003946 ) // ACTOR1 = ESMOUR
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=ESMOUR
          }
          break;
        }
        if( param1 == 1004575 ) // ACTOR2 = ZOZORU
        {
          if( quest.UI8BH != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=ZOZORU
          }
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00005: Normal(Talk, TargetCanMove), id=ERMEGARDE
        break;
      }
      //seq 3 event item ITEM2 = UI8BH max stack 1
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00006(); // Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00007: Normal(Talk, TargetCanMove), id=ILIUD
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BARRYN
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
      if( quest.UI8BH == 1 )
      {
        quest.UI8AL = 0 ;
        quest.UI8BH = 0 ;
        quest.setBitFlag8( 1, false );
        quest.setBitFlag8( 2, false );
        quest.UI8BL = 0;
        quest.Sequence = 2;
        quest.UI8BH = 1;
      }
  }
  void checkProgressSeq2()
  {
    quest.UI8BH = 0;
    quest.Sequence = 3;
    quest.UI8BH = 1;
  }
  void checkProgressSeq3()
  {
    quest.UI8BH = 0;
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb506:66411 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BARRYN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00002: Normal(Talk, TargetCanMove), id=ESMOUR" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00003: Normal(Talk, TargetCanMove), id=ZOZORU" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00004: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00005();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_2: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00005: Normal(Talk, TargetCanMove), id=ERMEGARDE" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_3: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00007();
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_3: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00007: Normal(Talk, TargetCanMove), id=ILIUD" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb506:66411 calling Scene00008: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BARRYN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
