// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65919)]
public class SubFst067 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 7 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1000494
  //ACTOR1 = 1002946
  //ENEMY0 = 3841338
  //ENEMY1 = 3841340
  //EOBJECT0 = 2001007
  //EOBJECT1 = 2001008
  //EOBJECT2 = 2001009
  //EOBJECT3 = 2001844
  //EVENTRANGE0 = 3841476
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000192

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=FINNEA
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=BALARR
        break;
      }
      //seq 2 event item ITEM0 = UI8BH max stack ?
      case 2:
      {
        if( param1 == 2001007 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00003(); // Scene00003: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001008 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00005(); // Scene00005: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001009 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            if( type == EVENT_ON_TALK ) Scene00006(); // Scene00006: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00007(); // Scene00007: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 3841476 ) // EVENTRANGE0 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          // +Callback Scene00009: Normal(Inventory), id=unknown
          break;
        }
        if( param1 == 3841338 ) // ENEMY0 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 3841340 ) // ENEMY1 = unknown
        {
          Scene00012(); // Scene00012: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 2001844 ) // EOBJECT3 = unknown
        {
          Scene00013(); // Scene00013: Normal(Message, PopBNpc), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1002946 ) // ACTOR1 = BALARR
        {
          Scene00015(); // Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BALARR
          break;
        }
        if( param1 == 2001007 ) // EOBJECT0 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001008 ) // EOBJECT1 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001009 ) // EOBJECT2 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
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
    quest.Sequence = 2;
    quest.UI8BH = 1;
  }
  void checkProgressSeq2()
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
    player.sendDebug("SubFst067:65919 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("SubFst067:65919 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=FINNEA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("SubFst067:65919 calling Scene00002: Normal(Talk, TargetCanMove), id=BALARR" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: EOBJECT0, UI8AL = 3, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubFst067:65919 calling Scene00003: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00004() //SEQ_2: EOBJECT0, UI8AL = 3, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubFst067:65919 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
    checkProgressSeq2();
  }

private void Scene00005() //SEQ_2: EOBJECT1, UI8AL = 3, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("SubFst067:65919 calling Scene00005: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 2, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
    checkProgressSeq2();
  }

private void Scene00006() //SEQ_2: EOBJECT2, UI8AL = 3, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubFst067:65919 calling Scene00006: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00007() //SEQ_2: EOBJECT2, UI8AL = 3, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubFst067:65919 calling Scene00007: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( quest.UI8AL + 1);
    quest.setBitFlag8( 3, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
    checkProgressSeq2();
  }

private void Scene00008() //SEQ_2: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00008: Empty(None), id=unknown" );
    Scene00009();
  }
private void Scene00009() //SEQ_2: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00009: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_2: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_2: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00012: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_2: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00013: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00015: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BALARR" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00018: Empty(None), id=unknown" );
  }

private void Scene00021() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00021: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubFst067:65919 calling Scene00024: Empty(None), id=unknown" );
  }
};
}
