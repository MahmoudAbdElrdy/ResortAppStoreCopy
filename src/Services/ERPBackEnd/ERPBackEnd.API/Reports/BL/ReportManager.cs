
using ResortAppStore.Services.ERPBackEnd.Application.Reports;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;

namespace ResortAppStore.Services.ERPBackEnd.API.Reports.BL
{
    public class ReportManager
    {
        private readonly IReportFileRepository reportFileService;
        public ReportManager()
        {
            //reportFileService = new ReportFileService();

        }


        public List<ReportFile> ReportFilesList(int reportType, int reportTypeID)
        {
            List<ReportFile> reportList = reportFileService.GetReportList(reportType, reportTypeID);
            if (reportList.Any(x => x.IsDefault == true))
            {
                return reportList.Where(x => x.IsDefault == true).ToList();
            }
            return reportList;
        }


        public string Add(ReportFile report)
        {
            try
            {
                if (reportFileService.CheckExistByName(report.ReportNameAr))
                {
                    //File Name is Exist
                    return "File Is Exist";
                }
                else
                {
                    reportFileService.AddReport(report);
                    return "Success";
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        public bool deleteReport(int reportId)
        {
            return reportFileService.DeleteByID(reportId);
        }

        public bool setDefaultReport(int defaultRptID)
        {
            return reportFileService.UpdateReportDefault(defaultRptID);
        }

        public void cancelDefaultReport(int dReportType, int dReportTypeID)
        {
            reportFileService.CancelDefaultReportStatus(dReportType, dReportTypeID);
        }
    }
}