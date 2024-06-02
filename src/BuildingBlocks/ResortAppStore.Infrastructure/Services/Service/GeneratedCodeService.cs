using Common.Entity;
using Common.Helper;
using Common.Interfaces;
using Common.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Service
{
    public class GeneratedCodeService<TEntity, TKey>  where TEntity : class, IEntity<TKey>
    {
        private readonly IGRepository<TEntity> _context;
        private readonly IAuditService _auditService;
        public GeneratedCodeService(IGRepository<TEntity> context, IAuditService auditService)
        {
            _context = context;
            _auditService = auditService;
        }
        //public virtual async Task<string> GetCode(Expression<Func<TEntity, bool>> predicateId, Expression<Func<TEntity, bool>> predicateIsDeleted)
        //{
        //    var lastCode = await _context.GetAll().OrderByDescending(predicateId).FirstOrDefaultAsync(predicateIsDeleted);
        //    var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));
        //    _auditService.Code = code;
        //    return code;
        //  //  return string.Empty;
        //}
        public virtual async Task<string> CheckCodeWithAdd()
        {
            return string.Empty;
        }
        public virtual async Task<string> CheckCCodeWithEdit()  
        {
            return string.Empty;
        }
    }
}
