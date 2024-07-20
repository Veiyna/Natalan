using System;

namespace Shared.Network
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SubPacketAttribute : Attribute
    {
        public SubPacketClientHandlerId ClientHandlerId { get; }
        public SubPacketServerHandlerId ServerHandlerId { get; }
        public SubPacketType Type { get; }
        public bool Log { get; }

        public SubPacketAttribute(SubPacketClientHandlerId handlerId, bool log = true)
        {
            this.ClientHandlerId = handlerId;
            Log          = log;
        }

        public SubPacketAttribute(SubPacketServerHandlerId handlerId, bool log = true)
        {
            this.ServerHandlerId = handlerId;
            Log          = log;
        }

        public SubPacketAttribute(SubPacketType type, bool log = true)
        {
            Type = type;
            Log  = log;
        }
    }
}
