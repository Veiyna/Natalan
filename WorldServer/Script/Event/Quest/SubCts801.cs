// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66746)]
public class SubCts801 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 2 entries
  //SEQ_3, 5 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1003601
  //ACTOR1 = 1005516
  //ACTOR2 = 1005515
  //ACTOR3 = 1001000
  //ACTOR4 = 1000153
  //ACTOR5 = 1002299
  //CUTSCENE01 = 449
  //ITEM0 = 2001110
  //ITEM1 = 2001111
  //ITEM2 = 2001112
  //LOGBEAUTYSALONUNLOCK = 3705
  //UNLOCKIMAGEHAIRCUT = 138

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SDHODJBI
        break;
      }
      case 1:
      {
        if( param1 == 1005516 ) // ACTOR1 = GOFUJINN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=GOFUJINN
          }
          break;
        }
        if( param1 == 1005515 ) // ACTOR2 = JANDELAINE
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=JANDELAINE
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1005515 ) // ACTOR2 = JANDELAINE
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=JANDELAINE
            // +Callback Scene00005: Normal(Talk, TargetCanMove), id=JANDELAINE
            // +Callback Scene00006: Normal(Talk, TargetCanMove), id=JANDELAINE
          }
          break;
        }
        if( param1 == 1005516 ) // ACTOR1 = GOFUJINN
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=GOFUJINN
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8CH max stack 1
      //seq 3 event item ITEM1 = UI8CL max stack 1
      //seq 3 event item ITEM2 = UI8DH max stack 1
      case 3:
      {
        if( param1 == 1001000 ) // ACTOR3 = HNAANZA
        {
          if( quest.UI8BL != 1 )
          {
            Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=HNAANZA
          }
          break;
        }
        if( param1 == 1000153 ) // ACTOR4 = BEATINE
        {
          if( quest.UI8AL != 1 )
          {
            Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=BEATINE
          }
          break;
        }
        if( param1 == 1002299 ) // ACTOR5 = SEVERIAN
        {
          if( quest.UI8BH != 1 )
          {
            Scene00010(); // Scene00010: Normal(Talk, TargetCanMove), id=SEVERIAN
          }
          break;
        }
        if( param1 == 1005515 ) // ACTOR2 = JANDELAINE
        {
          Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=JANDELAINE
          break;
        }
        if( param1 == 1005516 ) // ACTOR1 = GOFUJINN
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=GOFUJINN
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      //seq 255 event item ITEM1 = UI8BL max stack 1
      //seq 255 event item ITEM2 = UI8CH max stack 1
      case 255:
      {
        if( param1 == 1005515 ) // ACTOR2 = unknown
        {
          Scene00013(); // Scene00013: NpcTrade(Talk), id=unknown
          // +Callback Scene00014: Normal(CutScene, Message, FadeIn, QuestReward, QuestComplete), id=unknown
          break;
        }
        if( param1 == 1005516 ) // ACTOR1 = GOFUJINN
        {
          Scene00015(); // Scene00015: Normal(Talk, TargetCanMove), id=GOFUJINN
          break;
        }
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
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 2;
    }
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
    if( quest.UI8BL == 1 )
      if( quest.UI8AL == 1 )
        if( quest.UI8BH == 1 )
        {
          quest.UI8BL = 0 ;
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.UI8CH = 0;
          quest.UI8CL = 0;
          quest.UI8DH = 0;
          quest.Sequence = 255;
          quest.UI8BH = 1;
          quest.UI8BL = 1;
          quest.UI8CH = 1;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("SubCts801:66746 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SDHODJBI" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubCts801:66746 calling Scene00002: Normal(Talk, TargetCanMove), id=GOFUJINN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00003: Normal(Talk, TargetCanMove), id=JANDELAINE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubCts801:66746 calling Scene00004: Normal(Talk, TargetCanMove), id=JANDELAINE" );
    var callback = (SceneResult result) =>
    {
      Scene00005();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubCts801:66746 calling Scene00005: Normal(Talk, TargetCanMove), id=JANDELAINE" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True
  {
    player.sendDebug("SubCts801:66746 calling Scene00006: Normal(Talk, TargetCanMove), id=JANDELAINE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00007: Normal(Talk, TargetCanMove), id=GOFUJINN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: ACTOR3, UI8BL = 1, Flag8(1)=True
  {
    player.sendDebug("SubCts801:66746 calling Scene00008: Normal(Talk, TargetCanMove), id=HNAANZA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_3: ACTOR4, UI8AL = 1, Flag8(2)=True
  {
    player.sendDebug("SubCts801:66746 calling Scene00009: Normal(Talk, TargetCanMove), id=BEATINE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_3: ACTOR5, UI8BH = 1, Flag8(3)=True
  {
    player.sendDebug("SubCts801:66746 calling Scene00010: Normal(Talk, TargetCanMove), id=SEVERIAN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00011: Normal(Talk, TargetCanMove), id=JANDELAINE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_3: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00012: Normal(Talk, TargetCanMove), id=GOFUJINN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00013: NpcTrade(Talk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00014();
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00014() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00014: Normal(CutScene, Message, FadeIn, QuestReward, QuestComplete), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.SetMasterUnlock((ushort)UnlockEntry.Aesthetician, true);
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 14, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00015() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubCts801:66746 calling Scene00015: Normal(Talk, TargetCanMove), id=GOFUJINN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
