// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66276)]
public class GaiUsa308 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 5 entries
  //SEQ_3, 7 entries
  //SEQ_4, 2 entries
  //SEQ_5, 2 entries
  //SEQ_6, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1000590
  //ACTOR1 = 1006199
  //ACTOR2 = 1006200
  //ACTOR3 = 1006201
  //ACTOR4 = 1006202
  //ACTOR5 = 1006203
  //ENEMY0 = 4293402
  //EOBJECT0 = 2001651
  //EOBJECT1 = 2001652
  //EOBJECT2 = 2001653
  //EOBJECT3 = 2001654
  //EOBJECT4 = 2001954
  //EOBJECT5 = 2002271
  //EVENTACTIONSEARCH = 1
  //QUESTBATTLE0 = 13
  //SEQ0ACTOR0LQ = 90
  //TERRITORYTYPE0 = 232

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
        // +Callback Scene00090: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=BUSCARRON
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS
        break;
      }
      case 2:
      {
        if( param1 == 1006200 ) // ACTOR2 = LAURENTIUS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS
          }
          break;
        }
        if( param1 == 2001651 ) // EOBJECT0 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001652 ) // EOBJECT1 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001653 ) // EOBJECT2 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001654 ) // EOBJECT3 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1006201 ) // ACTOR3 = LAURENTIUS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00012(); // Scene00012: Normal(Talk, Message, PopBNpc, TargetCanMove), id=LAURENTIUS
          }
          break;
        }
        // BNpcHack credit moved to ACTOR3
        if( param1 == 4293402 ) // ENEMY0 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001954 ) // EOBJECT4 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001651 ) // EOBJECT0 = unknown
        {
          Scene00017(); // Scene00017: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001652 ) // EOBJECT1 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001653 ) // EOBJECT2 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001654 ) // EOBJECT3 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 4:
      {
        if( param1 == 1006201 ) // ACTOR3 = LAURENTIUS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00023(); // Scene00023: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS
          }
          break;
        }
        if( param1 == 2001954 ) // EOBJECT4 = unknown
        {
          Scene00025(); // Scene00025: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 5:
      {
        if( param1 == 1006202 ) // ACTOR4 = LAURENTIUS
        {
          if( quest.UI8AL != 1 )
          {
            Scene00026(); // Scene00026: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS
          }
          break;
        }
        if( param1 == 2001954 ) // EOBJECT4 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 6:
      {
        if( param1 == 1006203 ) // ACTOR5 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00029(); // Scene00029: Normal(QuestBattle, YesNo), id=unknown
          }
          break;
        }
        if( param1 == 2002271 ) // EOBJECT5 = unknown
        {
          Scene00031(); // Scene00031: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00032(); // Scene00032: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BUSCARRON
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
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 5;
    }
  }
  void checkProgressSeq5()
  {
    if( quest.UI8AL == 1 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.Sequence = 6;
    }
  }
  void checkProgressSeq6()
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
    player.sendDebug("GaiUsa308:66276 calling Scene00000: Normal(QuestOffer), id=unknown" );
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
    player.sendDebug("GaiUsa308:66276 calling Scene00090: Normal(Talk, FadeIn, QuestAccept, TargetCanMove), id=BUSCARRON" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 90, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00002: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00005: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00007() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_2: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00011() //SEQ_2: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_3: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00012: Normal(Talk, Message, PopBNpc, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_3: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00013: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00015() //SEQ_3: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00015: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00017() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00017: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00019() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00019: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00021() //SEQ_3: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00021: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00022() //SEQ_3: EOBJECT3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00022: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00023() //SEQ_4: ACTOR3, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00023: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00025() //SEQ_4: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00025: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00026() //SEQ_5: ACTOR4, UI8AL = 1, Flag8(1)=True(Todo:4)
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00026: Normal(Talk, NpcDespawn, TargetCanMove), id=LAURENTIUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00028() //SEQ_5: EOBJECT4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00028: Empty(None), id=unknown" );
    checkProgressSeq5();
  }

private void Scene00029() //SEQ_6: ACTOR5, UI8AL = 1, Flag8(1)=True(Todo:5)
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00029: Normal(QuestBattle, YesNo), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        //quest battle
        owner.Event.StopEvent(Id);
        player.createAndJoinQuestBattle( 13 );
      }
    };
    owner.Event.NewScene( Id, 29, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00031() //SEQ_6: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00031: Empty(None), id=unknown" );
    checkProgressSeq6();
  }

private void Scene00032() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa308:66276 calling Scene00032: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=BUSCARRON" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 32, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
