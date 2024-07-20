// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66395)]
public class GaiUsb403 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 4 entries
  //SEQ_3, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006325
  //ACTOR1 = 1006326
  //ACTOR2 = 1006327
  //ACTOR3 = 1006328
  //EOBJECT0 = 2002087
  //EOBJECT1 = 2002298
  //EOBJECT2 = 2002602
  //EOBJECT3 = 2002603
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONSEARCHLONG = 4
  //ITEM0 = 2000666
  //ITEM1 = 2000667

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
      //seq 0 event item ITEM0 = UI8BH max stack ?
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BLOEIDIN
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( type == EVENT_ON_TALK ) Scene00002(); // Scene00002: Normal(Inventory), id=unknown
        if( type == EVENT_ON_EVENT_ITEM ) Scene00003(); // Scene00003: Empty(None), id=unknown
        break;
      }
      //seq 2 event item ITEM1 = UI8BH max stack 1
      case 2:
      {
        if( param1 == 2002087 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002298 ) // EOBJECT1 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002602 ) // EOBJECT2 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002603 ) // EOBJECT3 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 3 event item ITEM1 = UI8CH max stack 1
      case 3:
      {
        if( param1 == 1006326 ) // ACTOR1 = SOLDIERA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00012(); // Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=SOLDIERA
          }
          break;
        }
        if( param1 == 1006327 ) // ACTOR2 = SOLDIERB
        {
          if( quest.UI8BH != 1 )
          {
            Scene00014(); // Scene00014: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00015: Normal(Talk, NpcDespawn, TargetCanMove), id=SOLDIERB
          }
          break;
        }
        if( param1 == 1006328 ) // ACTOR3 = SOLDIERC
        {
          if( quest.UI8BL != 1 )
          {
            Scene00016(); // Scene00016: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00017: Normal(Talk, NpcDespawn, TargetCanMove), id=SOLDIERC
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00018(); // Scene00018: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BLOEIDIN
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
    quest.UI8BH = 1;
  }
  void checkProgressSeq1()
  {
    quest.UI8BH = 0;
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.UI8BH = 0;
      quest.Sequence = 3;
      quest.UI8CH = 1;
    }
  }
  void checkProgressSeq3()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
        {
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.UI8CH = 0;
          quest.Sequence = 255;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb403:66395 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BLOEIDIN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00002: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00003: Empty(None), id=unknown" );
    player.SendQuestMessage(Id, 0, 0, 0, 0 );
    checkProgressSeq1();
  }

private void Scene00005() //SEQ_2: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_2: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00011() //SEQ_2: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00012: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00013();
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00013: Normal(Talk, NpcDespawn, TargetCanMove), id=SOLDIERA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_3: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00014: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00015();
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_3: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00015: Normal(Talk, NpcDespawn, TargetCanMove), id=SOLDIERB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00016: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00017();
      }
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00017() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00017: Normal(Talk, NpcDespawn, TargetCanMove), id=SOLDIERC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb403:66395 calling Scene00018: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BLOEIDIN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
