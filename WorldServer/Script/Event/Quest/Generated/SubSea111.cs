// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65944)]
public class SubSea111 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 6 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002232
  //ACTOR1 = 1002234
  //ACTOR2 = 1002235
  //EOBJECT0 = 2000760
  //EOBJECT1 = 2000764
  //EOBJECT2 = 2000765
  //EOBJECT3 = 2000761
  //EVENTACTIONPROCESSSHOR = 15
  //SEQ0ACTOR0 = 0
  //SEQ1ACTOR1 = 1
  //SEQ1ACTOR2 = 2
  //SEQ1EOBJECT0 = 3
  //SEQ1EOBJECT0EVENTACTIONNO = 99
  //SEQ1EOBJECT0EVENTACTIONOK = 100
  //SEQ1EOBJECT1 = 4
  //SEQ1EOBJECT1EVENTACTIONNO = 97
  //SEQ1EOBJECT1EVENTACTIONOK = 98
  //SEQ1EOBJECT2 = 5
  //SEQ1EOBJECT2EVENTACTIONNO = 95
  //SEQ1EOBJECT2EVENTACTIONOK = 96
  //SEQ1EOBJECT3 = 6
  //SEQ1EOBJECT3EVENTACTIONNO = 93
  //SEQ1EOBJECT3EVENTACTIONOK = 94
  //SEQ2ACTOR0 = 7

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=YULGIHONALGI
        break;
      }
      case 1:
      {
        if( param1 == 1002234 ) // ACTOR1 = RAZYNMOLZYN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=RAZYNMOLZYN
          }
          break;
        }
        if( param1 == 1002235 ) // ACTOR2 = RUIMOFALAIMO
        {
          if( quest.UI8BH != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=RUIMOFALAIMO
          }
          break;
        }
        if( param1 == 2000760 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00099(); // Scene00099: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000764 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00097(); // Scene00097: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000765 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00095(); // Scene00095: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2000761 ) // EOBJECT3 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00093(); // Scene00093: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YULGIHONALGI
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
        if( quest.UI8BL == 3 )
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
            quest.setBitFlag8( 5, false );
            quest.setBitFlag8( 6, false );
            quest.Sequence = 255;
          }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea111:65944 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=YULGIHONALGI" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubSea111:65944 calling Scene00001: Normal(Talk, TargetCanMove), id=RAZYNMOLZYN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubSea111:65944 calling Scene00002: Normal(Talk, TargetCanMove), id=RUIMOFALAIMO" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00099() //SEQ_1: EOBJECT0, UI8BL = 3, Flag8(3)=True
  {
    player.sendDebug("SubSea111:65944 calling Scene00099: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( quest.UI8BL + 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq1();
  }

private void Scene00097() //SEQ_1: EOBJECT1, UI8BL = 3, Flag8(4)=True
  {
    player.sendDebug("SubSea111:65944 calling Scene00097: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( quest.UI8BL + 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq1();
  }

private void Scene00095() //SEQ_1: EOBJECT2, UI8BL = 3, Flag8(5)=True
  {
    player.sendDebug("SubSea111:65944 calling Scene00095: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( quest.UI8BL + 1);
    quest.setBitFlag8( 5, true );
    checkProgressSeq1();
  }

private void Scene00093() //SEQ_1: EOBJECT3, UI8CH = 1, Flag8(6)=True
  {
    player.sendDebug("SubSea111:65944 calling Scene00093: Empty(None), id=unknown" );
    quest.UI8CH =  (byte)( 1);
    quest.setBitFlag8( 6, true );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea111:65944 calling Scene00007: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YULGIHONALGI" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
