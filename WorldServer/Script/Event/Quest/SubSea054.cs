// FFXIVTheMovie.ParserV3.11
// param used:
//_ACTOR1 = E
//_ACTOR1E = 1|3,12
//SCENE_15 = SOZAIRARZAI
//_ALLOW_EMPTY_ENTRY SET!!
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66002)]
public class SubSea054 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 2 entries
  //SEQ_2, 9 entries
  //SEQ_3, 2 entries
  //SEQ_4, 2 entries
  //SEQ_5, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002626
  //ACTOR1 = 1002640
  //ACTOR2 = 1002641
  //ACTOR3 = 1002642
  //ACTOR4 = 1002643
  //ACTOR5 = 1002630
  //ENEMY0 = 3931777
  //ENEMY1 = 3931779
  //EOBJECT0 = 2001279
  //EOBJECT1 = 2000451
  //EOBJECT2 = 2000449
  //EVENTRANGE0 = 3929588
  //EVENTACTION = 35
  //EVENTACTIONPROCESSMIDDLE = 16
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000356
  //ITEM1 = 2000357
  //QSTACCEPTCHECK = 66000

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=STAELWYRN
        break;
      }
      case 1:
      {
        if( param1 == 1002640 ) // ACTOR1 = SEVRIN, EB=3(emote=12)
        {
          if( quest.UI8AL != 1 )
          {
            if( type == EVENT_ON_TALK ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=SEVRIN
            if( type == EVENT_ON_EMOTE )
            {
              if( param3 == 12 ) Scene00002(); // Correct Scene00002: Normal(Talk, TargetCanMove), id=SEVRIN
              else Scene00003(); // Incorrect Scene00003: Normal(None), id=unknown
            }
          }
          break;
        }
        if( param1 == 2001279 ) // EOBJECT0 = unknown
        {
          Scene00005(); // Scene00005: Normal(None), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 1002641 ) // ACTOR2 = AYLMER
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00006(); // Scene00006: Normal(None), id=unknown
            // +Callback Scene00008: Normal(Talk, NpcDespawn, TargetCanMove), id=AYLMER
          }
          break;
        }
        if( param1 == 1002642 ) // ACTOR3 = EYRIMHUS
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00009(); // Scene00009: Normal(None), id=unknown
            // +Callback Scene00011: Normal(Talk, NpcDespawn, TargetCanMove), id=EYRIMHUS
          }
          break;
        }
        if( param1 == 1002643 ) // ACTOR4 = SOZAIRARZAI
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00012(); // Scene00012: Normal(None), id=unknown
            // +Callback Scene00014: Normal(Talk, NpcDespawn, TargetCanMove), id=SOZAIRARZAI
            // +Callback Scene00015: Normal(None), id=SOZAIRARZAI
          }
          break;
        }
        if( param1 == 3929588 ) // EVENTRANGE0 = unknown
        {
          Scene00016(); // Scene00016: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 3931777 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 3931779 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 1002640 ) // ACTOR1 = SEVRIN
        {
          Scene00017(); // Scene00017: Normal(None), id=unknown
          // +Callback Scene00018: Normal(Talk, TargetCanMove), id=SEVRIN
          break;
        }
        if( param1 == 2001279 ) // EOBJECT0 = unknown
        {
          Scene00020(); // Scene00020: Normal(None), id=unknown
          break;
        }
        if( param1 == 2000451 ) // EOBJECT1 = unknown
        {
          Scene00021(); // Scene00021: Normal(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1002640 ) // ACTOR1 = SEVRIN
        {
          if( quest.UI8AL != 1 )
          {
            Scene00024(); // Scene00024: Normal(Talk, NpcDespawn, TargetCanMove), id=SEVRIN
          }
          break;
        }
        if( param1 == 2001279 ) // EOBJECT0 = unknown
        {
          Scene00026(); // Scene00026: Normal(None), id=unknown
          break;
        }
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack 1
      case 4:
      {
        if( param1 == 2001279 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00027(); // Scene00027: Normal(None), id=unknown
            // +Callback Scene00029: Normal(None), id=unknown
          }
          break;
        }
        if( param1 == 2000449 ) // EOBJECT2 = unknown
        {
          Scene00030(); // Scene00030: Normal(None), id=unknown
          break;
        }
        break;
      }
      //seq 5 event item ITEM0 = UI8BH max stack 1
      //seq 5 event item ITEM1 = UI8BL max stack 1
      case 5:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00033(); // Scene00033: NpcTrade(Talk, TargetCanMove), id=OSSINE
        // +Callback Scene00034: Normal(Talk, TargetCanMove), id=OSSINE
        // +Callback Scene00035: Normal(None), id=unknown
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      //seq 255 event item ITEM1 = UI8BL max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00036(); // Scene00036: NpcTrade(Talk, TargetCanMove), id=STAELWYRN
        // +Callback Scene00037: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STAELWYRN
        // +Callback Scene00038: Normal(None), id=unknown
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
    if( quest.UI8AL == 3 )
    {
      quest.UI8AL = 0 ;
      quest.setBitFlag8( 1, false );
      quest.setBitFlag8( 2, false );
      quest.setBitFlag8( 3, false );
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
      quest.UI8BH = 1;
      quest.UI8BL = 1;
    }
  }
  void checkProgressSeq5()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove, SystemTalk, CanCancel), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True, Branch(Todo:0)
  {
    player.sendDebug("SubSea054:66002 calling Scene00001: Normal(Talk, TargetCanMove), id=SEVRIN" );
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True, Branch(Todo:0)
  {
    player.sendDebug("SubSea054:66002 calling Scene00002: Normal(Talk, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True, Branch(Todo:0)
  {
    player.sendDebug("SubSea054:66002 calling Scene00003: Normal(None), id=unknown" );
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR);
  }

private void Scene00005() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00005: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR2, UI8AL = 3, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubSea054:66002 calling Scene00006: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00008();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_2: ACTOR2, UI8AL = 3, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("SubSea054:66002 calling Scene00008: Normal(Talk, NpcDespawn, TargetCanMove), id=AYLMER" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: ACTOR3, UI8AL = 3, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("SubSea054:66002 calling Scene00009: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00011();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_2: ACTOR3, UI8AL = 3, Flag8(2)=True(Todo:1)
  {
    player.sendDebug("SubSea054:66002 calling Scene00011: Normal(Talk, NpcDespawn, TargetCanMove), id=EYRIMHUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 2, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ACTOR4, UI8AL = 3, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubSea054:66002 calling Scene00012: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00014();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00014() //SEQ_2: ACTOR4, UI8AL = 3, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubSea054:66002 calling Scene00014: Normal(Talk, NpcDespawn, TargetCanMove), id=SOZAIRARZAI" );
    var callback = (SceneResult result) =>
    {
      Scene00015();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_2: ACTOR4, UI8AL = 3, Flag8(3)=True(Todo:1)
  {
    player.sendDebug("SubSea054:66002 calling Scene00015: Normal(None), id=SOZAIRARZAI" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( quest.UI8AL + 1);
      quest.setBitFlag8( 3, true );
      player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 3 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_2: EVENTRANGE0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00016: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      this.owner.Map.CreateBNpcFromLayoutId(3931777, this.owner.Id);
      this.owner.Map.CreateBNpcFromLayoutId(3931779, this.owner.Id);
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }



private void Scene00017() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00017: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00018();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00018() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00018: Normal(Talk, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00020: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00021: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00024() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("SubSea054:66002 calling Scene00024: Normal(Talk, NpcDespawn, TargetCanMove), id=SEVRIN" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00026() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00026: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 26, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00027() //SEQ_4: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubSea054:66002 calling Scene00027: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00029();
    };
    owner.Event.NewScene( Id, 27, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00029() //SEQ_4: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("SubSea054:66002 calling Scene00029: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 29, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00030() //SEQ_4: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00030: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 30, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00033() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubSea054:66002 calling Scene00033: NpcTrade(Talk, TargetCanMove), id=OSSINE" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00034();
      }
    };
    owner.Event.NewScene( Id, 33, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00034() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubSea054:66002 calling Scene00034: Normal(Talk, TargetCanMove), id=OSSINE" );
    var callback = (SceneResult result) =>
    {
      Scene00035();
    };
    owner.Event.NewScene( Id, 34, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00035() //SEQ_5: , <No Var>, <No Flag>(Todo:4)
  {
    player.sendDebug("SubSea054:66002 calling Scene00035: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 4, 0, 0, 0 );
      checkProgressSeq5();
    };
    owner.Event.NewScene( Id, 35, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00036() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00036: NpcTrade(Talk, TargetCanMove), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00037();
      }
    };
    owner.Event.NewScene( Id, 36, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00037() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00037: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=STAELWYRN" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00038();
      }
    };
    owner.Event.NewScene( Id, 37, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00038() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea054:66002 calling Scene00038: Normal(None), id=unknown" );
    var callback = (SceneResult result) =>
    {
      player.FinishQuest( Id, result.GetResult( 1 ) );
    };
    owner.Event.NewScene( Id, 38, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
