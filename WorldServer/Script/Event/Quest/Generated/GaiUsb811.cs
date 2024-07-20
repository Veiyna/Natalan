// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66456)]
public class GaiUsb811 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_2, 13 entries
  //SEQ_3, 4 entries
  //SEQ_255, 5 entries

  //ACTOR0 = 1006438
  //ACTOR1 = 1006718
  //ENEMY0 = 4293195
  //ENEMY1 = 4293196
  //ENEMY2 = 4293197
  //ENEMY3 = 4293198
  //ENEMY4 = 4293199
  //ENEMY5 = 4293200
  //EOBJECT0 = 2002620
  //EOBJECT1 = 2002621
  //EOBJECT2 = 2002622
  //EOBJECT3 = 2002153
  //EOBJECT4 = 2002154
  //EOBJECT5 = 2002155
  //EVENTACTIONSEARCH = 1
  //EVENTACTIONSEARCHMIDDLE = 3
  //EVENTACTIONSEARCHSHORT = 2

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PAPAWAZU
        break;
      }
      case 1:
      {
        if( param1 == 1006718 ) // ACTOR1 = NPCA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=NPCA
            // +Callback Scene00003: Normal(Talk, TargetCanMove), id=NPCA
            // +Callback Scene00004: Normal(Talk, TargetCanMove), id=NPCA
          }
          break;
        }
        if( param1 == 2002620 ) // EOBJECT0 = unknown
        {
          Scene00005(); // Scene00005: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002621 ) // EOBJECT1 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002622 ) // EOBJECT2 = unknown
        {
          Scene00007(); // Scene00007: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 2:
      {
        if( param1 == 2002153 ) // EOBJECT3 = unknown
        {
          if( !quest.getBitFlag8( 1 ) )
          {
            Scene00008(); // Scene00008: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT3
        if( param1 == 4293195 ) // ENEMY0 = unknown
        {
          Scene00009(); // Scene00009: Empty(None), id=unknown
          break;
        }
        if( param1 == 4293196 ) // ENEMY1 = unknown
        {
          Scene00010(); // Scene00010: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002154 ) // EOBJECT4 = unknown
        {
          if( !quest.getBitFlag8( 2 ) )
          {
            Scene00011(); // Scene00011: Empty(None), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT4
        if( param1 == 4293197 ) // ENEMY2 = unknown
        {
          Scene00012(); // Scene00012: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 4293198 ) // ENEMY3 = unknown
        {
          Scene00013(); // Scene00013: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002155 ) // EOBJECT5 = unknown
        {
          if( !quest.getBitFlag8( 3 ) )
          {
            Scene00014(); // Scene00014: Normal(Message, PopBNpc), id=unknown
          }
          break;
        }
        // BNpcHack credit moved to EOBJECT5
        if( param1 == 4293199 ) // ENEMY4 = unknown
        {
          Scene00015(); // Scene00015: Empty(None), id=unknown
          break;
        }
        if( param1 == 4293200 ) // ENEMY5 = unknown
        {
          Scene00016(); // Scene00016: Normal(Message, PopBNpc), id=unknown
          break;
        }
        if( param1 == 1006718 ) // ACTOR1 = NPCA
        {
          Scene00017(); // Scene00017: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 2002620 ) // EOBJECT0 = unknown
        {
          Scene00019(); // Scene00019: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002621 ) // EOBJECT1 = unknown
        {
          Scene00021(); // Scene00021: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002622 ) // EOBJECT2 = unknown
        {
          Scene00023(); // Scene00023: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 3:
      {
        if( param1 == 1006718 ) // ACTOR1 = NPCA
        {
          if( quest.UI8AL != 1 )
          {
            Scene00024(); // Scene00024: Normal(Talk, TargetCanMove), id=NPCA
          }
          break;
        }
        if( param1 == 2002620 ) // EOBJECT0 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002621 ) // EOBJECT1 = unknown
        {
          Scene00028(); // Scene00028: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002622 ) // EOBJECT2 = unknown
        {
          Scene00030(); // Scene00030: Empty(None), id=unknown
          break;
        }
        break;
      }
      case 255:
      {
        if( param1 == 1006438 ) // ACTOR0 = PAPAWAZU
        {
          Scene00031(); // Scene00031: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=PAPAWAZU
          break;
        }
        if( param1 == 1006718 ) // ACTOR1 = NPCA
        {
          Scene00032(); // Scene00032: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 2002620 ) // EOBJECT0 = unknown
        {
          Scene00034(); // Scene00034: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002621 ) // EOBJECT1 = unknown
        {
          Scene00036(); // Scene00036: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002622 ) // EOBJECT2 = unknown
        {
          Scene00038(); // Scene00038: Empty(None), id=unknown
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
    if( quest.UI8AL == 2 )
      if( quest.UI8BH == 2 )
        if( quest.UI8BL == 2 )
        {
          quest.UI8AL = 0 ;
          quest.UI8BH = 0 ;
          quest.UI8BL = 0 ;
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
      quest.Sequence = 255;
    }
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb811:66456 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=PAPAWAZU" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00002: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
      Scene00003();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00003() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00003: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
      Scene00004();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_1: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:0)
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00004: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00005() //SEQ_1: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00005: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00006() //SEQ_1: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00007() //SEQ_1: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00007: Empty(None), id=unknown" );
    checkProgressSeq1();
  }

private void Scene00008() //SEQ_2: EOBJECT3, UI8AL = 2, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00008: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 2);
    quest.setBitFlag8( 1, true );
    player.SendQuestMessage(Id, 1, 2, quest.UI8AL, 2 );
    checkProgressSeq2();
  }

private void Scene00009() //SEQ_2: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00009: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00010() //SEQ_2: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00010: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00011() //SEQ_2: EOBJECT4, UI8BH = 2, Flag8(2)=True
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00011: Empty(None), id=unknown" );
    quest.UI8BH =  (byte)( 2);
    quest.setBitFlag8( 2, true );
    checkProgressSeq2();
  }

private void Scene00012() //SEQ_2: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00012: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00013() //SEQ_2: ENEMY3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00013: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00014() //SEQ_2: EOBJECT5, UI8BL = 2, Flag8(3)=True
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00014: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 2);
      quest.setBitFlag8( 3, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_2: ENEMY4, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00015: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00016() //SEQ_2: ENEMY5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00016: Normal(Message, PopBNpc), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 16, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00017() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00017: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 17, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00019() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00019: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00021() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00021: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00023() //SEQ_2: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00023: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00024() //SEQ_3: ACTOR1, UI8AL = 1, Flag8(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00024: Normal(Talk, TargetCanMove), id=NPCA" );
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
    player.sendDebug("GaiUsb811:66456 calling Scene00026: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00028() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00028: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00030() //SEQ_3: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00030: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00031() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00031: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=PAPAWAZU" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 31, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00032() //SEQ_255: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00032: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 32, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00034() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00034: Empty(None), id=unknown" );
  }

private void Scene00036() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00036: Empty(None), id=unknown" );
  }

private void Scene00038() //SEQ_255: EOBJECT2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb811:66456 calling Scene00038: Empty(None), id=unknown" );
  }
};
}
