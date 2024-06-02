using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Web.Globals;
using Boxed.AspNetCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Administration.API.Helper;
using Common.Options;
using AuthDomain.Entities.Auth;
using ResortAppStore.Services.Administration.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using dotnet_6_json_localization;
using ResortAppStore.Repositories;
using FluentValidation;
using System.Reflection;
using Administration.API.Helpers;
using CRM.Services.Helpers;
using ResortAppStore.Services.Administration.Application.Services;
using Common.Services.Service;
using Common.Repositories;
using Common.Helper;
using ResortAppStore.Services.Administration.Application.Subscription;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Repository;
using Microsoft.AspNetCore.Localization;
using Autofac.Core;
using Polly.Extensions.Http;
using Refit;
using Nashmi.Services.NPay.Data.ExternalServices.PaymentApi;
using Polly;
using ResortAppStore.Services.Administration.API.Handlers;
using ResortAppStore.Services.Administration.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServicePool(builder.Configuration, new OpenApiInfo { Title = "Administration APIs", Version = "v1" });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors",
        builder =>
        {
            builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://broadcastmp-001-site3.itempurl.com/")
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
        });
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
AppSettings appSettings = new AppSettings();
builder.Configuration.Bind("SystemSetting", appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    //options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(10);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = false;

});
builder.Services.AddAuthorization(options =>
{
});


builder.Services.AddDbContext<IdentityServiceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
      b => b
#if Sql2008
          .UseRowNumberForPaging()
#endif
          .MigrationsAssembly(typeof(IdentityServiceDbContext).Assembly.FullName));
});
builder.Services
.ConfigureAndValidateSingleton<JwtOption>(builder.Configuration.GetSection(nameof(Sections.JwtOption)))
.ConfigureAndValidateSingleton<AppInfoOption>(builder.Configuration.GetSection(nameof(Sections.AppInfoOption)))
.ConfigureAndValidateSingleton<ImageOption>(builder.Configuration.GetSection(nameof(Sections.ImageOption)))
.ConfigureAndValidateSingleton<AppSettings>(builder.Configuration.GetSection(nameof(Sections.AppSettings)));

builder.Services
         .AddIdentity<User, Role>(config =>
         {
             config.Password = new PasswordOptions
             {
                 RequiredLength = 6,
                 RequireUppercase = false,
                 RequireNonAlphanumeric = false,
                 RequireLowercase = false,
                 RequireDigit = false
             };
             config.Lockout = new LockoutOptions
             {
                 AllowedForNewUsers = true,
                 MaxFailedAccessAttempts = 3,
                 DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10)
             };
             config.User.RequireUniqueEmail = false;
         })
         .AddEntityFrameworkStores<IdentityServiceDbContext>()
         .AddDefaultTokenProviders();

builder.Services.AddScoped<DbContext, IdentityServiceDbContext>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IModuleRepo, ModuleRepo>();
builder.Services.AddScoped<IPromoCodeRespository, PromoCodeRespository>();

var jwtOption = builder.Configuration?.GetSection(nameof(Sections.JwtOption))?.Get<JwtOption>();
builder.Services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
builder.Services.AddScoped<DeleteService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly, });
//builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
//    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(),
                          
                           typeof(IGRepository<>).Assembly,
                           typeof(GRepository<>).Assembly
                          );



builder.Services.AddScoped(typeof(GMappRepository<,,>));
var repoInterfaces = DependenciesHelper.GetAssemblies().SelectMany(x => x.GetTypes()).ToList().Where(t => t.IsInterface && t.Name.EndsWith("Repository") && t.FullName.Contains("ResortAppStore")).ToList();
var repoClasses = DependenciesHelper.GetAssemblies().SelectMany(x => x.GetTypes()).ToList().Where(t => !t.IsAbstract && t.IsClass && t.Name.EndsWith("Repository") && t.FullName.Contains("ResortAppStore")).ToList();


repoInterfaces.ForEach(inter =>
{
    var cls = repoClasses.FirstOrDefault(x => inter.IsAssignableFrom(x));

    if (cls != null)
        builder.Services.AddScoped(inter, cls);
});

builder.Services.AddOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthorizationHandler, CustomRequireUserClaim>();
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddLocalization();
builder.Services.AddSingleton<LocalizationMiddleware>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

builder.Services.AddHttpClient();
var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
              .WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 2));

builder.Services.AddRefitClient<ISettingErpApi>()
 .ConfigureHttpClient(c =>
 {
     c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ExternalServices:ErpApi"));
     c.Timeout = TimeSpan.FromMinutes(5);
 })
 .AddPolicyHandler(retryPolicy);
//builder.Services.AddHostedService<SessionCleanupService>();
var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{

    serviceScope.ServiceProvider.GetService<AuthorizeByPermissionsAttribute>();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<IdentityServiceDbContext>();
    var serviceProvider = serviceScope.ServiceProvider;
    if (!serviceScope.ServiceProvider.GetService<IdentityServiceDbContext>().AllMigrationsApplied())
    {
        serviceScope.ServiceProvider.GetService<IdentityServiceDbContext>().Migrate();
    }
    //ma IntegratedRecruitmentContextSeed.Seed(dbContext, serviceProvider);
    AppDbInitializer.Initialize(dbContext);
}
EmailTemplates.Initialize(app.Environment);
DatabaseHelper.Initialize(app.Environment);
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "Adminstorator ERP"));
}

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}
app.UseApiVersioning();
app.UseDeveloperExceptionPage();
//app.UseMiddleware<ExceptionMiddleware>();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads")),
//    RequestPath = "/wwwroot/Uploads"
//});


app.UseCors("AllowCors");
var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo("ar"))
};
app.UseRequestLocalization(options);
app.UseMiddleware<LocalizationMiddleware>();
app.UseTokenValidationMiddleware();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
//app.UseIdentityServer();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public static class TokenValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenValidationMiddleware>();
    }
}