namespace ef_core_example.Models
{
    public class DepotDto
    {
        public string DepotId { get; set; }

        public string ProfileId { get; set; }

        public string DisplayName { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public AddressDto DeliveryAddress { get; set; }

        public AddressDto BillingAddress { get; set; }
        
        public string DeliveryTerms { get; set; }

        public bool MarketplaceEnabled { get; set; }
    }
}