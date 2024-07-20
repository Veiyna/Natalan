// FFXIVTheMovie.ParserV3.11

using System.Numerics;
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65647)]
public class SubSea001 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 7 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002698
  //ACTOR1 = 1000969
  //ACTOR2 = 1003604
  //EOBJECT0 = 2001563
  //EOBJECT1 = 2001564
  //EOBJECT2 = 2001565
  //EOBJECT3 = 2001566
  //EOBJECT4 = 2001567
  //EOBJECT5 = 2001568
  //EVENTACTIONPROCESS = 14
  //ITEM0 = 2000447
  //POPRANGE0 = 4161445
  //QUEST0 = 65644
  //QUEST1 = 65645
  //TERRITORYTYPE0 = 129

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=NINIYA
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, YesNo, TargetCanMove, SystemTalk, CanCancel), id=SKAENRAEL
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 6
      case 2:
      {
        if( param1 == 2001563 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001564 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00006(); // Scene00006: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001565 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00008(); // Scene00008: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001566 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00010(); // Scene00010: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001567 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00012(); // Scene00012: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001568 ) // EOBJECT5 = unknown
        {
          if( !quest.getBitFlag8( 6 ) )
          {
            Scene00014(); // Scene00014: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1000969 ) // ACTOR1 = SKAENRAEL
        {
          Scene00015(); // Scene00015: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=SKAENRAEL
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 6
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00016(); // Scene00016: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00017: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AHLDSKYF
        break;
      }
      default:
      {
        player.sendUrgent("Sequence {} not defined. quest.Sequence ");
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

  public override void OnAreaTrigger(ulong actorId, WorldPosition position)
  {
    onProgress(EVENT_ON_WITHIN_RANGE, 0, 0, 0 );
  }

  public override void OnEventTerritory()
  {
    onProgress(EVENT_ON_ENTER_TERRITORY, 0, 0, 0 );
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
    if( quest.UI8AL == 6 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.setBitFlag8( 5, false );
      quest.setBitFlag8( 6, false );
      quest.Sequence = 255;
      quest.UI8BH = 6;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea001:65647 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=NINIYA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubSea001:65647 calling Scene00002: Normal(Talk, YesNo, TargetCanMove, SystemTalk, CanCancel), id=SKAENRAEL" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        player.TeleportTo(new WorldPosition(129, new Vector3(11, 20, 13), -2));
        player.SendQuestMessage(Id, 0, 0, 0, 0 );
        checkProgressSeq1();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: EOBJECT0, UI8AL = 6, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubSea001:65647 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 6 );
    checkProgressSeq2();
    this.owner.Event.StopEvent(Id);
  }

private void Scene00006() //SEQ_2: EOBJECT1, UI8AL = 6, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("SubSea001:65647 calling Scene00006: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 6 );
    checkProgressSeq2();
    this.owner.Event.StopEvent(Id);
  }

private void Scene00008() //SEQ_2: EOBJECT2, UI8AL = 6, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubSea001:65647 calling Scene00008: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 6 );
    checkProgressSeq2();
    this.owner.Event.StopEvent(Id);
  }

private void Scene00010() //SEQ_2: EOBJECT3, UI8AL = 6, Flag8(4)=True(Todo:1)
  {
    player.sendDebug("SubSea001:65647 calling Scene00010: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 6 );
    checkProgressSeq2();
    this.owner.Event.StopEvent(Id);
  }

private void Scene00012() //SEQ_2: EOBJECT4, UI8AL = 6, Flag8(5)=True(Todo:1)
  {
    player.sendDebug("SubSea001:65647 calling Scene00012: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 5, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 6 );
    checkProgressSeq2();
    this.owner.Event.StopEvent(Id);
  }

private void Scene00014() //SEQ_2: EOBJECT5, UI8AL = 6, Flag8(6)=True(Todo:1)
  {
    player.sendDebug("SubSea001:65647 calling Scene00014: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 6, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 6 );
    checkProgressSeq2();
    this.owner.Event.StopEvent(Id);
  }

private void Scene00015() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea001:65647 calling Scene00015: Normal(Talk, YesNo, TargetCanMove, CanCancel), id=SKAENRAEL" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
      }
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea001:65647 calling Scene00016: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00017();
      }
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00017() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea001:65647 calling Scene00017: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AHLDSKYF" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
