using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Cryptography;
using Shared.Database.Datacentre.Models;
using Shared.Game;
using Shared.Game.Enum;
using Shared.SqPack;
using ClassJob = Lumina.Excel.GeneratedSheets.ClassJob;

namespace Shared.Database.Datacentre;

public class CharacterInfoJson
{
    [JsonProperty(PropertyName = "content")]
    [JsonConverter(typeof(StringConverter))]
    public JArray Content { get; set; }
    [JsonProperty(PropertyName = "classname")]
    public string ClassName { get; set; }
    [JsonProperty(PropertyName = "classid")]
    public uint ClassId { get; set; }
}

[BsonIgnoreExtraElements]
public class CharacterInfo
{
    // forename and surname together can't total more than 20 (21 including space) characters
    // forename and surname separately must be between 2 and 15 characters
    // only the first character should be uppercase
    // hyphens and apostrophes can't be used in succession or placed immediatley before or after the other
    private static readonly Regex nameRegex = new Regex(@"^(?=.{5,21})^(?=.{2,15}\s.{2,15})^(?!.*?[-']{2})^(?:[A-Z][-'a-z]+)\s(?:[A-Z][-'a-z]+)$");
        
    [BsonRepresentation(BsonType.Int64, AllowOverflow = true)]
    public ulong Id { get; set; }

    public uint AccountId { get; set; }
    [BsonRepresentation(BsonType.Int32, AllowOverflow = true)]
    public uint ActorId { get; set; }
        
    public ushort CurrentRealmId { get; set; }
    public ushort RealmId { get; set; }
    public string Name { get; set; }
        
        
    public CharacterAppearanceInfo Appearance { get; set; }
    public CharacterProgressionInfo Progression { get; set; }
    public CharacterSocialInfo SocialInfo { get; set; }
    public CharacterAdventurerPlate AdventurerPlate { get; set; }
    public CharacterCompanionInfo CompanionInfo { get; set; }
    public CharacterGlamourInfo GlamourInfo { get; set; }
    public CharacterEurekaInfo EurekaInfo { get; set; }
        
    public ushort TerritoryId { get; set; }
    public float LocationX { get; set; }
    public float LocationY { get; set; }
    public float LocationZ { get; set; }

    public byte Voice { get; set; }

    public byte Guardian { get; set; }

    public byte BirthMonth { get; set; }

    public byte BirthDay { get; set; }

    public byte ClassJobId { get; set; }

    public byte ClassId => (byte)GameTableManager.ClassJobs.GetRow(this.ClassJobId)!.ClassJobParent.Row;

    public ushort CurrentTitle { get; set; }
    public byte EquipDisplayFlags { get; set; }
        
    public byte State { get; set; }
    public uint StateParam { get; set; }
        
    public byte[] Pose { get; set; }
        
    public byte Homepoint { get; set; }
        
    public uint Mount { get; set; }
        
    public ushort Companion { get; set; }
        
    public bool IsNewAdventurer { get; set; }
    public List<CharacterClassInfo> Classes { get; set; }
        
    public ICollection<ItemModel> Item { get; set; } = new HashSet<ItemModel>();
        
    public List<QuestModel> Quests { get; set; } = new();
        
    public PlayerFlagsCu FlagsCu { get; set; } = PlayerFlagsCu.FirstLogin;

    public byte OpeningSequence { get; set; }

    public byte StartTown { get; set; } = 2;
        

    public CharacterClassInfo Class => Classes[GameTableManager.ClassJobs.GetRow(ClassJobId).ExpArrayIndex];

    public byte GrandCompany { get; set; }
        

    public byte[] GrandCompanyRanks { get; set; } = new byte[3];

    public byte CurrentGrandCompanyRank => (byte)(this.GrandCompany != 0 ? this.GrandCompanyRanks[this.GrandCompany - 1] : 0);

    public byte RemakeFlags { get; set; }
    public uint Glasses { get; set; }
        
        

