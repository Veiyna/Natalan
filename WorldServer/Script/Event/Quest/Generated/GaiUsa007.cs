// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66249)]
public class GaiUsa007 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 9 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1006189
  //ENEMY0 = 4277274
  //ENEMY1 = 4277391
  //ENEMY2 = 4278132
  //EOBJECT0 = 2002606
  //EOBJECT1 = 2002607
  //EOBJECT2 = 2002608
  //EOBJECT3 = 2000717
  //EOBJECT4 = 2000718
  //EOBJECT5 = 2000719
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONWATCHGUARDLONG = 28

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PIRALNAUT
        break;
      }
      case 1:
      {
        if( param1 == 2002606 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4277274 ) // ENEMY0 = unknown
        {
          Scene00005(); // Scene00005: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2002607 ) // EOBJECT1 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00007(); // Scene00007: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT1
        if( param1 == 4277391 ) // ENEMY1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002608 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT2
        if( param1 == 4278132 ) // ENEMY2 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000717 ) // EOBJECT3 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000718 ) // EOBJECT4 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000719 ) // EOBJECT5 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006189 ) // ACTOR0 = PIRALNAUT
        {
          Scene00014(); // Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=PIRALNAUT
          break;
        }
        if( param1 == 2000717 ) // EOBJECT3 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000718 ) // EOBJECT4 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 2000719 ) // EOBJECT5 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
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
          quest.Sequence = 255;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa007:66249 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PIRALNAUT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00003: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00005: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: EOBJECT1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00007: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00008: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT2, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_1: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00012() //SEQ_1: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00012: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_1: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00013: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00014() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=PIRALNAUT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00018() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00018: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_255: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa007:66249 calling Scene00020: Empty(None), id=unknown" );
  }
};
}
