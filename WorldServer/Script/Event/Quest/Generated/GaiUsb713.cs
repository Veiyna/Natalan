// FFXIVTheMovie.ParserV3.11
using System.Numerics;
using Shared.Game;
using WorldServer.Game.Entity;
using WorldServer.Game.Event;

namespace WorldServer.Script.Quest
{
[EventScript(66444)]
public class GaiUsb713 : QuestScript
{

  //SEQ_0, 1 entries
  //SEQ_1, 4 entries
  //SEQ_255, 1 entries

  //ACTOR0 = 1006406
  //ENEMY0 = 4293085
  //ENEMY1 = 4293087
  //ENEMY2 = 4293088
  //EOBJECT0 = 2002134
  //ITEM0 = 2000709

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
      //seq 0 event item ITEM0 = UI8BH max stack ?
      case 0:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00000(); // Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown
        // +Callback Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MIAHMOLKOT
        break;
      }
      //seq 1 event item ITEM0 = UI8BH max stack ?
      case 1:
      {
        if( param1 == 2002134 || param1 == 0xF000000000000000/*Ground aoe hack enabled*/ ) // EOBJECT0 = unknown
        {
          if( type == EVENT_ON_TALK ) Scene00002(); // Scene00002: Normal(Inventory), id=unknown
          if( type == EVENT_ON_EVENT_ITEM ) Scene00003(); // Scene00003: Normal(Message, PopBNpc, QuestGimmickReaction), id=unknown
          break;
        }
        if( param1 == 4293085 ) // ENEMY0 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293087 ) // ENEMY1 = unknown
        {
        // empty entry
          break;
        }
        if( param1 == 4293088 ) // ENEMY2 = unknown
        {
        // empty entry
          break;
        }
        break;
      }
      case 255:
      {
        if( type != EVENT_ON_BNPC_KILL ) Scene00004(); // Scene00004: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=MIAHMOLKOT
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
    quest.UI8BH = 1;
  }
  void checkProgressSeq1()
  {
    quest.setBitFlag8( 1, false );
    quest.UI8BH = 0;
    quest.Sequence = 255;
  }

private void Scene00000() //SEQ_0: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb713:66444 calling Scene00000: Normal(QuestOffer, TargetCanMove), id=unknown" );
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
    player.sendDebug("GaiUsb713:66444 calling Scene00001: Normal(Talk, QuestAccept, TargetCanMove), id=MIAHMOLKOT" );
    var callback = (SceneResult result) =>
    {
      checkProgressSeq0();
    };
    owner.Event.NewScene( Id, 1, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }

private void Scene00002() //SEQ_1: EOBJECT0, <No Var>, Flag8(1)=True
  {
    player.sendDebug("GaiUsb713:66444 calling Scene00002: Normal(Inventory), id=unknown" );
    owner.Event.NewScene( Id, 2, SceneFlags.HIDE_HOTBAR);
  }
private void Scene00003() //SEQ_1: EOBJECT0, <No Var>, Flag8(1)=True
  {
    player.sendDebug("GaiUsb713:66444 calling Scene00003: Normal(Message, PopBNpc, QuestGimmickReaction), id=unknown" );
    var callback = (SceneResult result) =>
    {
      quest.setBitFlag8( 1, true );
      checkProgressSeq1();
    };
    owner.Event.NewScene( Id, 3, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }




private void Scene00004() //SEQ_255: , <No Var>, <No Flag>
  {
    player.sendDebug("GaiUsb713:66444 calling Scene00004: Normal(Talk, QuestReward, QuestComplete, TargetCanMove), id=MIAHMOLKOT" );
    var callback = (SceneResult result) =>
    {
      if( result.numOfResults > 0 && result.GetResult( 0 ) == 1 )
      {
        player.FinishQuest( Id, result.GetResult( 1 ) );
      }
    };
    owner.Event.NewScene( Id, 4, SceneFlags.HIDE_HOTBAR, Callback: callback );
  }
};
}
