// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.UI
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("order.read", "OrderManagement Api"),
                new ApiScope("manage", "Write access")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("ordermanagement", "OrderManagement Api")
                {
                    Scopes = { "order.read", "manage" },

                },

            };



        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "order.mobile",
                    ClientName = "Order mobile application",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("MobileClientSecret".Sha256()) },
                    RedirectUris = {"myapp://mauicallback"},
                    AllowedScopes = {"openid", "order.read"}
                }

            };
    }
}