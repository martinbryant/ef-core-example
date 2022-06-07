namespace ef_core_example.Models
{
    public class MarketplaceDepot
    {
        public string DepotId { get; set; }

        public string ProfileId { get; set; }

        public string DisplayName { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public string DeliveryAddress1 { get; set; }

        public string DeliveryAddress2 { get; set; }

        public string DeliveryAddress3 { get; set; }

        public string DeliveryAddress4 { get; set; }

        public string DeliveryPostCode { get; set; }

        public string BillingAddress1 { get; set; }

        public string BillingAddress2 { get; set; }

        public string BillingAddress3 { get; set; }

        public string BillingAddress4 { get; set; }

        public string BillingPostCode { get; set; }
        
        public string DeliveryTerms { get; set; }

        public bool MarketplaceEnabled { get; set; }
    }
}