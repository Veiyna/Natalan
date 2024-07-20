

namespace Shared.Database.Authentication
{
    public class AccountInfo
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string salt { get; set; }
        public string session { get; set; }
        
        public string Version { get; set; }
    }
}