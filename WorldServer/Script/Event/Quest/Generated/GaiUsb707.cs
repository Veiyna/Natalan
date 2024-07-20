// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66438)]
public class GaiUsb707 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_255, 6 entries

  //ACTOR0 = 1006402
  //EOBJECT0 = 2002126
  //EOBJECT1 = 2002127
  //EOBJECT2 = 2002128
  //EOBJECT3 = 2002129
  //EOBJECT4 = 2002130
  //EOBJECT5 = 2002561
  //EOBJECT6 = 2002562
  //EOBJECT7 = 2002563
  //EOBJECT8 = 2002564
  //EOBJECT9 = 2002565
  //ITEM0 = 2000706

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ARTHURIOUX
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( param1 == 2002126 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00001(); // Scene00001: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002127 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00002(); // Scene00002: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00003(); // Scene00003: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002128 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00004(); // Scene00004: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002129 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 4 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00006(); // Scene00006: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002130 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 5 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00008(); // Scene00008: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00010(); // Scene00010: Normal(Inventory), id=unknown
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006402 ) // ACTOR0 = ARTHURIOUX
        {
          Scene00012(); // Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ARTHURIOUX
          break;
        }
        if( param1 == 2002561 ) // EOBJECT5 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002562 ) // EOBJECT6 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002563 ) // EOBJECT7 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002564 ) // EOBJECT8 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002565 ) // EOBJECT9 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
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
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=ARTHURIOUX" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: EOBJECT0, UI8AL = 5, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00001: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00002() //SEQ_1: EOBJECT1, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00002: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00003() //SEQ_1: EOBJECT1, UI8AL = 5, Flag8(2)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00003: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00004() //SEQ_1: EOBJECT2, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00004: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00005() //SEQ_1: EOBJECT2, UI8AL = 5, Flag8(3)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT3, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00006: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00007() //SEQ_1: EOBJECT3, UI8AL = 5, Flag8(4)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 4, true );
    player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: EOBJECT4, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00008: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00010() //SEQ_1: EOBJECT4, UI8AL = 5, Flag8(5)=True(Todo:0)
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00010: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 5, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 5 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00012: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ARTHURIOUX" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_255: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00013: Empty(None), id=unknown" );
  }

private void Scene00014() //SEQ_255: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00014: Empty(None), id=unknown" );
  }

private void Scene00015() //SEQ_255: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00016() //SEQ_255: EOBJECT8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_255: EOBJECT9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb707:66438 calling Scene00017: Empty(None), id=unknown" );
  }
};
}
