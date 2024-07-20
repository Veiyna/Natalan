// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66409)]
public class GaiUsb504 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 4 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006355
  //ACTOR1 = 1006359
  //EOBJECT0 = 2002097
  //EOBJECT1 = 2002098
  //EOBJECT2 = 2002099
  //EOBJECT3 = 2002100
  //ITEM0 = 2000681

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
      //seq 0 event item ITEM0 = UI8BH max stack 4
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=ILIUD
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 4
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00003: Normal(Talk, TargetCanMove), id=ZAZAWAKA
        break;
      }
      //seq 2 event item ITEM0 = UI8CL max stack 4
      case 2:
      {
        if( param1 == 2002097 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            if( type == EVENT_ON_TALK ) Scene00004(); // Scene00004: Normal(Inventory), id=unknown
            if( type == EVENT_ON_EVENT_ITEM ) Scene00005(); // Scene00005: Normal(QuestGimmickReaction), id=unknown
          }
          break;
        }
        if( param1 == 2002098 ) // EOBJECT1 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00006(); // Scene00006: Normal(Inventory), id=unknown
            // +Callback Scene00007: Normal(QuestGimmickReaction), id=unknown
          }
          break;
        }
        if( param1 == 2002099 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00008(); // Scene00008: Normal(Inventory), id=unknown
            // +Callback Scene00009: Normal(QuestGimmickReaction), id=unknown
          }
          break;
        }
        if( param1 == 2002100 ) // EOBJECT3 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00010(); // Scene00010: Normal(Inventory), id=unknown
            // +Callback Scene00011: Normal(QuestGimmickReaction), id=unknown
          }
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 4
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00012(); // Scene00012: Normal(Talk, TargetCanMove), id=ZAZAWAKA
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 4
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00013(); // Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ILIUD
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
    quest.UI8BH = 4;
  }
  void checkProgressSeq1()
  {
    quest.UI8BH = 0;
    quest.Sequence = 2;
    quest.UI8CL = 4;
  }
  void checkProgressSeq2()
  {
    if( quest.UI8AL == 1 )
      if( quest.UI8BH == 1 )
        if( quest.UI8BL == 1 )
          if( quest.UI8CH == 1 )
          {
            quest.UI8AL = 0 ;
            quest.UI8BH = 0 ;
            quest.UI8BL = 0 ;
            quest.UI8CH = 0 ;
            quest.setBitFlag8( 1, false );
            quest.setBitFlag8( 2, false );
            quest.setBitFlag8( 3, false );
            quest.setBitFlag8( 4, false );
            quest.UI8CL = 0;
            quest.Sequence = 3;
          }
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb504:66409 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=ILIUD" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00002: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00003();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00003: Normal(Talk, TargetCanMove), id=ZAZAWAKA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00004: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00005() //SEQ_2: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00005: Normal(QuestGimmickReaction), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: EOBJECT1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00006: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00007();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_2: EOBJECT1, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00007: Normal(QuestGimmickReaction), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_2: EOBJECT2, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00008: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_2: EOBJECT2, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00009: Normal(QuestGimmickReaction), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_2: EOBJECT3, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00010: Normal(Inventory), id=unknown" );
    var callback = (SceneResult result) =>
    {
      Scene00011();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_2: EOBJECT3, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00011: Normal(QuestGimmickReaction), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag8( 4, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_3: , <No Var>, <No Flag>(Todo:2)
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00012: Normal(Talk, TargetCanMove), id=ZAZAWAKA" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb504:66409 calling Scene00013: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=ILIUD" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
