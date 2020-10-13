// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Collections.Generic;

namespace SoftDrive.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("country",new [] {"country" })
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            { new ApiResource("croudseekapi",
                "CroudSeek Api",
                new [] {"country" })
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client
                {
                    ClientName="CroudSeek",
                    ClientId="croudseekclient",
                    AllowedGrantTypes=GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = { "https://localhost:57854/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:57854/authentication/logout-callback" },
                    AllowedScopes = { "openid", "profile", "email", "croudaeekapi", "country" },
                    AllowedCorsOrigins = { "https://localhost:57854" },
                    RequireConsent = false
                }
            };
    }
}