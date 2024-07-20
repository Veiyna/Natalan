// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66272)]
public class GaiUsa304 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 6 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006195
  //EOBJECT0 = 2002827
  //EOBJECT1 = 2002828
  //EOBJECT2 = 2001947
  //EOBJECT3 = 2001948
  //EOBJECT4 = 2002829
  //EOBJECT5 = 2002830
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000593
  //ITEM1 = 2000592

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=TANGATOTUNGA
        break;
      }
      //seq 1 event item ITEM1 = UI8BL max stack 2
      //seq 1 event item ITEM0 = UI8CH max stack 1
      case 1:
      {
        if( param1 == 2002827 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002828 ) // EOBJECT1 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001947 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001948 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002829 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002830 ) // EOBJECT5 = unknown
        {
          if( !quest.getBitFlag8( 6 ) )
          {
            Scene00013(); // Scene00013: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM1 = UI8BH max stack 2
      //seq 255 event item ITEM0 = UI8BL max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00014(); // Scene00014: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TANGATOTUNGA
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
      if( quest.UI8BH == 2 )
      {
        quest.UI8AL = 0 ;
        quest.UI8BH = 0 ;
        quest.setBitFlag8( 1, false );
        quest.setBitFlag8( 2, false );
        quest.setBitFlag8( 3, false );
        quest.setBitFlag8( 4, false );
        quest.setBitFlag8( 5, false );
        quest.setBitFlag8( 6, false );
        quest.UI8BL = 0;
        quest.UI8CH = 0;
        quest.Sequence = 255;
        quest.UI8BH = 2;
        quest.UI8BL = 1;
      }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa304:66272 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=TANGATOTUNGA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT1, UI8AL = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 2, true );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, UI8BH = 2, Flag8(3)=True
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT3, UI8BH = 2, Flag8(4)=True
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: EOBJECT4, UI8BH = 2, Flag8(5)=True
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    quest.setBitFlag8( 5, true );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_1: EOBJECT5, UI8BH = 2, Flag8(6)=True
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( quest.UI8BH + 1);
    quest.setBitFlag8( 6, true );
    checkProgressSeq1();
  }

private void Scene00014() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa304:66272 calling Scene00014: NpcTrade(Talk, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa304:66272 calling Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=TANGATOTUNGA" );
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
