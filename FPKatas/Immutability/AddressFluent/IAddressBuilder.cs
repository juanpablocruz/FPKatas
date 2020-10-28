namespace FPKatas.Immutability
{
    public interface IAddressBuilder
    {
        Address Build();
        AddressBuilder WithCity(string newCity);
        AddressBuilder WithNoPostcode();
        AddressBuilder WithPostCode(PostCode newPostCode);
        AddressBuilder WithStreet(string newStreet);
    }
}
