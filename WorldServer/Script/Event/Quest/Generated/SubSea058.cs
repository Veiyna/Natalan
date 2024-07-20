// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66006)]
public class SubSea058 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 9 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002119
  //ENEMY0 = 3929271
  //ENEMY1 = 3929272
  //ENEMY2 = 3929274
  //ENEMY3 = 3929275
  //ENEMY4 = 3929277
  //ENEMY5 = 3929278
  //EOBJECT0 = 2001240
  //EOBJECT1 = 2001241
  //EOBJECT2 = 2001242
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONSEARCHSHORT = 2

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=OSTFYR
        break;
      }
      case 1:
      {
        if( param1 == 2001240 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00001(); // Scene00001: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 3929271 ) // ENEMY0 = unknown
        {
          Scene00002(); // Scene00002: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3929272 ) // ENEMY1 = unknown
        {
          Scene00003(); // Scene00003: Normal(None), id=unknown
          break;
        }
        if( param1 == 2001241 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT1
        if( param1 == 3929274 ) // ENEMY2 = unknown
        {
          Scene00005(); // Scene00005: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3929275 ) // ENEMY3 = unknown
        {
          Scene00006(); // Scene00006: Normal(None), id=unknown
          break;
        }
        if( param1 == 2001242 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT2
        if( param1 == 3929277 ) // ENEMY4 = unknown
        {
          Scene00008(); // Scene00008: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3929278 ) // ENEMY5 = unknown
        {
          Scene00009(); // Scene00009: Normal(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00010(); // Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=OSTFYR
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
    onProgress(EVENT_ON_WITHIN_RANGE, 0, 0, 0 );
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
    if( quest.UI8AL == 2 )
      if( quest.UI8BH == 2 )
        if( quest.UI8BL == 2 )
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
    player.sendDebug("SubSea058:66006 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=OSTFYR" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: EOBJECT0, UI8AL = 2, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubSea058:66006 calling Scene00001: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 2);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 2 );
    checkProgressSeq1();
  }

private void Scene00002() //SEQ_1: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea058:66006 calling Scene00002: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea058:66006 calling Scene00003: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: EOBJECT1, UI8BH = 2, Flag8(2)=True
  {
    player.sendDebug("SubSea058:66006 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 2);
    quest.setBitFlag8( 2, true );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_1: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea058:66006 calling Scene00005: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_1: ENEMY3, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea058:66006 calling Scene00006: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: EOBJECT2, UI8BL = 2, Flag8(3)=True
  {
    player.sendDebug("SubSea058:66006 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 2);
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: ENEMY4, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea058:66006 calling Scene00008: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_1: ENEMY5, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea058:66006 calling Scene00009: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea058:66006 calling Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=OSTFYR" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
