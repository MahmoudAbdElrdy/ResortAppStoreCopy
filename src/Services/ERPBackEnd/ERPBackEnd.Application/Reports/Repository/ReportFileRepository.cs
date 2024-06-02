
using AutoMapper;
using Common.Constants;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Reports.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ResortAppStore.Services.ERPBackEnd.Application.Reports
{
    // public class ReportFileRepository : IReportFileRepository
    public class ReportFileRepository : GMappRepository<ReportFile, ReportFileDto, long>, IReportFileRepository

    {
        #region property
        private readonly DbContext context;
        #endregion

        #region constructor 
        public ReportFileRepository(IGRepository<ReportFile> mainRepos, IMapper mapper, DeleteService deleteService, DbContext context):
            base(mainRepos, mapper, deleteService)
        {
            this.context = context;
        }

        public ReportFile Add(ReportFile entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportFile> AddRange(IEnumerable<ReportFile> entities)
        {
            throw new NotImplementedException();
        }
        #endregion
        public void AddReport(ReportFile reportFile)
        {
            try
            {
                context.Set<ReportFile>().Add(reportFile);
                context.SaveChanges();

                //this.context.ReportFile.Add(reportFile);
                //this.context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public void CancelDefaultReportStatus(int ReportType, int ReportTypeID)
        {
            foreach (ReportFile rpt in context.Set<ReportFile>().Where(x => x.ReportType == ReportType && x.ReportTypeId == ReportTypeID && x.IsDefault == true).ToList())
            {
                var existingItem = context.Set<ReportFile>().Find(rpt.Id);
                // validate
                if (existingItem != null)
                {

                    // update and save
                    existingItem.IsDefault = false;
                    context.Set<ReportFile>().Update(existingItem);
                    context.SaveChanges();
                }
            }
        }

        public bool CheckExist(ReportFile item)
        {
            throw new NotImplementedException();
        }

        public bool CheckExistById(long id)
        {
            throw new NotImplementedException();
        }

        public bool CheckExistByName(string reportName)
        {
            if (context.Set<ReportFile>().Any(x => x.ReportNameAr == reportName))
            {
                return true;
            }
            return false;
        }

        public bool DeleteByID(long reportId)
        {
            try
            {
                ReportFile rpt = this.context.Set<ReportFile>().Single(x => x.Id == reportId);
                this.context.Remove(rpt);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }

        public ReportFile Find(long reportId)
        {
           // return context.Set<ReportFile>().Find(x => x.Id == reportId);
            return context.Set<ReportFile>().Find(reportId);

           // throw new NotImplementedException();
        }

        public IEnumerable<ReportFile> Find(Expression<Func<ReportFile, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportFile> GetAll()
        {
            throw new NotImplementedException();
        }

        public ReportFile GetById(long id)
        {
            return context.Set<ReportFile>().Find(id);
        }

        public List<ReportFile> GetByType(int reportType)
        {
            throw new NotImplementedException();
        }

        public List<ReportFile> GetReportList(int ReportType, int ReportTypeID)
        {
            return this.context.Set<ReportFile>().Where(x => x.ReportType == ReportType && x.ReportTypeId == ReportTypeID).ToList();
        }

        public List<ReportFile> GetReportList()
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

        public bool Remove(ReportFile entity)
        {
            throw new NotImplementedException();
        }

        public bool RemoveById(long id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRange(IEnumerable<ReportFile> entities)
        {
            throw new NotImplementedException();
        }

        public void SetDatabaseName(string dbName)
        {
            throw new NotImplementedException();
        }

        public ReportFile Update(ReportFile item)
        {
            throw new NotImplementedException();
        }

        public bool UpdateReportDefault(long reportId)
        {
            try
            {
                List<ReportFile> rptList = context.Set<ReportFile>().Where(x => x.Id == reportId).ToList();
                foreach (ReportFile rpt in rptList)
                {
                    rpt.IsDefault = true;
                    context.Set<ReportFile>().Update(rpt);
                    context.SaveChanges();
                }
                //this.context.Update(rpt,);
                //this.context.SaveChanges();

                //ReportFile selectedRpt = context.ReportFile.Single(x => x.Id == reportId);
                //selectedRpt.IsDefault = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        string IReportFileRepository.AddReport(ReportFile rpt)
        {
            try
            {
                this.context.Set<ReportFile>().Add(rpt);
                this.context.SaveChanges();
                return "Success";
            }
            catch (Exception)
            {
                return "";

            }
        }
    }
}
