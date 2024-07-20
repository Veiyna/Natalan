// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65574)]
public class SubFst012 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000768
  //ACTOR1 = 1000214
  //ACTOR2 = 1000218
  //ACTOR3 = 1000238
  //ACTOR4 = 1000248
  //ITEM0 = 2000081
  //ITEM1 = 2000082
  //ITEM2 = 2000083
  //SEQ0ACTOR0 = 0
  //SEQ1ACTOR1 = 1
  //SEQ1ACTOR2 = 2
  //SEQ1ACTOR3 = 3
  //SEQ2ACTOR4 = 4
  //SEQ2ACTOR4NPCTRADENO = 99
  //SEQ2ACTOR4NPCTRADEOK = 100

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=PARSEMONTRET
        break;
      }
      //seq 1 event item ITEM0 = UI8CH max stack 1
      //seq 1 event item ITEM1 = UI8CL max stack 1
      //seq 1 event item ITEM2 = UI8DH max stack 1
      case 1:
      {
        if( param1 == 1000214 ) // ACTOR1 = GURTHCID
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=GURTHCID
          }
          break;
        }
        if( param1 == 1000218 ) // ACTOR2 = ADMIRANDA
        {
          if( quest.UI8BH != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=ADMIRANDA
          }
          break;
        }
        if( param1 == 1000238 ) // ACTOR3 = ALARIC
        {
          if( quest.UI8BL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=ALARIC
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      //seq 255 event item ITEM1 = UI8BL max stack 1
      //seq 255 event item ITEM2 = UI8CH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: NpcTrade(Talk, TargetCanMove), id=CEINGULED
        // +Callback Scene00100: Normal(QuestReward, QuestComplete), id=unknown
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
        if( quest.UI8BL == 1 )
        {
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.UI8CH = 0;
          quest.UI8CL = 0;
          quest.UI8DH = 0;
          quest.Sequence = 255;
          quest.UI8BH = 1;
          quest.UI8BL = 1;
          quest.UI8CH = 1;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst012:65574 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=PARSEMONTRET" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubFst012:65574 calling Scene00001: Normal(Talk, TargetCanMove), id=GURTHCID" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubFst012:65574 calling Scene00002: Normal(Talk, TargetCanMove), id=ADMIRANDA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubFst012:65574 calling Scene00003: Normal(Talk, TargetCanMove), id=ALARIC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst012:65574 calling Scene00004: NpcTrade(Talk, TargetCanMove), id=CEINGULED" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00100();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00100() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst012:65574 calling Scene00100: Normal(QuestReward, QuestComplete), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 100, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
