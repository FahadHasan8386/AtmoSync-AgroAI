using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmoSync.Shared.Models.DtoModels
{
    public class DHTSensorDto : BaseModel
    {
        public long Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}
