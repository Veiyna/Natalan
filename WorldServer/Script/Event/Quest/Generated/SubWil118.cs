// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66163)]
public class SubWil118 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 5 entries
  //SEQ_255, 6 entries

  //ACTOR0 = 1003936
  //EOBJECT0 = 2001413
  //EOBJECT1 = 2001414
  //EOBJECT2 = 2001415
  //EOBJECT3 = 2001416
  //EOBJECT4 = 2001417
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000400

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=AIRELL
        break;
      }
      //seq 1 event item ITEM0 = UI8DH max stack ?
      case 1:
      {
        if( param1 == 2001413 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            if( type == EVENT_ON_TALK ) Scene00001(); // Scene00001: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00002(); // Scene00002: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001414 ) // EOBJECT1 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00003(); // Scene00003: Empty(None), id=unknown
            // +Callback Scene00004: Normal(Inventory), id=unknown
          }
          break;
        }
        if( param1 == 2001415 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00006(); // Scene00006: Empty(None), id=unknown
            // +Callback Scene00007: Normal(Inventory), id=unknown
          }
          break;
        }
        if( param1 == 2001416 ) // EOBJECT3 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
            // +Callback Scene00010: Normal(Inventory), id=unknown
          }
          break;
        }
        if( param1 == 2001417 ) // EOBJECT4 = unknown
        {
          if( quest.UI8CL != 1 )
          {
            Scene00012(); // Scene00012: Empty(None), id=unknown
            // +Callback Scene00013: Normal(Inventory), id=unknown
          }
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack ?
      case 255:
      {
        if( param1 == 1003936 ) // ACTOR0 = AIRELL
        {
          Scene00016(); // Scene00016: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AIRELL
          break;
        }
        if( param1 == 2001413 ) // EOBJECT0 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001414 ) // EOBJECT1 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001415 ) // EOBJECT2 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001416 ) // EOBJECT3 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001417 ) // EOBJECT4 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
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
    quest.UI8DH = 1;
  }
  void checkProgressSeq1()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
          if( quest.UI8CH == 1 )
            if( quest.UI8CL == 1 )
            {
              quest.UI8AL = 0 ;
              quest.UI8BH = 0 ;
              quest.UI8BL = 0 ;
              quest.UI8CH = 0 ;
              quest.UI8CL = 0 ;
              quest.setBitFlag8( 1, false );
              quest.setBitFlag8( 2, false );
              quest.setBitFlag8( 3, false );
              quest.setBitFlag8( 4, false );
              quest.setBitFlag8( 5, false );
              quest.UI8DH = 0;
              quest.Sequence = 255;
            }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubWil118:66163 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=AIRELL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil118:66163 calling Scene00001: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00002() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("SubWil118:66163 calling Scene00002: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 0, 0, 0, 0 );
    checkProgressSeq1();
  }

private void Scene00003() //SEQ_1: EOBJECT1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00003: Empty(None), id=unknown" );
    Scene00004();
  }
private void Scene00004() //SEQ_1: EOBJECT1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00004: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_1: EOBJECT2, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00006: Empty(None), id=unknown" );
    Scene00007();
  }
private void Scene00007() //SEQ_1: EOBJECT2, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00007: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_1: EOBJECT3, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00009: Empty(None), id=unknown" );
    Scene00010();
  }
private void Scene00010() //SEQ_1: EOBJECT3, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00010: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag8( 4, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_1: EOBJECT4, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00012: Empty(None), id=unknown" );
    Scene00013();
  }
private void Scene00013() //SEQ_1: EOBJECT4, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("SubWil118:66163 calling Scene00013: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CL =  (byte)( 1);
      quest.setBitFlag8( 5, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil118:66163 calling Scene00016: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AIRELL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil118:66163 calling Scene00018: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil118:66163 calling Scene00020: Empty(None), id=unknown" );
  }

private void Scene00022() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil118:66163 calling Scene00022: Empty(None), id=unknown" );
  }

private void Scene00024() //SEQ_255: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil118:66163 calling Scene00024: Empty(None), id=unknown" );
  }

private void Scene00026() //SEQ_255: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("SubWil118:66163 calling Scene00026: Empty(None), id=unknown" );
  }
};
}
