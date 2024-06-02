using AutoMapper;
using Common.Infrastructures;
using Common.Mapper;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Dto
{
    public class AccountDto : IHaveCustomMapping
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(36)]
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        [MaxLength(36)]
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        [MaxLength(300)]
        public string? Notes { get; set; }
        public bool IsActive { get; set; }

        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(200)]
        public string? Code { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }

        public Decimal? OpenBalanceDebit { get; set; }
        public Decimal? OpenBalanceCredit { get; set; }
        public Decimal? DebitLimit { get; set; }
        public Decimal? CreditLimit { get; set; }
        public string? TaxNumber { get; set; }
        public string? NoteNotActive { get; set; }
        public long? CompanyId { get; set; }

        public long? CurrencyId { get; set; }

        public long? CostCenterId { get; set; }

        public string? ParentId { get; set; }
        public long? AccountId { get; set; }
        public int? AccountType { get; set; }
        public bool? IsLeafAccount { get; set; }
        public int? Budget { get; set; }
        public long? AccountGroupId { get; set; }
        public long? AccountClassificationIdOfIncomeStatement { get; set; }
        public int AccountClassificationId { get; set; }
        public Guid? Guid { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Account, AccountDto>()
                          .ReverseMap();
        }
    }

    public class AccountTreeDto
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(36)]
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        [MaxLength(36)]
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        [MaxLength(300)]
        public string? Notes { get; set; }
        public bool IsActive { get; set; }

        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(200)]
        public string? Code { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }

        public Decimal? OpenBalanceDebit { get; set; }
        public Decimal? OpenBalanceCredit { get; set; }
        public Decimal? DebitLimit { get; set; }
        public Decimal? CreditLimit { get; set; }
        public string TaxNumber { get; set; }
        public long? CompanyId { get; set; }
        public long? CurrencyId { get; set; }
        public long? CostCenterId { get; set; }
        public string? ParentId { get; set; }
        public long? AccountId { get; set; }
        public IEnumerable<AccountTreeDto> children { get; set; }
        public bool expanded { get; set; }
        public long? AccountGroupId { get; set; }
        public bool? IsLeafAccount { get; set; }

        public int? AccountType { get; set; }

        public int? Budget { get; set; }

        public long? AccountClassificationIdOfIncomeStatement { get; set; }
        public int AccountClassificationId { get; set; }

    }
    public class AccountSearch : Paging
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public Int64? SelectedId { get; set; }

    }
    public class GetAllAccountTreeCommand
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public string? SelectedId { get; set; }
    }
    public class GetLastCode
    {
        public string ParentId { get; set; }
    }
    public class DeleteAccountCommand
    {
        public string Id { get; set; }
    }
    public class AccountBalanceDto
    {
        public double ? Balance { get; set; }

    }
}
