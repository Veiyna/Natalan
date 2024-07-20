using System;
using System.IO;
using Shared;
using Shared.Network;
using WorldServer.Game.Event;

namespace WorldServer.Network.Message;

[SubPacket(SubPacketServerHandlerId.ServerEventYield)]
public class ServerEventYield : SubPacket
{
    public Event Event;
    public byte ResumeId;
    public uint[] Data;

    public override void Write(BinaryWriter writer)
    {
        this.Data ??= new uint[0];
        var count = (byte)this.Data.Length;
        Array.Resize(ref Data, 2);
        writer.Write(Event.Id);
        writer.Write(Event.ActiveScene.Id);
        writer.Write(this.ResumeId);
        writer.Write(count);
        writer.Write(this.Data.ToByteArray());
    }
}