// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66168)]
public class SubWil122 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_2, 6 entries
  //SEQ_3, 5 entries
  //SEQ_255, 6 entries

  //ACTOR0 = 1003939
  //ACTOR1 = 1003952
  //ACTOR2 = 1003953
  //ACTOR3 = 1003954
  //ACTOR4 = 1003955
  //ACTOR5 = 1003956
  //EOBJECT0 = 2001423
  //EOBJECT1 = 2001424
  //EOBJECT2 = 2001425
  //EOBJECT3 = 2001453
  //EOBJECT4 = 2001454
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000393
  //ITEM1 = 2000412

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=BENEGER
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( param1 == 2001423 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00001(); // Scene00001: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001424 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
            // +Callback Scene00004: Normal(Inventory), id=unknown
          }
          break;
        }
        if( param1 == 2001425 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
            // +Callback Scene00007: Normal(Inventory), id=unknown
          }
          break;
        }
        if( param1 == 2001453 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00008(); // Scene00008: Empty(None), id=unknown
            // +Callback Scene00010: Normal(Inventory), id=unknown
          }
          break;
        }
        if( param1 == 2001454 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
            // +Callback Scene00013: Normal(Inventory), id=unknown
          }
          break;
        }
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack ?
      //seq 2 event item ITEM1 = UI8BL max stack 1
      case 2:
      {
        if( param1 == 1003939 ) // ACTOR0 = BENEGER
        {
          if( quest.UI8AL != 1 )
          {
            Scene00016(); // Scene00016: Normal(Talk, TargetCanMove), id=BENEGER
          }
          break;
        }
        if( param1 == 2001423 ) // EOBJECT0 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001424 ) // EOBJECT1 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001425 ) // EOBJECT2 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001453 ) // EOBJECT3 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001454 ) // EOBJECT4 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8DH max stack ?
      //seq 3 event item ITEM1 = UI8DL max stack 1
      case 3:
      {
        if( param1 == 1003952 ) // ACTOR1 = MOBA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00027(); // Scene00027: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00028: Normal(Talk, TargetCanMove), id=MOBA
            // +Callback Scene00030: Normal(Talk, TargetCanMove), id=MOBA
          }
          break;
        }
        if( param1 == 1003953 ) // ACTOR2 = MOBB
        {
          if( quest.UI8BH != 1 )
          {
            Scene00031(); // Scene00031: NpcTrade(Talk, TargetCanMove), id=MOBB
            // +Callback Scene00032: Normal(Talk, TargetCanMove), id=MOBB
            // +Callback Scene00034: Normal(Talk, TargetCanMove), id=MOBB
          }
          break;
        }
        if( param1 == 1003954 ) // ACTOR3 = MOBC
        {
          if( quest.UI8BL != 1 )
          {
            Scene00035(); // Scene00035: NpcTrade(Talk, TargetCanMove), id=MOBC
            // +Callback Scene00036: Normal(Talk, TargetCanMove), id=MOBC
            // +Callback Scene00038: Normal(Talk, TargetCanMove), id=MOBC
          }
          break;
        }
        if( param1 == 1003955 ) // ACTOR4 = MOBD
        {
          if( quest.UI8CH != 1 )
          {
            Scene00039(); // Scene00039: NpcTrade(Talk, TargetCanMove), id=MOBD
            // +Callback Scene00040: Normal(Talk, TargetCanMove), id=MOBD
            // +Callback Scene00042: Normal(Talk, TargetCanMove), id=MOBD
          }
          break;
        }
        if( param1 == 1003956 ) // ACTOR5 = MOBE
        {
          if( quest.UI8CL != 1 )
          {
            Scene00043(); // Scene00043: NpcTrade(Talk, TargetCanMove), id=MOBE
            // +Callback Scene00044: Normal(Talk, TargetCanMove), id=MOBE
            // +Callback Scene00046: Normal(Talk, TargetCanMove), id=MOBE
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack ?
      //seq 255 event item ITEM1 = UI8BL max stack 1
      case 255:
      {
        if( param1 == 1003939 ) // ACTOR0 = BENEGER
        {
          Scene00047(); // Scene00047: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BENEGER
          break;
        }
        if( param1 == 1003952 ) // ACTOR1 = MOBA
        {
          Scene00048(); // Scene00048: Normal(Talk, TargetCanMove), id=MOBA
          break;
        }
        if( param1 == 1003953 ) // ACTOR2 = MOBB
        {
          Scene00049(); // Scene00049: Normal(Talk, TargetCanMove), id=MOBB
          break;
        }
        if( param1 == 1003954 ) // ACTOR3 = MOBC
        {
          Scene00050(); // Scene00050: Normal(Talk, TargetCanMove), id=MOBC
          break;
        }
        if( param1 == 1003955 ) // ACTOR4 = MOBD
        {
          Scene00051(); // Scene00051: Normal(Talk, TargetCanMove), id=MOBD
          break;
        }
        if( param1 == 1003956 ) // ACTOR5 = MOBE
        {
          Scene00052(); // Scene00052: Normal(Talk, TargetCanMove), id=MOBE
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
    quest.UI8BH = 1;
  }
  void checkProgressSeq1()
  {
    if( quest.UI8AL == 5 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.setBitFlag8( 5, false );
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.UI8BH = 0;
      quest.UI8BL = 0;
      quest.Sequence = 3;
      quest.UI8DH = 1;
      quest.UI8DL = 1;
    }
  }
  void checkProgressSeq3()
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
            }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=BENEGER" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: EOBJECT0, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00001: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00002() //SEQ_1: EOBJECT0, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00003() //SEQ_1: EOBJECT1, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00003: Empty(None), id=unknown" );
    Scene00004();
  }
