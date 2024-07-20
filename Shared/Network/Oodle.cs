using System;
using System.Runtime.InteropServices;

namespace Shared.Network
{
    public class Oodle
    {

        [DllImport("oo2net_9_win64.dll")]
        static extern bool OodleNetwork1TCP_Decode(byte[] state, byte[] shared, byte[] compressed, uint compressedSize, byte[] raw, uint rawSize);
        
        [DllImport("oo2net_9_win64.dll")]
        static extern IntPtr OodleNetwork1TCP_Encode(byte[] state, byte[] shared, byte[] raw, int rawSize, byte[] compressed);
        [DllImport("oo2net_9_win64.dll")]
        static extern void OodleNetwork1_Shared_SetWindow(byte[] data, int length, byte[] data2, int length2);
        [DllImport("oo2net_9_win64.dll")]
        static extern int OodleNetwork1TCP_State_Size();

        [DllImport("oo2net_9_win64.dll")]
        static extern int OodleNetwork1TCP_Train(byte[] state,
            byte[] shared,
            IntPtr training_packet_pointers,
            IntPtr training_packet_sizes,
            int num_training_packets);
        
        [DllImport("oo2net_9_win64.dll")]
        static extern int OodleNetwork1_Shared_Size(int bits);

        private const byte HashtableBits = 17;

        private const int WindowSize = 0x100000;

        private readonly byte[] _state;
        private readonly byte[] _shared;
        private readonly byte[] _window = new byte[WindowSize];

        public Oodle()
        {
            int stateSize = OodleNetwork1TCP_State_Size();
            int sharedSize = OodleNetwork1_Shared_Size(HashtableBits);
            _state = new byte[stateSize];
            _shared = new byte[sharedSize];
            OodleNetwork1_Shared_SetWindow(_shared, HashtableBits, _window, _window.Length);
            OodleNetwork1TCP_Train(_state, _shared, IntPtr.Zero, IntPtr.Zero, 0);
            Console.WriteLine("Oodle initialised.");
        }


        public bool OodleDecode(byte[] payload, uint compressedLength, byte[] plaintext, uint decompressedLength)
        {
            return OodleNetwork1TCP_Decode(_state, _shared, payload,
                    compressedLength, plaintext, decompressedLength);
        }

        public IntPtr OodleEncode(byte[] raw, int rawSize, byte[] compressed)
        {
            return OodleNetwork1TCP_Encode(_state, _shared, raw, rawSize, compressed);
        }


    }
}