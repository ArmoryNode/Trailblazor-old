using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Trailblazor.Server.Data;
using Trailblazor.Server.Models;
using Trailblazor.Server.Infrastructure;

using static Trailblazor.Constants.Authentication;
using static Trailblazor.Shared.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options => {
        // Configure user image claim and identity resource
        options.ApiResources.Single().UserClaims.Add(CustomClaimTypes.Image);
        options.IdentityResources["openid"].UserClaims.Add(CustomClaimTypes.Image);
    });

builder.Services.AddAuthentication()
    .AddIdentityServerJwt()
    .AddGoogle(options =>
    {
        var authenticationSection = builder.Configuration.GetSection("Authentication")
                                                         .GetSection(ExternalProviders.Google);
        
        options.ClientId = authenticationSection.GetValue<string>(Sections.ClientId);
        options.ClientSecret = authenticationSection.GetValue<string>(Sections.ClientSecret);

        // Map inbound user claims https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/additional-claims#map-user-data-keys-and-create-claims
        options.ClaimActions.MapJsonKey(CustomClaimTypes.Image, JwtClaimTypes.Picture, "url");

        options.SaveTokens = true;
    });

// Configure Trailblazor database settings
builder.Services.Configure<TrailblazorDatabaseSettings>(builder.Configuration.GetSection(nameof(TrailblazorDatabaseSettings)));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
