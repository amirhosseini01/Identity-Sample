﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
     new List<IdentityResource>
     {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource()
        {
            Name = "verification",
            UserClaims = new List<string>
            {
                JwtClaimTypes.Email,
                JwtClaimTypes.EmailVerified
            }
        }
     };

    // Define API Scopedotnet add ./Api/Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(name: "api1", displayName: "MyAPI")
        };

    // Define Client
    public static IEnumerable<Client> Clients =>
     new List<Client>
     {
        // machine to machine client (from quickstart 1)
        new Client
        {
            ClientId = "client",
            ClientSecrets = { new Secret("secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.ClientCredentials,
            // scopes that client has access to
            AllowedScopes = { "api1" }
        },
        // interactive ASP.NET Core Web App
        new Client
        {
            ClientId = "web",
            ClientSecrets = { new Secret("secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.Code,

            // where to redirect to after login
            RedirectUris = { "http://localhost:5002/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

            AllowOfflineAccess = true,

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "api1"
            }
        }
     };
}