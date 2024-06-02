using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity
{
    public class DeleteEntity
    {
        public string TableName { get; set; }
        public object Id { get; set; }
        public string IdName { get; set; } 
    }
    public class DeleteListEntity
    {
        public string TableName { get; set; }
        public List<object> Ids { get; set; } 
        public string IdName { get; set; }
    }
}
