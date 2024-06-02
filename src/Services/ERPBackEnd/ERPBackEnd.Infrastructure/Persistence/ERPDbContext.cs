using Configuration.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using Warehouses.Entities;
using System.Text.Json;
using Z.EntityFramework.Extensions;
using ResortAppStore.Services.ERPBackEnd.Domain.Inventory;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ErpBackEnd.Infrastructure.Persistence
{
    public class ErpDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
      UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>

    {

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyTransaction> CurrencyTransactions { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<UsersCompany> UsersCompanies { get; set; }
        public DbSet<UserCompaniesBranch> UsersCompaniesBranches { get; set; }
        public DbSet<GeneralConfiguration> GeneralConfigurations { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<FiscalPeriod> FiscalPeriods { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountClassification> AccountClassifications { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherDetail> VoucherDetails { get; set; }
        public DbSet<JournalEntriesMaster> JournalEntriesMasters { get; set; }
        public DbSet<JournalEntriesDetail> JournalEntriesDetails { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<IncomingChequeMaster> IncomingChequeMasters { get; set; }
        public DbSet<IncomingChequeDetail> IncomingChequeDetails { get; set; }
        public DbSet<IncomingChequeStatusDetail> IncomingChequeStatusDetails { get; set; }
        public DbSet<IssuingChequeMaster> IssuingChequeMasters { get; set; }
        public DbSet<IssuingChequeDetail> IssuingChequeDetails { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitTransaction> UnitTransactions { get; set; }
        public DbSet<TaxMaster> TaxMasters { get; set; }
        public DbSet<TaxDetail> TaxDetails { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<StoreCard> StoreCards { get; set; }
        public DbSet<CustomerCard> CustomerCards { get; set; }
        public DbSet<SupplierCard> SupplierCards { get; set; }
        public DbSet<ItemGroupsCard> ItemGroupsCards { get; set; }
        public DbSet<ItemCard> ItemCards { get; set; }
        public DbSet<ItemCardAlternative> ItemCardAlternatives { get; set; }
        public DbSet<ItemCardUnit> ItemCardUnits { get; set; }
        public DbSet<ItemCardBalance> ItemCardBalances { get; set; }

        public DbSet<ItemCardDeterminant> ItemCardDeterminants { get; set; }

        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<BillTypeDefaultValueUser> BillTypeDefaultValueUsers { get; set; }

        public DbSet<SalesPersonCard> SalesPersonCards { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<BillItemTax> BillItemTaxes { get; set; }
        public DbSet<BillAdditionAndDiscount> BillAdditionAndDiscounts { get; set; }
        public DbSet<BillPay> BillPays { get; set; }
        public DbSet<BillPaymentDetail> BillPaymentDetails { get; set; }
        public DbSet<SalesPersonCommission> SalesPersonCommissions { get; set; }
        public DbSet<ReportFile> ReportFiles { get; set; }
        public DbSet<DeterminantsMaster> DeterminantsMaster { get; set; }
        public DbSet<DeterminantsDetail> DeterminantsDetails { get; set; }
        public DbSet<SubTaxDetail> SubTaxDetails { get; set; }
        public DbSet<VouchersRolesPermissions> VouchersRolesPermissions { get; set; }
        public DbSet<BillsRolesPermissions> BillsRolesPermissions { get; set; }
        public DbSet<BillDynamicDeterminant> BillDynamicDeterminants { get; set; }
        public DbSet<AccountingPeriod> AccountingPeriods { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Beneficiaries> Beneficiaries { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<SupplierGroup> SupplierGroups { get; set; }
        public DbSet<BeneficiariesGroup> BeneficiariesGroups { get; set; }
        public DbSet<BeneficiariesGroupDetails> BeneficiariesGroupDetails { get; set; }
        public DbSet<InventoryList> InventoryLists { get; set; }
        public DbSet<InventoryListsDetail> InventoryListsDetails { get; set; }
        public DbSet<ManualInventoryApproval> ManualInventoryApprovals { get; set; }
        public DbSet<NotificationConfiguration> NotificationConfigurations { get; set; }
        public DbSet<EmailConfiguration> EmailConfigurations { get; set; }
        public DbSet<WhatsAppConfiguration> WhatsAppConfigurations { get; set; }
        public DbSet<SMSProviderConfiguration> SMSProviderConfigurations { get; set; }
        public DbSet<BranchPermission> BranchPermissions { get; set; }
        public DbSet<NotificationsManagement> NotificationsManagements { get; set; }
        public DbSet<PriceListMaster> PriceListMasters { get; set; }
        public DbSet<PriceListDetail> PriceListDetails { get; set; }
        public DbSet<InventoryDynamicDeterminant> InventoryDynamicDeterminants { get; set; }

        public DbSet<POSBillType> POSBillTypes { get; set; }
        public DbSet<POSBillTypeDefaultValueUser> POSBillTypeDefaultValueUsers { get; set; }
        public DbSet<POSBillsRolesPermissions> POSBillsRolesPermissions { get; set; }
        public DbSet<PointOfSaleCard> PointOfSaleCards { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<POSTable> POSTables { get; set; }

        public DbSet<POSBill> POSBills { get; set; }
        public DbSet<POSBillItem> POSBillItems { get; set; }
        public DbSet<POSBillItemTax> POSBillItemTaxes { get; set; }
        public DbSet<POSBillPaymentDetail> POSBillPaymentDetails { get; set; }
        public DbSet<POSBillDynamicDeterminant> POSBillDynamicDeterminants { get; set; }

        public DbSet<ShiftDetail> ShiftDetails { get; set; }
        public DbSet<ShiftMaster> ShiftMaster { get; set; }
        public DbSet<CashTransfer> CashTransfers { get; set; }
        public DbSet<UserDetailsModule> UserDetailsModules { get; set; }  
        public DbSet<UserDetailsPackage> UserDetailsPackages { get; set; }
        public DbSet<POSBillsUser> POSBillsUsers { get; set; }

        public DbSet<POSDeliveryDetail> POSDeliveryDetails { get; set; }
        public DbSet<POSDeliveryMaster> POSDeliveryMasters { get; set; }



        public ErpDbContext(DbContextOptions options) : base(options)
        {
            EntityFrameworkManager.ContextFactory = context => new ErpDbContext(options);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErpDbContext).Assembly);

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

            modelBuilder.Entity<CurrencyTransaction>()
       .HasOne(r => r.CurrencyMaster)
       .WithMany(c => c.CurrenciesMaster)
        .HasForeignKey(c => c.CurrencyMasterId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurrencyTransaction>()
                .HasOne(r => r.CurrencyDetail)
                .WithMany(c => c.CurrenciesDetail)
                .HasForeignKey(c => c.CurrencyDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
            .HasOne(r => r.Country)
             .WithMany(c => c.Branches)
            .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<IncomingChequeDetail>()
            // .HasOne(p => p.IncomingChequeMaster)
            // .WithMany(b => b.IncomingChequeDetails)
            // //.HasForeignKey(p => p.IncomingChequeId)
            // .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UnitTransaction>()
.HasOne(r => r.UnitMaster)
.WithMany(c => c.UnitsMaster)
 .HasForeignKey(c => c.UnitMasterId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UnitTransaction>()
    .HasOne(r => r.UnitDetail)
    .WithMany(c => c.UnitsDetail)
     .HasForeignKey(c => c.UnitDetailId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillDynamicDeterminant>(entity =>
            {

                // Map the list property to a JSON column in the database
                entity.Property(e => e.DeterminantsData)

                    .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<DeterminantData>>(v, (JsonSerializerOptions)null)).HasColumnName("DeterminantData");
                entity.Property(e => e.DeterminantsValue)

                        .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<DeterminantValue>>(v, (JsonSerializerOptions)null)).HasColumnName("DeterminantValue");

            });

            modelBuilder.Entity<InventoryDynamicDeterminant>(entity =>
            {

                // Map the list property to a JSON column in the database
                entity.Property(e => e.DeterminantsData)

                    .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<InventoryDeterminantData>>(v, (JsonSerializerOptions)null)).HasColumnName("InventoryDeterminantData");
                entity.Property(e => e.DeterminantsValue)

                        .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<InventoryDeterminantValue>>(v, (JsonSerializerOptions)null)).HasColumnName("InventoryDeterminantValue");

            });

            modelBuilder.Entity<POSBillDynamicDeterminant>(entity =>
            {

                // Map the list property to a JSON column in the database
                entity.Property(e => e.DeterminantsData)

                    .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<POSDeterminantData>>(v, (JsonSerializerOptions)null)).HasColumnName("DeterminantData");
                entity.Property(e => e.DeterminantsValue)

                        .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<POSDeterminantValue>>(v, (JsonSerializerOptions)null)).HasColumnName("DeterminantValue");

            });

            modelBuilder.Entity<POSBill>()
        .HasOne(p => p.POSBillType)
        .WithMany()
        .HasForeignKey(p => p.BillTypeId)
        .OnDelete(DeleteBehavior.Restrict); // Or use .OnDelete(DeleteBehavior.NoAction)



        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.InvalidIncludePathError));
            // other configuration code
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
            this.Database.Migrate();
        }


    }
}
