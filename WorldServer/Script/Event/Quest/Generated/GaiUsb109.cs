// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66364)]
public class GaiUsb109 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 6 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006296
  //ACTOR1 = 1006297
  //ACTOR2 = 1006298
  //ACTOR3 = 1006299
  //ACTOR4 = 1006300
  //ACTOR5 = 1006301
  //ACTOR6 = 1006302

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=URAHTALO
        break;
      }
      case 1:
      {
        if( param1 == 1006297 ) // ACTOR1 = PEEPA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=PEEPA
            // +Callback Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPA
            // +Callback Scene00004: Normal(Talk, TargetCanMove), id=PEEPA
          }
          break;
        }
        if( param1 == 1006298 ) // ACTOR2 = PEEPB
        {
          if( quest.UI8BH != 1 )
          {
            Scene00005(); // Scene00005: Normal(Talk, TargetCanMove), id=PEEPB
            // +Callback Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPB
            // +Callback Scene00007: Normal(Talk, TargetCanMove), id=PEEPB
          }
          break;
        }
        if( param1 == 1006299 ) // ACTOR3 = PEEPC
        {
          if( quest.UI8BL != 1 )
          {
            Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=PEEPC
            // +Callback Scene00009: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPC
            // +Callback Scene00010: Normal(Talk, TargetCanMove), id=PEEPC
          }
          break;
        }
        if( param1 == 1006300 ) // ACTOR4 = PEEPD
        {
          if( quest.UI8CH != 1 )
          {
            Scene00011(); // Scene00011: Normal(Talk, TargetCanMove), id=PEEPD
            // +Callback Scene00012: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPD
            // +Callback Scene00013: Normal(Talk, TargetCanMove), id=PEEPD
          }
          break;
        }
        if( param1 == 1006301 ) // ACTOR5 = PEEPE
        {
          if( quest.UI8CL != 1 )
          {
            Scene00014(); // Scene00014: Normal(Talk, TargetCanMove), id=PEEPE
            // +Callback Scene00015: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPE
            // +Callback Scene00016: Normal(Talk, TargetCanMove), id=PEEPE
          }
          break;
        }
        if( param1 == 1006302 ) // ACTOR6 = PEEPF
        {
          if( quest.UI8DH != 1 )
          {
            Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=PEEPF
            // +Callback Scene00018: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPF
            // +Callback Scene00019: Normal(Talk, TargetCanMove), id=PEEPF
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00020(); // Scene00020: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=URAHTALO
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
              if( quest.UI8DH == 1 )
              {
                quest.UI8AL = 0 ;
                quest.UI8BH = 0 ;
                quest.UI8BL = 0 ;
                quest.UI8CH = 0 ;
                quest.UI8CL = 0 ;
                quest.UI8DH = 0 ;
                quest.setBitFlag8( 1, false );
                quest.setBitFlag8( 2, false );
                quest.setBitFlag8( 3, false );
                quest.setBitFlag8( 4, false );
                quest.setBitFlag8( 5, false );
                quest.setBitFlag8( 6, false );
                quest.Sequence = 255;
              }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb109:66364 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=URAHTALO" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00002: Normal(Talk, TargetCanMove), id=PEEPA" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00003: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPA" );
    var callback = (SceneResult result) =>
    {
      Scene00004();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00004: Normal(Talk, TargetCanMove), id=PEEPA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00005: Normal(Talk, TargetCanMove), id=PEEPB" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00006: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPB" );
    var callback = (SceneResult result) =>
    {
      Scene00007();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00007: Normal(Talk, TargetCanMove), id=PEEPB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00008: Normal(Talk, TargetCanMove), id=PEEPC" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00009: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPC" );
    var callback = (SceneResult result) =>
    {
      Scene00010();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00010() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00010: Normal(Talk, TargetCanMove), id=PEEPC" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_1: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00011: Normal(Talk, TargetCanMove), id=PEEPD" );
    var callback = (SceneResult result) =>
    {
      Scene00012();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00012() //SEQ_1: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00012: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPD" );
    var callback = (SceneResult result) =>
    {
      Scene00013();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00013() //SEQ_1: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00013: Normal(Talk, TargetCanMove), id=PEEPD" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag8( 4, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00014() //SEQ_1: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00014: Normal(Talk, TargetCanMove), id=PEEPE" );
    var callback = (SceneResult result) =>
    {
      Scene00015();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_1: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00015: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPE" );
    var callback = (SceneResult result) =>
    {
      Scene00016();
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00016() //SEQ_1: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00016: Normal(Talk, TargetCanMove), id=PEEPE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CL =  (byte)( 1);
      quest.setBitFlag8( 5, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_1: ACTOR6, UI8DH = 1, Flag8(6)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00017: Normal(Talk, TargetCanMove), id=PEEPF" );
    var callback = (SceneResult result) =>
    {
      Scene00018();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00018() //SEQ_1: ACTOR6, UI8DH = 1, Flag8(6)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00018: Normal(Talk, NpcDespawn, TargetCanMove), id=PEEPF" );
    var callback = (SceneResult result) =>
    {
      Scene00019();
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00019() //SEQ_1: ACTOR6, UI8DH = 1, Flag8(6)=True
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00019: Normal(Talk, TargetCanMove), id=PEEPF" );
    var callback = (SceneResult result) =>
    {
      quest.UI8DH =  (byte)( 1);
      quest.setBitFlag8( 6, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb109:66364 calling Scene00020: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=URAHTALO" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
