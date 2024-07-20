// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66158)]
public class SubWil114 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 2 entries
  //SEQ_3, 1 entries
  //SEQ_4, 4 entries
  //SEQ_5, 4 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1003929
  //ACTOR1 = 1003977
  //ACTOR2 = 1004323
  //ACTOR3 = 1003978
  //ACTOR4 = 1004340
  //ACTOR5 = 1004341
  //EOBJECT0 = 2001410
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000390

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ISEMBARD
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=THANCRED
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 1
      case 2:
      {
        if( param1 == 2001410 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1003977 ) // ACTOR1 = THANCRED
        {
          Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=THANCRED
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00006(); // Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack 1
      case 4:
      {
        if( param1 == 1004323 ) // ACTOR2 = OURCEN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00008(); // Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00009: Normal(Talk, TargetCanMove), id=OURCEN
          }
          break;
        }
        if( param1 == 1003978 ) // ACTOR3 = THANCRED
        {
          Scene00010(); // Scene00010: Normal(Talk, TargetCanMove), id=THANCRED
          break;
        }
        if( param1 == 1004340 ) // ACTOR4 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004341 ) // ACTOR5 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 5 event item ITEM0 = UI8BH max stack 1
      case 5:
      {
        if( param1 == 1003978 ) // ACTOR3 = THANCRED
        {
          if( quest.UI8AL != 1 )
          {
            Scene00013(); // Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED
          }
          break;
        }
        if( param1 == 1004323 ) // ACTOR2 = OURCEN
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=OURCEN
          break;
        }
        if( param1 == 1004340 ) // ACTOR4 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004341 ) // ACTOR5 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1003929 ) // ACTOR0 = ISEMBARD
        {
          Scene00017(); // Scene00017: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISEMBARD
          break;
        }
        if( param1 == 1004323 ) // ACTOR2 = OURCEN
        {
          Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=OURCEN
          break;
        }
        if( param1 == 1004340 ) // ACTOR4 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004341 ) // ACTOR5 = unknown
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
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 3;
      quest.UI8BH = 1;
    }
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 4;
  }
  void checkProgressSeq4()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 5;
    }
  }
  void checkProgressSeq5()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubWil114:66158 calling Scene00001: Normal(Talk, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubWil114:66158 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00005() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00005: Normal(Talk, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("SubWil114:66158 calling Scene00006: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00007();
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("SubWil114:66158 calling Scene00007: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_4: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubWil114:66158 calling Scene00008: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00009();
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_4: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubWil114:66158 calling Scene00009: Normal(Talk, TargetCanMove), id=OURCEN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_4: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00010: Normal(Talk, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_4: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00011: Empty(None), id=unknown" );
  }

private void Scene00012() //SEQ_4: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_5: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("SubWil114:66158 calling Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_5: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00014: Normal(Talk, TargetCanMove), id=OURCEN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_5: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00016() //SEQ_5: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00017: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00018: Normal(Talk, TargetCanMove), id=OURCEN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_255: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00019: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_255: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil114:66158 calling Scene00020: Empty(None), id=unknown" );
  }
};
}
