namespace WorldServer.Data;

    public class BNpcTemplate
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string TerritoryName { get; set; }
        public string name { get; set; }
        public uint instanceId { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public double Rotation { get; set; }
        public uint BaseId { get; set; }
        public uint PopWeather { get; set; }
        public byte PopTimeStart { get; set; }
        public byte PopTimeEnd { get; set; }
        public uint MoveAI { get; set; }
        public byte WanderingRange { get; set; }
        public byte Route { get; set; }
        public ushort EventGroup { get; set; }
        public uint NameId { get; set; }
        public uint DropItem { get; set; }
        public float SenseRangeRate { get; set; }
        public ushort Level { get; set; }
        public byte ActiveType { get; set; }
        public byte PopInterval { get; set; }
        public byte PopRate { get; set; }
        public byte PopEvent { get; set; }
        public byte LinkGroup { get; set; }
        public byte LinkFamily { get; set; }
        public byte LinkRange { get; set; }
        public byte LinkCountLimit { get; set; }
        public sbyte NonpopInitZone { get; set; }
        public sbyte InvalidRepop { get; set; }
        public sbyte LinkParent { get; set; }
        public sbyte LinkOverride { get; set; }
        public sbyte LinkReply { get; set; }
        public sbyte Nonpop { get; set; }
        public float HorizontalPopRange { get; set; }
        public float VerticalPopRange { get; set; }
        public uint BNpcBaseData { get; set; }
        public byte RepopId { get; set; }
        public byte BNPCRankId { get; set; }
        public ushort TerritoryRange { get; set; }
        public uint BoundInstanceID { get; set; }
        public uint FateLayoutLabelId { get; set; }
        public uint NormalAI { get; set; }
        public uint ServerPathId { get; set; }
        public uint EquipmentID { get; set; }
        public uint CustomizeID { get; set; }
    }