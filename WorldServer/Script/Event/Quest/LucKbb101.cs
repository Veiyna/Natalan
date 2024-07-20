// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using Shared.Game.Enum;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(68797)]
public class LucKbb101 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 7 entries
  //SEQ_3, 3 entries
  //SEQ_255, 2 entries

  //ACTOR0 = 1029028
  //ACTOR1 = 1029032
  //ACTOR2 = 1029029
  //ACTOR3 = 1029030
  //ACTOR4 = 1029031
  //ACTOR5 = 1029033
  //ACTOR6 = 1029034
  //CLASSJOB = 37
  //CUTSCENE00 = 2073
  //ENEMY0 = 7953134
  //EOBJECT0 = 2010237
  //EOBJECT1 = 2010238
  //EVENTACTION0 = 50
  //EVENTACTION1 = 1
  //EVENTACTION2 = 35
  //EVENTRANGE0 = 7953099
  //LEVELENPCID0 = 7953076
  //LEVELENPCID1 = 7953149
  //LOCACTOR0 = 1029026
  //LOCACTOR1 = 1029027
  //LOCACTOR2 = 1029049
  //LOCACTOR3 = 1029050
  //LOCACTOR4 = 1029051
  //LOCBGM0 = 70
  //LOCENPCID0 = 1029030
  //POPRANGE0 = 7953091
  //QUEST0 = 67982
  //UNLOCKIMAGECLASS = 760
  //UNLOCKIMAGEJOBCHANGE = 1207

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HONESTGODSQUIVERBOW
        break;
      }
      case 1:
      {
        if( param1 == 2010237 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(CutScene, AutoFadeIn), id=unknown
          }
          break;
        }
        if( param1 == 1029028 ) // ACTOR0 = HONESTGODSQUIVERBOW
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=HONESTGODSQUIVERBOW
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 7953099 ) // EVENTRANGE0 = unknown
        {
          Scene00005(); // Scene00005: Normal(Message), id=unknown
          break;
        }
        if( param1 == 7953134 ) // ENEMY0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00006(); // Scene00006: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1029032 ) // ACTOR1 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2010238 ) // EOBJECT1 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 1029029 ) // ACTOR2 = unknown
        {
          Scene00010(); // Scene00010: Normal(Message), id=unknown
          break;
        }
        if( param1 == 1029030 ) // ACTOR3 = unknown
        {
          Scene00011(); // Scene00011: Normal(Message), id=unknown
          break;
        }
        if( param1 == 1029031 ) // ACTOR4 = unknown
        {
          Scene00012(); // Scene00012: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1029029 ) // ACTOR2 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00014(); // Scene00014: Normal(Talk, FadeIn, ENpcBind), id=unknown
          }
          break;
        }
        if( param1 == 1029030 ) // ACTOR3 = unknown
        {
          Scene00015();
          break;
        }
        if( param1 == 1029031 ) // ACTOR4 = SOPHIE
        {
          Scene00015(); // Scene00015: Normal(Talk, TargetCanMove), id=SOPHIE
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1029033 ) // ACTOR5 = RADOVAN
        {
          Scene00017(); // Scene00017: Normal(Talk, YesNo, QuestReward, QuestComplete, TargetCanMove, SystemTalk, CanCancel, ENpcBind), id=RADOVAN
          break;
        }
        if( param1 == 1029034 ) // ACTOR6 = SOPHIE
        {
          Scene00018(); // Scene00018: Normal(Talk, TargetCanMove), id=SOPHIE
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

  public override void OnAreaTrigger(ulong actorId, WorldPosition position)
  {
    onProgress(EVENT_ON_WITHIN_RANGE, actorId, 0, 0 );
  }

  public override void OnBNpcKill(BNpc bNpc)
  {
    onProgress(EVENT_ON_BNPC_KILL, bNpc.InstanceId, bNpc.BNpcNameId, 0);
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
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("LucKbb101:68797 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HONESTGODSQUIVERBOW" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("LucKbb101:68797 calling Scene00003: Normal(CutScene, AutoFadeIn), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
      player.sendDebug("Finished with AutoFadeIn scene, reloading zone..." );
      player.Event.StopEvent(Id);
      player.TeleportTo(new WorldPosition(player.Position.TerritoryId, new Vector3(-214, 11,14), (float)-0.6, player.Position.InstanceId));
    };
    owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00004: Normal(Talk, TargetCanMove), id=HONESTGODSQUIVERBOW" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: EVENTRANGE0, <No Var>, Flag8(1)=True
  {
    player.sendDebug("LucKbb101:68797 calling Scene00005: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 1, true );
      player.Map.CreateBNpcFromLayoutId(7953134, player.Id);
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ENEMY0, UI8AL = 1, <No Flag>(Todo:1)
  {
    player.sendDebug("LucKbb101:68797 calling Scene00006: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00007: Empty(None), id=unknown" );
  }

private void Scene00009() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00010() //SEQ_2: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00010: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00011: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00012: Empty(None), id=unknown" );
  }

private void Scene00013() //SEQ_3: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("LucKbb101:68797 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 2, 0, 0, 0 );
    checkProgressSeq3();
  }

private void Scene00014() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00014: Normal(Talk, FadeIn, ENpcBind), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00013();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00015() //SEQ_3: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00015: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_255: ACTOR5, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00017: Normal(Talk, YesNo, QuestReward, QuestComplete, TargetCanMove, SystemTalk, CanCancel, ENpcBind), id=RADOVAN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
        player.SetLevelForClass(ClassJob.Gunbreaker, 60);
      }
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_255: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("LucKbb101:68797 calling Scene00018: Normal(Talk, TargetCanMove), id=SOPHIE" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
