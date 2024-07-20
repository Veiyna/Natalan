// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66255)]
public class GaiUsa105 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 1 entries
  //SEQ_3, 7 entries
  //SEQ_255, 2 entries

  //ACTIONTIMELINEEVENTJOYBIG = 945
  //ACTIONTIMELINEEVENTTALKENTREAT = 951
  //ACTIONTIMELINEEVENTTROUBLE = 944
  //ACTOR0 = 1000580
  //ACTOR1 = 1000585
  //ACTOR2 = 1000277
  //ACTOR3 = 1000553
  //ACTOR4 = 1000545
  //ENEMY0 = 4278151
  //ENEMY1 = 4278153
  //ENEMY2 = 4278162
  //ENEMY3 = 4278167
  //EOBJECT0 = 2001918
  //EOBJECT1 = 2002280
  //EVENTRANGE0 = 4289786
  //EVENTACTIONGATHERMIDDLE = 7
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000578

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=KOMUXIO
        // +Callback Scene00001: Normal(QuestAccept), id=unknown
        break;
      }
      case 1:
      {
        if( param1 == 1000585 ) // ACTOR1 = PELIXIA
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=PELIXIA
          }
          break;
        }
        if( param1 == 1000277 ) // ACTOR2 = NATHAXIO
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=NATHAXIO
          }
          break;
        }
        if( param1 == 1000553 ) // ACTOR3 = MONNE
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=MONNE
          }
          break;
        }
        if( param1 == 1000545 ) // ACTOR4 = VICTOR
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=VICTOR
          }
          break;
        }
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=KOMUXIO
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      case 3:
      {
        if( param1 == 4289786 ) // EVENTRANGE0 = unknown
        {
          Scene00007(); // Scene00007: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2001918 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00008(); // Scene00008: Normal(Message), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4278151 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4278153 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4278162 ) // ENEMY2 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4278167 ) // ENEMY3 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 2002280 ) // EOBJECT1 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      case 255:
      {
        if( param1 == 1000580 ) // ACTOR0 = KOMUXIO
        {
          Scene00012(); // Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO
          break;
        }
        if( param1 == 2001918 ) // EOBJECT0 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
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
    if( quest.UI8AL == 4 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.setBitFlag8( 4, false );
      quest.Sequence = 2;
    }
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
      quest.Sequence = 255;
      quest.UI8BH = 1;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=KOMUXIO" );
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
    player.sendDebug("GaiUsa105:66255 calling Scene00001: Normal(QuestAccept), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 4, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00002: Normal(Talk, TargetCanMove), id=PELIXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, UI8AL = 4, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00003: Normal(Talk, TargetCanMove), id=NATHAXIO" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR3, UI8AL = 4, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00004: Normal(Talk, TargetCanMove), id=MONNE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR4, UI8AL = 4, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00005: Normal(Talk, TargetCanMove), id=VICTOR" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 4, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 4 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00006: Normal(Talk, TargetCanMove), id=KOMUXIO" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00007: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: EOBJECT0, UI8AL = 4, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00008: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 4);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 2, quest.UI8AL, 4 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }





private void Scene00011() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00012() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00013();
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KOMUXIO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa105:66255 calling Scene00015: Empty(None), id=unknown" );
  }
};
}
