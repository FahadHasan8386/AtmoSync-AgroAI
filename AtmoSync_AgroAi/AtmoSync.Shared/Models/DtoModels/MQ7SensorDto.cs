using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmoSync.Shared.Models.DtoModels
{
    public class MQ7SensorDto : BaseModel
    {
        public long Id { get; set; }
        public float COLevel { get; set; }
    }
}
