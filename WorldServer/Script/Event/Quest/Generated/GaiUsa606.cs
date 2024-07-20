// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66304)]
public class GaiUsa606 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 9 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006236
  //ACTOR1 = 1006237
  //ENEMY0 = 4278319
  //ENEMY1 = 4278320
  //ENEMY2 = 4278321
  //EOBJECT0 = 2001969
  //EOBJECT1 = 2001970
  //EOBJECT2 = 2001971
  //EOBJECT3 = 2001972
  //EOBJECT4 = 2002269
  //EOBJECT5 = 2001973
  //EVENTACTIONPROCESSUPPERMIDDLE = 32
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000608

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=DEIDRA
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=FAWKES
        break;
      }
      //seq 2 event item ITEM0 = UI8DL max stack 6
      case 2:
      {
        if( param1 == 2001969 ) // EOBJECT0 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00004(); // Scene00004: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001970 ) // EOBJECT1 = unknown
        {
          if( quest.UI8CH != 1 )
          {
            Scene00006(); // Scene00006: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT1
        if( param1 == 4278319 ) // ENEMY0 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        if( param1 == 2001971 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00009(); // Scene00009: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001972 ) // EOBJECT3 = unknown
        {
          if( quest.UI8CL != 1 )
          {
            Scene00010(); // Scene00010: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT3
        if( param1 == 4278320 ) // ENEMY1 = unknown
        {
          Scene00011(); // Scene00011: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002269 ) // EOBJECT4 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00012(); // Scene00012: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2001973 ) // EOBJECT5 = unknown
        {
          if( quest.UI8DH != 1 )
          {
            Scene00013(); // Scene00013: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT5
        if( param1 == 4278321 ) // ENEMY2 = unknown
        {
          Scene00014(); // Scene00014: Normal(Message, PopBNpc), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 6
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00015(); // Scene00015: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00016: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=FAWKES
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
      if( quest.UI8CH == 1 )
        if( quest.UI8BH == 1 )
          if( quest.UI8CL == 1 )
            if( quest.UI8BL == 1 )
              if( quest.UI8DH == 1 )
              {
                quest.UI8AL = 0 ;
                quest.UI8CH = 0 ;
                quest.UI8BH = 0 ;
                quest.UI8CL = 0 ;
                quest.UI8BL = 0 ;
                quest.UI8DH = 0 ;
                quest.setBitFlag8( 1, false );
                quest.setBitFlag8( 2, false );
                quest.setBitFlag8( 3, false );
                quest.setBitFlag8( 4, false );
                quest.setBitFlag8( 5, false );
                quest.setBitFlag8( 6, false );
                quest.UI8DL = 0;
                quest.Sequence = 255;
                quest.UI8BH = 6;
              }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsa606:66304 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=DEIDRA" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00002: Normal(Talk, TargetCanMove), id=FAWKES" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: EOBJECT0, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00004: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 0, 0, 0 );
    checkProgressSeq2();
  }

private void Scene00006() //SEQ_2: EOBJECT1, UI8CH = 1, Flag8(2)=True
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00006: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CH =  (byte)( 1);
      quest.setBitFlag8( 2, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00007() //SEQ_2: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_2: EOBJECT2, UI8BH = 1, Flag8(3)=True
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00009: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 1);
    quest.setBitFlag8( 3, true );
    checkProgressSeq2();
  }

private void Scene00010() //SEQ_2: EOBJECT3, UI8CL = 1, Flag8(4)=True
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00010: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8CL =  (byte)( 1);
      quest.setBitFlag8( 4, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00011() //SEQ_2: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00011: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_2: EOBJECT4, UI8BL = 1, Flag8(5)=True
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00012: Empty(None), id=unknown" );
    quest.UI8BL =  (byte)( 1);
    quest.setBitFlag8( 5, true );
    checkProgressSeq2();
  }

private void Scene00013() //SEQ_2: EOBJECT5, UI8DH = 1, Flag8(6)=True
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00013: Empty(None), id=unknown" );
    quest.UI8DH =  (byte)( 1);
    quest.setBitFlag8( 6, true );
    checkProgressSeq2();
  }

private void Scene00014() //SEQ_2: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00014: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00015: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00016();
      }
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00016() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsa606:66304 calling Scene00016: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=FAWKES" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
