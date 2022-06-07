using System;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Order : MarketplaceModel
    {
        public OrderStatus Status { get; set; }

        public DateTime ActionedOn { get; set; }
    }

    public enum OrderStatus
    {
        NotSet = 0,
        Reserved = 10,

        Confirmed = 20
    }
}