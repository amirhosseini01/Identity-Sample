using System.IdentityModel.Tokens.Jwt;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "http://localhost:5001";

        options.RequireHttpsMetadata = false;

        options.ClientId = "web";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        // options.Scope.Add("verification");
        options.Scope.Add("api1");
        options.Scope.Add("offline_access");
        options.ClaimActions.MapJsonKey("email_verified", "email_verified");

        options.GetClaimsFromUserInfoEndpoint = true;

        //options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        //options.SignOutScheme = IdentityServerConstants.SignoutScheme;
        options.SaveTokens = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    })
    // .AddGoogle("Google", options =>
    // {
    //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

    //     options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    //     options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    // })
    ;


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