    public CharacterInfo(uint serviceAccountId, ushort realmId, string name, string json)
    {
        AccountId  = serviceAccountId;
        CurrentRealmId = realmId;
        RealmId    = realmId;
        Name       = name;

        var characterInfo = JsonProvider.DeserializeObject<CharacterInfoJson>(json);
        Progression = new CharacterProgressionInfo
        {
            MasterMask = new BitArray(64 * 8, false),
            Aetheryte = new BitArray(BitsToBytes(GameTableManager.Aetheryte.RowCount) * 8, false),
            HowTo = new BitArray(BitsToBytes(GameTableManager.HowTo.RowCount) * 8, false),
            Minion = new BitArray(BitsToBytes(GameTableManager.Companion.RowCount) * 8, false),
            Mount = new BitArray(BitsToBytes((uint)GameTableManager.Mount.Max(row => row.Order)) * 8, false),
            Cutscene = new BitArray(154 * 8, false),//new BitArray(BitsToBytes(GameTableManager.CutsceneWorkIndex.Max(row => row.WorkIndex)) * 8, true),
            Discovery = new BitArray(480 * 8, false),
            Quest = new BitArray(727 * 8, false)
        };
        Appearance = new CharacterAppearanceInfo(characterInfo.Content.Value<JArray>(0));
        AdventurerPlate = new CharacterAdventurerPlate();
        SocialInfo = new CharacterSocialInfo
        {
            SearchComment = "",
            SelectRegion = 2,
        };

        CompanionInfo = new CharacterCompanionInfo();
        GlamourInfo = new CharacterGlamourInfo();
        EurekaInfo = new CharacterEurekaInfo();
        var data       = characterInfo.Content.Skip(1).Values<byte>().ToArray();
        Voice = data[0];
        Guardian = data[1];
        BirthMonth = data[2];
        BirthDay = data[3];
        ClassJobId = data[4];
        Classes = new List<CharacterClassInfo>();
        EquipDisplayFlags = new byte();
        State = 1;
        IsNewAdventurer = true;
        Pose = new byte[7];
        Homepoint = 8;
    }

    public void FixUp()
    {
    }
        
    public int BitsToBytes(uint bits)
    {
        return (int)(bits + 7) >> 3;
    }
    public void Finalise(ulong id, WorldPosition position)
    {
        Id            = id;
        ActorId       = XxHash.CalculateHash(Encoding.UTF8.GetBytes($"{id:X8}:{Name}"));
        TerritoryId = position.TerritoryId;
        LocationX = position.Offset.X;
        LocationY = position.Offset.Y;
        LocationZ = position.Offset.Z;
    }

    public void AddClassInfo(byte classId)
    {
        var classInfo = new CharacterClassInfo(classId);
        Classes.Add(classInfo);
    }
        
    public CharacterClassInfo GetClassInfo(uint classJobId)
    {
        if (!GameTableManager.ClassJobs.TryGetValue(classJobId, out ClassJob classJobEntry))
            throw new ArgumentException($"Invalid ClassJobId: {classJobId}!");

        if (this.Classes.All(e => e.classId != classJobEntry.ExpArrayIndex))
        {
            AddClassInfo((byte)classJobEntry.ExpArrayIndex);
        }
                
        return Classes.First(e => e.classId == classJobEntry.ExpArrayIndex);
    }

    public bool Verify()
    {
        // TODO: verify remaining data
        if (!VerifyName(Name))
            return false;

        if (!Appearance.Verify())
            return false;

        return true;
    }

    public static bool VerifyName(string name)
    {
        return nameRegex.IsMatch(name);
    }
        
    public string BuildJsonData()
    {
        var equipped = Item.Where(i => i.ContainerType == 1000);
        var itemModels = equipped.ToList();
        var characterJson = new CharacterInfoJson
        {
            Content = new JArray
            {
                Name,
                ClassJobId,
                new JArray(Classes.Select(e => e.Level)),
                0,
                0,
                0,
                BirthMonth,
                BirthDay,
                Guardian,
                0,
                0,
                TerritoryId,
                0,
                Appearance.Array,
                itemModels.FirstOrDefault(i => i.Slot == 0)?.GetModel() ?? 0, // main hand
                itemModels.FirstOrDefault(i => i.Slot == 1)?.GetModel() ?? 0, // off hand
                new JArray
                {
                    itemModels.FirstOrDefault(i => i.Slot == 2)?.GetModel() ?? 0, // head
                    itemModels.FirstOrDefault(i => i.Slot == 3)?.GetModel() ?? 0, // body
                    itemModels.FirstOrDefault(i => i.Slot == 4)?.GetModel() ?? 0, // hands
                    itemModels.FirstOrDefault(i => i.Slot == 6)?.GetModel() ?? 0, // legs
                    itemModels.FirstOrDefault(i => i.Slot == 7)?.GetModel() ?? 0, // feet
                    itemModels.FirstOrDefault(i => i.Slot == 8)?.GetModel() ?? 0, // ear
                    itemModels.FirstOrDefault(i => i.Slot == 9)?.GetModel() ?? 0, // neck
                    itemModels.FirstOrDefault(i => i.Slot == 10)?.GetModel() ?? 0, // wrist
                    itemModels.FirstOrDefault(i => i.Slot == 11)?.GetModel() ?? 0, // ring RH
                    itemModels.FirstOrDefault(i => i.Slot == 12)?.GetModel() ?? 0, // ring LH
                },
                FlagsCu.HasFlag(PlayerFlagsCu.FirstLogin) ? 0 : 1,
                0,
                0, // ORIGINAL 7 DAWNTRAIL
                RemakeFlags, // flags (0x01: legacy class and edit, 0x04: edit)
                EquipDisplayFlags,
                0,
                "",
                0,
                0
            },
            ClassName = "ClientSelectData",
            ClassId   = 116
        };

        return JsonProvider.SerializeObject(characterJson);
    }
}