// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66472)]
public class GaiUsb913 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 12 entries
  //SEQ_2, 1 entries
  //SEQ_3, 2 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006460
  //ENEMY0 = 4293236
  //ENEMY1 = 4293237
  //ENEMY2 = 4293238
  //ENEMY3 = 4293239
  //ENEMY4 = 4293240
  //ENEMY5 = 4293241
  //ENEMY6 = 4293242
  //ENEMY7 = 4293243
  //ENEMY8 = 4293244
  //ENEMY9 = 4293246
  //EOBJECT0 = 2002166
  //EOBJECT1 = 2002167
  //EOBJECT2 = 2002168
  //EOBJECT3 = 2002169
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONSEARCHMIDDLE = 3

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=FOUSQUENET
        break;
      }
      case 1:
      {
        if( param1 == 2002166 ) // EOBJECT0 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00003(); // Scene00003: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT0
        if( param1 == 4293236 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293237 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293238 ) // ENEMY2 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 2002167 ) // EOBJECT1 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00005(); // Scene00005: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT1
        if( param1 == 4293239 ) // ENEMY3 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293240 ) // ENEMY4 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293241 ) // ENEMY5 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 2002168 ) // EOBJECT2 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00007(); // Scene00007: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT2
        if( param1 == 4293242 ) // ENEMY6 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293243 ) // ENEMY7 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293244 ) // ENEMY8 = unknown
        {
        // empty entry
          break;
        }
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00008(); // Scene00008: Normal(Talk, TargetCanMove), id=FOUSQUENET
        break;
      }
      case 3:
      {
        if( param1 == 2002169 ) // EOBJECT3 = unknown
        {
          Scene00010(); // Scene00010: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 4293246 ) // ENEMY9 = unknown
        {
        // empty entry
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00011(); // Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=FOUSQUENET
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
    if( quest.UI8AL == 3 )
      if( quest.UI8BH == 3 )
        if( quest.UI8BL == 3 )
        {
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
          quest.setBitFlag8( 1, false );
          quest.setBitFlag8( 2, false );
          quest.setBitFlag8( 3, false );
          quest.Sequence = 2;
        }
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 3;
  }
  void checkProgressSeq3()
  {
    quest.setBitFlag8( 1, false );
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb913:66472 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb913:66472 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=FOUSQUENET" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_1: EOBJECT0, UI8AL = 3, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb913:66472 calling Scene00003: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 3);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 2, quest.UI8AL, 3 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }




private void Scene00005() //SEQ_1: EOBJECT1, UI8BH = 3, Flag8(2)=True
  {
    player.sendDebug("GaiUsb913:66472 calling Scene00005: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 3);
      quest.setBitFlag8( 2, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }




private void Scene00007() //SEQ_1: EOBJECT2, UI8BL = 3, Flag8(3)=True
  {
    player.sendDebug("GaiUsb913:66472 calling Scene00007: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 3);
      quest.setBitFlag8( 3, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }




private void Scene00008() //SEQ_2: , <No Var>, <No Flag>(Todo:1)
  {
    player.sendDebug("GaiUsb913:66472 calling Scene00008: Normal(Talk, TargetCanMove), id=FOUSQUENET" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 8, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00010() //SEQ_3: EOBJECT3, <No Var>, Flag8(1)=True
  {
    player.sendDebug("GaiUsb913:66472 calling Scene00010: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 1, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }


private void Scene00011() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb913:66472 calling Scene00011: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=FOUSQUENET" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 11, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
