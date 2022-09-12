using System.Text.Json.Serialization;

namespace CAVU.ParkingAPI.RequestDto
{
    public class GetAvilablilityRequestDto
    {
        [JsonPropertyName("StartDate")]
        public DateTime  StartDate { get; set; }


        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
    }
}
