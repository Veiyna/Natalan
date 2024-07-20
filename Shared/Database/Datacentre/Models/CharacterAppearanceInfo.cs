using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using Shared.Game.Enum;

namespace Shared.Database.Datacentre.Models;

[BsonIgnoreExtraElements]
public class CharacterAppearanceInfo
{


    public Race Race
    {
        get => (Race)this.Data[0];
        private set => this.Data[0] = (byte)value;
    }
        
    public Sex Sex
    {
        get => (Sex)this.Data[1];
        private set => this.Data[1] = (byte)value;
    }
        
    public byte Unknown
    {
        get => this.Data[2];
        private set => this.Data[2] = value;
    }

    public byte Height
    {
        get => this.Data[3];
        private set => this.Data[3] = value;
    }

    public byte Clan
    {
        get => this.Data[4];
        private set => this.Data[4] = value;
    }

    public byte Face
    {
        get => this.Data[5];
        private set => this.Data[5] = value;
    }

    public byte HairStyle
    {
        get => this.Data[6];
        private set => this.Data[6] = value;
    }

    public byte HairColourHl
    {
        get => this.Data[7];
        private set => this.Data[7] = value;
    }

    public byte SkinColour
    {
        get => this.Data[8];
        private set => this.Data[8] = value;
    }

    public byte EyeColourOdd
    {
        get => this.Data[9];
        private set => this.Data[9] = value;
    }

    public byte HairColour
    {
        get => this.Data[10];
        private set => this.Data[10] = value;
    }
    public byte Unknown2
    {
        get => this.Data[11];
        private set => this.Data[11] = value;
    }
    public byte FacialFeatures
    {
        get => this.Data[12];
        private set => this.Data[12] = value;
    }

    public byte TattooColour
    {
        get => this.Data[13];
        private set => this.Data[13] = value;
    }

    public byte Eyebrows
    {
        get => this.Data[14];
        private set => this.Data[14] = value;
    }

    public byte EyeColour
    {
        get => this.Data[15];
        private set => this.Data[15] = value;
    }

    public byte Eye
    {
        get => this.Data[16];
        private set => this.Data[16] = value;
    }

    public byte Nose
    {
        get => this.Data[17];
        private set => this.Data[17] = value;
    }

    public byte Jaw
    {
        get => this.Data[18];
        private set => this.Data[18] = value;
    }

    public byte Mouth
    {
        get => this.Data[19];
        private set => this.Data[19] = value;
    }

    public byte LipColour
    {
        get => this.Data[20];
        private set => this.Data[20] = value;
    }

    public byte TailLength
    {
        get => this.Data[21];
        private set => this.Data[21] = value;
    }

    public byte TailShape
    {
        get => this.Data[22];
        private set => this.Data[22] = value;
    }

    public byte BustSize
    {
        get => this.Data[23];
        private set => this.Data[23] = value;
    }

    public byte FacePaint
    {
        get => this.Data[24];
        private set => this.Data[24] = value;
    }

    public byte FacePaintColour
    {
        get => this.Data[25];
        private set => this.Data[25] = value;
    }
    [NotMapped]
    public JArray Array => JArray.FromObject(new List<byte>(this.Data));
        
        
    [NotMapped]
    public byte[] Data { get; }

    public CharacterAppearanceInfo(JArray array)
    {
        this.Data = array.Values<byte>().ToArray();
            
    }
        
    public CharacterAppearanceInfo()
    {
        this.Data = new byte[26];
    }
        

    public bool Verify()
    {
        // TODO: verify data
        return true;
    }
        
}