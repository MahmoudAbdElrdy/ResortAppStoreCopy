using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ResortAppStore.Services.ERPBackEnd.Application.Reports.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using Common.Repositories;
using Common.Constants;

namespace ResortAppStore.Services.ERPBackEnd.Application.Reports
{
   public class ReportFileService : IReportFileRepository
    {
        #region property
        private readonly DbContext context;
        #endregion
        public void AddReport(ReportFile reportFile)
        {
            context.Set<ReportFile>().Add(reportFile);
            context.SaveChanges();
          

        }

        public List<ReportFile> GetReportList(int ReportType, int ReportTypeID)
        {
            //throw new NotImplementedException();
            return context.Set<ReportFile>().Where(x => x.ReportType == ReportType && x.ReportTypeId == ReportTypeID).ToList();
        }

        //public List<ReportFiles> GetInvoiceReportList(int billSettingId)
        //{
        //    //throw new NotImplementedException();
        //    return ReportFilesRepos.GetAll().Where(x => x.BILL_SETTING_ID == billSettingId).ToList();
        //}

        public bool UpdateReportDefault(long reportId)
        {
            try
            {
                ReportFile rpt = context.Set<ReportFile>().Single(x => x.Id == reportId);
                rpt.IsDefault = true;
                context.SaveChanges();
                //ReportFilesRepos.Update(rpt, reportId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckExistByName(string reportName)
        {
            if (context.Set<ReportFile>().Any(x => x.ReportNameAr == reportName))
            {
                //Report Exist
                return true;
            }
            else
            {
                //New Report Name
                return false;
            }
        }

        public bool DeleteByID(long reportId)
        {
            try
            {
                ReportFile report = context.Set<ReportFile>().Single(x => x.Id == reportId);

                context.Set<ReportFile>().Remove(report);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public void CancelDefaultReportStatus(int ReportType, int ReportTypeID)
        {
            foreach (ReportFile rpt in context.Set<ReportFile>().Where(x => x.ReportType == ReportType && x.ReportTypeId == ReportTypeID && x.IsDefault == true))
            {
                rpt.IsDefault = false;
                context.SaveChanges();
            }
        }

        public List<ReportFile> GetByType(int reportType)
        {
            throw new NotImplementedException();
        }

        public List<ReportFile> GetReportList()
        {
            throw new NotImplementedException();
        }

        public void SetDatabaseName(string dbName)
        {
            throw new NotImplementedException();
        }

        string IReportFileRepository.AddReport(ReportFile rpt)
        {
            throw new NotImplementedException();
        }

        public ReportFile Find(int id)
        {
            throw new NotImplementedException();
        }

       

        public ReportFile GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportFile> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportFile> Find(Expression<Func<ReportFile, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ReportFile Add(ReportFile entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportFile> AddRange(IEnumerable<ReportFile> entities)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ReportFile entity)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRange(IEnumerable<ReportFile> entities)
        {
            throw new NotImplementedException();
        }

      

        public ReportFile Update(ReportFile item)
        {
            throw new NotImplementedException();
        }

        public bool CheckExist(ReportFile item)
        {
            throw new NotImplementedException();
        }

        public ReportFile Find(long reportId)
        {
            throw new NotImplementedException();
        }

    

        public ResponseResult<ReportFile> Insert(ReportFile entity)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool RemoveById(long id)
        {
            throw new NotImplementedException();
        }

        public bool CheckExistById(long id)
        {
            throw new NotImplementedException();
        }
    }
}     


    

