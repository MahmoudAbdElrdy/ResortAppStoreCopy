using Common.BaseController;
using Common.Interfaces;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using SaudiEinvoiceService.Constants;
using SaudiEinvoiceService.IRepos;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.SaudiaEInvoices.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class SaudiaEInvoicesController : BaseController
    {
        private readonly IEInvoiceManager _eInvoiceManager;
        private readonly ILogger<SaudiaEInvoicesController> _logger;
        private readonly IConfiguration _configuration;
        private IAuditService _auditService;

        public SaudiaEInvoicesController(IEInvoiceManager eInvoiceManager, ILogger<SaudiaEInvoicesController> logger , IConfiguration configuration
            , IAuditService auditService
            )
        {
            _eInvoiceManager = eInvoiceManager;
            _logger = logger;
            _configuration = configuration;
            _auditService = auditService;
        }
        [HttpGet("getCertificate")]
        public async Task GetCertificate()
        {
            _logger.LogInformation(Helper.EncodeTo64(Helper.CreateSha256Hash("0")));
            long companyId = Convert.ToInt64(_auditService.CompanyId);


            // return Ok((await _eInvoiceManager.GetAllRolesQuery()));
        }
    }
}
