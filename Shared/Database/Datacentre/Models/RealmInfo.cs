namespace Shared.Database.Datacentre;

public class RealmInfo
{
    public ushort Id { get; set; }
    public string Name { get; set; }
    public uint Flags { get; set; }
    public string Host { get; set; }
    public ushort Port { get; set; }
        
}