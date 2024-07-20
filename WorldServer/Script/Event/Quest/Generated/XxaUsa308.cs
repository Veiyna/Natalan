// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(69398)]
public class XxaUsa308 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 3 entries
  //SEQ_3, 2 entries
  //SEQ_4, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000590
  //ACTOR1 = 1006199
  //ACTOR2 = 1006201
  //ACTOR3 = 1006203
  //ENEMY0 = 4293402
  //EOBJECT0 = 2001954
  //EOBJECT1 = 2002271
  //QSTACCEPTCHECK = 66276
  //QUESTBATTLE0 = 13
  //TERRITORYTYPE0 = 232

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=BUSCARRON
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS
        break;
      }
      case 2:
      {
        if( param1 == 1006201 ) // ACTOR2 = LAURENTIUS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, Message, PopBNpc, TargetCanMove), id=LAURENTIUS
          }
          break;
        }
        // BNpcHack credit moved to ACTOR2
        if( param1 == 4293402 ) // ENEMY0 = unknown
        {
          Scene00004(); // Scene00004: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2001954 ) // EOBJECT0 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1006201 ) // ACTOR2 = LAURENTIUS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00006(); // Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS
          }
          break;
        }
        if( param1 == 2001954 ) // EOBJECT0 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1006203 ) // ACTOR3 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00008(); // Scene00008: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 2002271 ) // EOBJECT1 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001954 ) // EOBJECT0 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00011(); // Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BUSCARRON
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
    }
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 4;
    }
  }
  void checkProgressSeq4()
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
    player.sendDebug("XxaUsa308:69398 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("XxaUsa308:69398 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=BUSCARRON" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00003: Normal(Talk, Message, PopBNpc, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00004: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00005: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00006() //SEQ_3: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00008() //SEQ_4: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00008: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle(13);
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_4: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00010() //SEQ_4: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00011() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("XxaUsa308:69398 calling Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BUSCARRON" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
