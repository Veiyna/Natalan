using WorldServer.Game.Entity.Enums;
using WorldServer.Game.Event;

namespace WorldServer.Script.Common
{
    [EventScript(720905)]
    public class CmnDefRetainerDesk : EventScript
    {
        public override void OnGossip(ulong actorId)
        {
            Scene00004();
        }

        private void Scene00000()
        {
            var callback = (SceneResult result) =>
            {
                switch (result.GetResult(0))
                {
                    case 1:  // hire retainer
                    {
                        if(player.GetMasterUnlock((ushort)UnlockEntry.Retainers))
                            Scene00001();
                        else
                            Scene00003();
                        break;
                    }
                    case 2: // dispatch retainer
                    {
                        Scene00006();
                        break;
                    }
                    case 3: // release a retainer
                    {
                        Scene00002();
                        break;
                    }
                    case 4: // ask about retainers
                    {
                        Scene00005();
                        break;
                    }
                    case 5: // tax rates
                    {
                        Scene00007();
                        break;
                    }
                    case 7: // appearance change
                    {
                        Scene00008();
                        break;
                    }
                    case 8: // retainer jobs
                    {
                        Scene00009();
                        break;
                    }
                }
                
            };
            owner.Event.NewScene(Id,0, (SceneFlags)8192, Callback: callback); 
        }
        
        private void Scene00001()
        {
            owner.Event.NewScene(Id,1, (SceneFlags)8192); 
        }
        
        private void Scene00002()
        {
            owner.Event.NewScene(Id,2, (SceneFlags)8192); 
        }
        private void Scene00003()
        {
            owner.Event.NewScene(Id,3, (SceneFlags)8192); 
        }
        private void Scene00004()
        {
            owner.Event.NewScene(Id,4, (SceneFlags)8192, Callback: _ => Scene00000()); 
        }
        private void Scene00005()
        {
            owner.Event.NewScene(Id,5, (SceneFlags)8192); 
        }
        private void Scene00006()
        {
            owner.Event.NewScene(Id,6, (SceneFlags)8192); 
        }
        
        private void Scene00007()
        {
            owner.Event.NewScene(Id,7, (SceneFlags)8192); 
        }
        
        private void Scene00008()
        {
            owner.Event.NewScene(Id,8, (SceneFlags)8192); 
        }
        
        private void Scene00009()
        {
            owner.Event.NewScene(Id,9, (SceneFlags)8192); 
        }
         

        public override void OnYield(byte yieldId, uint[] data)
        {
            switch (yieldId)
            {
                case 0x25: // shows retainer appearance change in list when 1
                {
                    this.owner.Event.ResumeEvent(this.Id,yieldId, 1);
                    break;
                }
                case 0x3:
                {
                    this.owner.Event.ResumeEvent(this.Id,yieldId, 0);
                    break;
                }
                
                case 0x8: // tax rate
                {
                    this.owner.Event.ResumeEvent(this.Id,yieldId);
                    break;
                }
                case 0x18:
                {
                    this.owner.Event.ResumeEvent(this.Id,yieldId, 0);
                    break;
                }
                case 0x19: // opens retainer appearance creator
                {
                    this.owner.Event.ResumeEvent(this.Id,yieldId, 1);
                    break;
                }
                case 0x0D:
                {
                    this.owner.Event.ResumeEvent(this.Id,yieldId, 0);
                    break;
                }
                default:
                {
                    this.owner.Event.ResumeEvent(this.Id,yieldId, 1);
                    break;
                }
                    
            }
            
        }

    }
}