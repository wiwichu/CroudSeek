// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };
                
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "9017061D-2F9E-4F1C-A52F-C34F05B2B429",
                        Username = "pat",
                        Password = "password",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Pat Mac"),
                            new Claim(JwtClaimTypes.GivenName, "Pat"),
                            new Claim(JwtClaimTypes.FamilyName, "Mac"),
                            new Claim(JwtClaimTypes.Email, "PAtMac@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://pat.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "306930BF-9CB2-4750-A753-95CCF4C55B61",
                        Username = "jack",
                        Password = "password",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Jack Cam"),
                            new Claim(JwtClaimTypes.GivenName, "Jack"),
                            new Claim(JwtClaimTypes.FamilyName, "Cam"),
                            new Claim(JwtClaimTypes.Email, "JackCam@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://jack.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                };
            }
        }
    }
}