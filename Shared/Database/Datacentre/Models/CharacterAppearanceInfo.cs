using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using Shared.Game.Enum;


namespace Shared.Database.Datacentre
{
    [BsonIgnoreExtraElements]
    public class CharacterAppearanceInfo
    {


        public Race Race
        {
            get => (Race)Data[0];
            private set => Data[0] = (byte)value;
        }
        
        public Sex Sex
        {
            get => (Sex)Data[1];
            private set => Data[1] = (byte)value;
        }
        
        public byte Unknown
        {
            get => Data[2];
            private set => Data[2] = value;
        }

        public byte Height
        {
            get => Data[3];
            private set => Data[3] = value;
        }

        public byte Clan
        {
            get => Data[4];
            private set => Data[4] = value;
        }

        public byte Face
        {
            get => Data[5];
            private set => Data[5] = value;
        }

        public byte HairStyle
        {
            get => Data[6];
            private set => Data[6] = value;
        }

        public byte HairColourHl
        {
            get => Data[7];
            private set => Data[7] = value;
        }

        public byte SkinColour
        {
            get => Data[8];
            private set => Data[8] = value;
        }

        public byte EyeColourOdd
        {
            get => Data[9];
            private set => Data[9] = value;
        }

        public byte HairColour
        {
            get => Data[10];
            private set => Data[10] = value;
        }
        public byte Unknown2
        {
            get => Data[11];
            private set => Data[11] = value;
        }
        public byte FacialFeatures
        {
            get => Data[12];
            private set => Data[12] = value;
        }

        public byte TattooColour
        {
            get => Data[13];
            private set => Data[13] = value;
        }

        public byte Eyebrows
        {
            get => Data[14];
            private set => Data[14] = value;
        }

        public byte EyeColour
        {
            get => Data[15];
            private set => Data[15] = value;
        }

        public byte Eye
        {
            get => Data[16];
            private set => Data[16] = value;
        }

        public byte Nose
        {
            get => Data[17];
            private set => Data[17] = value;
        }

        public byte Jaw
        {
            get => Data[18];
            private set => Data[18] = value;
        }

        public byte Mouth
        {
            get => Data[19];
            private set => Data[19] = value;
        }

        public byte LipColour
        {
            get => Data[20];
            private set => Data[20] = value;
        }

        public byte TailLength
        {
            get => Data[21];
            private set => Data[21] = value;
        }

        public byte TailShape
        {
            get => Data[22];
            private set => Data[22] = value;
        }

        public byte BustSize
        {
            get => Data[23];
            private set => Data[23] = value;
        }

        public byte FacePaint
        {
            get => Data[24];
            private set => Data[24] = value;
        }

        public byte FacePaintColour
        {
            get => Data[25];
            private set => Data[25] = value;
        }
        [NotMapped]
        public JArray Array => JArray.FromObject(new List<byte>(Data));
        
        
        [NotMapped]
        public byte[] Data { get; }

        public CharacterAppearanceInfo(JArray array)
        {
            Data = array.Values<byte>().ToArray();
            
        }
        
        public CharacterAppearanceInfo()
        {
            Data = new byte[26];
        }
        

        public bool Verify()
        {
            // TODO: verify data
            return true;
        }
        
    }
}
