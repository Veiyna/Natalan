// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66450)]
public class GaiUsb805 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 10 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1006430
  //ACTOR1 = 1006431
  //ENEMY0 = 4293168
  //ENEMY1 = 4293169
  //ENEMY2 = 4293167
  //EOBJECT0 = 2002409
  //EOBJECT1 = 2002410
  //EOBJECT2 = 2002411
  //EOBJECT3 = 2002412
  //EOBJECT4 = 2002413
  //EOBJECT5 = 2002414
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000717

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=JULINE
        break;
      }
      //seq 1 event item ITEM0 = UI8DL max stack 3
      case 1:
      {
        if( param1 == 2002409 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Message), id=unknown
          }
          break;
        }
        if( param1 == 2002410 ) // EOBJECT1 = unknown
        {
          Scene00005(); // Scene00005: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 4293168 ) // ENEMY0 = unknown
        {
          Scene00007(); // Scene00007: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2002411 ) // EOBJECT2 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 4293169 ) // ENEMY1 = unknown
        {
          Scene00009(); // Scene00009: Normal(Message), id=unknown
          break;
        }
        if( param1 == 2002412 ) // EOBJECT3 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00010(); // Scene00010: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002413 ) // EOBJECT4 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00011(); // Scene00011: Normal(Message), id=unknown
          }
          break;
        }
        if( param1 == 2002414 ) // EOBJECT5 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        if( param1 == 4293167 ) // ENEMY2 = unknown
        {
          Scene00013(); // Scene00013: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 1006431 ) // ACTOR1 = MATIGNIANT
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=MATIGNIANT
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 3
      case 255:
      {
        if( param1 == 1006430 ) // ACTOR0 = JULINE
        {
          Scene00015(); // Scene00015: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00016: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=JULINE
          break;
        }
        if( param1 == 1006431 ) // ACTOR1 = MATIGNIANT
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=MATIGNIANT
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
          quest.setBitFlag8( 4, false );
          quest.setBitFlag8( 5, false );
          quest.setBitFlag8( 6, false );
          quest.UI8DL = 0;
          quest.Sequence = 255;
          quest.UI8BH = 3;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb805:66450 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=JULINE" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00003: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: EOBJECT1, <No Var>, Flag8(2)=True
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00005: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00007: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: EOBJECT2, <No Var>, Flag8(3)=True
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00008: Empty(None), id=unknown" );
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00009: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_1: EOBJECT3, UI8BH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00010: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: EOBJECT4, UI8BL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00011: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 5, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_1: EOBJECT5, <No Var>, Flag8(6)=True
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00012: Empty(None), id=unknown" );
    quest.setBitFlag8( 6, true );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_1: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00013: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_1: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00014: Normal(Talk, TargetCanMove), id=MATIGNIANT" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00015: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00016();
      }
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00016() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00016: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=JULINE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb805:66450 calling Scene00017: Normal(Talk, TargetCanMove), id=MATIGNIANT" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
