using AtmoSync.Shared;

namespace AtmoSync.API.Model
{
    public class MQ136Sensor : BaseModel
    {
        public long Id { get; set; }
        public float H2SLevel { get; set; }
    }
}
