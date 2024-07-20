// FFXIVTheMovie.ParserV3.11
// param used:
//ID_ACTOR2 = 4299289701
//ID_ACTOR3 = 4299289705
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66318)]
public class GaiUsa709 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 6 entries
  //SEQ_2, 2 entries
  //SEQ_3, 8 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006215
  //ACTOR1 = 1006225
  //ACTOR10 = 1007605
  //ACTOR2 = 1007616
  //ACTOR3 = 1007617
  //ACTOR4 = 1007635
  //ACTOR5 = 1006230
  //ACTOR6 = 1006229
  //ACTOR7 = 1006231
  //ACTOR8 = 1006232
  //ACTOR9 = 1006233
  //EOBJECT0 = 2001980
  //EOBJECT1 = 2001981
  //EOBJECT2 = 2002272
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000974
  //ITEM1 = 2000973
  //LOCACTOR0 = 1008115
  //LOCACTOR1 = 1008116
  //LOCACTOR2 = 1006226
  //QUESTBATTLE0 = 58
  //SEQ0ACTOR0LQ = 90
  //TERRITORYTYPE0 = 306

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer), id=unknown
        // +Callback Scene00090: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=GUNDOBALD
        break;
      }
      //seq 1 event item ITEM1 = UI8DH max stack 1
      //seq 1 event item ITEM0 = UI8DL max stack 1
      case 1:
      {
        if( param1 == 1006225 ) // ACTOR1 = WILRED
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=WILRED
          }
          break;
        }
        if( param1 == 1007616 || param1 == 4299289701 ) // ACTOR2 = NPCA
        {
          if( quest.UI8BH != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCA
          }
          break;
        }
        if( param1 == 1007617 || param1 == 4299289705 ) // ACTOR3 = NPCB
        {
          if( quest.UI8BL != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCB
          }
          break;
        }
        if( param1 == 2001980 ) // EOBJECT0 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00006(); // Scene00006: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001981 ) // EOBJECT1 = unknown
        {
          if( quest.UI8CL != 1 )
          {
            Scene00008(); // Scene00008: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 1007635 ) // ACTOR4 = BERTLIANA
        {
          Scene00009(); // Scene00009: Normal(Talk, TargetCanMove), id=BERTLIANA
          break;
        }
        break;
      }
      //seq 2 event item ITEM1 = UI8BH max stack 1
      //seq 2 event item ITEM0 = UI8BL max stack 1
      case 2:
      {
        if( param1 == 1006215 ) // ACTOR0 = GUNDOBALD
        {
          if( quest.UI8AL != 1 )
          {
            Scene00010(); // Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00011: Normal(Talk, TargetCanMove), id=GUNDOBALD
          }
          break;
        }
        if( param1 == 1007635 ) // ACTOR4 = BERTLIANA
        {
          Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=BERTLIANA
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1006230 ) // ACTOR5 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00013(); // Scene00013: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 2002272 ) // EOBJECT2 = unknown
        {
          Scene00014(); // Scene00014: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006229 ) // ACTOR6 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006231 ) // ACTOR7 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006232 ) // ACTOR8 = NPCA
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=NPCA
          // +Callback Scene00018: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 1006233 ) // ACTOR9 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007605 ) // ACTOR10 = unknown
        {
          Scene00020(); // Scene00020: Empty(None), id=unknown
          break;
        }
        if( param1 == 1007635 ) // ACTOR4 = BERTLIANA
        {
          Scene00021(); // Scene00021: Normal(Talk, TargetCanMove), id=BERTLIANA
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00022(); // Scene00022: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=GUNDOBALD
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
              quest.UI8DL = 0;
              quest.Sequence = 2;
              quest.UI8BH = 1;
              quest.UI8BL = 1;
            }
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.UI8BH = 0;
      quest.UI8BL = 0;
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
    player.sendDebug("GaiUsa709:66318 calling Scene00000: Normal(QuestOffer), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00090();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00090() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00090: Normal(Talk, FadeIn, QuestAccept, TargetCanMove, CreateCharacterTalk), id=GUNDOBALD" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 90, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=WILRED" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00004: Normal(Talk, NpcDespawn, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_1: EOBJECT0, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00006: Empty(None), id=unknown" );
    quest.UI8CH =  (byte)( 1);
    quest.setBitFlag8( 4, true );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_1: EOBJECT1, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00008: Empty(None), id=unknown" );
    quest.UI8CL =  (byte)( 1);
    quest.setBitFlag8( 5, true );
    checkProgressSeq1();
  }

private void Scene00009() //SEQ_1: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00009: Normal(Talk, TargetCanMove), id=BERTLIANA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_2: ACTOR0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00010: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00011();
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_2: ACTOR0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00011: Normal(Talk, TargetCanMove), id=GUNDOBALD" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00012: Normal(Talk, TargetCanMove), id=BERTLIANA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_3: ACTOR5, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00013: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 58 );
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_3: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00014: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00015() //SEQ_3: ACTOR6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00015: Empty(None), id=unknown" );
  }

private void Scene00016() //SEQ_3: ACTOR7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00016: Empty(None), id=unknown" );
  }

private void Scene00017() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00017: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
      Scene00018();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00018() //SEQ_3: ACTOR8, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00018: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_3: ACTOR9, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00019: Empty(None), id=unknown" );
  }

private void Scene00020() //SEQ_3: ACTOR10, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00020: Empty(None), id=unknown" );
  }

private void Scene00021() //SEQ_3: ACTOR4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00021: Normal(Talk, TargetCanMove), id=BERTLIANA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa709:66318 calling Scene00022: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove), id=GUNDOBALD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 22, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
