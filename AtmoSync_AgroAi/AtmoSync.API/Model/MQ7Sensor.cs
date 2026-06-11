using AtmoSync.Shared;

namespace AtmoSync.API.Model
{
    public class MQ7Sensor : BaseModel
    {
        public long Id { get; set; }
        public float COLevel { get; set; }
    }
}
