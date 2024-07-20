using System;

namespace WorldServer.Game.Map
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InstanceContentScriptAttribute : Attribute
    {
        public uint InstanceId { get; }

        public InstanceContentScriptAttribute(uint instanceId)
        {
            this.InstanceId = instanceId;
        }
    }
}