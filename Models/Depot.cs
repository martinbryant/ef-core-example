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

        public Address DeliveryAddress { get; set; }

        public override string ToString() => "Depot";
    }

    [Owned]
    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string PostCode { get; set; }
    }
}