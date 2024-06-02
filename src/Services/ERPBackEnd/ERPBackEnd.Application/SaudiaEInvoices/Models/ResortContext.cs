using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace SaudiEinvoiceService.Models
{
    public partial class ResortContext : DbContext
    {
        public string DbName { set; get; }
        public ResortContext()
        {
        }

        public ResortContext(DbContextOptions<ResortContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<BillItem> BillItems { get; set; } = null!;
        public virtual DbSet<BillType> BillTypes { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Mat> Mats { get; set; } = null!;
        public virtual DbSet<MatUnit> MatUnits { get; set; } = null!;
        public virtual DbSet<Option> Options { get; set; } = null!;
        public virtual DbSet<vwTaxBillMaster> vwTaxBillMaster { get; set; } = null!;
        public virtual DbSet<vwTaxBillLine> vwTaxBillLine { get; set; } = null!;
        public virtual DbSet<ElectBill> ElectBill { get; set; } = null!;
        
        public virtual DbSet<EInvoiceResponseLog> EInvoiceResponseLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                 .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                                 .AddJsonFile("appsettings.json")
                                 .Build();

                //var val = System.Web.HttpContext.Current

                //string connectionString = configuration.GetConnectionString("ResortData") + ";Initial Catalog=" + DbName;
                string connectionString = configuration.GetConnectionString("dbConString") + ";Initial Catalog=" + DbName;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__Account__15B69B8E35A91B4A");

                entity.ToTable("Account");

                entity.HasIndex(e => new { e.Branch, e.Number }, "UQ_Account_Number")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AdditionalInformation)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AdditionalNumber)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AdditionalStreetName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Asset).HasDefaultValueSql("((0))");

                entity.Property(e => e.AssetDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BankAccNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Branch).HasDefaultValueSql("((1))");

                entity.Property(e => e.BuildingNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1/1/1980')");

                entity.Property(e => e.CalcDep).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("CDate")
                    .HasDefaultValueSql("('1/1/1980')");

                entity.Property(e => e.CheckDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1/1/1980')");

                entity.Property(e => e.CityName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CmbFld1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CmbFld2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CmbFld3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CmbFld4)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CmbFld5)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CmbFld6)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Code)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CostGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasDefaultValueSql("((0))");

                entity.Property(e => e.CurrencyPtr).HasDefaultValueSql("((0))");

                entity.Property(e => e.CurrencyVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.Debit).HasDefaultValueSql("((0))");

                entity.Property(e => e.DebitOrCredit).HasDefaultValueSql("((0))");

                entity.Property(e => e.DepGrpAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.District)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("EMail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Final).HasDefaultValueSql("((0))");

                entity.Property(e => e.FloorNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gln)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("GLN");

                entity.Property(e => e.Governate)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ibanno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IBANNo");

                entity.Property(e => e.InActCase)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InitCredit).HasDefaultValueSql("((0))");

                entity.Property(e => e.InitDebit).HasDefaultValueSql("((0))");

                entity.Property(e => e.LandMark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastMatchDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1-1-1980')");

                entity.Property(e => e.LatinName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaxCrDebt).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxDebit).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Notes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Notes2)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nsons)
                    .HasColumnName("NSons")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Parent).HasDefaultValueSql("((0))");

                entity.Property(e => e.ParentGuid).HasColumnName("ParentGUID");

                entity.Property(e => e.Phone1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReceiverType)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegionCity)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Room)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SchemaldType)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SchemaldValue)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ScrapValue).HasDefaultValueSql("((1))");

                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaxName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaxNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telex)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TxtFld1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TxtFld2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TxtFld3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnPostedCredit).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnPostedDebit).HasDefaultValueSql("((0))");

                entity.Property(e => e.UseFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Warn).HasDefaultValueSql("((0))");

                entity.Property(e => e.Website)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__Bill__A2B5777CA47F8907");

                entity.ToTable("Bill");

                entity.HasIndex(e => new { e.Branch, e.TypeGuid, e.Number }, "UQ_Bill_Number")
                    .IsUnique();

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BillExtra).HasDefaultValueSql("((0))");

                entity.Property(e => e.BillNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Branch).HasDefaultValueSql("((1))");

                entity.Property(e => e.CashAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.CashPaid).HasDefaultValueSql("((0))");

                entity.Property(e => e.CashierGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Centry).HasColumnName("CEntry");

                entity.Property(e => e.ClientGuid).HasColumnName("ClientGUID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(host_name())");

                entity.Property(e => e.CompanyGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.CostGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.CustName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CustomsTransactionProcGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Dcguid)
                    .HasColumnName("DCGuid")
                    .HasDefaultValueSql("(0x00)");

                entity.Property(e => e.DelegateGuid).HasDefaultValueSql("(0x00)");

                //entity.Property(e => e.EinvoiceGuid)
                //    .HasMaxLength(255)
                //    .IsUnicode(false)
                //    .HasColumnName("EInvoiceGuid");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ExtId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Field1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Field2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Field3)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Field4)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FunTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.Granting).HasDefaultValueSql("((0))");

                entity.Property(e => e.IdcardNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDCardNo");

                entity.Property(e => e.InvoiceHash)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IsExternal).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMerged).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMultiRef).HasDefaultValueSql("((0))");

                entity.Property(e => e.MemDiscRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NotesDiscPer)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NotesDiscValue)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NotesExtra)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ParentGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.ParentTypeGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PeriodGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PersonGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PolicyGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PrStepGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PriceOfferGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Qr)
                    .HasMaxLength(700)
                    .IsUnicode(false);

                entity.Property(e => e.RefDate).HasColumnType("datetime");

                entity.Property(e => e.RefGuid).HasColumnName("RefGUID");

                entity.Property(e => e.RefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RefRefNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RefTguid).HasColumnName("RefTGUID");

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RsdNotificationId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Rsd_NotificationID");

                entity.Property(e => e.ShippingKind).HasDefaultValueSql("((0))");

                entity.Property(e => e.Side)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StoreGuid).HasColumnName("StoreGUID");

                entity.Property(e => e.SupplyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Synced).HasDefaultValueSql("((0))");

                entity.Property(e => e.Synced2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyncedDate).HasColumnType("datetime");

                entity.Property(e => e.SyncedDate2).HasColumnType("datetime");

                entity.Property(e => e.TaxManually).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxManuallyPercent).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalPaid).HasDefaultValueSql("((0))");

                entity.Property(e => e.TransOrderGuid).HasColumnName("TransOrderGUID");

                entity.Property(e => e.TransOrderPtr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserNumber).HasDefaultValueSql("((0))");

                entity.Property(e => e.VersionFromGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.VersionNumber).HasDefaultValueSql("((1))");

                //entity.Property(e => e.ZidOrderCode)
                //    .HasMaxLength(255)
                //    .IsUnicode(false)
                //    .HasColumnName("Zid_Order_Code");

                //entity.Property(e => e.ZidOrderId).HasColumnName("Zid_Order_Id");
            });

            modelBuilder.Entity<BillItem>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__BillItem__A2B5777C4AAA07A8");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.AddTaxRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.AssetBookVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.AssetBuyVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BatchNo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bprice).HasColumnName("BPrice");

                entity.Property(e => e.Bqty).HasColumnName("BQty");

                entity.Property(e => e.Cnt).HasDefaultValueSql("((0))");

                entity.Property(e => e.Desc)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DescGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Disc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Disc2).HasDefaultValueSql("((0))");

                entity.Property(e => e.Disc3).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscQuota).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraQuota).HasDefaultValueSql("((0))");

                entity.Property(e => e.FileGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Flag)
                    .HasColumnName("flag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FunTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.FunTaxRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.GroupGuid)
                    .HasColumnName("GroupGUID")
                    .HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Height).HasDefaultValueSql("((0))");

                entity.Property(e => e.ImageGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.ItemCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Length).HasDefaultValueSql("((0))");

                entity.Property(e => e.MatType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Notes)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Notes2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ParentItemGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PriceByOffer).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ref)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RefTguid).HasColumnName("RefTGuid");

                entity.Property(e => e.ShipmentQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.StandardVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.Total).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasDefaultValueSql("((0))");

                entity.Property(e => e.WeightAfter).HasDefaultValueSql("((0))");

                entity.Property(e => e.WeightBefor).HasDefaultValueSql("((0))");

                entity.Property(e => e.Width).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<BillType>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__BillType__A2B5777CB5760A18");

                entity.ToTable("BillType");

                entity.HasIndex(e => new { e.Branch, e.Number }, "UQ_Billtype_Number")
                    .IsUnique();

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccMatNumber).HasDefaultValueSql("((0))");

                entity.Property(e => e.AddTaxAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.AffectCostPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllowMultiRef).HasDefaultValueSql("((0))");

                entity.Property(e => e.ApplySourceTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.AutoAccPosted).HasDefaultValueSql("((0))");

                entity.Property(e => e.AutoEntry).HasDefaultValueSql("((0))");

                entity.Property(e => e.AutoPosted).HasDefaultValueSql("((4))");

                entity.Property(e => e.BalanceAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.BillMatClassDesign).HasDefaultValueSql("((0))");

                entity.Property(e => e.Branch).HasDefaultValueSql("((1))");

                entity.Property(e => e.BringBillRelated).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringBranchRef).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringCurrentContentSalePrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringDefineDefaultValue).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringPriceFromLastPriceOffer).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringPurchaseDiscForExpDate).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringRefAddData).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringRefEntryValues).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringRefEntryValuesWithoutTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringRefMainStore).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringRefPayType).HasDefaultValueSql("((0))");

                entity.Property(e => e.BringRefStore).HasDefaultValueSql("((0))");

                entity.Property(e => e.CalcAddTaxAfterFunTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.CalcPaidInBillPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.CalcTaxAfterDiscAndExtra).HasDefaultValueSql("((0))");

                entity.Property(e => e.Cfcolor).HasColumnName("CFColor");

                entity.Property(e => e.Cfheight).HasColumnName("CFHeight");

                entity.Property(e => e.Cfname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("CFName");

                entity.Property(e => e.Cfsize).HasColumnName("CFSize");

                entity.Property(e => e.Cfstyle).HasColumnName("CFStyle");

                entity.Property(e => e.CodeByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.ColorFromMatCard).HasDefaultValueSql("((0))");

                entity.Property(e => e.ColorOfferPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.ComBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.ComStructureEmp)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ComposedBarcode).HasDefaultValueSql("((0))");

                entity.Property(e => e.ComposedBarcodeKind).HasDefaultValueSql("((0))");

                entity.Property(e => e.ConfirmEmp).HasDefaultValueSql("((0))");

                entity.Property(e => e.CopyValToPaid).HasDefaultValueSql("((0))");

                entity.Property(e => e.CostGuid).HasColumnName("CostGUID");

                entity.Property(e => e.CstKind).HasDefaultValueSql("((0))");

                entity.Property(e => e.CustAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.CustomsClearanceBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefUnit).HasDefaultValueSql("((0))");

                entity.Property(e => e.DelegateGuid)
                    .HasColumnName("DelegateGUID")
                    .HasDefaultValueSql("(0x00)");

                entity.Property(e => e.DiscAffectCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.DiscExtraDistWay).HasDefaultValueSql("((0))");

                entity.Property(e => e.DoNotBringServiceMat).HasDefaultValueSql("((0))");

                entity.Property(e => e.DoNotShowMatCaptionWithImage).HasDefaultValueSql("((0))");

                entity.Property(e => e.ElectBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.ElectBillDesign).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExtraAffectCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.FilterByGroup).HasDefaultValueSql("((0))");

                entity.Property(e => e.FilterCompByAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.FilterPersonByAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.Flag).HasDefaultValueSql("((0))");

                entity.Property(e => e.FunTaxAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.GenBillsByMatStore).HasDefaultValueSql("((0))");

                entity.Property(e => e.GenStoreBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.GenerateTranBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.GetRefAddRecords).HasDefaultValueSql("((0))");

                entity.Property(e => e.GetRefBillRef).HasDefaultValueSql("((0))");

                entity.Property(e => e.GetRefCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.GetRefDetRemark).HasDefaultValueSql("((1))");

                entity.Property(e => e.GetRefOppAcc).HasDefaultValueSql("((0))");

                entity.Property(e => e.GetRefPeriod).HasDefaultValueSql("((0))");

                entity.Property(e => e.GetTotalRefQtyInfo).HasDefaultValueSql("((0))");

                entity.Property(e => e.GrantingAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.GroupGuid)
                    .HasColumnName("GroupGUID")
                    .HasDefaultValueSql("(0x00)");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GroupedBillType).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.HorzMatCount).HasDefaultValueSql("((4))");

                entity.Property(e => e.InBillTypeGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.InvBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.KeepRefNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Kind)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.KindId).HasColumnName("KindID");

                entity.Property(e => e.LatinName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ManuallyTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.ManuallyTaxType).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxRow).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxRowCount).HasDefaultValueSql("((10))");

                entity.Property(e => e.Mfcolor).HasColumnName("MFColor");

                entity.Property(e => e.Mfheight).HasColumnName("MFHeight");

                entity.Property(e => e.Mfname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("MFName");

                entity.Property(e => e.Mfsize).HasColumnName("MFSize");

                entity.Property(e => e.Mfstyle).HasColumnName("MFStyle");

                entity.Property(e => e.ModifyCarWeight).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifyPriceWhenModifyTotal).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifyQtyWhenModifyRealQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.MultiVersion).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NationGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.NotEntry).HasDefaultValueSql("((0))");

                entity.Property(e => e.NotPosted).HasDefaultValueSql("((0))");

                entity.Property(e => e.OpenDrawer).HasDefaultValueSql("((0))");

                entity.Property(e => e.OutBillTypeGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.OutExpDateByPurchasesPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.PaidAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PayType).HasDefaultValueSql("((0))");

                entity.Property(e => e.PoBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.PosDiscAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PosExtraAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PosbillMatClassDesign)
                    .HasColumnName("POSBillMatClassDesign")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrOnLine).HasDefaultValueSql("((0))");

                entity.Property(e => e.PriceOfferGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.PrintByMatClass).HasDefaultValueSql("((0))");

                entity.Property(e => e.PrintMatPicture).HasDefaultValueSql("((0))");

                entity.Property(e => e.Project).HasDefaultValueSql("((0))");

                entity.Property(e => e.QtyDecimalCount).HasDefaultValueSql("((2))");

                entity.Property(e => e.QualityItemGuids)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QuickExchangeTypeGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.RefQualiyFieldGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.RefreshRowTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.RelateEmpsCarsTrailer).HasDefaultValueSql("((0))");

                entity.Property(e => e.RelateMatWithDefStore).HasDefaultValueSql("((0))");

                entity.Property(e => e.RelateWithCustomsTransaction).HasDefaultValueSql("((0))");

                entity.Property(e => e.RelatedGuid).HasColumnName("RElatedGuid");

                entity.Property(e => e.RemainAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.RentingBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.RoundQtyValues).HasDefaultValueSql("((0))");

                entity.Property(e => e.RoundValues).HasDefaultValueSql("((0))");

                entity.Property(e => e.SearchInRefMat).HasDefaultValueSql("((0))");

                entity.Property(e => e.SearchInUnFinishRefMat).HasDefaultValueSql("((0))");

                entity.Property(e => e.SelectTranStore).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShowDiscShortCut).HasDefaultValueSql("((1))");

                entity.Property(e => e.ShowMatTableShortcut).HasDefaultValueSql("((1))");

                entity.Property(e => e.ShowMemberShortcut).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShowPersonShortcut).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShowRefCode).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShowRefRefNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShowTranBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.ShowUpClassShortcut).HasDefaultValueSql("((0))");

                entity.Property(e => e.SimpleWork).HasDefaultValueSql("((0))");

                entity.Property(e => e.SnSeparator)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceTaxPercent).HasDefaultValueSql("((0))");

                entity.Property(e => e.SourceTaxType).HasDefaultValueSql("((0))");

                entity.Property(e => e.StkTrans).HasDefaultValueSql("((0))");

                entity.Property(e => e.StoreNumber).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxPercent).HasDefaultValueSql("((0))");

                entity.Property(e => e.TranStoreGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.TransBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnCalcTax).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateDataAfterSave).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateExpDate).HasDefaultValueSql("((0))");

                entity.Property(e => e.ValuesDecimalCount).HasDefaultValueSql("((2))");

                entity.Property(e => e.VerColCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.VertMatCount).HasDefaultValueSql("((6))");

                entity.Property(e => e.WaitingRec).HasDefaultValueSql("((0))");

                entity.Property(e => e.WaitingRecFrom).HasDefaultValueSql("((0))");

                entity.Property(e => e.WaitingRecKind).HasDefaultValueSql("((0))");

                entity.Property(e => e.WaitingRecTo).HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkBill).HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkTouch).HasDefaultValueSql("((0))");

                entity.Property(e => e.Yard).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActivityCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AgreeNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DetailType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FooterType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.HeaderType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LatinAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LatinName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile2)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.Picture2).HasColumnType("image");

                entity.Property(e => e.Pobox)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("POBox");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TaxNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.WebSite)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mat>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__Mats__A2B5777C87FB92A3");

                entity.HasIndex(e => new { e.Branch, e.Number }, "UQ_Mats_Number")
                    .IsUnique();

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.ActDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddTaxPurAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.AddTaxRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.AddTaxSaleAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AssetGroupGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Balance).HasDefaultValueSql("((0))");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Branch).HasDefaultValueSql("((1))");

                entity.Property(e => e.BuyDiscRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.BuyDiscVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.BuyFqrate).HasColumnName("BuyFQRate");

                entity.Property(e => e.BuyFreeQtyIsInteger).HasDefaultValueSql("((1))");

                entity.Property(e => e.CalcFirstCostPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Color).HasDefaultValueSql("((536870911))");

                entity.Property(e => e.CustName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DefAge).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefAgeType).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefUnit).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DiscAffectCost).HasDefaultValueSql("((0))");

                entity.Property(e => e.Egscode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("EGSCode");

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstBalance).HasDefaultValueSql("((0))");

                entity.Property(e => e.Flag).HasColumnName("FLAG");

                entity.Property(e => e.Fld1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fld2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fld3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fld4)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fld5)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fld6)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FunTaxPurAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.FunTaxRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.FunTaxSaleAccGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Gpccode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GPCCode");

                entity.Property(e => e.Gs1code)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GS1Code");

                entity.Property(e => e.Gtin)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("GTIN");

                entity.Property(e => e.HeightRate).HasDefaultValueSql("((1))");

                entity.Property(e => e.InUnit)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntegerQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsBatchNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastBuyPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.LatinName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LengthRate).HasDefaultValueSql("((1))");

                entity.Property(e => e.LessBuyPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MaxBuyDiscRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxBuyDiscVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxSaleDiscRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxSaleDiscVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.MinQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Model)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OneDefinitionInOper).HasDefaultValueSql("((0))");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PictureGuid).HasColumnName("PictureGUID");

                entity.Property(e => e.Producer)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReorderQty).HasDefaultValueSql("((0))");

                entity.Property(e => e.SaleDiscRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.SaleDiscVal).HasDefaultValueSql("((0))");

                entity.Property(e => e.SaleFqrate).HasColumnName("SaleFQRate");

                entity.Property(e => e.SaleFreeQtyIsInteger).HasDefaultValueSql("((1))");

                entity.Property(e => e.ShowInExternal).HasDefaultValueSql("((0))");

                entity.Property(e => e.StoreGuid).HasDefaultValueSql("(0x00)");

                entity.Property(e => e.Taxable).HasDefaultValueSql("((0))");

                entity.Property(e => e.TaxableType).HasDefaultValueSql("((0))");

                entity.Property(e => e.TransPrice).HasColumnName("transPrice");

                entity.Property(e => e.Unit1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ViewWay).HasDefaultValueSql("((0))");

                entity.Property(e => e.WarrType)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasDefaultValueSql("((0))");

                entity.Property(e => e.WidthRate).HasDefaultValueSql("((1))");

                entity.Property(e => e.WithDefinition).HasDefaultValueSql("((0))");

                entity.Property(e => e.ZidId)
                    .HasMaxLength(255)
                    .HasColumnName("Zid_Id");
            });

            modelBuilder.Entity<MatUnit>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__MatUnits__15B69B8E5CBE521C");

                entity.HasIndex(e => new { e.Matguid, e.Number }, "UQ_MatUnits_Number")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.LessSalePrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.Matguid).HasColumnName("MATGUID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.RateType).HasDefaultValueSql("((0))");

                entity.Property(e => e.SalePrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.Weight).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__Options__15B69B8ED838FB99");

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ComputerName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DateVal).HasColumnType("datetime");

                entity.Property(e => e.IsSec).HasDefaultValueSql("((0))");

                entity.Property(e => e.Section)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.StrVal)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StrVal2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StrVal3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TextVal).HasColumnType("text");

                entity.Property(e => e.Type)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
