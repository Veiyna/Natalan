using System.Collections.Generic;

namespace LobbyServer.Network
{
    public class Token
    {
        public string SessionId { get; }

        private readonly Dictionary<string, string> tokenParameters = new();

        public Token(string token)
        {
            foreach (var parameter in token.Split(' '))
            {
                if (parameter.Contains('='))
                {
                    var parameterExplode = parameter.Split('=');
                    if (parameterExplode.Length == 2)
                        tokenParameters.Add(parameterExplode[0], parameterExplode[1]);
                }
                else if (SessionId == null)
                    SessionId = parameter;
            }
        }

        public bool TryGetValue(string key, out string value)
        {
            return tokenParameters.TryGetValue(key, out value);
        }
    }
}
