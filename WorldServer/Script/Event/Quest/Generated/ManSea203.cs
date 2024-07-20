// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65781)]
public class ManSea203 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 2 entries
  //SEQ_3, 9 entries
  //SEQ_4, 7 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000972
  //ACTOR1 = 1003621
  //ACTOR10 = 5010000
  //ACTOR11 = 6229214
  //ACTOR2 = 1017075
  //ACTOR3 = 1004472
  //ACTOR4 = 1004473
  //ACTOR5 = 1004474
  //ACTOR6 = 1004475
  //ACTOR7 = 1004476
  //ACTOR8 = 1004477
  //ACTOR9 = 1004478
  //COLLECTPARTYUNLOCK = 3704
  //CONTENTSTUTORIALSDS01 = 2
  //INSTANCEDUNGEON0 = 4
  //LOCACTOR0 = 1004471
  //LOCACTOR1 = 1004479
  //LOCACTOR2 = 1004480
  //LOCACTOR3 = 1004481
  //QSTACCEPTCHECK = 66211
  //SCREENIMAGE1 = 1098
  //SCREENIMAGE2 = 1097
  //SEQ0ACTOR0LQ = 90
  //UNLOCKADDNEWCONTENTTOCF = 3702
  //UNLOCKIMAGEDUNGEONSASTASHA = 74
  //UNLOCKLFGPARTY = 137

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
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=BADERON
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=VMELLPA
        break;
      }
      case 2:
      {
        if( param1 == 1017075 ) // ACTOR2 = AIDE00675
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove, ENpcBind), id=AIDE00675
          }
          break;
        }
        if( param1 == 1003621 ) // ACTOR1 = VMELLPA
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=VMELLPA
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1004472 ) // ACTOR3 = YELLOWJACKET
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Normal(Talk, TargetCanMove, CanCancel), id=YELLOWJACKET
          }
          break;
        }
        if( param1 == 1017075 ) // ACTOR2 = AIDE00675
        {
          Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=AIDE00675
          break;
        }
        if( param1 == 1004473 ) // ACTOR4 = AVERE
        {
          Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=AVERE
          break;
        }
        if( param1 == 1004474 ) // ACTOR5 = LIAVINNE
        {
          Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=LIAVINNE
          break;
        }
        if( param1 == 1004475 ) // ACTOR6 = PAIYOREIYO
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=PAIYOREIYO
          break;
        }
        if( param1 == 1004476 ) // ACTOR7 = EDDA
        {
          Scene00010(); // Scene00010: Normal(Talk, TargetCanMove), id=EDDA
          break;
        }
        if( param1 == 1004477 ) // ACTOR8 = ISILDAURE
        {
          Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=ISILDAURE
          break;
        }
        if( param1 == 1004478 ) // ACTOR9 = ALIANNE
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=ALIANNE
          break;
        }
        if( param1 == 1003621 ) // ACTOR1 = VMELLPA
        {
          Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=VMELLPA
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1004473 ) // ACTOR4 = AVERE
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=AVERE
          break;
        }
        if( param1 == 1004474 ) // ACTOR5 = LIAVINNE
        {
          Scene00015(); // Scene00015: Normal(Talk, TargetCanMove), id=LIAVINNE
          break;
        }
        if( param1 == 1004475 ) // ACTOR6 = PAIYOREIYO
        {
          Scene00016(); // Scene00016: Normal(Talk, TargetCanMove), id=PAIYOREIYO
          break;
        }
        if( param1 == 1004476 ) // ACTOR7 = EDDA
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=EDDA
          break;
        }
        if( param1 == 1004477 ) // ACTOR8 = ISILDAURE
        {
          Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=ISILDAURE
          break;
        }
        if( param1 == 1004478 ) // ACTOR9 = ALIANNE
        {
          Scene00019(); // Scene00019: Normal(Talk, TargetCanMove), id=ALIANNE
          break;
        }
        if( param1 == 1004472 ) // ACTOR3 = YELLOWJACKET
        {
          Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=YELLOWJACKET
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00021(); // Scene00021: Normal(Talk, Message, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=BADERON
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
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00000: Normal(QuestOffer, TargetCanMove, SystemTalk, CanCancel), id=unknown" );
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
    player.sendDebug("ManSea203:65781 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=BADERON" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("ManSea203:65781 calling Scene00002: Normal(Talk, TargetCanMove), id=VMELLPA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("ManSea203:65781 calling Scene00003: Normal(Talk, TargetCanMove, ENpcBind), id=AIDE00675" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00004: Normal(Talk, TargetCanMove), id=VMELLPA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_3: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("ManSea203:65781 calling Scene00005: Normal(Talk, TargetCanMove, CanCancel), id=YELLOWJACKET" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults == 1 || ( result.errorCode == 0 && result.numOfResults == 2 ) )
      {
        quest.UI8AL =  (byte)( 1);
        quest.setBitFlag8( 1, true );
        player.SendQuestMessage(Id, 2, 0, 0, 0 );
        checkProgressSeq3();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00006: Normal(Talk, TargetCanMove), id=AIDE00675" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_3: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00007: Normal(Talk, TargetCanMove), id=AVERE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_3: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00008: Normal(Talk, TargetCanMove), id=LIAVINNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_3: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00009: Normal(Talk, TargetCanMove), id=PAIYOREIYO" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_3: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00010: Normal(Talk, TargetCanMove), id=EDDA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00011: Normal(Talk, TargetCanMove), id=ISILDAURE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_3: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00012: Normal(Talk, TargetCanMove), id=ALIANNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_3: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00013: Normal(Talk, TargetCanMove), id=VMELLPA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_4: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00014: Normal(Talk, TargetCanMove), id=AVERE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_4: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00015: Normal(Talk, TargetCanMove), id=LIAVINNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_4: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00016: Normal(Talk, TargetCanMove), id=PAIYOREIYO" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_4: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00017: Normal(Talk, TargetCanMove), id=EDDA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_4: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00018: Normal(Talk, TargetCanMove), id=ISILDAURE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_4: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00019: Normal(Talk, TargetCanMove), id=ALIANNE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_4: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00020: Normal(Talk, TargetCanMove), id=YELLOWJACKET" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea203:65781 calling Scene00021: Normal(Talk, Message, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=BADERON" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 21, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
