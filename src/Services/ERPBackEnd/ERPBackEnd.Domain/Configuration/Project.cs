using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Configuration.Entities
{
    public class Project : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(200)]
        public string? Code { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Project Parent { get; set; }
        public virtual ICollection<Project> Children { get; set; }
    }
}