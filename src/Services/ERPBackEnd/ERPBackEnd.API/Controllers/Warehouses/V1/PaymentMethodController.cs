
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PaymentMethods.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PaymentMethods.Repository;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class PaymentMethodController : MainController<PaymentMethod, PaymentMethodDto, long>
    {
        public PaymentMethodController(GMappRepository<PaymentMethod, PaymentMethodDto, long> mainRepos,
            IPaymentMethodRepository PaymentMethodRepository) : base(mainRepos)
        {
        }

       

       
    }
}
