using ef_core_example.Models;
using ef_core_example.Tests;

public class DepotTests
{
    [Fact(DisplayName = "[Depot] - Create depot a valid depot")]
    public void DepotCreationSuccessful()
    {
        var depotDto = ValidDepotDto;
        var profile  = ValidProfile;

        var actual = Depot.Create(depotDto, profile);

        Assert.True(actual.IsSuccess);
    }


    [Fact(DisplayName = "[Depot] - Fails on dto being null")]
    public void DepotDtoIsNull()
    {
        var depotDto = default(DepotDto);
        var profile  = ValidProfile;

        var actual = Depot.Create(depotDto, profile);

        Assert.False(actual.IsSuccess);
        Assert.Equal(Errors.General.Value_Is_Required, actual.Error.Code);
    }

    [Fact(DisplayName = "[Depot] - Fails on invalid billing adddress")]
    public void DepotFailedInValidBillingAddress()
    {
        var depotDto = ValidDepotDto;
        var profile  = ValidProfile;

        depotDto.BillingAddress = null;

        var actual = Depot.Create(depotDto, profile);

        Assert.False(actual.IsSuccess);
        Assert.Equal(Errors.General.Value_Is_Required, actual.Error.Code);
    }

    [Fact(DisplayName = "[Depot] - Fails on invalid billing adddress")]
    public void DepotFailedInValidDeliveryAddress()
    {
        var depotDto = ValidDepotDto;
        var profile  = ValidProfile;

        var validAddress = AddressTests.ValidAddressDto;

        depotDto.DeliveryAddress = null;

        var actual = Depot.Create(depotDto, profile);

        Assert.False(actual.IsSuccess);
        Assert.Equal(Errors.General.Value_Is_Required, actual.Error.Code);
    }

    [Fact(DisplayName = "[Depot] - Fails on invalid email")]
    public void DepotFailedInValidEmail()
    {
        var depotDto = ValidDepotDto;
        var profile  = ValidProfile;

        var validAddress = AddressTests.ValidAddressDto;

        depotDto.ContactEmail = null;

        var actual = Depot.Create(depotDto, profile);

        Assert.False(actual.IsSuccess);
        Assert.Equal(Errors.General.Value_Is_Required, actual.Error.Code);
    }
    
    public Address ValidAddress =>
        Address.Create(AddressTests.ValidAddressDto).Value;

    public Profile ValidProfile =>
        Profile.Create("a good name").Value;

    public static DepotDto ValidDepotDto =>
        new DepotDto()
        {
            DisplayName = "Name",
            ContactName = "Name",
            DeliveryAddress = AddressTests.ValidAddressDto,
            BillingAddress = AddressTests.ValidAddressDto,
            ContactEmail = "mail@mail.net",
            DepotId = "A"
        };
}