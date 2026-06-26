using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmoSync.Shared
{
    public class BaseModel
    {
        public string CreatedBy { get; set; } = "AtmoSync Agro Ai";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool InActive { get; set; } = false;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
