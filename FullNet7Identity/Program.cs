using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FullNet7Identity.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using FullNet7Identity.Pages;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FullNet7IdentityIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'FullNet7IdentityIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<FullNet7IdentityIdentityDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FullNet7IdentityIdentityDbContext>();

builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"]!;
        microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"]!;
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Founders", policy =>
                  policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5"));

    options.AddPolicy("EditPolicy", policy =>
policy.Requirements.Add(new SameAuthorRequirement()));

});

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddSingleton<IAuthorizationHandler, AuthorizationHandlerCustom1>();
builder.Services.AddSingleton<IAuthorizationHandler, AuthorizationHandlerCustom2>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