private void Scene00004() //SEQ_1: EOBJECT1, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00004: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: EOBJECT2, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00005: Empty(None), id=unknown" );
    Scene00007();
  }
private void Scene00007() //SEQ_1: EOBJECT2, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00007: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: EOBJECT3, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00008: Empty(None), id=unknown" );
    Scene00010();
  }
private void Scene00010() //SEQ_1: EOBJECT3, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00010: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 4, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_1: EOBJECT4, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00011: Empty(None), id=unknown" );
    Scene00013();
  }
private void Scene00013() //SEQ_1: EOBJECT4, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("SubWil122:66168 calling Scene00013: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 5, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_2: ACTOR0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubWil122:66168 calling Scene00016: Normal(Talk, TargetCanMove), id=BENEGER" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00018: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00020() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00020: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00022() //SEQ_2: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00022: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00024() //SEQ_2: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00024: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00026() //SEQ_2: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00026: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00027() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubWil122:66168 calling Scene00027: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00028();
      }
    };
    owner.Event.NewScene( Id, 27, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00028() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubWil122:66168 calling Scene00028: Normal(Talk, TargetCanMove), id=MOBA" );
    var callback = (SceneResult result) =>
    {
      Scene00030();
    };
    owner.Event.NewScene( Id, 28, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00030() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubWil122:66168 calling Scene00030: Normal(Talk, TargetCanMove), id=MOBA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 30, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00031() //SEQ_3: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00031: NpcTrade(Talk, TargetCanMove), id=MOBB" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00032();
      }
    };
    owner.Event.NewScene( Id, 31, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00032() //SEQ_3: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00032: Normal(Talk, TargetCanMove), id=MOBB" );
    var callback = (SceneResult result) =>
    {
      Scene00034();
    };
    owner.Event.NewScene( Id, 32, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00034() //SEQ_3: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00034: Normal(Talk, TargetCanMove), id=MOBB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 34, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00035() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00035: NpcTrade(Talk, TargetCanMove), id=MOBC" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00036();
      }
    };
    owner.Event.NewScene( Id, 35, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00036() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00036: Normal(Talk, TargetCanMove), id=MOBC" );
    var callback = (SceneResult result) =>
    {
      Scene00038();
    };
    owner.Event.NewScene( Id, 36, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00038() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00038: Normal(Talk, TargetCanMove), id=MOBC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 38, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00039() //SEQ_3: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00039: NpcTrade(Talk, TargetCanMove), id=MOBD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00040();
      }
    };
    owner.Event.NewScene( Id, 39, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00040() //SEQ_3: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00040: Normal(Talk, TargetCanMove), id=MOBD" );
    var callback = (SceneResult result) =>
    {
      Scene00042();
    };
    owner.Event.NewScene( Id, 40, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00042() //SEQ_3: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00042: Normal(Talk, TargetCanMove), id=MOBD" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag8( 4, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 42, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00043() //SEQ_3: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00043: NpcTrade(Talk, TargetCanMove), id=MOBE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00044();
      }
    };
    owner.Event.NewScene( Id, 43, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00044() //SEQ_3: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00044: Normal(Talk, TargetCanMove), id=MOBE" );
    var callback = (SceneResult result) =>
    {
      Scene00046();
    };
    owner.Event.NewScene( Id, 44, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00046() //SEQ_3: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("SubWil122:66168 calling Scene00046: Normal(Talk, TargetCanMove), id=MOBE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CL =  (byte)( 1);
      quest.setBitFlag8( 5, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 46, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00047() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00047: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BENEGER" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 47, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00048() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00048: Normal(Talk, TargetCanMove), id=MOBA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 48, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00049() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00049: Normal(Talk, TargetCanMove), id=MOBB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 49, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00050() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00050: Normal(Talk, TargetCanMove), id=MOBC" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 50, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00051() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00051: Normal(Talk, TargetCanMove), id=MOBD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 51, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00052() //SEQ_255: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil122:66168 calling Scene00052: Normal(Talk, TargetCanMove), id=MOBE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 52, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
