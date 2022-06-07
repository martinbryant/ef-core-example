using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ef_core_example.Models
{
    public class Manufacturer : MarketplaceModel
    {
        public string Name { get; set; }
    }
}