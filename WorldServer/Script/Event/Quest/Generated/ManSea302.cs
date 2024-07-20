// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66217)]
public class ManSea302 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 12 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1004885
  //ACTOR1 = 1002388
  //ACTOR2 = 1004883
  //ACTOR3 = 1004884
  //ACTOR4 = 1004888
  //ACTOR5 = 1004999
  //ACTOR6 = 1005000
  //ACTOR7 = 1005001
  //ACTOR8 = 1005002
  //EOBJECT0 = 2001656
  //EOBJECT1 = 2001807
  //EVENTRANGE0 = 4126868
  //EVENTACTIONSEARCH = 1
  //LOCACTOR0 = 1005105
  //NCUT0 = 218
  //QUESTBATTLE0 = 48
  //TERRITORYTYPE0 = 279

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, YesNo, QuestOffer, QuestAccept, TargetCanMove), id=STORMOFFICER
        break;
      }
      case 1:
      {
        if( param1 == 1002388 ) // ACTOR1 = STORMPERSONNEL
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, FadeIn, TargetCanMove, Menu), id=STORMPERSONNEL
          }
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR3 = SERPENTOFFICER
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR0 = STORMOFFICER
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=STORMOFFICER
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1004888 ) // ACTOR4 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 4126868 ) // EVENTRANGE0 = unknown
        {
          Scene00006(); // Scene00006: Normal(QuestBattle, YesNo), id=unknown
          break;
        }
        if( param1 == 1004999 ) // ACTOR5 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005000 ) // ACTOR6 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005001 ) // ACTOR7 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1005002 ) // ACTOR8 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 1002388 ) // ACTOR1 = STORMPERSONNEL
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=STORMPERSONNEL
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR3 = SERPENTOFFICER
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR0 = STORMOFFICER
        {
          Scene00015(); // Scene00015: Normal(Talk, TargetCanMove), id=STORMOFFICER
          break;
        }
        if( param1 == 2001656 ) // EOBJECT0 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001807 ) // EOBJECT1 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1002388 ) // ACTOR1 = STORMPERSONNEL
        {
          Scene00020(); // Scene00020: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STORMPERSONNEL
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00021(); // Scene00021: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR3 = SERPENTOFFICER
        {
          Scene00022(); // Scene00022: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR0 = STORMOFFICER
        {
          Scene00023(); // Scene00023: Normal(Talk, TargetCanMove), id=STORMOFFICER
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
      quest.setBitFlag16( 1, false );
      quest.setBitFlag16( 2, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00000: Normal(Talk, YesNo, QuestOffer, QuestAccept, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("ManSea302:66217 calling Scene00001: Normal(Talk, FadeIn, TargetCanMove, Menu), id=STORMPERSONNEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults == 1 || ( result.errorCode == 0 && result.numOfResults == 2 ) )
      {
        quest.UI8AL =  (byte)( 1);
        quest.setBitFlag8( 1, true );
        player.SendQuestMessage(Id, 0, 0, 0, 0 );
        checkProgressSeq1();
      }
    };
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI | (SceneFlags)4165480447, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00002: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00003: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00004: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR4, UI8AL = 1, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("ManSea302:66217 calling Scene00005: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 48 );
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: EVENTRANGE0, <No Var>, Flag16(2)=True
  {
    player.sendDebug("ManSea302:66217 calling Scene00006: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 48 );
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_2: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00012() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00012: Normal(Talk, TargetCanMove), id=STORMPERSONNEL" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00013: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00014: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_2: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00015: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00017: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00019() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00019: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00020() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00020: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STORMPERSONNEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00021: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00022: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00023() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("ManSea302:66217 calling Scene00023: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
