// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66467)]
public class GaiUsb908 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006456
  //EOBJECT0 = 2002159
  //EOBJECT1 = 2002160
  //EOBJECT2 = 2002161
  //EOBJECT3 = 2002162
  //EOBJECT4 = 2002163
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONSEARCHMIDDLE = 3

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=CHAUNOLLET
        break;
      }
      case 1:
      {
        if( param1 == 2002159 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002160 ) // EOBJECT1 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002161 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002162 ) // EOBJECT3 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002163 ) // EOBJECT4 = unknown
        {
          if( quest.UI8CL != 1 )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=CHAUNOLLET
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
              quest.Sequence = 255;
            }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb908:66467 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb908:66467 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=CHAUNOLLET" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb908:66467 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 0, 0, 0 );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb908:66467 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag8( 2, true );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb908:66467 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT3, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb908:66467 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8CH =  (byte)( 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: EOBJECT4, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsb908:66467 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8CL =  (byte)( 1);
    quest.setBitFlag8( 5, true );
    checkProgressSeq1();
  }

private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb908:66467 calling Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=CHAUNOLLET" );
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
