// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65645)]
public class ManSea003 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002697
  //ACTOR1 = 1001217
  //ACTOR2 = 1000895
  //ACTOR3 = 1000972
  //AETHERYTE0 = 8
  //BINDACTOR0 = 6229226
  //ITEM0 = 2000105
  //LOCACTOR1 = 1001023
  //LOCFACE0 = 604
  //LOCFACE1 = 605
  //LOCPOSCAM1 = 4106696
  //LOCPOSCAM2 = 4106698
  //REWARD0 = 1
  //SCREENIMAGE0 = 14
  //UNLOCKDESION = 14

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
      //seq 0 event item ITEM0 = UI8BH max stack 1
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, SystemTalk, ENpcBind), id=BADERON
        break;
      }
      //seq 1 event item ITEM0 = UI8CH max stack 1
      case 1:
      {
        if( param1 == 8 ) // AETHERYTE0 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00002(); // Scene00002: Normal(None), id=unknown
            // +Callback Scene00003: Normal(Talk, FadeIn, SystemTalk), id=unknown
          }
          break;
        }
        if( param1 == 1001217 ) // ACTOR1 = SWOZBLAET
        {
          if( quest.UI8BH != 1 )
          {
            Scene00004(); // Scene00004: NpcTrade(Talk, TargetCanMove), id=SWOZBLAET
            // +Callback Scene00005: Normal(Talk, FadeIn, TargetCanMove), id=SWOZBLAET
          }
          break;
        }
        if( param1 == 1000895 ) // ACTOR2 = MURIE
        {
          if( quest.UI8AL != 1 )
          {
            Scene00006(); // Scene00006: Normal(Talk, TargetCanMove), id=MURIE
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=BADERON
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
    onProgress(EVENT_ON_WITHIN_RANGE, 0, 0, 0 );
  }

  public override void OnEventTerritory()
  {
    onProgress(EVENT_ON_ENTER_TERRITORY, 0, 0, 0 );
  }
  void checkProgressSeq0()
  {
    quest.Sequence = 1;
    quest.UI8CH = 1;
  }
  void checkProgressSeq1()
  {
    if( quest.UI8BL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8AL == 1 )
        {
          quest.UI8BL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8AL = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.UI8CH = 0;
          quest.Sequence = 255;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea003:65645 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("ManSea003:65645 calling Scene00001: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, SystemTalk, ENpcBind), id=BADERON" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: AETHERYTE0, UI8BL = 1, Flag8(1)=True
  {
    player.sendDebug("ManSea003:65645 calling Scene00002: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: AETHERYTE0, UI8BL = 1, Flag8(1)=True
  {
    player.sendDebug("ManSea003:65645 calling Scene00003: Normal(Talk, FadeIn, SystemTalk), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(0x050002, 0, 1);
      player.SetAetheryte(8, true);
      player.SetMasterUnlock(1, true);
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("ManSea003:65645 calling Scene00004: NpcTrade(Talk, TargetCanMove), id=SWOZBLAET" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00005();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_1: ACTOR1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("ManSea003:65645 calling Scene00005: Normal(Talk, FadeIn, TargetCanMove), id=SWOZBLAET" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00006() //SEQ_1: ACTOR2, UI8AL = 1, Flag8(3)=True
  {
    player.sendDebug("ManSea003:65645 calling Scene00006: Normal(Talk, TargetCanMove), id=MURIE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea003:65645 calling Scene00007: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=BADERON" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
