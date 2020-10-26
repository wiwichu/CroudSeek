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

        public static IEnumerable<ApiScope> Apis =>
            new ApiScope[]
            { new ApiScope("croudseekapi",
                "CroudSeek Api",
                new [] {"country" })
            };
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
            new ApiScope("croudseekapi",  "CroudSeek Api"    )
            };
        }
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
                    RedirectUris = { "https://localhost:44363/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44363/authentication/logout-callback" },
                    AllowedScopes = { "openid", "profile", 
                        "email", 
                        "croudseekapi" },
                    AllowedCorsOrigins = { "https://localhost:44363" },
                    RequireConsent = false
                }
            };
    }
}