// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66323)]
public class GaiUsa803 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 5 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006241
  //EOBJECT0 = 2001983
  //EOBJECT1 = 2001984
  //EOBJECT2 = 2001985
  //EOBJECT3 = 2001986
  //EOBJECT4 = 2001987
  //EOBJECT5 = 2001988
  //EVENTACTIONSEARCHMIDDLE = 3
  //ITEM0 = 2000616
  //ITEM1 = 2000617

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
      //seq 0 event item ITEM0 = UI8BH max stack ?
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=AIDEEN
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( type == EVENT_ON_TALK ) Scene00002(); // Scene00002: Normal(Inventory), id=unknown
        if( type == EVENT_ON_EVENT_ITEM ) Scene00003(); // Scene00003: Normal(QuestGimmickReaction), id=unknown
        break;
      }
      //seq 2 event item ITEM0 = UI8DH max stack ?
      //seq 2 event item ITEM1 = UI8DL max stack 5
      case 2:
      {
        if( param1 == 2001984 ) // EOBJECT1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001985 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001986 ) // EOBJECT3 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001987 ) // EOBJECT4 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001988 ) // EOBJECT5 = unknown
        {
          if( quest.UI8CL != 1 )
          {
            Scene00013(); // Scene00013: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack ?
      //seq 255 event item ITEM1 = UI8BL max stack 5
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00014(); // Scene00014: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AIDEEN
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
    quest.UI8BH = 1;
  }
  void checkProgressSeq1()
  {
    quest.UI8BH = 0;
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
          if( quest.UI8CH == 1 )
            if( quest.UI8CL == 1 )
            {
              quest.UI8AL = 0 ;
              quest.UI8BH = 0 ;
              quest.UI8BL = 0 ;
              quest.UI8CH = 0 ;
              quest.UI8CL = 0 ;
              quest.setBitFlag8( 1, false );
              quest.setBitFlag8( 2, false );
              quest.setBitFlag8( 3, false );
              quest.setBitFlag8( 4, false );
              quest.setBitFlag8( 5, false );
              quest.UI8DH = 0;
              quest.UI8DL = 0;
              quest.Sequence = 255;
              quest.UI8BH = 1;
              quest.UI8BL = 5;
            }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa803:66323 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=AIDEEN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00002: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00003: Normal(QuestGimmickReaction), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: EOBJECT1, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_2: EOBJECT2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag8( 2, true );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_2: EOBJECT3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq2();
  }

private void Scene00011() //SEQ_2: EOBJECT4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8CH =  (byte)( 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq2();
  }

private void Scene00013() //SEQ_2: EOBJECT5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8CL =  (byte)( 1);
    quest.setBitFlag8( 5, true );
    checkProgressSeq2();
  }

private void Scene00014() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00014: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00015();
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa803:66323 calling Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AIDEEN" );
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
