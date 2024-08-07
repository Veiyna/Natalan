// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66253)]
public class GaiUsa103 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 8 entries
  //SEQ_255, 1 entries

  //ACTIONTIMELINEEVENTBYEBYE = 962
  //ACTIONTIMELINEEVENTFIDGET = 968
  //ACTIONTIMELINEEVENTJOYBIG = 945
  //ACTIONTIMELINEEVENTLAUGH = 952
  //ACTIONTIMELINEEVENTTROUBLE = 944
  //ACTOR0 = 1006674
  //ACTOR1 = 1000563
  //ACTOR2 = 1000437
  //ACTOR3 = 1000576
  //ACTOR4 = 1000585
  //ACTOR5 = 1000584
  //ACTOR6 = 1000587
  //ACTOR7 = 1000578
  //ACTOR8 = 1000277

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=YDA
        break;
      }
      case 1:
      {
        if( param1 == 1000563 ) // ACTOR1 = AMEEXIA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=AMEEXIA
            // +Callback Scene00002: Normal(Talk, TargetCanMove), id=AMEEXIA
            // +Callback Scene00003: Normal(Talk, TargetCanMove), id=AMEEXIA
          }
          break;
        }
        if( param1 == 1000437 ) // ACTOR2 = ZACHEUS
        {
          if( quest.UI8BH != 1 )
          {
            Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=ZACHEUS
            // +Callback Scene00005: Normal(Talk, TargetCanMove), id=ZACHEUS
            // +Callback Scene00006: Normal(Talk, TargetCanMove), id=ZACHEUS
          }
          break;
        }
        if( param1 == 1000576 ) // ACTOR3 = KNOLEXIA
        {
          if( quest.UI8BL != 1 )
          {
            Scene00007(); // Scene00007: Normal(Talk, TargetCanMove), id=KNOLEXIA
            // +Callback Scene00008: Normal(Talk, TargetCanMove), id=KNOLEXIA
            // +Callback Scene00009: Normal(Talk, TargetCanMove), id=KNOLEXIA
          }
          break;
        }
        if( param1 == 1000585 ) // ACTOR4 = PELIXIA
        {
          if( quest.UI8CH != 1 )
          {
            Scene00010(); // Scene00010: Normal(Talk, TargetCanMove), id=PELIXIA
            // +Callback Scene00011: Normal(Talk, TargetCanMove), id=PELIXIA
            // +Callback Scene00012: Normal(Talk, TargetCanMove), id=PELIXIA
          }
          break;
        }
        if( param1 == 1000584 ) // ACTOR5 = IMEDIA
        {
          if( quest.UI8CL != 1 )
          {
            Scene00013(); // Scene00013: Normal(Talk, TargetCanMove), id=IMEDIA
            // +Callback Scene00014: Normal(Talk, TargetCanMove), id=IMEDIA
            // +Callback Scene00015: Normal(Talk, TargetCanMove), id=IMEDIA
          }
          break;
        }
        if( param1 == 1000587 ) // ACTOR6 = DELLEXIA
        {
          if( quest.UI8DH != 1 )
          {
            Scene00016(); // Scene00016: Normal(Talk, TargetCanMove), id=DELLEXIA
            // +Callback Scene00017: Normal(Talk, TargetCanMove), id=DELLEXIA
            // +Callback Scene00018: Normal(Talk, TargetCanMove), id=DELLEXIA
          }
          break;
        }
        if( param1 == 1000578 ) // ACTOR7 = MARINTERRE
        {
          if( quest.UI8DL != 1 )
          {
            Scene00019(); // Scene00019: Normal(Talk, TargetCanMove), id=MARINTERRE
            // +Callback Scene00020: Normal(Talk, TargetCanMove), id=MARINTERRE
            // +Callback Scene00021: Normal(Talk, TargetCanMove), id=MARINTERRE
          }
          break;
        }
        if( param1 == 1000277 ) // ACTOR8 = NATHAXIO
        {
          if( quest.UI8EH != 1 )
          {
            Scene00022(); // Scene00022: Normal(Talk, TargetCanMove), id=NATHAXIO
            // +Callback Scene00023: Normal(Talk, TargetCanMove), id=NATHAXIO
            // +Callback Scene00024: Normal(Talk, TargetCanMove), id=NATHAXIO
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00025(); // Scene00025: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YDA
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
                if( quest.UI8DL == 1 )
                  if( quest.UI8EH == 1 )
                  {
                    quest.UI8AL = 0 ;
                    quest.UI8BH = 0 ;
                    quest.UI8BL = 0 ;
                    quest.UI8CH = 0 ;
                    quest.UI8CL = 0 ;
                    quest.UI8DH = 0 ;
                    quest.UI8DL = 0 ;
                    quest.UI8EH = 0 ;
                    quest.setBitFlag8( 1, false );
                    quest.setBitFlag8( 2, false );
                    quest.setBitFlag8( 3, false );
                    quest.setBitFlag8( 4, false );
                    quest.setBitFlag8( 5, false );
                    quest.setBitFlag8( 6, false );
                    quest.setBitFlag8( 7, false );
                    quest.setBitFlag8( 8, false );
                    quest.Sequence = 255;
                  }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=YDA" );
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
    player.sendDebug("GaiUsa103:66253 calling Scene00001: Normal(Talk, TargetCanMove), id=AMEEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00002();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00002: Normal(Talk, TargetCanMove), id=AMEEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00003: Normal(Talk, TargetCanMove), id=AMEEXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00004: Normal(Talk, TargetCanMove), id=ZACHEUS" );
    var callback = (SceneResult result) =>
    {
      Scene00005();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00005: Normal(Talk, TargetCanMove), id=ZACHEUS" );
    var callback = (SceneResult result) =>
    {
      Scene00006();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_1: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00006: Normal(Talk, TargetCanMove), id=ZACHEUS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00007: Normal(Talk, TargetCanMove), id=KNOLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00008();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00008: Normal(Talk, TargetCanMove), id=KNOLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00009();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_1: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00009: Normal(Talk, TargetCanMove), id=KNOLEXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_1: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00010: Normal(Talk, TargetCanMove), id=PELIXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00011();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00011() //SEQ_1: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00011: Normal(Talk, TargetCanMove), id=PELIXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00012();
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00012() //SEQ_1: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00012: Normal(Talk, TargetCanMove), id=PELIXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag8( 4, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_1: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00013: Normal(Talk, TargetCanMove), id=IMEDIA" );
    var callback = (SceneResult result) =>
    {
      Scene00014();
    };
    owner.Event.NewScene( Id, 13, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00014() //SEQ_1: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00014: Normal(Talk, TargetCanMove), id=IMEDIA" );
    var callback = (SceneResult result) =>
    {
      Scene00015();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00015() //SEQ_1: ACTOR5, UI8CL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00015: Normal(Talk, TargetCanMove), id=IMEDIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CL =  (byte)( 1);
      quest.setBitFlag8( 5, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_1: ACTOR6, UI8DH = 1, Flag8(6)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00016: Normal(Talk, TargetCanMove), id=DELLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00017();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00017() //SEQ_1: ACTOR6, UI8DH = 1, Flag8(6)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00017: Normal(Talk, TargetCanMove), id=DELLEXIA" );
    var callback = (SceneResult result) =>
    {
      Scene00018();
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00018() //SEQ_1: ACTOR6, UI8DH = 1, Flag8(6)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00018: Normal(Talk, TargetCanMove), id=DELLEXIA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8DH =  (byte)( 1);
      quest.setBitFlag8( 6, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_1: ACTOR7, UI8DL = 1, Flag8(7)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00019: Normal(Talk, TargetCanMove), id=MARINTERRE" );
    var callback = (SceneResult result) =>
    {
      Scene00020();
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00020() //SEQ_1: ACTOR7, UI8DL = 1, Flag8(7)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00020: Normal(Talk, TargetCanMove), id=MARINTERRE" );
    var callback = (SceneResult result) =>
    {
      Scene00021();
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00021() //SEQ_1: ACTOR7, UI8DL = 1, Flag8(7)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00021: Normal(Talk, TargetCanMove), id=MARINTERRE" );
    var callback = (SceneResult result) =>
    {
      quest.UI8DL =  (byte)( 1);
      quest.setBitFlag8( 7, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_1: ACTOR8, UI8EH = 1, Flag8(8)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00022: Normal(Talk, TargetCanMove), id=NATHAXIO" );
    var callback = (SceneResult result) =>
    {
      Scene00023();
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00023() //SEQ_1: ACTOR8, UI8EH = 1, Flag8(8)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00023: Normal(Talk, TargetCanMove), id=NATHAXIO" );
    var callback = (SceneResult result) =>
    {
      Scene00024();
    };
    owner.Event.NewScene( Id, 23, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00024() //SEQ_1: ACTOR8, UI8EH = 1, Flag8(8)=True
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00024: Normal(Talk, TargetCanMove), id=NATHAXIO" );
    var callback = (SceneResult result) =>
    {
      quest.UI8EH =  (byte)( 1);
      quest.setBitFlag8( 8, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00025() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa103:66253 calling Scene00025: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=YDA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 25, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
