using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ef_core_example.Models
{
    [Index(nameof(DepotId), nameof(ProfileId), IsUnique = true)]
    public class Depot : Marketplace
    {
        public Guid ProfileId { get; set; }

        [MaxLength(1)]
        public string DepotId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [JsonIgnore]
        public Profile Profile { get; set; }

        public string DeliveryAddress1 { get; set; }
        public string DeliveryAddress2 { get; set; }
        public string DeliveryAddress3 { get; set; }
        public string DeliveryAddress4 { get; set; }
        public string DeliveryPostCode { get; set; }

        public override string ToString() => "Depot";
    }
}