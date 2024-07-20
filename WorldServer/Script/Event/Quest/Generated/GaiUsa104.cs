// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66254)]
public class GaiUsa104 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 7 entries
  //SEQ_255, 1 entries

  //ACTIONTIMELINEEVENTFIDGET = 968
  //ACTOR0 = 1006675
  //ACTOR1 = 1000584
  //ACTOR2 = 1000580
  //ENEMY0 = 179
  //ENEMY1 = 7
  //EOBJECT0 = 2001913
  //EOBJECT1 = 2001914
  //EOBJECT2 = 2001915
  //EOBJECT3 = 2001916
  //EOBJECT4 = 2001917
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000577

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=PAPALYMO
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=IMEDIA
        break;
      }
      //seq 2 event item ITEM0 = UI8CH max stack 5
      case 2:
      {
        if( param1 == 179 ) // ENEMY0 = unknown
        {
          if( quest.UI8BH != 5 )
          {
            Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 7 ) // ENEMY1 = unknown
        {
          if( quest.UI8BL != 5 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001913 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001914 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001915 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001916 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00010(); // Scene00010: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001917 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 5
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO
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
    if( quest.UI8BH == 5 )
      if( quest.UI8BL == 5 )
        if( quest.UI8AL == 5 )
        {
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
          quest.UI8AL = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.setBitFlag8( 4, false );
          quest.setBitFlag8( 5, false );
          quest.UI8CH = 0;
          quest.Sequence = 255;
          quest.UI8BH = 5;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=PAPALYMO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00001: Normal(Talk, TargetCanMove), id=IMEDIA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_2: ENEMY0, UI8BH = 5, <No Flag>
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    checkProgressSeq2();
  }

private void Scene00003() //SEQ_2: ENEMY1, UI8BL = 5, <No Flag>
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( quest.UI8BL + 1);
    checkProgressSeq2();
  }

private void Scene00005() //SEQ_2: EOBJECT0, UI8AL = 5, Flag8(1)=True
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_2: EOBJECT1, UI8AL = 5, Flag8(2)=True
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_2: EOBJECT2, UI8AL = 5, Flag8(3)=True
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq2();
  }

private void Scene00010() //SEQ_2: EOBJECT3, UI8AL = 5, Flag8(4)=True
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00010: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq2();
  }

private void Scene00011() //SEQ_2: EOBJECT4, UI8AL = 5, Flag8(5)=True
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 5, true );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00013();
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa104:66254 calling Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
