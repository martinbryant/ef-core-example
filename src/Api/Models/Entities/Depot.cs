using System;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Depot : MarketplaceModel
    {
        public const int Max_DisplayName_Length = 29;
        public const int Max_ContactName_Length = 41;
        public const int Max_ContactPhone_Length = 28;
        public const int Max_DepotId_Length = 1;

        private Depot(
                Profile profile,
                Address delivery,
                Address billing,
                string depotId,
                string displayName,
                string contactName,
                Email contactEmail,
                string contactPhone)
        {
            Profile = Profile;
            DeliveryAddress = delivery;
            BillingAddress = billing;
            DepotId = depotId;
            DisplayName = displayName;
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
        }

        private Depot() { }

        public Profile Profile { get; private set; }

        public string DepotId { get; private set; }

        public string DisplayName { get; private set; }

        public string ContactName { get; private set; }

        public string ContactPhone { get; private set; }

        public Email ContactEmail { get; private set; }

        public Address BillingAddress { get; private set; }

        public Address DeliveryAddress { get; private set; }

        // [JsonIgnore]
        // public Profile Profile { get; set; }

        // internal static Result<Depot, Error> EditDepot(Depot depot, DepotDto depotDto)
        // {
        //     var email = Email.Create(depotDto?.ContactEmail);
        // }

        internal static Result<Depot, Error> Create(DepotDto depotDto, Profile profile)
        {
            var delivery = Address.Create(depotDto.DeliveryAddress);

            var billing = Address.Create(depotDto.BillingAddress);

            var contactEmail = Email.Create(depotDto.ContactEmail);

            var depotId         = depotDto.DepotId;
            var displayName     = depotDto.DisplayName;
            var contactName     = depotDto.ContactName;
            var contactPhone    = depotDto.ContactPhone;

            return delivery
                    .Bind(_ => billing)
                    .Bind(_ => contactEmail)
                    .Map(_ => new Depot(profile, delivery.Value, billing.Value, depotId, displayName, contactName, contactEmail.Value, contactPhone));
        }
    }
}