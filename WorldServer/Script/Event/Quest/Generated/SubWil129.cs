// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66110)]
public class SubWil129 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 7 entries
  //SEQ_2, 1 entries
  //SEQ_3, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1003929
  //ACTOR1 = 1003947
  //ACTOR2 = 1004325
  //ACTOR3 = 1004327
  //ACTOR4 = 1004506
  //ACTOR5 = 1004556
  //ACTOR6 = 1004324
  //ACTOR7 = 1004330
  //ACTOR8 = 1005116
  //LOCACTOR0 = 1005015
  //LOCACTOR1 = 1004580
  //LOCACTOR2 = 1004581
  //LOCACTOR3 = 1003932
  //LOCFACE0 = 604
  //LOCFACE1 = 617
  //LOCFACE2 = 605
  //LOCPOSACTOR1 = 3967322
  //RITEM0 = 2995
  //RITEM1 = 3306
  //SEQ0ACTOR0LQ = 90

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=ISEMBARD
        break;
      }
      case 1:
      {
        if( param1 == 1003947 ) // ACTOR1 = KNERL
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=KNERL
            // +Callback Scene00003: Normal(Talk, TargetCanMove), id=KNERL
            // +Callback Scene00004: Normal(Talk, TargetCanMove), id=KNERL
          }
          break;
        }
        if( param1 == 1004325 ) // ACTOR2 = SWAENHYLT
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=SWAENHYLT
            // +Callback Scene00006: Normal(Talk, TargetCanMove), id=SWAENHYLT
            // +Callback Scene00007: Normal(Talk, TargetCanMove), id=SWAENHYLT
          }
          break;
        }
        if( param1 == 1004327 ) // ACTOR3 = AURILDIS
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=AURILDIS
            // +Callback Scene00009: Normal(Talk, TargetCanMove), id=AURILDIS
            // +Callback Scene00010: Normal(Talk, TargetCanMove), id=AURILDIS
          }
          break;
        }
        if( param1 == 1004506 ) // ACTOR4 = ERMEGARDE
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=ERMEGARDE
            // +Callback Scene00012: Normal(Talk, TargetCanMove), id=ERMEGARDE
            // +Callback Scene00013: Normal(Talk, TargetCanMove), id=ERMEGARDE
          }
          break;
        }
        if( param1 == 1004556 ) // ACTOR5 = ADELSTAN
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=ADELSTAN
            // +Callback Scene00015: Normal(Talk, TargetCanMove), id=ADELSTAN
            // +Callback Scene00016: Normal(Talk, TargetCanMove), id=ADELSTAN
          }
          break;
        }
        if( param1 == 1004324 ) // ACTOR6 = THANCRED
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=THANCRED
          break;
        }
        if( param1 == 1003929 ) // ACTOR0 = ISEMBARD
        {
          Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=ISEMBARD
          break;
        }
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00019(); // Scene00019: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED
        break;
      }
      case 3:
      {
        if( param1 == 1004330 ) // ACTOR7 = THANCRED
        {
          if( quest.UI8AL != 1 )
          {
            Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=THANCRED
            // +Callback Scene00021: Normal(Talk, FadeIn, TargetCanMove, CreateCharacterTalk), id=THANCRED
            // +Callback Scene00022: Normal(Talk, TargetCanMove), id=THANCRED
          }
          break;
        }
        if( param1 == 1003929 ) // ACTOR0 = ISEMBARD
        {
          Scene00023(); // Scene00023: Normal(Talk, TargetCanMove), id=ISEMBARD
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00024(); // Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=unknown
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
    quest.Sequence = 3;
  }
  void checkProgressSeq3()
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
    player.sendDebug("SubWil129:66110 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("SubWil129:66110 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
      player.Inventory.NewItem(2995);
      player.Inventory.NewItem(3306);
      
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00002: Normal(Talk, TargetCanMove), id=KNERL" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00003: Normal(Talk, TargetCanMove), id=KNERL" );
    var callback = (SceneResult result) =>
    {
      Scene00004();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_1: ACTOR1, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00004: Normal(Talk, TargetCanMove), id=KNERL" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR2, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00005: Normal(Talk, TargetCanMove), id=SWAENHYLT" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_1: ACTOR2, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00006: Normal(Talk, TargetCanMove), id=SWAENHYLT" );
    var callback = (SceneResult result) =>
    {
      Scene00007();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_1: ACTOR2, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00007: Normal(Talk, TargetCanMove), id=SWAENHYLT" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: ACTOR3, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00008: Normal(Talk, TargetCanMove), id=AURILDIS" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_1: ACTOR3, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00009: Normal(Talk, TargetCanMove), id=AURILDIS" );
    var callback = (SceneResult result) =>
    {
      Scene00010();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00010() //SEQ_1: ACTOR3, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00010: Normal(Talk, TargetCanMove), id=AURILDIS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_1: ACTOR4, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00011: Normal(Talk, TargetCanMove), id=ERMEGARDE" );
    var callback = (SceneResult result) =>
    {
      Scene00012();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00012() //SEQ_1: ACTOR4, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00012: Normal(Talk, TargetCanMove), id=ERMEGARDE" );
    var callback = (SceneResult result) =>
    {
      Scene00013();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_1: ACTOR4, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00013: Normal(Talk, TargetCanMove), id=ERMEGARDE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 4, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_1: ACTOR5, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00014: Normal(Talk, TargetCanMove), id=ADELSTAN" );
    var callback = (SceneResult result) =>
    {
      Scene00015();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_1: ACTOR5, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00015: Normal(Talk, TargetCanMove), id=ADELSTAN" );
    var callback = (SceneResult result) =>
    {
      Scene00016();
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00016() //SEQ_1: ACTOR5, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("SubWil129:66110 calling Scene00016: Normal(Talk, TargetCanMove), id=ADELSTAN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 5, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_1: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil129:66110 calling Scene00017: Normal(Talk, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_1: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil129:66110 calling Scene00018: Normal(Talk, TargetCanMove), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("SubWil129:66110 calling Scene00019: Normal(Talk, NpcDespawn, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_3: ACTOR7, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubWil129:66110 calling Scene00020: Normal(Talk, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      Scene00021();
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00021() //SEQ_3: ACTOR7, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubWil129:66110 calling Scene00021: Normal(Talk, FadeIn, TargetCanMove, CreateCharacterTalk), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      Scene00022();
    };
    owner.Event.NewScene( Id, 21, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
private void Scene00022() //SEQ_3: ACTOR7, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubWil129:66110 calling Scene00022: Normal(Talk, TargetCanMove), id=THANCRED" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00023() //SEQ_3: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil129:66110 calling Scene00023: Normal(Talk, TargetCanMove), id=ISEMBARD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00024() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil129:66110 calling Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
