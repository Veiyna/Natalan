using System;
using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(721044)]
    public class CmnDefBeautySalon : EventScript
    {
        
        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                if(result.GetResult(0) == 1)
                    Scene00001();
            };
            owner.Event.NewScene( Id, 0, SceneFlags.HIDE_HOTBAR, Callback: callback );
        }
        
        private void Scene00001()
        {
            var callback = (SceneResult result) =>
            {
                Scene00002();
            };
            owner.Event.NewScene( Id, 1, (SceneFlags)4165480179, Callback: callback );
        }
        
        private void Scene00002()
        {
            var callback = (SceneResult result) =>
            {
                if(result.GetResult(0) == 65535)
                    Scene00003(true);
                else
                {
                    Scene00003(false);
                }
            };
            owner.Event.NewScene( Id, 2, (SceneFlags)8192, Callback: callback );
        }
        
        private void Scene00003(bool value)
        {
            owner.Event.NewScene( Id, 3, SceneFlags.FADE_OUT | SceneFlags.HIDE_UI, null, Convert.ToByte(value));
        }
        
        public override void OnGossip(ulong actorId)
        {
            Scene00000();
        }

        public override void OnYield(byte yieldId, uint[] data)
        {
            switch (yieldId)
            {
                case 25:
                {
                    
                    break;
                }

                case 27:
                {
                    for (int i = 1; i < data.Length; i++)
                    {
                        
                        if (i > 26)
                            break;
                        var param = data[i];
                        player.Character.Appearance.Data[i-1] = (byte)param;
                        
                    }
                    break;
                }

            }
            this.owner.Event.ResumeEvent(this.Id,yieldId, 1);
            
            
        }

    }
}