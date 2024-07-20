using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Shared.Cryptography;

namespace Shared.Network
{
    public class Packet
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Header
        {
            public const int Length = 0x28;

            public ulong Unknown1;
            public ulong Unknown2;
            public ulong Timestamp;
            public uint Size;
            public ConnectionChannel Channel;
            public ushort SubPackets;
            public byte Unknown3;
            public byte CompressionType;
            public ushort Unknown4;
            public uint oodleDecompressedSize;
        }

        public Header PacketHeader { get; private set; }
        public List<SubPacket> SubPackets { get; } = new List<SubPacket>();

        public PacketResult Process(byte[] payload, Session session, Blowfish blowfish, Oodle oodle)
        {
            if (payload.Length < Header.Length)
                return PacketResult.Malformed;

            PacketHeader = payload.Copy(0, Header.Length).UnMarshal<Header>();

            // missing data, need to wait for remaining data and combine
            if (PacketHeader.Size > payload.Length)
                return PacketResult.Fragmented;

            if (PacketHeader.SubPackets == 0)
                return PacketResult.Malformed;

            var payloadData = new byte[PacketHeader.Size - Header.Length];
            Buffer.BlockCopy(payload, Header.Length, payloadData, 0, payloadData.Length);

            if (PacketHeader.CompressionType == 1)
                if (!ZlibProvider.Deflate(payloadData, out payloadData))
                    return PacketResult.Malformed;

            if (PacketHeader.CompressionType == 2)
            {
                int oldSize = payloadData.Length;
                byte[] compressedData = (byte[])payloadData.Clone();
                Array.Clear(payloadData, 0, payloadData.Length);
                Array.Resize(ref payloadData, (int)PacketHeader.oodleDecompressedSize );
                if (!oodle.OodleDecode(compressedData,  (uint)compressedData.Length, payloadData, PacketHeader.oodleDecompressedSize ))
                    return PacketResult.Malformed;
                
            }

            using (var stream = new MemoryStream(payloadData))
            {
                using (var reader = new BinaryReader(stream))
                {
                    for (uint i = 0u; i < PacketHeader.SubPackets; i++)
                    {
                        SubPacket subPacket;
                        PacketResult result = SubPacket.Process(reader, session, blowfish, out subPacket);
                        if (result != PacketResult.Ok)
                        {
                            return result;
                        }


                        SubPackets.Add(subPacket);
                    }
                }
            }

            return PacketResult.Ok;
        }

        public static byte[] Build(Session session, Blowfish blowfish, Oodle oodle, IEnumerable<PendingSubPacket> pendingSubPackets)
        {
            ushort subPacketCount = 0;

            IEnumerable<byte> subPacketPayload = new byte[]
            {
            };

            foreach (PendingSubPacket pendingSubPacket in pendingSubPackets)
            {
                subPacketPayload = subPacketPayload.Concat(pendingSubPacket.SubPacket.Build(session, blowfish, pendingSubPacket.Source, pendingSubPacket.Target));
                subPacketCount++;

                PacketManager.CLogPacket(SubPacketDirection.Server, pendingSubPacket.SubPacket);
            }

            // TODO: compress outbound packets

            byte[] finalSubPacketPayload = subPacketPayload.ToArray();


            

            var packetHeader = new Header
            {
                Timestamp  = (ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Size       = (uint)(Header.Length + finalSubPacketPayload.Length),
                SubPackets = subPacketCount
            };
            /*
            if (oodle != null)
            {
                byte[] decompressed = (byte[])finalSubPacketPayload.Clone();
                IntPtr pEnc = oodle.OodleEncode(decompressed, decompressed.Length, finalSubPacketPayload);
                Array.Resize(ref finalSubPacketPayload, (int) pEnc );
                packetHeader.CompressionType = 2;
                packetHeader.Size = (uint)(Header.Length + finalSubPacketPayload.Length);
                packetHeader.oodleDecompressedSize = (uint)decompressed.Length;
            }
            */ 
            return packetHeader.Marshal().Concat(finalSubPacketPayload).ToArray();
        }
    }
}  
