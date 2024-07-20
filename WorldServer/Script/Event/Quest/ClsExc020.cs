// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65847)]
public class ClsExc020 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 3 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000926
  //ACTOR1 = 1000927
  //ENEMY0 = 347
  //ENEMY1 = 338
  //ENEMY2 = 49
  //LOGMESSAGEMONSTERNOTEPAGEUNLOCK = 1015
  //UNLOCKIMAGEMONSTERNOTE = 32

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=BLAUTHOTA
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BLAUTHOTA
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=BLAUTHOTA
        break;
      }
      case 2:
      {
        if( param1 == 347 ) // ENEMY0 = unknown
        {
          if( quest.UI8AL != 3 )
          {
            quest.UI8AL =  (byte)( quest.UI8AL + 1);
            checkProgressSeq2();
          }
          break;
        }
        if( param1 == 338 ) // ENEMY1 = unknown
        {
          if( quest.UI8BH != 3 )
          {
            quest.UI8BH =  (byte)( quest.UI8BH + 1);
            checkProgressSeq2();
          }
          break;
        }
        if( param1 == 49 ) // ENEMY2 = unknown
        {
          if( quest.UI8BL != 3 )
          {
            quest.UI8BL =  (byte)( quest.UI8BL + 1);
            checkProgressSeq2();
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: Normal(Talk, YesNo, FadeIn, TargetCanMove, CanCancel), id=WYRNZOEN
        // +Callback Scene00004: Normal(Talk, Message, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=WYRNZOEN
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
    if( quest.UI8AL == 3 )
      if( quest.UI8BH == 3 )
        if( quest.UI8BL == 3 )
        {
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
          quest.Sequence = 255;
        }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ClsExc020:65847 calling Scene00000: Normal(Talk, QuestOffer, TargetCanMove), id=BLAUTHOTA" );
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
    player.sendDebug("ClsExc020:65847 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=BLAUTHOTA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
      //temporary
      quest.UI8AL = 0 ;
      quest.UI8BH = 0 ;
      quest.UI8BL = 0 ;
      quest.Sequence = 255;
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>
  {
    player.sendDebug("ClsExc020:65847 calling Scene00002: Normal(Talk, TargetCanMove), id=BLAUTHOTA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }




private void Scene00003() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ClsExc020:65847 calling Scene00003: Normal(Talk, YesNo, FadeIn, TargetCanMove, CanCancel), id=WYRNZOEN" );
    var callback = (SceneResult result) =>
    {
      if( result.errorCode == 0 || ( result.numOfResults > 0 && result.GetResult( 0 ) == 1 ) )
      {
        Scene00004();
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI | SceneFlags.DISABLE_SKIP, Callback: callback );
  }
private void Scene00004() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("ClsExc020:65847 calling Scene00004: Normal(Talk, Message, QuestReward, QuestComplete, TargetCanMove, SystemTalk), id=WYRNZOEN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
        player.SetMasterUnlock((ushort)UnlockEntry.HuntingLog);
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
