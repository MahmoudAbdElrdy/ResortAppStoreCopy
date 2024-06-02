using AutoMapper;
using Common.Constants;
using Common.Extensions;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Entities;
using Egypt_EInvoice_Api.EInvoiceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.Users.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.EInvoices.BLL;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Repository
{
    public class ItemCardRepository : GMappRepository<Domain.Warehouses.ItemCard, ItemCardDto, long>, IItemCardRepository
    {
        private IGRepository<Domain.Warehouses.ItemCard> _itemCardRepos { get; set; }
        private IGRepository<ItemGroupsCard> _itemGroupsRepos { get; set; }
        private IGRepository<ItemCardUnit> _itemCardUnitRepos { get; set; }
        private IGRepository<ItemCardBalance> _itemCardBalanceRepos { get; set; }
        private IGRepository<ItemCardAlternative> _itemCardAlternativeRepos { get; set; }
        private IGRepository<ItemCardDeterminant> _itemCardDeterminantRepos { get; set; }
        private IGRepository<BillDynamicDeterminant> _billDynamicDeterminantRepos { get; set; }
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        public IEInvoiceGovManager _invoiceGovManager { get; set; }
        private IAuditService _auditService;
        private IMapper _mpper;
        private IUserRepository _userRepository;
        private readonly IGRepository<Company> _companyRepository;
        HttpContext _context;

        public ItemCardRepository(
            IGRepository<Domain.Warehouses.ItemCard> mainRepos,
            IAuditService auditService,
             IGRepository<ItemCardUnit> itemCardUnitRepos,
             IGRepository<ItemCardBalance> itemCardBalanceRepos,
            IMapper mapper, DeleteService deleteService,
            IGRepository<ItemCardAlternative> itemCardAlternativeRepos,
            IGRepository<ItemCardDeterminant> itemCardDeterminantRepos,
            IGRepository<BillDynamicDeterminant> billDynamicDeterminantRepos,
            IGRepository<GeneralConfiguration> generalConfiguration,
            IUserRepository userRepository,
            IGRepository<ItemGroupsCard> itemGroupsRepos,
            IEInvoiceGovManager invoiceGovManager,
            IGRepository<Company> companyRepository
            )
            : base(mainRepos, mapper, deleteService)
        {
            _itemCardRepos = mainRepos;
            _itemCardUnitRepos = itemCardUnitRepos;
            _itemCardBalanceRepos = itemCardBalanceRepos;
            _itemCardAlternativeRepos = itemCardAlternativeRepos;
            _itemCardDeterminantRepos = itemCardDeterminantRepos;
            _auditService = auditService;
            _mpper = mapper;
            _billDynamicDeterminantRepos = billDynamicDeterminantRepos;
            _generalConfiguration = generalConfiguration;
            _userRepository = userRepository;
            _itemGroupsRepos = itemGroupsRepos;
            _invoiceGovManager = invoiceGovManager;
            _companyRepository = companyRepository;
        }

        public async Task<ItemCardDto> FirstInclude(long id)
        {
            var item = await _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits).Include(c => c.ItemCardAlternatives).Include(c=>c.ItemCardBalances).Include(c => c.ItemCardDeterminants).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mpper.Map<ItemCardDto>(item);
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {

            var itemResult = await FirstInclude(id);

            if (itemResult?.ItemCardDeterminants != null)
            {
                foreach (var item in itemResult?.ItemCardDeterminants)
                {
                    await _itemCardDeterminantRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }

            if (itemResult?.ItemCardUnits != null)
            {
                foreach (var item in itemResult?.ItemCardUnits)
                {
                    await _itemCardUnitRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (itemResult?.ItemCardBalances != null)
            {
                foreach (var item in itemResult?.ItemCardBalances)
                {
                    await _itemCardBalanceRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (itemResult?.ItemCardAlternatives != null)
            {
                foreach (var item in itemResult?.ItemCardAlternatives)
                {
                    await _itemCardAlternativeRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }

            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "ItemCardUnits", "ItemCardAlternatives", "ItemCardDeterminants" }, "ItemCards", "Id");


        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var itemCardResult = await FirstInclude(Convert.ToInt64(id));

                if (itemCardResult?.ItemCardDeterminants != null)
                {
                    foreach (var item in itemCardResult?.ItemCardDeterminants)
                    {
                        await _itemCardDeterminantRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }

                if (itemCardResult?.ItemCardUnits != null)
                {
                    foreach (var item in itemCardResult?.ItemCardUnits)
                    {
                        await _itemCardUnitRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (itemCardResult?.ItemCardBalances != null)
                {
                    foreach (var item in itemCardResult?.ItemCardBalances)
                    {
                        await _itemCardBalanceRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (itemCardResult?.ItemCardAlternatives != null)
                {
                    foreach (var item in itemCardResult?.ItemCardAlternatives)
                    {
                        await _itemCardAlternativeRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }


            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "ItemCardUnits", "ItemCardAlternatives", "ItemCardDeterminants" }, "ItemCard", "Id");
        }
        public async Task<ItemCardDto> CreateItemCard(ItemCardDto input)
        {
            var result = await base.Create(input);
            return input;
        }

        public async Task<ItemCardDto> UpdateItemCard(ItemCardDto input)
        {

            var itemCardsResult = await FirstInclude(Convert.ToInt64(input.Id));

            if (itemCardsResult?.ItemCardDeterminants != null)
            {
                foreach (var item in itemCardsResult?.ItemCardDeterminants)
                {
                    await _itemCardDeterminantRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }

            if (itemCardsResult?.ItemCardUnits != null)
            {
                foreach (var item in itemCardsResult?.ItemCardUnits)
                {
                    var entity = _mpper.Map<ItemCardUnit>(item);
                    await _itemCardUnitRepos.SoftDeleteAsync(entity);
                }

            }

            if (itemCardsResult?.ItemCardBalances != null)
            {
                foreach (var item in itemCardsResult?.ItemCardBalances)
                {
                    var entity = _mpper.Map<ItemCardBalance>(item);
                    await _itemCardBalanceRepos.SoftDeleteAsync(entity);
                }

            }
            if (itemCardsResult?.ItemCardAlternatives != null)
            {
                foreach (var item in itemCardsResult?.ItemCardAlternatives)
                {
                   
                        var entity = _mpper.Map<ItemCardAlternative>(item);
                        await _itemCardAlternativeRepos.SoftDeleteAsync(entity);
                  }

            }
            var result = await base.Update(input);

            return input;

        }
        #region Get Last Code  
        public string getLastCodeByItemGroupId(long itemGroupId)
        {
            var code = "";

            List<Domain.Warehouses.ItemCard> ItemsList = _itemCardRepos.GetAll().Where(x => x.ItemGroupId == itemGroupId && x.IsDeleted != true).ToList();
            if (ItemsList.Count > 0 && ItemsList != null)
            {
                code = ItemsList.Select(x => x.Code).LastOrDefault().ToString();
                code = (long.Parse(code) + 1).ToString();

            }
            else
            {
                code = itemGroupId + "1";
                return code;
            }


            return code;

        }

        #endregion
        public async Task<bool> CheckHaveDeterminant(long itemCardId)
        {
            var check =await _itemCardDeterminantRepos.GetAll().AnyAsync(c => c.ItemCardId == itemCardId);
            return check;
        }
        public virtual async Task<ResponseResult<List<CalculateItemCardBalanceDto>>> CalculateItemBalances(long ItemCardId,long StoreId,DateTime? BillDate )
        {
            var sp = "SP_Item_Balances";
            var fiscalPeriodId = "7";
            long? fiscalPeriodIdValue = null;

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            var branchIds = "";

            if(BillDate == null)
            {
                
               var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();
               // long fiscalPeriodIdValue = 0;
                if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
                {
                    fiscalPeriodIdValue = long.Parse(fiscalPeriodIdResult.Value);
                }
            }
           

            var  calculateBalances = "10";
            long calculateBalanceValue = 1;
            var calculateBalanceResult = await _generalConfiguration.GetAll().Where(x => x.Code == calculateBalances).FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(calculateBalanceResult.Value))
            {
                calculateBalanceValue = long.Parse(calculateBalanceResult.Value);
            }
           
            if (calculateBalanceValue == (int)CalculateBalancesEnum.UserBranches)
            {
                var userId = _auditService.UserId;
                var user = await _userRepository.GetById(new GetById() { Id = userId });
                if(user!=null && user.Branches!=null)
                {
                    foreach (var item in user.Branches)
                    {
                        branchIds += item + ",";
                    }
                    

                }


            }
            else if (calculateBalanceValue == (int)CalculateBalancesEnum.CurrentBranch)
            {
                branchIds = _auditService.BranchId;
            }


            var result = _itemCardRepos.Excute<CalculateItemCardBalanceDto>(sp, new List<SqlParameter>() {
                 new SqlParameter(){
                    ParameterName = "@itemCardId",
                    Value = ItemCardId,


                },
                  new SqlParameter(){
                    ParameterName = "@storeId ",
                    Value = StoreId,


                },

                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchIds == "" ? null : branchIds,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId" ,
                    Value = fiscalPeriodIdValue,

                },
                new SqlParameter(){
                    ParameterName = "@billDate",
                    Value = BillDate,

                }
            }, true);

            return result;
        }
        public virtual async Task<ResponseResult<List<ItemCardDto>>> ExecuteGetItemsCardInRefernces()
        {
            var sp = "SP_Get_Item_Card_In_References";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);

            var  userId = _auditService.UserId;

            var result =  _itemCardRepos.Excute<ItemCardDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                   new SqlParameter(){
                    ParameterName = "@userId",
                    Value = userId,

                }

            }, true);

            return result;
        }

        public  List<ItemCardDto> GetItemsByItemGroupId(long ItemGroupId)
        {

            List<Domain.Warehouses.ItemCard> ItemsList = _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits).Include(c => c.ItemCardAlternatives).Include(c => c.ItemCardBalances).Include(c => c.ItemCardDeterminants).Where(x => x.ItemGroupId == ItemGroupId && x.IsDeleted != true).ToList();
            if (ItemsList.Count > 0 && ItemsList != null)
            {

                var result = _mpper.Map<List<ItemCardDto>>(ItemsList);
                return result;

            }
            return null;
        }
        public async Task UpdateGPCCode(long ItemGroupId , string GPCCode )
        {
            //var itemGroupsCard = await _itemGroupsRepos.FirstOrDefaultAsync(x => x.Id == ItemGroupId);
            //if(itemGroupsCard!=null)
            //{
            //    itemGroupsCard.GPCCode = GPCCode;
            //    await _itemGroupsRepos.Update(itemGroupsCard);
            //}
            List<ItemCardDto> ItemsList = GetItemsByItemGroupId(ItemGroupId);

            if(ItemsList!=null && ItemsList.Count>0)
            {
                foreach (var item in ItemsList)
                {
                    item.GPCCode = GPCCode;
                    await base.Update(item);


                }

            }

        }

        public async Task<int> UploadItems(List<object> ids)
        {
            long companyId = Convert.ToInt64(_auditService.CompanyId);

            var company = await _companyRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(c => c.Id == companyId && c.IsDeleted != true);

            var companyResult = _mpper.Map<CompanyDto>(company);

            if (string.IsNullOrEmpty(companyResult.ClientId))
            {
                throw new UserFriendlyException("Client Id is required");
            }
            if (string.IsNullOrEmpty(companyResult.ClientSecret))
            {
                throw new UserFriendlyException("Client Secret is required");
            }
            List<ESGItem> list = new List<ESGItem>();

            foreach (var id in ids)
            {

                var itemCardResult = await FirstInclude(Convert.ToInt64(id));

                if (itemCardResult != null)
                {

                    ESGItem obj = new  ESGItem();

                    if (string.IsNullOrEmpty(itemCardResult.ItemCodeType))
                    {
                        throw new UserFriendlyException("Item Code Type is Required For Item : " + itemCardResult.NameEn);
                    }
                    if (string.IsNullOrEmpty(itemCardResult.ItemCode))
                    {
                        throw new UserFriendlyException("Item Code is Required For Item : " + itemCardResult.NameEn);

                    }
                    if (string.IsNullOrEmpty(itemCardResult.GPCCode))
                    {
                        throw new UserFriendlyException("GPC Code is Required For Item : " + itemCardResult.NameEn);

                    }
                    obj.codeType = itemCardResult.ItemCodeType;
                    obj.itemCode = itemCardResult.ItemCode;
                  
                    obj.activeFrom = DateTime.Now;
                    obj.activeTo = "";
                    obj.codeName = itemCardResult.NameAr;
                    obj.codeNameAr = itemCardResult.NameEn;
                    obj.description = itemCardResult.Description;
                    obj.descriptionAr = itemCardResult.Description;
                    obj.linkedCode = "";
                    obj.parentCode = itemCardResult.GPCCode;
                    obj.requestReason = "New Product";

                    list.Add(obj);
                 

                }
            }

            var loginResponse = await _invoiceGovManager.Login();
            if (loginResponse!=null)
            {
                _invoiceGovManager.CreateESGCode(list);

            }
            else
            {
                throw new UserFriendlyException("authentication-with-egyptian-Tax-authority-failed");
            }
            return 0;
        }
        public List<ItemCardDto> GetPOSItemsByItemSearch(string item)
        {

            List<Domain.Warehouses.ItemCard> ItemsList = _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits)
    .Where(x => (x.Code.Contains(item) || x.NameAr.ToLower().Contains(item.ToLower()) || x.NameEn.ToLower().Contains(item.ToLower()))
        && x.IsDeleted != true)
    .Join(_itemGroupsRepos.GetAll(), 
          itemCard => itemCard.ItemGroupId,
          itemGroup => itemGroup.Id,
          (itemCard, itemGroup) => new { ItemCard = itemCard, ItemGroup = itemGroup }).Where(x=>x.ItemGroup.IsBelongsToPOS ==true)
    .Select(joined => joined.ItemCard) 
    .ToList();


            if (ItemsList.Count > 0 && ItemsList != null)
            {

                var result = _mpper.Map<List<ItemCardDto>>(ItemsList);
                return result;

            }
            return null;
        }

        public List<ItemCardDto> GetPOSItems()
        {

            List<Domain.Warehouses.ItemCard> ItemsList = _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits)
    .Where(x=>x.IsDeleted != true)
    .Join(_itemGroupsRepos.GetAll(),  
          itemCard => itemCard.ItemGroupId,
          itemGroup => itemGroup.Id,
          (itemCard, itemGroup) => new { ItemCard = itemCard, ItemGroup = itemGroup }).Where(x => x.ItemGroup.IsBelongsToPOS == true)
    .Select(joined => joined.ItemCard)  
    .ToList();


            if (ItemsList.Count > 0 && ItemsList != null)
            {

                var result = _mpper.Map<List<ItemCardDto>>(ItemsList);
                return result;

            }
            return null;
        }





    }
}
