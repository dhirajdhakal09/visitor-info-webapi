using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Domain.DTO
{
    public class VisitorDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "checkinTime")]
        public DateTime CheckInTime { get; set; }
        [JsonProperty(PropertyName = "checkoutTime")]
        public DateTime CheckOutTime { get; set; }
        [JsonProperty(PropertyName = "checkinStatus")]
        public bool CheckInStatus { get; set; }
    }
}
