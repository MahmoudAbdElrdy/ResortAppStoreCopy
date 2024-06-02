
using Common.Enums;
using Common.SharedDto;
using Configuration.Entities;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResortAppStore.Services.ErpBackEnd.Infrastructure.Persistence;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using static Azure.Core.HttpHeader;

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Persistence
{
    public partial class AppDbInitializer
    {
        public void SeedAuthEverything(ErpDbContext context)
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
            if (!context.UserRoles.Any())
            {
                SeedUserRoles(context);
            }
            if (!context.Companies.Any())
            {
                //  SeedCompanies(context);
            }
            if (!context.Subscriptions.Any())
            {
                SeedSubscriptions(context);
            }
            //  if (!context.GeneralConfigurations.Any())
            {
                SeedGeneralConfiguration(context);
            }
            // if (!context.Permissions.Any())
            {
                SeedScreen(context);
                SeedPermission(context);
                SeedPermissionRole(context);
                SeedReportFile(context);
                //SeedBranch(context);

            }

        }
        public void SeedUsers(ErpDbContext context)
        {

            var id = Guid.NewGuid().ToString();
            var companies = new[]
            {
    new Company
    {
        Email = "mahmoudabd231@gmail.com",
        NameAr = "First Company",
        NameEn = "First Company",
        CreatedAt = DateTime.UtcNow,
        CreatedBy = "System",
        Code = "1",
        IsActive = true,
        IsDeleted = false,
        WebSite = "http://gold.com"
    }
};

            // Make sure to save the companies to the database first
            context.Companies.AddRange(companies);
            context.SaveChanges();

            var branches = new[]
            {
    new Branch()
    {
        NameAr = "Branch Ar",
        NameEn = "Branch En",
        CreatedAt = DateTime.UtcNow,
        CreatedBy = "System",
        Code = "1",
        IsActive = true,
        IsDeleted = false,
        CompanyId = companies[0].Id // Set the correct CompanyId from the existing Companies
    }
};

            var users = new[]
            {
    new User()
    {
        AccessFailedCount = 0,
        ConcurrencyStamp = Guid.NewGuid().ToString(),
        Email = "mahmoudabd231@gmail.com",
        EmailConfirmed = true,
        Id = id,
        LockoutEnabled = false,
        LockoutEnd = null,
        NormalizedEmail = ("mahmoudabd231@gmail.com").ToUpper(),
        NormalizedUserName = ("Admin").ToUpper(),
        PasswordHash = "AQAAAAIAAYagAAAAEC2Fz5Rgkm9NYuR1bEAiOVjjddSlLyKPyRvX7cc9r56BGaL1Ue7LZHuNYfSGlQfqxQ==",//12345678
        PhoneNumber = "01234543333",
        PhoneNumberConfirmed = true,
        SecurityStamp = Guid.NewGuid().ToString(),
        TwoFactorEnabled = false,
        UserName = id,
        FullName = "Admin",
        NameAr = "SuperAdmin",
        NameEn = "SuperAdmin",
        CreatedOn = DateTime.UtcNow,
        CreatedBy = "System",
        Code = "1",
        UserType = UserType.Technical,
        IsAddPassword = true,
        LoginCount = 1,
        UsersCompanies = new List<UsersCompany>()
        {
            new UsersCompany()
            {
                Company = companies[0],
                UserCompaniesBranchs = new List<UserCompaniesBranch>()
                {
                    new UserCompaniesBranch()
                    {
                        Branch = branches[0] // Set the Branch object here
                    }
                }
            }
        }
    }
};

            context.Branches.AddRange(branches);
            context.Users.AddRange(users);
            context.SaveChanges();
        }
        public void SeedRoles(ErpDbContext context)
        {
            var roles = new List<Role>() { new Role() { Name = "SuperAdmin", Id = "1", Code = "1", NameAr = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper() } };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
        public void SeedUserRoles(ErpDbContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.FullName == "Admin"|| u.FullName ==null);
            var role = context.Roles.FirstOrDefault(r => r.Name == "SuperAdmin");
            if (user != null && role != null)
            {
                context.UserRoles.Add(new UserRole() { RoleId = role?.Id, UserId = user?.Id });
                context.SaveChanges();

            }
          
        }
        private void SeedPermission(ErpDbContext context)
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
        private void SeedScreen(ErpDbContext context)
        {
            string screenAsJson = File.ReadAllText(@"wwwroot/SeedData" + Path.DirectorySeparatorChar + "ScreenSeed.json");

            List<Screen> screens = JsonConvert.DeserializeObject<List<Screen>>(screenAsJson);

            var screenDb = context.Screens;

            var listInsert = screens.Where(x => !screenDb.Any(z => z.Id == x.Id)).ToList();

            if (listInsert.Count > 0)
            {
                context.Screens.AddRange(listInsert);
                context.SaveChanges();
            }


        }
        private void SeedPermissionRole(ErpDbContext context)
        {
            string permissionRoleAsJson = File.ReadAllText(@"wwwroot/SeedData" + Path.DirectorySeparatorChar + "PermissionRoleSeed.json");

            List<PermissionRole> permissionRoles = JsonConvert.DeserializeObject<List<PermissionRole>>(permissionRoleAsJson);

            var permissionRoleDb = context.PermissionRoles;

            var listInsert = permissionRoles.Where(x => !permissionRoleDb.Any(z => z.RoleId == x.RoleId && x.PermissionId == z.PermissionId)).ToList();

            if (listInsert.Count > 0)
            {
                context.PermissionRoles.AddRange(listInsert);
                context.SaveChanges();
            }


        }

        public void SeedSubscriptions(ErpDbContext context)
        {
            // var id = Guid.NewGuid().ToString();
            var entities = new[]
            {

                new Subscriptions()
                {

                    Id =1,
                    Applications="5",
                    CreatedAt=DateTime.UtcNow,
                    CreatedBy="System",
                    ContractEndDate=DateTime.UtcNow,
                    ContractStartDate=DateTime.UtcNow,
                    NumberOfBranch=9,
                    NumberOfCompany=2,
                    IsActive=true,
                    IsDeleted=false,

                },

            };
            context.Subscriptions.AddRange(entities);

            context.SaveChanges();
        }
        public void SeedGeneralConfiguration(ErpDbContext context)
        {
            // var id = Guid.NewGuid().ToString();
            var entities = new[]
            {

                new GeneralConfiguration()
                {

                  Id =1,
                  Code="1",
                  ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="العملة الرئيسية",
                 NameEn="Main Currency",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                new GeneralConfiguration()
                {

                 Id =2,
                 Code="2",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="متعدد العملات ",
                 NameEn="Multi Currency",
                 Value="",
                 ValueType=ValueTypeEnum.Boolean,
                 IsActive=true,
                 IsDeleted=false,

                },

                 new GeneralConfiguration()
                {

                 Id =3,
                 Code="3",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" ترقيم القيود المحاسبية ",
                 NameEn="Journal Entries Serial",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },

                 new GeneralConfiguration()
                {

                 Id =4,
                 Code="4",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" دورة القيد المالي ",
                 NameEn="Financial Entry Cycle",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                  new GeneralConfiguration()
                {

                 Id =5,
                 Code="5",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" حساب اغلاق العام ",
                 NameEn="Closing Account",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                new GeneralConfiguration()
                {

                 Id =6,
                 Code="6",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="حساب أوراق القبض ",
                 NameEn="Account Receivables",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                new GeneralConfiguration()
                {

                 Id =7,
                 Code="7",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" الفترة المحاسبية ",
                 NameEn="Accounting Period",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },

                  new GeneralConfiguration()
                {

                 Id =8,
                 Code="8",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="حساب أوراق الصرف ",
                 NameEn="Account Exchange",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                    new GeneralConfiguration()
                {

                  Id =1000,
                  Code="1000",
                  ModuleType=ModuleType.FixedAssets,
                  CreatedAt=DateTime.UtcNow,
                  CreatedBy="System",
                  NameAr=" يومية الأصول الثابتة",
                  NameEn="Fixed Assets Journal ",
                  Value="1",
                  ValueType=ValueTypeEnum.Intger,
                  IsActive=true,
                  IsDeleted=false,

                },
                new GeneralConfiguration()
                {

                 Id =1001,
                 Code="1001",
                 ModuleType=ModuleType.FixedAssets,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="خيارات قيد الإهلاك ",
                 NameEn="Deprecation Journal Entries ",
                 Value="2",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },

                 new GeneralConfiguration()
                {

                 Id =1002,
                 Code="1002",
                 ModuleType=ModuleType.FixedAssets,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" مسلسل مجموعة الأصول",
                 NameEn="Assets Group Serial",
                 Value="2",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },

                 new GeneralConfiguration()
                {

                 Id =1003,
                 Code="1003",
                 ModuleType=ModuleType.FixedAssets,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" نمط مجموعة مسلسل الأصول",
                 NameEn="Assets Group Serial Format",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                  new GeneralConfiguration()
                {

                 Id =1004,
                 Code="1004",
                 ModuleType=ModuleType.FixedAssets,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" مسلسل الأصول ",
                 NameEn="Assets Serial ",
                 Value="2",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                  new GeneralConfiguration()
                {

                 Id =1005,
                 Code="1005",
                 ModuleType=ModuleType.FixedAssets,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" نمط مسلسل الأصول ",
                 NameEn="Assets Serial format ",
                 Value="2",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                     new GeneralConfiguration()
                {

                 Id =1006,
                 Code="1006",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" اليومية الابتدائية ",
                 NameEn="Default Journal ",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },

                new GeneralConfiguration()
                {

                 Id =1007,
                 Code="1007",
                 ModuleType=ModuleType.Accounting,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" شكيات اليومية ",
                 NameEn="Cheques Journal ",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                    new GeneralConfiguration()
                {

                 Id =10001,
                 Code="10001",
                 ModuleType=ModuleType.Settings,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" وقت الخمول ",
                 NameEn="Idle Time",
                 Value="5",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                      new GeneralConfiguration()
                {

                 Id =9,
                 Code="9",
                 ModuleType=ModuleType.Warehouses,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="نظام الجرد",
                 NameEn="Inventory system",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                 new GeneralConfiguration()
                {

                 Id =10,
                 Code="10",
                 ModuleType=ModuleType.Warehouses,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="أحتساب أرصدة الأصناف",
                 NameEn="Calculate Item Balances",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                  new GeneralConfiguration()
                {

                 Id =11,
                 Code="11",
                 ModuleType=ModuleType.Settings,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="العرض بعد الحفظ ",
                 NameEn="Show After Save ",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },

                  new GeneralConfiguration()
                {

                 Id =12,
                 Code="12",
                 ModuleType=ModuleType.Warehouses,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="التحقق من الحد الأدنى لسعر البيع ",
                 NameEn="Validation on minimum selling price ",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                   new GeneralConfiguration()
                {

                 Id =13,
                 Code="13",
                 ModuleType=ModuleType.Settings,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="إظهار الكسور التقريبية",
                 NameEn="Show Rounding Fractions",
                 Value="",
                 ValueType=ValueTypeEnum.Boolean,
                 IsActive=true,
                 IsDeleted=false,

                },
                     new GeneralConfiguration()
                {

                 Id =14,
                 Code="14",
                 ModuleType=ModuleType.Settings,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="عدد الارقام بعد العلامة العشرية",
                 NameEn="Number Of Fraction",
                 Value="2",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,
                },
                     new GeneralConfiguration()
                {
                 Id =15,
                 Code="15",
                 ModuleType=ModuleType.Settings,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="إظهار العلامة العشرية",
                 NameEn="Show Decimal Point",
                 Value="",
                 ValueType=ValueTypeEnum.Boolean,
                 IsActive=true,
                 IsDeleted=false,

                },
                     new GeneralConfiguration()
                 {
                 Id =16,
                 Code="16",
                 ModuleType=ModuleType.Settings,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" إظهار الفاصلة الألفية",
                 NameEn="Show Thousands Comma",
                 Value="",
                 ValueType=ValueTypeEnum.Boolean,
                 IsActive=true,
                 IsDeleted=false,

                },
                    new GeneralConfiguration()
                 {
                 Id =17,
                 Code="17",
                 ModuleType=ModuleType.Sales,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="ورديات نقاط البيع",
                 NameEn="Point of Sale Shifts ",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },
                  new GeneralConfiguration()
                 {
                 Id =18,
                 Code="18",
                 ModuleType=ModuleType.Sales,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr=" إدخال كلمة المرور عند الحذف ",
                 NameEn="Enter Password on Delete",
                 Value="",
                 ValueType=ValueTypeEnum.Boolean,
                 IsActive=true,
                 IsDeleted=false,

                }, new GeneralConfiguration()
                 {
                 Id =19,
                 Code="19",
                 ModuleType=ModuleType.Sales,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="  كلمة المرور عند الحذف ",
                 NameEn="Password on Delete",
                 Value="",
                 ValueType=ValueTypeEnum.text,
                 IsActive=true,
                 IsDeleted=false,

                },
                  new GeneralConfiguration()
                 {
                 Id =20,
                 Code="20",
                 ModuleType=ModuleType.Sales,
                 CreatedAt=DateTime.UtcNow,
                 CreatedBy="System",
                 NameAr="العملاء",
                 NameEn="Customers",
                 Value="",
                 ValueType=ValueTypeEnum.Intger,
                 IsActive=true,
                 IsDeleted=false,

                },





            };

            var enityDb = context.GeneralConfigurations;

            var listInsert = entities.Where(x => !enityDb.Any(z => z.Id == x.Id)).ToList();

            if (listInsert.Count > 0)
            {
                context.GeneralConfigurations.AddRange(listInsert);
                context.SaveChanges();
            }
            //context.GeneralConfigurations.AddRange(entities);

            //context.SaveChanges();
        }

        private void SeedReportFile(ErpDbContext context)
        {
            string reportFileAsJson = File.ReadAllText(@"wwwroot/SeedData" + Path.DirectorySeparatorChar + "ReportFileSeed.json");

            List<ReportFile> reportFiles = JsonConvert.DeserializeObject<List<ReportFile>>(reportFileAsJson);

            var reportFilesDb = context.ReportFiles;

            var listInsert = reportFiles.Where(x => !reportFilesDb.Any(z => z.Id == x.Id)).ToList();

            if (listInsert.Count > 0)
            {
                context.ReportFiles.AddRange(listInsert);
                context.SaveChanges();
            }


        }
        private void SeedBranch(ErpDbContext context)
        {


            var insertIfEmpty = context.BranchPermissions.ToList(); // Materialize insertIfEmpty
            var branches = context.Branches.ToList(); // Materialize branches

            foreach (var branch in branches)
            {
                if (!insertIfEmpty.Any(c => c.BranchId == branch.Id))
                {
                    var entities = new[]
                   {
                  new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    BranchId = branch.Id,
                    ActionName = "Show",
                    ActionNameEn = "Show",
                    ActionNameAr = "عرض",
                },
                   new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    BranchId = branch.Id,
                    ActionName = "Add",
                    ActionNameEn = "Add",
                    ActionNameAr = "اضافة",
                },
                    new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    BranchId = branch.Id,
                    ActionName = "Edit",
                    ActionNameEn = "Edit",
                    ActionNameAr = "تعديل",
                },
                     new BranchPermission()
                  {
                   IsActive = true,
                    IsChecked = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    BranchId = branch.Id,
                    ActionName = "Delete",
                    ActionNameEn = "Delete",
                    ActionNameAr = "حذف",
                }

                };

                    context.BranchPermissions.AddRange(entities);
                }

            }


            context.SaveChanges();


        }
        public async void SeedUserOwner(ErpDbContext context, SettingDataBaseDto userOwner)
        {
            context.Database.EnsureCreated();
            var userDetailsPackage = new List<UserDetailsPackage>();
            var userDetailsModule = new List<UserDetailsModule>();

            var user = new User()
            {
                Id = userOwner.OwnerInfoDto.Id,
                AccessFailedCount = 0,
                ConcurrencyStamp = userOwner.OwnerInfoDto.Id.ToString(),
                Email = userOwner.OwnerInfoDto.Email,
                EmailConfirmed = true,
                LockoutEnabled = false,
                NormalizedEmail = userOwner.OwnerInfoDto.Email.ToUpper(),
                NormalizedUserName = userOwner.OwnerInfoDto.UserName.ToUpper(),
                PasswordHash = userOwner.OwnerInfoDto.PasswordHash,
                PhoneNumber = userOwner.OwnerInfoDto.PhoneNumber,
                PhoneNumberConfirmed = true,
                SecurityStamp = userOwner.OwnerInfoDto.Id.ToString(),
                TwoFactorEnabled = false,
                UserName = userOwner.OwnerInfoDto.UserName,
                FullName = userOwner.OwnerInfoDto.FullName,
                NameAr = userOwner.OwnerInfoDto.NameAr,
                NameEn = userOwner.OwnerInfoDto.NameEn,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "Owner",
                Code = userOwner.OwnerInfoDto.Code,
                UserType = UserType.Owner,
                IsAddPassword = true,
                LoginCount = 1,
                UsersCompanies = new List<UsersCompany>()
            };

            // Associate user with companies and branches
            foreach (var companyDto in userOwner.UserCompaniesDto)
            {
                var company = new Company()
                {
                    NameAr = companyDto.CompanyNameAr,
                    NameEn = companyDto.CompanyNameEn,
                    NumberOfBranches = companyDto.NumberOfBranches,
                    OrganizationId=companyDto.OrganizationId,
                    Branches = new List<Branch>()
          
                    {
          
                        new Branch()
           
                        {
                    NameEn = "Main Branch",
                    NameAr = "الفرع الرئيسي",

                }
              // Assuming one branch per company for simplicity
            }
                };

                var userCompany = new UsersCompany()
                {
                    Company = company,
                    User = user,
                    IsActive = true,
                    IsDeleted = false,
                    UserCompaniesBranchs = new List<UserCompaniesBranch>()
                };

                foreach (var branch in company.Branches)
                {
                    var userCompanyBranch = new UserCompaniesBranch()
                    {
                        Branch = branch
                    };
                    userCompany.UserCompaniesBranchs.Add(userCompanyBranch);
                }
                foreach (var package in userOwner.UserDetailsPackage)
                {
                    var packageDatabase = new UserDetailsPackage()
                    {
                        UserPackageId = package.Id,
                        UserId = package.UserId,
                        NameAr = package.NameAr,
                        NameEn = package.NameEn,
                        NumberOfUsers = package.NumberOfUsers,
                        NumberOfCompanies = package.NumberOfCompanies,
                        NumberOfBranches = package.NumberOfBranches,
                        BillPattrenNumber = package.BillPattrenNumber,
                        InstrumentPattrenNumber = package.InstrumentPattrenNumber,
                        TypeOfSubscription = package.TypeOfSubscription,
                        SubscriptionPrice = package.SubscriptionPrice,
                        SubscriptionStartDate = package.SubscriptionStartDate,
                        SubscriptionExpiaryDate = package.SubscriptionExpiaryDate,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = user.Id,
                        CompanyId=companyDto.CompanyId

                    };

                    userDetailsPackage.Add(packageDatabase);

                }
                foreach (var module in userOwner.UserDetailsModules)
                {
                    var moduleDatabase = new UserDetailsModule()
                    {
                        UserModuleId = module.Id,
                        UserId = module.UserId,
                        NameAr = module.NameAr,
                        NameEn = module.NameEn,
                        BillPattrenPrice = module.BillPattrenPrice,
                        Code = module.Code,
                        InstrumentPattrenPrice = module.InstrumentPattrenPrice,
                        IsFree = module.IsFree,
                        IsPackageModule = module.IsPackageModule,
                        NumberOfUser = module.NumberOfUser,
                        OtherModuleId = module.OtherModuleId,
                        OtherUserFullBuyingSubscriptionPrice = module.OtherUserFullBuyingSubscriptionPrice,
                        OtherUserMonthlySubscriptionPrice = module.OtherUserMonthlySubscriptionPrice,
                        OtherUserYearlySubscriptionPrice = module.OtherUserYearlySubscriptionPrice,
                        NumberOfCompanies = module.NumberOfCompanies,
                        NumberOfBranches = module.NumberOfBranches,
                        BillPattrenNumber = module.BillPattrenNumber,
                        InstrumentPattrenNumber = module.InstrumentPattrenNumber,
                        TypeOfSubscription = module.TypeOfSubscription,
                        SubscriptionPrice = module.SubscriptionPrice,
                        SubscriptionStartDate = module.SubscriptionStartDate,
                        SubscriptionExpiaryDate = module.SubscriptionExpiaryDate,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedBy = user.Id,
                        PackgId = module.PackgId,
                        CompanyId = companyDto.CompanyId
                    };

                    userDetailsModule.Add(moduleDatabase);

                }
                user.UsersCompanies.Add(userCompany);
            }

            // Add user to the context
            var checkUser = context.Users.FirstOrDefault(c => c.Id == user.Id);
            if (checkUser == null)
            {
                context.Users.Add(user);
            }
           
            context.UserDetailsPackages.AddRange(userDetailsPackage);
            context.UserDetailsModules.AddRange(userDetailsModule);
          
            // Save changes to the database
            context.SaveChanges();
            //SeedRoles(context);
            //SeedUserRoles(context);
            //SeedScreen(context);
            //SeedPermission(context);
            //SeedPermissionRole(context);
            //SeedReportFile(context);
        }



    }

}