// FFXIVTheMovie.ParserV3.11
using Shared.Game;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(65951)]
public class SubSea118 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 7 entries
  //SEQ_3, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1002274
  //ACTOR1 = 1002456
  //ACTOR2 = 1002238
  //ENEMY0 = 3781826
  //ENEMY1 = 3781831
  //ENEMY2 = 3781834
  //EOBJECT0 = 2000769
  //EOBJECT1 = 2000770
  //EOBJECT2 = 2000771
  //EVENTACTIONGATHERSHORT = 6
  //EVENTACTIONSEARCH = 1
  //ITEM0 = 2000222
  //ITEM1 = 2000223
  //ITEM2 = 2000224

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=HALDBRODA
        break;
      }
      case 1:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00001(); // Scene00001: Normal(Talk, TargetCanMove), id=FYRILSMYD
        break;
      }
      //seq 2 event item ITEM0 = UI8BL max stack 1
      //seq 2 event item ITEM1 = UI8CH max stack 1
      //seq 2 event item ITEM2 = UI8CL max stack 1
      case 2:
      {
        if( param1 == 2000769 ) // EOBJECT0 = unknown
        {
          Scene00003(); // Scene00003: Normal(Message), id=unknown
          break;
        }
        if( param1 == 3781826 ) // ENEMY0 = unknown
        {
          Scene00006(); // Scene00006: Normal(Message), id=unknown
          break;
        }
        if( param1 == 2000770 ) // EOBJECT1 = unknown
        {
          Scene00009(); // Scene00009: Normal(Message), id=unknown
          break;
        }
        if( param1 == 3781831 ) // ENEMY1 = unknown
        {
          Scene00012(); // Scene00012: Normal(Message), id=unknown
          break;
        }
        if( param1 == 2000771 ) // EOBJECT2 = unknown
        {
          Scene00015(); // Scene00015: Normal(Message), id=unknown
          break;
        }
        if( param1 == 3781834 ) // ENEMY2 = unknown
        {
          Scene00018(); // Scene00018: Normal(Message), id=unknown
          break;
        }
        if( param1 == 1002456 ) // ACTOR1 = FYRILSMYD
        {
          Scene00020(); // Scene00020: Normal(Talk, TargetCanMove), id=FYRILSMYD
          break;
        }
        break;
      }
      //seq 3 event item ITEM0 = UI8BH max stack 1
      //seq 3 event item ITEM1 = UI8BL max stack 1
      //seq 3 event item ITEM2 = UI8CH max stack 1
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00021(); // Scene00021: NpcTrade(Talk, TargetCanMove), id=unknown
        // +Callback Scene00022: Normal(Talk, NpcDespawn, TargetCanMove), id=FYRILSMYD
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      //seq 255 event item ITEM1 = UI8BL max stack 1
      //seq 255 event item ITEM2 = UI8CH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00024(); // Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AHTBYRM
        break;
      }
      default:
      {
        player.sendUrgent("Sequence {} not defined. quest.Sequence ");
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
    quest.setBitFlag8( 1, false );
    quest.setBitFlag8( 2, false );
    quest.setBitFlag8( 3, false );
    quest.UI8BL = 0;
    quest.UI8CH = 0;
    quest.UI8CL = 0;
    quest.Sequence = 3;
    quest.UI8BH = 1;
    quest.UI8BL = 1;
    quest.UI8CH = 1;
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00000: Normal(Talk, QuestOffer, QuestAccept, TargetCanMove), id=HALDBRODA" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        checkProgressSeq0();
      }
    };
    owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00001() //SEQ_1: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00001: Normal(Talk, TargetCanMove), id=FYRILSMYD" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_2: EOBJECT0, <No Var>, Flag8(1)=True
  {
    player.sendDebug("SubSea118:65951 calling Scene00003: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 1, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00006() //SEQ_2: ENEMY0, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00006: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00009() //SEQ_2: EOBJECT1, <No Var>, Flag8(2)=True
  {
    player.sendDebug("SubSea118:65951 calling Scene00009: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 2, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 9, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00012() //SEQ_2: ENEMY1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00012: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 12, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00015() //SEQ_2: EOBJECT2, <No Var>, Flag8(3)=True
  {
    player.sendDebug("SubSea118:65951 calling Scene00015: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 3, true );
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 15, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00018() //SEQ_2: ENEMY2, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00018: Normal(Message), id=unknown" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 18, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00020() //SEQ_2: ACTOR1, <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00020: Normal(Talk, TargetCanMove), id=FYRILSMYD" );
    var callback = (SceneResult result) =>
    {
    };
    owner.Event.NewScene( Id, 20, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00021() //SEQ_3: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00021: NpcTrade(Talk, TargetCanMove), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00022();
      }
    };
    owner.Event.NewScene( Id, 21, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00022() //SEQ_3: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00022: Normal(Talk, NpcDespawn, TargetCanMove), id=FYRILSMYD" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 22, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00024() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubSea118:65951 calling Scene00024: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=AHTBYRM" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 24, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
