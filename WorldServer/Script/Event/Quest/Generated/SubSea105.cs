// FFXIVTheMovie.ParserV3.11
// param used:
//SCENE_100 REMOVED!!
//SCENE_99 REMOVED!!
//SCENE_98 REMOVED!!
//SCENE_97 REMOVED!!
//SCENE_96 REMOVED!!
//SCENE_95 REMOVED!!
//SCENE_94 REMOVED!!
//SCENE_93 REMOVED!!
//SCENE_92 REMOVED!!
//SCENE_91 REMOVED!!
//SCENE_89 REMOVED!!
//SCENE_88 REMOVED!!
//SCENE_87 REMOVED!!
//SCENE_86 REMOVED!!
//SCENE_85 REMOVED!!
//SCENE_84 REMOVED!!
//SCENE_83 REMOVED!!
//SCENE_82 REMOVED!!
//SCENE_81 REMOVED!!
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65938)]
public class SubSea105 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 7 entries
  //SEQ_255, 5 entries

  //ACTOR0 = 1003239
  //ACTOR1 = 1003244
  //ENEMY0 = 3927183
  //ENEMY1 = 3927184
  //EOBJECT0 = 2001255
  //EOBJECT1 = 2001256
  //EOBJECT2 = 2001257
  //EOBJECT3 = 2001258
  //EOBJECT4 = 2001259
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000342
  //QSTACCEPTCHECK = 65934

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=WYRKRHIT
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 1
      case 1:
      {
        if( param1 == 2001255 ) // EOBJECT0 = unknown
        {
          Scene00001(); // Scene00001: Empty(None), id=unknown
          break;
        }
        if( param1 == 3927183 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 3927184 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 2001256 ) // EOBJECT1 = unknown
        {
          Scene00002(); // Scene00002: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001257 ) // EOBJECT2 = unknown
        {
          Scene00003(); // Scene00003: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001258 ) // EOBJECT3 = unknown
        {
          Scene00004(); // Scene00004: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001259 ) // EOBJECT4 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1003244 ) // ACTOR1 = LYULF
        {
          Scene00006(); // Scene00006: NpcTrade(Talk, TargetCanMove), id=LYULF
          // +Callback Scene00090: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LYULF
          break;
        }
        if( param1 == 2001256 ) // EOBJECT1 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001257 ) // EOBJECT2 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001258 ) // EOBJECT3 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001259 ) // EOBJECT4 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
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
    quest.setBitFlag8( 1, false );
    quest.Sequence = 255;
    quest.UI8BH = 1;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=WYRKRHIT" );
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
    player.sendDebug("SubSea105:65938 calling Scene00001: Empty(None), id=unknown" );
    quest.setBitFlag8( 1, true );
    checkProgressSeq1();
  }



private void Scene00002() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00002: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00003() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00003: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00004: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00005: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00006: NpcTrade(Talk, TargetCanMove), id=LYULF" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00090();
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00090() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00090: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=LYULF" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 90, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea105:65938 calling Scene00010: Empty(None), id=unknown" );
  }
};
}
