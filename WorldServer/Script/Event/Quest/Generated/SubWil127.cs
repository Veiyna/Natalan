// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66173)]
public class SubWil127 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 4 entries
  //SEQ_4, 1 entries
  //SEQ_5, 6 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003941
  //ENEMY0 = 4097176
  //ENEMY1 = 4097181
  //ENEMY2 = 3986940
  //EOBJECT0 = 2001422
  //EOBJECT1 = 2001446
  //EOBJECT2 = 2001447
  //EOBJECT3 = 2001448
  //EOBJECT4 = 2001449
  //EOBJECT5 = 2001450
  //EOBJECT6 = 2001451
  //EOBJECT7 = 2001452
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000395
  //ITEM1 = 2000407

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=JOSPAIRE
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Empty(None), id=unknown
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: NpcTrade(Talk, TargetCanMove), id=JOSPAIRE
        // +Callback Scene00005: Normal(Talk, TargetCanMove), id=JOSPAIRE
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      case 3:
      {
        if( param1 == 2001446 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001447 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00010(); // Scene00010: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001448 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00013(); // Scene00013: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001449 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00016(); // Scene00016: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack 1
      //seq 4 event item ITEM1 = UI8BL max stack ?
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00019(); // Scene00019: Normal(Talk, TargetCanMove), id=JOSPAIRE
        break;
      }
      //seq 5 event item ITEM0 = UI8CH max stack 1
      //seq 5 event item ITEM1 = UI8CL max stack ?
      case 5:
      {
        if( param1 == 2001450 ) // EOBJECT5 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00020(); // Scene00020: Normal(Inventory), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT5
        if( param1 == 4097176 ) // ENEMY0 = unknown
        {
          Scene00021(); // Scene00021: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2001451 ) // EOBJECT6 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00022(); // Scene00022: Normal(Inventory), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT6
        if( param1 == 4097181 ) // ENEMY1 = unknown
        {
          Scene00023(); // Scene00023: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2001452 ) // EOBJECT7 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00024(); // Scene00024: Normal(Inventory), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT7
        if( param1 == 3986940 ) // ENEMY2 = unknown
        {
          Scene00025(); // Scene00025: Normal(Message, PopBNpc), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      //seq 255 event item ITEM1 = UI8BL max stack ?
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00026(); // Scene00026: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=JOSPAIRE
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
    quest.UI8BH = 1;
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 3;
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 4 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.Sequence = 4;
    }
  }
  void checkProgressSeq4()
  {
    quest.UI8BH = 0;
    quest.UI8BL = 0;
    quest.Sequence = 5;
    quest.UI8CH = 1;
    quest.UI8CL = 1;
  }
  void checkProgressSeq5()
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
          quest.Sequence = 255;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil127:66173 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=JOSPAIRE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubWil127:66173 calling Scene00003: Empty(None), id=unknown" );
    player.SendQuestMessage(Id, 0, 0, 0, 0 );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubWil127:66173 calling Scene00004: NpcTrade(Talk, TargetCanMove), id=JOSPAIRE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00005();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubWil127:66173 calling Scene00005: Normal(Talk, TargetCanMove), id=JOSPAIRE" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: EOBJECT1, UI8AL = 4, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubWil127:66173 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 4 );
    checkProgressSeq3();
  }

private void Scene00010() //SEQ_3: EOBJECT2, UI8AL = 4, Flag8(2)=True(Todo:2)
  {
    player.sendDebug("SubWil127:66173 calling Scene00010: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 4 );
    checkProgressSeq3();
  }

private void Scene00013() //SEQ_3: EOBJECT3, UI8AL = 4, Flag8(3)=True(Todo:2)
  {
    player.sendDebug("SubWil127:66173 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 4 );
    checkProgressSeq3();
  }

private void Scene00016() //SEQ_3: EOBJECT4, UI8AL = 4, Flag8(4)=True(Todo:2)
  {
    player.sendDebug("SubWil127:66173 calling Scene00016: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 4 );
    checkProgressSeq3();
  }

private void Scene00019() //SEQ_4: , <No Var>, <No Flag>(Todo:3)
  {
    player.sendDebug("SubWil127:66173 calling Scene00019: Normal(Talk, TargetCanMove), id=JOSPAIRE" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_5: EOBJECT5, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("SubWil127:66173 calling Scene00020: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_5: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil127:66173 calling Scene00021: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_5: EOBJECT6, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubWil127:66173 calling Scene00022: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00023() //SEQ_5: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil127:66173 calling Scene00023: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00024() //SEQ_5: EOBJECT7, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubWil127:66173 calling Scene00024: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00025() //SEQ_5: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil127:66173 calling Scene00025: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 25, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00026() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil127:66173 calling Scene00026: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=JOSPAIRE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
