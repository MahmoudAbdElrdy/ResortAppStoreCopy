using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AuthDomain.Entities.Auth;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using ResortAppStore.Services.Administration.Domain.Entities.Configuration;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;

namespace ResortAppStore.Services.Administration.Infrastructure.Persistence
{
    public partial class AppDbInitializer
    {
        public void SeedAuthEverything(IdentityServiceDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                SeedUsers(context);
            }
            if (!context.Roles.Any())
            {
                SeedRoles(context);
            } 
            if (!context.CashPaymentInfos.Any())
            {
                SeedPayment(context);
            }
            if (!context.UserRoles.Any())
            {
                SeedUserRoles(context);
            }
           // if (!context.Permissions.Any())
            {
                SeedScreen(context);
                SeedPermission(context);
                SeedGeneralConfiguration(context);
            }
           // if (!context.PermissionRoles.Any())
            {
              //  SeedPermissionRole(context);
            }
        }
        public void SeedUsers(IdentityServiceDbContext context)
        {
            var id = Guid.NewGuid().ToString();
            var users = new[]
            {

                new User()
                {
                    AccessFailedCount = 0,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "mahmoudabd231@gmail.com",
                    EmailConfirmed = true,
                    Id =id,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = ("mahmoudabd231@gmail.com").ToUpper(),
                    NormalizedUserName = ("Admin").ToUpper(),
                    PasswordHash = "AQAAAAEAACcQAAAAECtxgG+poHaEKHqE2CDu2WAGow6eJJrE8fVw9X+Oz2154DZZexy98rjKPz5JvvpGRA==",
                    PhoneNumber = "01234543333",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    TwoFactorEnabled = false,
                    UserName = id,
                    FullName = "Admin",
                    NameAr = "SuperAdmin",
                    NameEn = "SuperAdmin",
                    CreatedOn=DateTime.UtcNow,
                    CreatedBy="System",
                    Code="1",
                    UserType=UserType.Technical,
                    IsAddPassword=true,
                    //password 123456
                },
            };
            context.Users.AddRange(users);

            context.SaveChanges();
        }
        public void SeedRoles(IdentityServiceDbContext context)
        {
            var roles = new List<Role>() { new Role() { Name = "SuperAdmin", Id = "1", Code = "1", NameAr = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper() } };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
        public void SeedPayment(IdentityServiceDbContext context) 
        {
            var roles = new List<CashPaymentInfo>() { new CashPaymentInfo() { 
                CreatedAt=DateTime.Now,
                Email="sales@g-mtcc.com",
                PhoneNumber= "+966 9200 17800" ,
                MobileNumber= "+966 9200 17800" ,
                CompanyAddress= "طريق الملك فهد، أحد، الدمام 32263، المملكة العربية السعودية" } };

            context.CashPaymentInfos.AddRange(roles);
            context.SaveChanges();
        }
        public void SeedUserRoles(IdentityServiceDbContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.FullName == "Admin");
            var role = context.Roles.FirstOrDefault(r => r.Name == "SuperAdmin");

            context.UserRoles.Add(new UserRole() { RoleId = role?.Id, UserId = user?.Id });
            context.SaveChanges();
        }
        private  void SeedPermission(IdentityServiceDbContext context)
        {
            string permissionAsJson = File.ReadAllText(@"wwwroot/SeedData" + Path.DirectorySeparatorChar + "PermissionSeed.json");
         
            List<Permission> permissions = JsonConvert.DeserializeObject<List<Permission>>(permissionAsJson);
         
            var permissionsDb = context.Permissions;
           
            var listInsertPermission = permissions.Where(x => !permissionsDb.Any(z => z.Id == x.Id)).ToList();
         
            if (listInsertPermission.Count > 0)
            {
                context.Permissions.AddRange(listInsertPermission);
                context.SaveChanges();
            }

  
        }
        private void SeedScreen(IdentityServiceDbContext context)
        {
            string screenAsJson = File.ReadAllText(@"wwwroot/SeedData" + Path.DirectorySeparatorChar + "ScreenSeed.json");

            List<Screen> screens = JsonConvert.DeserializeObject<List<Screen>>(screenAsJson);

            var screenDb = context.Permissions;

            var listInsert = screens.Where(x => !screenDb.Any(z => z.Id == x.Id)).ToList();

            if (listInsert.Count > 0)
            {
                context.Screens.AddRange(listInsert);
                context.SaveChanges();
            }


        }
        public void SeedGeneralConfiguration(IdentityServiceDbContext context)
        {
            // var id = Guid.NewGuid().ToString();
            var entities = new[]
            {

                new Setting()
                {

                  Id =1,
                  Code="1",
                 
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" سعر الشركة الاضافية",
                 NameEn="Additional company price ",
                 Value="5000",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                new Setting()
                {

                 Id =2,
                 Code="2",
               
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" سعر الفرع الاضافية",
                 NameEn="Additional branch price ",
                 Value="5000",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },





            };

            var enityDb = context.Settings;

            var listInsert = entities.Where(x => !enityDb.Any(z => z.Id == x.Id)).ToList();

            if (listInsert.Count > 0)
            {
                context.Settings.AddRange(listInsert);
                context.SaveChanges();
            }
            //context.GeneralConfigurations.AddRange(entities);

            //context.SaveChanges();
        }
    }

}