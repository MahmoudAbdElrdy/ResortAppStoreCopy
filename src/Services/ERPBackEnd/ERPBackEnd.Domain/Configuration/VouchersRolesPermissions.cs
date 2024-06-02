using AutoMapper;
using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class VouchersRolesPermissions : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        public long? VoucherTypeId { get; set; }
        [ForeignKey(nameof(VoucherTypeId))]
        public VoucherType? VoucherType { get; set; }
        public bool? IsUserChecked { get; set; }
        public string? PermissionsJson { get; set; }
      
        public string? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; set; }


    }
}
