using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Database.Datacentre.Models
{
    [BsonIgnoreExtraElements]
    public class QuestModel
    {
        public ulong Slot { get; set; }
        public ushort QuestId { get; set; }
        public byte Sequence { get; set; } = 1;
        public byte Flags { get; set; }
        public byte Data1 { get; set; }
        public byte Data2 { get; set; }
        public byte Data3 { get; set; }
        public byte Data4 { get; set; }
        public byte Data5 { get; set; }
        public byte Data6 { get; set; }
        
        [BsonIgnore]
        public byte UI8AL
        {
            get => (byte)(Data1 & 0x0f);
            set => Data1 = (byte)( (Data1 & 0xF0) | (value & 0x0f));
        }
        [BsonIgnore]
        public byte UI8AH
        {
            get => (byte)(Data1 >> 4);
            set => Data1 = (byte)((value << 4) | (Data1 & 0x0f));
        }
        [BsonIgnore]
        public byte UI8BL
        {
            get => (byte)(Data2 & 0x0f);
            set => Data2 = (byte)( (Data2 & 0xF0) | (value & 0x0f));
        }
        [BsonIgnore]
        public byte UI8BH
        {
            get => (byte)(Data2 >> 4);
            set => Data2 = (byte)((value << 4) | (Data2 & 0x0f));
        }
        [BsonIgnore]
        public byte UI8CL
        {
            get => (byte)(Data3 & 0x0f);
            set => Data3 = (byte)( (Data3 & 0xF0) | (value & 0x0f));
        }
        [BsonIgnore]
        public byte UI8CH
        {
            get => (byte)(Data3 >> 4);
            set => Data3 = (byte)((value << 4) | (Data3 & 0x0f));
        }
        [BsonIgnore]
        public byte UI8DL
        {
            get => (byte)(Data4 & 0x0f);
            set => Data4 = (byte)( (Data4 & 0xF0) | (value & 0x0f));
        }
        [BsonIgnore]
        public byte UI8DH
        {
            get => (byte)(Data4 >> 4);
            set => Data4 = (byte)((value << 4) | (Data4 & 0x0f));
        }
        [BsonIgnore]
        public byte UI8EL
        {
            get => (byte)(Data5 & 0x0f);
            set => Data5 = (byte)( (Data5 & 0xF0) | (value & 0x0f));
        }
        [BsonIgnore]
        public byte UI8EH
        {
            get => (byte)(Data5 >> 4);
            set => Data5 = (byte)((value << 4) | (Data5 & 0x0f));
        }
        [BsonIgnore]
        public byte UI8FL
        {
            get => (byte)(Data6 & 0x0f);
            set => Data6 = (byte)( (Data6 & 0xF0) | (value & 0x0f));
        }
        [BsonIgnore]
        public byte UI8FH
        {
            get => (byte)(Data5 >> 4);
            set => Data5 = (byte)((value << 4) | (Data5 & 0x0f));
        }


        public bool getBitFlag8(byte index)
        {
            byte realIdx = (byte)(8 - index);
            return (Data6 & ( 1 << realIdx )) == 1;
        }
        
        public bool getBitFlag16(byte index)
        {
            byte realIdx = (byte)(8 - index);
            return (Data5 & ( 1 << realIdx )) == 1;
        }
        
        public bool getBitFlag24(byte index)
        {
            byte realIdx = (byte)(8 - index);
            return (Data5 & ( 1 << realIdx )) == 1;
        }
        
        public bool getBitFlag32(byte index)
        {
            byte realIdx = (byte)(8 - index);
            return (Data4 & ( 1 << realIdx )) == 1;
        }
        
        public bool getBitFlag48(byte index)
        {
            byte realIdx = (byte)(8 - index);
            return (Data3 & ( 1 << realIdx )) == 1;
        }
        
        public void setBitFlag8(byte index, bool val)
        {
            byte realIdx = (byte)(8 - index);
            if (val)
                Data6 |= (byte)(1 << realIdx);
            else
            {
                Data6 &= (byte)~(1 << realIdx);
            }
        }
        
        public void setBitFlag16(byte index, bool val)
        {
            byte realIdx = (byte)(8 - index);
            if (val)
                Data5 |= (byte)(1 << realIdx);
            else
            {
                Data5 &= (byte)~(1 << realIdx);
            }
        }
        
        public void setBitFlag24(byte index, bool val)
        {
            byte realIdx = (byte)(8 - index);
            if (val)
                Data4 |= (byte)(1 << realIdx);
            else
            {
                Data4 &= (byte)~(1 << realIdx);
            }
        }
        
        public void setBitFlag32(byte index, bool val)
        {
            byte realIdx = (byte)(8 - index);
            if (val)
                Data3 |= (byte)(1 << realIdx);
            else
            {
                Data3 &= (byte)~(1 << realIdx);
            }
        }
        public void setBitFlag40(byte index, bool val)
        {
            byte realIdx = (byte)(8 - index);
            if (val)
                Data2 |= (byte)(1 << realIdx);
            else
            {
                Data2 &= (byte)~(1 << realIdx);

            }
            
        }



    }
}