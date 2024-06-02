using ResortAppStore.Services.ERPBackEnd.Application.Reports;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using System.Web.Http;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    public class ReportManagerController : ApiController
    {
        private readonly IReportFileRepository reportFileService;
        public ReportManagerController()
        {
            reportFileService = new ReportFileService();

        }

        [System.Web.Http.HttpGet]
        public List<ReportFile> ReportFilesList(int reportType, int reportTypeID)
        {
            List<ReportFile> rptList = reportFileService.GetReportList(reportType, reportTypeID);
            if (rptList.Any(x => x.IsDefault == true))
            {
                return rptList.Where(x => x.IsDefault == true).ToList();
            }
            return rptList;
        }

        [System.Web.Http.HttpPost]
        public string Add(ReportFile rpt)
        {
            try
            {
                if (reportFileService.CheckExistByName(rpt.ReportNameAr))
                {
                    //File Name is Exist
                    return "File Is Exist";
                }
                else
                {
                    reportFileService.AddReport(rpt);
                    return "Success";
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [System.Web.Http.HttpGet]
        public bool deleteReport(int reportId)
        {
            return reportFileService.DeleteByID(reportId);
        }
        [System.Web.Http.HttpGet]
        public bool setDefaultReport(int defaultRptID)
        {
            return reportFileService.UpdateReportDefault(defaultRptID);
        }
        [System.Web.Http.HttpGet]
        public void cancelDefaultReport(int dReportType, int dReportTypeID)
        {
            reportFileService.CancelDefaultReportStatus(dReportType, dReportTypeID);
        }
    }
}
