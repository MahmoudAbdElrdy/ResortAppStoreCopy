using Boxed.AspNetCore;
using Common.Extensions;
using Common.Helper;
using Common.Options;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Entities;
using Configuration.Repository;
using CRM.Services.Helpers;
using dotnet_6_json_localization;
using Egypt_EInvoice_Api.BLL;
using EntityFrameworkCore.UseRowNumberForPaging;
using ERPBackEnd.API.Helper;
using ERPBackEnd.API.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ErpBackEnd.Infrastructure.Persistence;
using ResortAppStore.Services.ERPBackEnd.Application.EInvoices.BLL;
using ResortAppStore.Services.ERPBackEnd.Application.Services;
using ResortAppStore.Services.ERPBackEnd.Infrastructure.Persistence;
using SaudiEinvoiceService.IRepos;
using System.Globalization;
using System.Reflection;
using Web.Globals;
using static Stimulsoft.Report.StiOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServicePool(builder.Configuration, new OpenApiInfo { Title = "ERPBackEnd APIs", Version = "v1" });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddCors(options =>
{
    //.WithOrigins("http://localhost:4200","http://broadcastmp-001-site3.itempurl.com") 
    options.AddPolicy("AllowCors", 
        builder =>
        {
            builder
                        .WithOrigins("http://localhost:4200", "http://broadcastmp-001-site3.itempurl.com")
                        //.SetIsOriginAllowed((host) => true)                        
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://broadcastmp-001-site3.itempurl.com/")
                        .SetIsOriginAllowed((host) => 
                        true)
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
builder.Services.AddDbContext<ErpDbContext>((serviceProvider, dbContextBuilder) =>
{
    var connectionStringPlaceHolder = builder.Configuration.GetConnectionString("DefaultConnection");
    //var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    //if (httpContextAccessor.HttpContext != null)
    //{
    //    var dbName = httpContextAccessor.HttpContext.Request.Headers["databaseName"].FirstOrDefault();
    //}
 
  //  var connectionString = connectionStringPlaceHolder.Replace("{dbName}", dbName);
    var connectionString = connectionStringPlaceHolder.Replace("{dbName}", "ResortERPTest");
    dbContextBuilder.UseSqlServer(connectionString,x=>x.UseRowNumberForPaging());
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
         .AddEntityFrameworkStores<ErpDbContext>()
         .AddDefaultTokenProviders();

builder.Services.AddScoped<DbContext, ErpDbContext>();
builder.Services.AddScoped<IPermissionService, PermissionService>();


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
builder.Services.AddScoped<Configuration.Repository.IBeneficiariesGroupRepository, BeneficiariesGroupRepository>();
builder.Services.AddScoped<IEInvoiceGovManager, EInvoiceGovManager>();






var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{

    serviceScope.ServiceProvider.GetService<AuthorizeByPermissionsAttribute>();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ErpDbContext>();
    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<SeedSQLScripts>>();
    var serviceProvider = serviceScope.ServiceProvider;
    if (!serviceScope.ServiceProvider.GetService<ErpDbContext>().AllMigrationsApplied())
    {
        serviceScope.ServiceProvider.GetService<ErpDbContext>().Migrate();
    }
    //ma IntegratedRecruitmentContextSeed.Seed(dbContext, serviceProvider);
    AppDbInitializer.Initialize(dbContext, logger);
}
EmailTemplates.Initialize(app.Environment);
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
var folderName = Path.Combine("wwwroot/Uploads/Company");
// Check if directory exist or not
if (Directory.Exists(folderName))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Company")),
        RequestPath = "/wwwroot/Uploads/Company"
    });

   
}
var archiceFolder = app.Configuration.GetSection("ArchiveFilePath").Value;
if (Directory.Exists(archiceFolder))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(archiceFolder),
        RequestPath = new PathString("/archive")
    });
}




app.UseCors("AllowCors");
var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo("ar"))
};
app.UseRequestLocalization(options);
app.UseMiddleware<LocalizationMiddleware>();
app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthentication();
//app.UseIdentityServer();
app.UseHttpsRedirection();

app.UseAuthorization();
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
#pragma warning restore ASP0014 // Suggest using top level route registrations
app.MapControllers();

app.Run();
