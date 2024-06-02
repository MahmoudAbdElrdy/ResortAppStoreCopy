using AuthDomain.Entities.Auth;
using AuthDomain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Org.BouncyCastle.Utilities.IO.Pem;
using ResortAppStore.Services.Administration.Domain;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using ResortAppStore.Services.Administration.Domain.Entities.Configuration;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System.Linq;

namespace ResortAppStore.Services.Administration.Infrastructure.Persistence
{
    public class IdentityServiceDbContext :  IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
      UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>

    {

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<Country> Countries { get; set; } 
        public DbSet<Business> Business { get; set; }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<PackagesModules> PackagesModules { get; set; }
        public DbSet<UserDetailsPackagesModules> UserDetailsPackagesModules { get; set; }
        public DbSet<OrganizationCompany> UserCompanies { get; set; }
        public DbSet<UserSubscriptionPromoCode> UserSubscriptionPromoCodes { get; set; }
        public DbSet<PackageModuleCompany> PackageModuleCompanies { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }

        public DbSet<UserOrgnizationType> UserOrgnizationTypes { get; set; }

        public DbSet<UserOrganization> UserOrgnizations { get; set; }
        public DbSet<PromoCodes> PromoCodes { get; set; }
        public DbSet<Screen> Screens { get; set; }   
        public DbSet<Setting> Settings { get; set; }   

        public DbSet<UserToken> UserTokens { get; set; }

        public DbSet<UserPaymentOnline> UserPaymentOnlines { get; set; }

        public DbSet<CashPaymentInfo> CashPaymentInfos { get; set; }     

        public IdentityServiceDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityServiceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Role>().Property(p => p.Name).HasColumnName("NameEn");
            modelBuilder.Entity<UserRole>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<PermissionRole>().HasKey(p => new { p.Id });
            modelBuilder.Entity<PermissionRole>().HasKey(p => new { p.RoleId, p.PermissionId });
            modelBuilder.Entity<PermissionRole>().Property(p => p.Id)
        .ValueGeneratedOnAdd()
        .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            modelBuilder.Entity<User>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

          

            modelBuilder.Entity<Module>().Property(p=>p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Package>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<PackagesModules>().Property(p => p.Id).ValueGeneratedOnAdd();

        }
      

        public bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public void Migrate()
        {
            //this.Database.Migrate();
        }

       
    }
}
