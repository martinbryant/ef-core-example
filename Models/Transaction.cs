using System;
using System.ComponentModel.DataAnnotations;

namespace ef_core_example.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public Depot Buyer { get; set; }
        public Listing Listing { get; set; }

        public Address DeliveryAddress { get; set;}

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [MaxLength(6)]
        public string OrderNumber { get; set; }

        [Required]
        [MaxLength(2)]
        public string ClientPrefix { get; set; }

        [MaxLength(256)]
        public string Error { get; set; }

        [MaxLength(12)]
        public string GoldReference { get; set; }
        
        [MaxLength(10)]
        public string ActionedBy { get; set; }

        public DateTime OrderedDate  { get; set; }
        public DateTime ActionedDate  { get; set; }
    }
}