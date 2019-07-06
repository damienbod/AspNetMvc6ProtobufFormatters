﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace StsServerIdentity
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apiproto")
                {
                    ApiSecrets =
                    {
                        new Secret("apiprotoSecret".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients(IConfigurationSection stsConfig)
        {
            // TODO use configs in app
            //var yourConfig = stsConfig["ClientUrl"];

            return new List<Client>
            {
                new Client
                {
                    ClientId = "ClientProtectedApi",
                    ClientName = "ClientProtectedApi",
                    ClientSecrets = new List<Secret> { new Secret { Value = "protected_api_client_secret".Sha256() } },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new List<string> { "apiproto" }
                },
            };
        }
    }
}