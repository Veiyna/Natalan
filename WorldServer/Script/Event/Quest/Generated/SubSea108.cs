// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65941)]
public class SubSea108 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 12 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003244
  //ENEMY0 = 3929524
  //ENEMY1 = 3929555
  //ENEMY2 = 3929528
  //ENEMY3 = 3929560
  //ENEMY4 = 3929529
  //ENEMY5 = 3929563
  //ENEMY6 = 3929531
  //ENEMY7 = 3929569
  //EOBJECT0 = 2001265
  //EOBJECT1 = 2001266
  //EOBJECT2 = 2001267
  //EOBJECT3 = 2001268
  //EVENTACTION = 32
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000346

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=LYULF
        break;
      }
      //seq 1 event item ITEM0 = UI8CL max stack 3
      case 1:
      {
        if( param1 == 2001265 ) // EOBJECT0 = unknown
        {
          Scene00001(); // Scene00001: Empty(None), id=unknown
          break;
        }
        if( param1 == 3929524 ) // ENEMY0 = unknown
        {
          Scene00002(); // Scene00002: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3929555 ) // ENEMY1 = unknown
        {
          Scene00003(); // Scene00003: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001266 ) // EOBJECT1 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 3929528 ) // ENEMY2 = unknown
        {
          Scene00005(); // Scene00005: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3929560 ) // ENEMY3 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001267 ) // EOBJECT2 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 3929529 ) // ENEMY4 = unknown
        {
          Scene00008(); // Scene00008: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3929563 ) // ENEMY5 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001268 ) // EOBJECT3 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 3929531 ) // ENEMY6 = unknown
        {
          Scene00011(); // Scene00011: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3929569 ) // ENEMY7 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 3
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00013(); // Scene00013: NpcTrade(Talk), id=unknown
        // +Callback Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LYULF
        break;
      }
      default:
      {
        player.sendUrgent("Sequence {} not defined. quest.Sequence ");
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

  public override void OnAreaTrigger(ulong actorId, WorldPosition position)
  {
    onProgress(EVENT_ON_WITHIN_RANGE, actorId, 0, 0 );
  }

  public override void OnEventTerritory()
  {
    onProgress(EVENT_ON_ENTER_TERRITORY, 0, 0, 0 );
  }
  void checkProgressSeq0()
  {
    quest.Sequence = 1;
  }
  void checkProgressSeq1()
  {
    quest.setBitFlag8( 1, false );
    quest.setBitFlag8( 2, false );
    quest.setBitFlag8( 3, false );
    quest.setBitFlag8( 4, false );
    quest.UI8CL = 0;
    quest.Sequence = 255;
    quest.UI8BH = 3;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=LYULF" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: EOBJECT0, <No Var>, Flag8(1)=True
  {
    player.sendDebug("SubSea108:65941 calling Scene00001: Empty(None), id=unknown" );
    quest.setBitFlag8( 1, true );
    checkProgressSeq1();
  }

private void Scene00002() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00002: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00003: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: EOBJECT1, <No Var>, Flag8(2)=True
  {
    player.sendDebug("SubSea108:65941 calling Scene00004: Empty(None), id=unknown" );
    quest.setBitFlag8( 2, true );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00005: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_1: ENEMY3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, <No Var>, Flag8(3)=True
  {
    player.sendDebug("SubSea108:65941 calling Scene00007: Empty(None), id=unknown" );
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: ENEMY4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00008: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_1: ENEMY5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_1: EOBJECT3, <No Var>, Flag8(4)=True
  {
    player.sendDebug("SubSea108:65941 calling Scene00010: Empty(None), id=unknown" );
    quest.setBitFlag8( 4, true );
    checkProgressSeq1();
  }

private void Scene00011() //SEQ_1: ENEMY6, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00011: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_1: ENEMY7, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00012: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00013: NpcTrade(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00014();
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00014() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea108:65941 calling Scene00014: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LYULF" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
