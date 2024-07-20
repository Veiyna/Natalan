// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66447)]
public class GaiUsb802 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 4 entries
  //SEQ_3, 10 entries
  //SEQ_4, 7 entries
  //SEQ_255, 8 entries

  //ACTOR0 = 1006384
  //ACTOR1 = 1006412
  //ACTOR2 = 1006413
  //ACTOR3 = 1006414
  //EOBJECT0 = 2002137
  //EOBJECT1 = 2002138
  //EOBJECT2 = 2002139
  //EOBJECT3 = 2002141
  //EOBJECT4 = 2002140
  //EOBJECT5 = 2002678
  //EOBJECT6 = 2002679
  //EOBJECT7 = 2002680
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000711

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
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HAURCHEFANT
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=RICKEMAN
        break;
      }
      case 2:
      {
        if( param1 == 1006413 ) // ACTOR2 = NPCB
        {
          if( quest.UI8AL != 1 )
          {
            Scene00003(); // Scene00003: Normal(Talk, TargetCanMove), id=NPCB
          }
          break;
        }
        if( param1 == 1006414 ) // ACTOR3 = NPCA
        {
          Scene00004(); // Scene00004: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 2002137 ) // EOBJECT0 = unknown
        {
          Scene00006(); // Scene00006: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002138 ) // EOBJECT1 = unknown
        {
          Scene00008(); // Scene00008: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8CH max stack 2
      case 3:
      {
        if( param1 == 2002139 ) // EOBJECT2 = unknown
        {
          if( quest.UI8BH != 1 )
          {
            Scene00010(); // Scene00010: Normal(Message), id=unknown
          }
          break;
        }
        if( param1 == 2002141 ) // EOBJECT3 = unknown
        {
          if( quest.UI8AL != 1 )
          {
            Scene00012(); // Scene00012: Empty(None), id=unknown
          }
          break;
        }
        if( param1 == 2002140 ) // EOBJECT4 = unknown
        {
          if( quest.UI8BL != 1 )
          {
            Scene00014(); // Scene00014: Normal(Message), id=unknown
          }
          break;
        }
        if( param1 == 2002137 ) // EOBJECT0 = unknown
        {
          Scene00016(); // Scene00016: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002138 ) // EOBJECT1 = unknown
        {
          Scene00018(); // Scene00018: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006413 ) // ACTOR2 = NPCB
        {
          Scene00019(); // Scene00019: Normal(Talk, TargetCanMove), id=NPCB
          break;
        }
        if( param1 == 1006414 ) // ACTOR3 = NPCA
        {
          Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 2002678 ) // EOBJECT5 = unknown
        {
          Scene00022(); // Scene00022: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002679 ) // EOBJECT6 = unknown
        {
          Scene00024(); // Scene00024: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002680 ) // EOBJECT7 = unknown
        {
          Scene00026(); // Scene00026: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 4 event item ITEM0 = UI8BH max stack 2
      case 4:
      {
        if( param1 == 1006413 ) // ACTOR2 = NPCB
        {
          if( quest.UI8AL != 1 )
          {
            Scene00027(); // Scene00027: NpcTrade(Talk, TargetCanMove), id=unknown
            // +Callback Scene00028: Normal(Talk, TargetCanMove), id=NPCB
          }
          break;
        }
        if( param1 == 2002137 ) // EOBJECT0 = unknown
        {
          Scene00030(); // Scene00030: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002138 ) // EOBJECT1 = unknown
        {
          Scene00032(); // Scene00032: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006414 ) // ACTOR3 = NPCA
        {
          Scene00033(); // Scene00033: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 2002678 ) // EOBJECT5 = unknown
        {
          Scene00035(); // Scene00035: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002679 ) // EOBJECT6 = unknown
        {
          Scene00037(); // Scene00037: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002680 ) // EOBJECT7 = unknown
        {
          Scene00039(); // Scene00039: Empty(None), id=unknown
          break;
        }
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 2
      case 255:
      {
        if( param1 == 1006384 ) // ACTOR0 = HAURCHEFANT
        {
          Scene00040(); // Scene00040: NpcTrade(Talk, TargetCanMove), id=unknown
          // +Callback Scene00041: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HAURCHEFANT
          break;
        }
        if( param1 == 2002138 ) // EOBJECT1 = unknown
        {
          Scene00043(); // Scene00043: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002137 ) // EOBJECT0 = unknown
        {
          Scene00045(); // Scene00045: Empty(None), id=unknown
          break;
        }
        if( param1 == 1006413 ) // ACTOR2 = NPCB
        {
          Scene00046(); // Scene00046: Normal(Talk, TargetCanMove), id=NPCB
          break;
        }
        if( param1 == 1006414 ) // ACTOR3 = NPCA
        {
          Scene00047(); // Scene00047: Normal(Talk, TargetCanMove), id=NPCA
          break;
        }
        if( param1 == 2002678 ) // EOBJECT5 = unknown
        {
          Scene00049(); // Scene00049: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002679 ) // EOBJECT6 = unknown
        {
          Scene00051(); // Scene00051: Empty(None), id=unknown
          break;
        }
        if( param1 == 2002680 ) // EOBJECT7 = unknown
        {
          Scene00053(); // Scene00053: Empty(None), id=unknown
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
    if( quest.UI8BH == 1 )
      if( quest.UI8AL == 1 )
        if( quest.UI8BL == 1 )
        {
          quest.UI8BH = 0 ;
          quest.UI8AL = 0 ;
          quest.UI8BL = 0 ;
          quest.setBitFlag16( 1, false );
          quest.setBitFlag16( 2, false );
          quest.setBitFlag16( 3, false );
          quest.UI8CH = 0;
          quest.Sequence = 4;
          quest.UI8BH = 2;
        }
  }
  void checkProgressSeq4()
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
    player.sendDebug("GaiUsb802:66447 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb802:66447 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=HAURCHEFANT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: , <No Var>, <No Flag>(Todo:0)
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00002: Normal(Talk, TargetCanMove), id=RICKEMAN" );
    var callback = (SceneResult result) =>
    {
      player.SendQuestMessage(Id, 0, 0, 0, 0 );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:1)
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00003: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 1, 0, 0, 0 );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00004() //SEQ_2: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00004: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00006: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00008() //SEQ_2: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00008: Empty(None), id=unknown" );
    checkProgressSeq2();
  }

private void Scene00010() //SEQ_3: EOBJECT2, UI8BH = 1, Flag16(1)=True(Todo:2)
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00010: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BH =  (byte)( 1);
      quest.setBitFlag16( 1, true );
      player.SendQuestMessage(Id, 2, 0, 0, 0 );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 10, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_3: EOBJECT3, UI8AL = 1, Flag16(2)=True
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00012: Empty(None), id=unknown" );
    quest.UI8AL =  (byte)( 1);
    quest.setBitFlag16( 2, true );
    checkProgressSeq3();
  }

private void Scene00014() //SEQ_3: EOBJECT4, UI8BL = 1, Flag16(3)=True
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00014: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.UI8BL =  (byte)( 1);
      quest.setBitFlag16( 3, true );
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 14, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00016() //SEQ_3: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00016: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00018() //SEQ_3: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00018: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00019() //SEQ_3: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00019: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 19, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_3: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00020: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00022() //SEQ_3: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00022: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00024() //SEQ_3: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00024: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00026() //SEQ_3: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00026: Empty(None), id=unknown" );
    checkProgressSeq3();
  }

private void Scene00027() //SEQ_4: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00027: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00028();
      }
    };
    owner.Event.NewScene( Id, 27, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00028() //SEQ_4: ACTOR2, UI8AL = 1, Flag8(1)=True(Todo:3)
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00028: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
      quest.UI8AL =  (byte)( 1);
      quest.setBitFlag8( 1, true );
      player.SendQuestMessage(Id, 3, 0, 0, 0 );
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 28, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00030() //SEQ_4: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00030: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00032() //SEQ_4: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00032: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00033() //SEQ_4: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00033: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 33, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00035() //SEQ_4: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00035: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00037() //SEQ_4: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00037: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00039() //SEQ_4: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00039: Empty(None), id=unknown" );
    checkProgressSeq4();
  }

private void Scene00040() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00040: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00041();
      }
    };
    owner.Event.NewScene( Id, 40, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00041() //SEQ_255: ACTOR0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00041: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=HAURCHEFANT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 41, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00043() //SEQ_255: EOBJECT1, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00043: Empty(None), id=unknown" );
  }

private void Scene00045() //SEQ_255: EOBJECT0, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00045: Empty(None), id=unknown" );
  }

private void Scene00046() //SEQ_255: ACTOR2, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00046: Normal(Talk, TargetCanMove), id=NPCB" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 46, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00047() //SEQ_255: ACTOR3, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00047: Normal(Talk, TargetCanMove), id=NPCA" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 47, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00049() //SEQ_255: EOBJECT5, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00049: Empty(None), id=unknown" );
  }

private void Scene00051() //SEQ_255: EOBJECT6, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00051: Empty(None), id=unknown" );
  }

private void Scene00053() //SEQ_255: EOBJECT7, <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb802:66447 calling Scene00053: Empty(None), id=unknown" );
  }
};
}
