// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66429)]
public class GaiUsb611 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 4 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006387
  //ACTOR1 = 1006388
  //ACTOR2 = 1006389
  //ACTOR3 = 1006390
  //ACTOR4 = 1006391
  //ENEMY0 = 719
  //ITEM0 = 2000699
  //ITEM1 = 2000700

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SYLVAINTEL
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack 3
      //seq 1 event item ITEM1 = UI8BL max stack 3
      case 1:
      {
        // empty entry
        break;
      }
      //seq 2 event item ITEM0 = UI8CL max stack 3
      //seq 2 event item ITEM1 = UI8DH max stack 3
      case 2:
      {
        if( param1 == 1006388 ) // ACTOR1 = PRAIRILLOT
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: NpcTrade(TargetCanMove), id=unknown
            // +Callback Scene00003: Normal(Talk, TargetCanMove), id=PRAIRILLOT
          }
          break;
        }
        if( param1 == 1006389 ) // ACTOR2 = SAISTENIOUX
        {
          if( quest.UI8BH != 1 )
          {
            Scene00004(); // Scene00004: NpcTrade(TargetCanMove), id=unknown
            // +Callback Scene00005: Normal(Talk, TargetCanMove), id=SAISTENIOUX
          }
          break;
        }
        if( param1 == 1006390 ) // ACTOR3 = TIRAULAND
        {
          if( quest.UI8BL != 1 )
          {
            Scene00006(); // Scene00006: NpcTrade(TargetCanMove), id=unknown
            // +Callback Scene00007: Normal(Talk, TargetCanMove), id=TIRAULAND
          }
          break;
        }
        if( param1 == 1006391 ) // ACTOR4 = MARTIALLAIS
        {
          if( quest.UI8CH != 1 )
          {
            Scene00008(); // Scene00008: NpcTrade(TargetCanMove), id=unknown
            // +Callback Scene00009: Normal(Talk, TargetCanMove), id=MARTIALLAIS
          }
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00010(); // Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SYLVAINTEL
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
    quest.Sequence = 2;
    quest.UI8CL = 3;
    quest.UI8DH = 3;
  }
  void checkProgressSeq1()
  {
    quest.UI8BH = 0;
    quest.UI8BL = 0;
    quest.Sequence = 2;
    quest.UI8CL = 3;
    quest.UI8DH = 3;
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
            quest.UI8DH = 0;
            quest.Sequence = 255;
          }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb611:66429 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=SYLVAINTEL" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }


private void Scene00002() //SEQ_2: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00002: NpcTrade(TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00003();
      }
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_2: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00003: Normal(Talk, TargetCanMove), id=PRAIRILLOT" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00004: NpcTrade(TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00005();
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00005() //SEQ_2: ACTOR2, UI8BH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00005: Normal(Talk, TargetCanMove), id=SAISTENIOUX" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00006: NpcTrade(TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00007();
      }
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00007() //SEQ_2: ACTOR3, UI8BL = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00007: Normal(Talk, TargetCanMove), id=TIRAULAND" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag8( 3, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00008() //SEQ_2: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00008: NpcTrade(TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00009();
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00009() //SEQ_2: ACTOR4, UI8CH = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00009: Normal(Talk, TargetCanMove), id=MARTIALLAIS" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag8( 4, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb611:66429 calling Scene00010: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=SYLVAINTEL" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
