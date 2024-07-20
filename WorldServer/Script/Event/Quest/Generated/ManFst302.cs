// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66216)]
public class ManFst302 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 12 entries
  //SEQ_255, 4 entries

  //ACTOR0 = 1004884
  //ACTOR1 = 1002394
  //ACTOR2 = 1004883
  //ACTOR3 = 1004885
  //ACTOR4 = 1004886
  //ACTOR5 = 1004991
  //ACTOR6 = 1004992
  //ACTOR7 = 1004993
  //ACTOR8 = 1004994
  //EOBJECT0 = 2001713
  //EOBJECT1 = 2001806
  //EVENTRANGE0 = 4126642
  //EVENTACTIONSEARCH = 1
  //LOCACTOR0 = 1005104
  //NCUT0 = 217
  //QUESTBATTLE0 = 46
  //TERRITORYTYPE0 = 277

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, YesNo, QuestOffer, QuestAccept, TargetCanMove), id=SERPENTOFFICER
        break;
      }
      case 1:
      {
        if( param1 == 1002394 ) // ACTOR1 = SERPENTPERSONNEL
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, FadeIn, TargetCanMove, Menu), id=SERPENTPERSONNEL
          }
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR3 = STORMOFFICER
        {
          Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=STORMOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR0 = SERPENTOFFICER
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1004886 ) // ACTOR4 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00005(); // Scene00005: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 4126642 ) // EVENTRANGE0 = unknown
        {
          Scene00006(); // Scene00006: Normal(QuestBattle, YesNo), id=unknown
          break;
        }
        if( param1 == 1004991 ) // ACTOR5 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004992 ) // ACTOR6 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004993 ) // ACTOR7 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004994 ) // ACTOR8 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR0 = SERPENTOFFICER
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR3 = STORMOFFICER
        {
          Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=STORMOFFICER
          break;
        }
        if( param1 == 1002394 ) // ACTOR1 = SERPENTPERSONNEL
        {
          Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=SERPENTPERSONNEL
          break;
        }
        if( param1 == 2001713 ) // EOBJECT0 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001806 ) // EOBJECT1 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1002394 ) // ACTOR1 = SERPENTPERSONNEL
        {
          Scene00019(); // Scene00019: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SERPENTPERSONNEL
          break;
        }
        if( param1 == 1004883 ) // ACTOR2 = FLAMEOFFICER
        {
          Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=FLAMEOFFICER
          break;
        }
        if( param1 == 1004884 ) // ACTOR0 = SERPENTOFFICER
        {
          Scene00021(); // Scene00021: Normal(Talk, TargetCanMove), id=SERPENTOFFICER
          break;
        }
        if( param1 == 1004885 ) // ACTOR3 = STORMOFFICER
        {
          Scene00022(); // Scene00022: Normal(Talk, TargetCanMove), id=STORMOFFICER
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
    player.sendDebug("ManFst302:66216 calling Scene00000: Normal(Talk, YesNo, QuestOffer, QuestAccept, TargetCanMove), id=SERPENTOFFICER" );
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
    player.sendDebug("ManFst302:66216 calling Scene00001: Normal(Talk, FadeIn, TargetCanMove, Menu), id=SERPENTPERSONNEL" );
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
    owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00002: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00003: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00004: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: ACTOR4, UI8AL = 1, Flag16(1)=True(Todo:1)
  {
    player.sendDebug("ManFst302:66216 calling Scene00005: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 46 );
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: EVENTRANGE0, <No Var>, Flag16(2)=True
  {
    player.sendDebug("ManFst302:66216 calling Scene00006: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 46 );
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00008() //SEQ_2: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00008: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_2: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00009: Empty(None), id=unknown" );
  }

private void Scene00010() //SEQ_2: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00010: Empty(None), id=unknown" );
  }

private void Scene00011() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00011: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00012: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00013: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00014: Normal(Talk, TargetCanMove), id=SERPENTPERSONNEL" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00016: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00018() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00018: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00019() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00019: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SERPENTPERSONNEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00020: Normal(Talk, TargetCanMove), id=FLAMEOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00021: Normal(Talk, TargetCanMove), id=SERPENTOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("ManFst302:66216 calling Scene00022: Normal(Talk, TargetCanMove), id=STORMOFFICER" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
