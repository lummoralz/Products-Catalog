using Microsoft.EntityFrameworkCore;
using Products_Catalog.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Products_Catalog.Models;
using Products_Catalog.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

// Add services to the container.
builder.Services
    .AddScoped<ITokenService, JwtService>()
    .AddHttpContextAccessor()
    .AddControllersWithViews();

builder.Services.AddLogging(builder =>
{
    var logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs\\development.log", rollingInterval: RollingInterval.Day)
        .CreateLogger();
    builder.AddSerilog(logger);
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var settings = new JwtSettings();
        builder.Configuration.GetSection("JWT").Bind(settings);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = settings.Audience,
            ValidIssuer = settings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key!))
        };
    });

builder.Services
    .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MasterConnStr")))
    .AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseSpa(_ => { });

app.MapFallbackToFile("index.html");

app.Run();
