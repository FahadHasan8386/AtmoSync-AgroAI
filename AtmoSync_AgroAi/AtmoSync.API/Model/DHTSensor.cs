using AtmoSync.Shared;

namespace AtmoSync.API.Model
{
    public class DHTSensor : BaseModel
    {
        public long Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}
