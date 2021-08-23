using System.ComponentModel.DataAnnotations;

namespace ef_core_example.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }
        public Depot Seller { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required]
        [MaxLength(12)]
        public string PartNumber { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        public int TotalQuantity { get; set; }
        public decimal ListingPrice { get; set; }
        public char Currency => '£';
        public bool InTransit { get; set; }
        public bool MarkedForDeletion { get; set; }
    }
}