using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ef_core_example.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Profile : Marketplace
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Depot> Depots { get; set; }

        public override string ToString() => "Profile";
    }
}