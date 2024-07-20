// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65945)]
public class SubSea112 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002233
  //ACTOR1 = 1002453
  //ACTOR2 = 1002455
  //EOBJECT0 = 2000762
  //EOBJECT1 = 2000763
  //EVENTACTIONGATHERSHORT = 6
  //ITEM0 = 2000218
  //SEQ0ACTOR0 = 0
  //SEQ1ACTOR1 = 1
  //SEQ1ACTOR2 = 2
  //SEQ1EOBJECT0 = 3
  //SEQ1EOBJECT0EVENTACTIONNO = 99
  //SEQ1EOBJECT0EVENTACTIONOK = 100
  //SEQ1EOBJECT1 = 4
  //SEQ1EOBJECT1EVENTACTIONNO = 97
  //SEQ1EOBJECT1EVENTACTIONOK = 98
  //SEQ2ACTOR0 = 5
  //SEQ2ACTOR0NPCTRADENO = 95
  //SEQ2ACTOR0NPCTRADEOK = 96

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=WAFUFU
        break;
      }
      //seq 1 event item ITEM0 = UI8CL max stack 4
      case 1:
      {
        if( param1 == 1002453 ) // ACTOR1 = LYNGSTYRM
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=LYNGSTYRM
          }
          break;
        }
        if( param1 == 1002455 ) // ACTOR2 = MAETIMYND
        {
          if( quest.UI8BH != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=MAETIMYND
          }
          break;
        }
        if( param1 == 2000762 ) // EOBJECT0 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00099(); // Scene00099: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000763 ) // EOBJECT1 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00097(); // Scene00097: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 4
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00005(); // Scene00005: NpcTrade(Talk, TargetCanMove), id=WAFUFU
        // +Callback Scene00096: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=WAFUFU
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
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
          if( quest.UI8CH == 1 )
          {
            quest.UI8AL = 0 ;
            quest.UI8BH = 0 ;
            quest.UI8BL = 0 ;
            quest.UI8CH = 0 ;
            quest.setBitFlag8( 1, false );
            quest.setBitFlag8( 2, false );
            quest.setBitFlag8( 3, false );
            quest.setBitFlag8( 4, false );
            quest.UI8CL = 0;
            quest.Sequence = 255;
            quest.UI8BH = 4;
          }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea112:65945 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=WAFUFU" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubSea112:65945 calling Scene00001: Normal(Talk, TargetCanMove), id=LYNGSTYRM" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubSea112:65945 calling Scene00002: Normal(Talk, TargetCanMove), id=MAETIMYND" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00099() //SEQ_1: EOBJECT0, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubSea112:65945 calling Scene00099: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00097() //SEQ_1: EOBJECT1, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("SubSea112:65945 calling Scene00097: Empty(None), id=unknown" );
    quest.UI8CH =  (byte)( 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea112:65945 calling Scene00005: NpcTrade(Talk, TargetCanMove), id=WAFUFU" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00096();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00096() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea112:65945 calling Scene00096: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=WAFUFU" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 96, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
