// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66295)]
public class GaiUsa507 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 1 entries
  //SEQ_4, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006211
  //ACTOR1 = 1006216
  //ACTOR2 = 1006212
  //ACTOR3 = 1006213
  //ACTOR4 = 1006214
  //ENEMY0 = 176
  //ITEM0 = 2000605
  //ITEM1 = 2000855

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HIHIRA
        break;
      }
      case 1:
      {
        // empty entry
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack 5
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=ALFAN
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 5
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00004: Normal(Talk, TargetCanMove), id=HIHIRA
        break;
      }
      //seq 4 event item ITEM1 = UI8BH max stack 1
      case 4:
      {
        if( param1 == 1006212 ) // ACTOR2 = BEROLD
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00005(); // Scene00005: NpcTrade(Talk), id=unknown
            // +Callback Scene00006: Normal(Talk, TargetCanMove), id=BEROLD
          }
          break;
        }
        if( param1 == 1006213 ) // ACTOR3 = OTELIN
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00007(); // Scene00007: NpcTrade(Talk), id=unknown
            // +Callback Scene00008: Normal(Talk, TargetCanMove), id=OTELIN
          }
          break;
        }
        if( param1 == 1006214 ) // ACTOR4 = ARCAVIUS
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00009(); // Scene00009: NpcTrade(Talk), id=unknown
            // +Callback Scene00010: Normal(Talk, TargetCanMove), id=ARCAVIUS
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00011(); // Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HIHIRA
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
    quest.Sequence = 2;
  }
  void checkProgressSeq1()
  {
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 3;
    quest.UI8BH = 5;
  }
  void checkProgressSeq3()
  {
    quest.UI8BH = 0;
    quest.Sequence = 4;
    quest.UI8BH = 1;
  }
  void checkProgressSeq4()
  {
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
      quest.UI8BH = 0;
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa507:66295 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HIHIRA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }


private void Scene00002() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00002: Normal(Talk, TargetCanMove), id=ALFAN" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00003: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00004();
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00004: Normal(Talk, TargetCanMove), id=HIHIRA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_4: ACTOR2, UI8AL = 3, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00005: NpcTrade(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00006();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_4: ACTOR2, UI8AL = 3, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00006: Normal(Talk, TargetCanMove), id=BEROLD" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 2, quest.UI8AL, 3 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_4: ACTOR3, UI8AL = 3, Flag8(2)=True(Todo:3)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00007: NpcTrade(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00008();
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_4: ACTOR3, UI8AL = 3, Flag8(2)=True(Todo:3)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00008: Normal(Talk, TargetCanMove), id=OTELIN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 3, 2, quest.UI8AL, 3 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_4: ACTOR4, UI8AL = 3, Flag8(3)=True(Todo:3)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00009: NpcTrade(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00010();
      }
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00010() //SEQ_4: ACTOR4, UI8AL = 3, Flag8(3)=True(Todo:3)
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00010: Normal(Talk, TargetCanMove), id=ARCAVIUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 3, 2, quest.UI8AL, 3 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa507:66295 calling Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HIHIRA" );
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
