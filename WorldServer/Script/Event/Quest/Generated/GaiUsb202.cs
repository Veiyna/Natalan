// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66369)]
public class GaiUsb202 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_2, 8 entries
  //SEQ_255, 6 entries

  //ACTOR0 = 1006269
  //ENEMY0 = 4293253
  //ENEMY1 = 4293254
  //EOBJECT0 = 2002050
  //EOBJECT1 = 2002291
  //EOBJECT2 = 2002292
  //EOBJECT3 = 2002293
  //EOBJECT4 = 2002294
  //EOBJECT5 = 2002051
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONWAITINGSHOR = 11
  //ITEM0 = 2000650

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=KUZAITAZAI
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( param1 == 2002050 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00002(); // Scene00002: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002291 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00004(); // Scene00004: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002292 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00006(); // Scene00006: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002293 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00008(); // Scene00008: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002294 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00010(); // Scene00010: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 2002051 ) // EOBJECT5 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00013(); // Scene00013: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT5
        if( param1 == 4293253 ) // ENEMY0 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        if( param1 == 4293254 ) // ENEMY1 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002050 ) // EOBJECT0 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002291 ) // EOBJECT1 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002292 ) // EOBJECT2 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002293 ) // EOBJECT3 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002294 ) // EOBJECT4 = unknown
        {
          Scene00023(); // Scene00023: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006269 ) // ACTOR0 = KUZAITAZAI
        {
          Scene00024(); // Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KUZAITAZAI
          break;
        }
        if( param1 == 2002050 ) // EOBJECT0 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002291 ) // EOBJECT1 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002292 ) // EOBJECT2 = unknown
        {
          Scene00030(); // Scene00030: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002293 ) // EOBJECT3 = unknown
        {
          Scene00032(); // Scene00032: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002294 ) // EOBJECT4 = unknown
        {
          Scene00034(); // Scene00034: Empty(None), id=unknown
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
    quest.UI8BH = 1;
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
      quest.UI8BH = 0;
      quest.Sequence = 2;
    }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 2 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb202:66369 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=KUZAITAZAI" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EOBJECT0, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00002: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: EOBJECT1, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00004: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00005() //SEQ_1: EOBJECT1, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT2, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00006: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00007() //SEQ_1: EOBJECT2, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: EOBJECT3, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00008: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00009() //SEQ_1: EOBJECT3, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00010() //SEQ_1: EOBJECT4, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00010: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00011() //SEQ_1: EOBJECT4, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 5, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00013() //SEQ_2: EOBJECT5, UI8AL = 2, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00013: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 2);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 2 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_2: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00014: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00015() //SEQ_2: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00015: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00017() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00017: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00019() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00019: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00021() //SEQ_2: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00021: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00022() //SEQ_2: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00022: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00023() //SEQ_2: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00023: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00024() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=KUZAITAZAI" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00026() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00026: Empty(None), id=unknown" );
  }

private void Scene00028() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00028: Empty(None), id=unknown" );
  }

private void Scene00030() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00030: Empty(None), id=unknown" );
  }

private void Scene00032() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00032: Empty(None), id=unknown" );
  }

private void Scene00034() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb202:66369 calling Scene00034: Empty(None), id=unknown" );
  }
};
}
