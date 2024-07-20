// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(67206)]
public class SubFst980 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 1 entries
  //SEQ_2, 1 entries
  //SEQ_3, 1 entries
  //SEQ_4, 1 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1008948
  //ACTOR1 = 1003075
  //ACTOR2 = 5010000
  //EVENTACTIONSIGH = 4229
  //INSTANCEDUNGEON0 = 20008
  //INSTANCEDUNGEON1 = 20010
  //INSTANCEDUNGEON2 = 20009
  //ITEM0 = 2001942
  //ITEM1 = 2001943
  //ITEM2 = 2001944
  //LOCACTION0 = 241
  //LOCACTION1 = 250
  //LOCACTION2 = 4261
  //LOCACTION3 = 827
  //LOCACTION5 = 1091
  //LOCACTION6 = 936
  //LOCACTOR0 = 4311940
  //LOCACTOR1 = 4697909
  //LOCACTOR2 = 4678141
  //LOCACTOR3 = 4328412
  //LOCBGM0 = 100
  //LOCBGM1 = 101
  //LOCBGM2 = 86
  //LOCBGM3 = 89
  //LOCFACE0 = 606
  //LOCFACE1 = 612
  //LOCITEM0 = 14884
  //LOCITEM1 = 14885
  //LOCITEM2 = 14886
  //LOCITEM3 = 13775
  //LOCOBJECT0 = 2006975
  //LOCSE0 = 72
  //LOCSE1 = 80
  //LOCVFX0 = 241
  //QUEST0 = 67747
  //RITEM0 = 7883
  //RITEM1 = 6268

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
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=JALZAHN
        break;
      }
      case 1:
      {
        // empty entry
        break;
      }
      case 2:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00002(); // Scene00002: Normal(Talk, TargetCanMove), id=GEROLT
        break;
      }
      case 3:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00003(); // Scene00003: NpcTrade(Basic), id=unknown
        // +Callback Scene00004: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=GEROLT
        break;
      }
      //seq 4 event item ITEM0 = UI8CH max stack 1
      //seq 4 event item ITEM1 = UI8CL max stack 1
      //seq 4 event item ITEM2 = UI8DH max stack 1
      case 4:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00005(); // Scene00005: NpcTrade(Basic), id=unknown
        // +Callback Scene00006: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=JALZAHN
        break;
      }
      //seq 255 event item ITEM0 = UI8BH max stack 1
      //seq 255 event item ITEM1 = UI8BL max stack 1
      //seq 255 event item ITEM2 = UI8CH max stack 1
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00007(); // Scene00007: NpcTrade(Basic), id=unknown
        // +Callback Scene00008: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk, ENpcBind), id=GEROLT
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
  }
  void checkProgressSeq1()
  {
    quest.Sequence = 2;
  }
  void checkProgressSeq2()
  {
    quest.Sequence = 3;
  }
  void checkProgressSeq3()
  {
    quest.Sequence = 4;
    quest.UI8CH = 1;
    quest.UI8CL = 1;
    quest.UI8DH = 1;
  }
  void checkProgressSeq4()
  {
    quest.UI8CH = 0;
    quest.UI8CL = 0;
    quest.UI8DH = 0;
    quest.Sequence = 255;
    quest.UI8BH = 1;
    quest.UI8BL = 1;
    quest.UI8CH = 1;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00000: Normal(QuestOffer), id=unknown" );
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
    player.sendDebug("SubFst980:67206 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=JALZAHN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }


private void Scene00002() //SEQ_2: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00002: Normal(Talk, TargetCanMove), id=GEROLT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq2();
    };
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00003() //SEQ_3: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00003: NpcTrade(Basic), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00004();
      }
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00004() //SEQ_3: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00004: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=GEROLT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq3();
    };
    owner.Event.NewScene( Id, 4, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00005() //SEQ_4: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00005: NpcTrade(Basic), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00006();
      }
    };
    owner.Event.NewScene( Id, 5, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00006() //SEQ_4: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00006: Normal(Talk, FadeIn, TargetCanMove, ENpcBind), id=JALZAHN" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq4();
    };
    owner.Event.NewScene( Id, 6, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }

private void Scene00007() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00007: NpcTrade(Basic), id=unknown" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        Scene00008();
      }
    };
    owner.Event.NewScene( Id, 7, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
private void Scene00008() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("SubFst980:67206 calling Scene00008: Normal(Talk, FadeIn, QuestReward, QuestComplete, TargetCanMove, SystemTalk, ENpcBind), id=GEROLT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 8, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, Callback: callback );
  }
};
}
