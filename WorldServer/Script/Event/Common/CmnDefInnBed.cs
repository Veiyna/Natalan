using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(720916)]
    public class CmnDefInnBed : EventScript
    {

        private void Scene00000()
        {
            owner.Event.NewScene(this.Id,0, SceneFlags.HIDE_HOTBAR, result =>
            {
                if (result.param2 > 0)
                {
                    if (result.param2 is 3 or 4)
                    {
                        Scene00002(result.param2);
                    }
                    else if (result.param2 == 2)
                    {
                        Scene00003();
                    }
                }
            });
        }

        //Going to sleep
        private void Scene00001(ushort param)
        {
            owner.Event.NewScene(this.Id,1, (SceneFlags)4164955891, result =>
            {
                if (result.param2 > 0)
                {
                    Scene00001(result.param2);
                }
            });
        }
        
        //Logout or exit game
        private void Scene00002(ushort logoutParam)
        {
            owner.Event.NewScene(this.Id,2, (SceneFlags)4165480179, null, 1, logoutParam);
        }
        
        //Dreamfitting
        private void Scene00003()
        {
            owner.Event.NewScene(this.Id,3, (SceneFlags)4165480179, Callback: _ =>
            {
                Scene00100();
            });
        }
        
        //Waking up
        private void Scene00100()
        {
            owner.Event.NewScene(this.Id,100, (SceneFlags)4164955891);
        }
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }
        


    }
}