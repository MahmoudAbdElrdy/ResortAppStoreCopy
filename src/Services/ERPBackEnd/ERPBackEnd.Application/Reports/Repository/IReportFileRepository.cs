using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResortAppStore.Services.ERPBackEnd.Application.Reports
{
    public interface IReportFileRepository
        //: IBaseRepository<ReportFile>
    {
        //void AddReport(ReportFile reportFile);
        List<ReportFile> GetReportList(int ReportType, int ReportTypeID);

        bool UpdateReportDefault(long reportId);

        bool CheckExistByName(string reportName);
        bool DeleteByID(long reportId);
        void CancelDefaultReportStatus(int ReportType, int ReportTypeID);
        List<ReportFile> GetByType(int reportType);
        // List<ReportFile> GetReportList();
        void SetDatabaseName(string dbName);
        string AddReport(ReportFile rpt);
        bool IsExist(string fileName);
        ReportFile Find(long reportId);
    }
}
