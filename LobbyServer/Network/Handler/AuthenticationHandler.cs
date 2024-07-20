﻿using System;
using System.Collections.Generic;
using LobbyServer.Network.Message;
using Shared.Cryptography;
using Shared.Database;
using Shared.Database.Authentication;
using Shared.Network;

namespace LobbyServer.Network.Handler
{
    public static class AuthenticationHandler
    {
        [SubPacketHandler(SubPacketType.ClientHello)]
        public static void HandleClientHello(LobbySession session, ClientHello hello)
        {
            session.CalculateNetworkKey(hello.Time, hello.Seed);
            session.SessionHash = XxHash.CalculateHash(Guid.NewGuid().ToByteArray());
            session.Send(new ServerHello
            {
                SessionHash = session.SessionHash
            });
        }

        [SubPacketHandler(SubPacketClientHandlerId.ClientLobbyRequest, SubPacketHandlerFlags.RequiresEncryption)]
        public static void HandleClientLobbyRequest(LobbySession session, ClientLobbyRequest sessionRequest)
        {
            session.Sequence = sessionRequest.Sequence;

            var versionExplode = sessionRequest.Version.Split('+');
            // module data, version...
            if (versionExplode.Length < 1)
                return;

            var ffxivModule = string.Empty;
            foreach (var moduleVersion in versionExplode[0].Split(','))
            {
                var moduleExplode = moduleVersion.Split('/');
                if (moduleExplode.Length != 3)
                    continue;

                if (moduleExplode[0] == "ffxiv_dx11.exe")
                    ffxivModule = moduleVersion;
                    

                #if DEBUG
                    Console.WriteLine($"Module - File: {moduleExplode[0]}, Version: {moduleExplode[1]}, Digest: {moduleExplode[2]}");
                #endif
                
                /*
                if (!AssetManager.IsValidVersion(moduleExplode[0], moduleExplode[1], moduleExplode[2]))
                {
                    session.SendError(1012, 13101);
                    return;
                }
                */
            }

            session.AuthToken = new Token(sessionRequest.Token);

#if DEBUG
                Console.WriteLine($"Token: {sessionRequest.Token}");
            #endif

            session.NewEvent(new DatabaseGenericEvent<uint?>(DatabaseManager.Authentication.GetAccount(session.AuthToken.SessionId), accountId =>
            {
                DatabaseManager.Authentication.SetAccountGameVersion(accountId.Value, ffxivModule);
                if (accountId == null)
                {
                    session.SendError(1000, 13100);
                    return;
                }

                session.NewEvent(new DatabaseGenericEvent<List<ServiceAccountInfo>>(DatabaseManager.Authentication.GetServiceAccounts(accountId.Value), serviceAccounts =>
                {
                    if (serviceAccounts.Count == 0)
                    {
                        // TODO: probably not the correct error to display when no service accounts are present
                        session.SendError(1000, 13209);
                        return;
                    }

                    session.ServiceAccounts = serviceAccounts;
                    session.Send(new ServerServiceAccountList
                    {
                        Sequence        = session.Sequence,
                        ServiceAccounts = session.ServiceAccounts
                    });
                }));
            }));
        }
    }
}
