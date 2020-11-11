using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CroudSeek.Shared
{
    public static class Policies
    {
        public const string CanManageQuests = "CanManageQuests";

        public static AuthorizationPolicy CanManageQuestsPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("subscriptionlevel", "PayingUser")
                .Build();
        }
    }
}
