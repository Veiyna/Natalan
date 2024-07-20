// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66252)]
public class GaiUsa102 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000557
  //ACTOR1 = 1000587
  //EOBJECT0 = 2001909
  //EOBJECT1 = 2001910
  //EOBJECT2 = 2001911
  //EOBJECT3 = 2001912
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000575
  //ITEM1 = 2000576
  //ITEM2 = 2000892

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BLAISETTE
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( param1 == 2001909 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001910 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001911 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001912 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00010(); // Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00011: Normal(Talk, TargetCanMove), id=BLAISETTE
        break;
      }
      //seq 255 event item ITEM2 = UI8BH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=DELLEXIA
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
    if( quest.UI8AL == 4 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.Sequence = 2;
      quest.UI8BH = 1;
    }
  }
  void checkProgressSeq2()
  {
    quest.UI8BH = 0;
    quest.Sequence = 255;
    quest.UI8BH = 1;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa102:66252 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BLAISETTE" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 4, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT1, UI8AL = 4, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, UI8AL = 4, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: EOBJECT3, UI8AL = 4, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00011();
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00011: Normal(Talk, TargetCanMove), id=BLAISETTE" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa102:66252 calling Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa102:66252 calling Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=DELLEXIA" );
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
