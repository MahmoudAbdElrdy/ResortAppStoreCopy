using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SharedDto
{
    public class OutPutCreateDataBaseDto 
    {
        public bool IsCreated { get; set; }
    }
    public class InputCreateDataBaseDto 
    {
        public string? NameDatabase { get; set; } 
        public string? ConnectionString{ get; set; }  
    }
}
