using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721028)]
    public class CmnDefCutSceneReplay : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                if(result.param2 != 0)
                    Scene00001(result.param2);
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, callback, 1);
        }
        
        private void Scene00001(ushort scene)
        {
            var callback = (SceneResult result) =>
            {
                Scene00000();
            };
            owner.Event.NewScene( Id, 1, SceneFlags.FADE_OUT | SceneFlags.CONDITION_CUTSCENE | SceneFlags.HIDE_UI, callback, 1, scene );
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        
    }
}