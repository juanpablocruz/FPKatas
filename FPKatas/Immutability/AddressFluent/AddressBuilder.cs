namespace FPKatas.Immutability
{
    public class AddressBuilder : IAddressBuilder
    {
        private string street;
        private string city;
        private PostCode postCode;
 
        public AddressBuilder(string street = "", string city = "", PostCode postCode = null)
        {
            this.street = "";
            this.city = "";
            this.postCode = postCode ?? new PostCodeBuilder().Build();
        }

        public AddressBuilder WithStreet(string newStreet)
            => new AddressBuilder(newStreet, this.city);

        public AddressBuilder WithCity(string newCity)
            => new AddressBuilder(this.street, newCity);

        public AddressBuilder WithPostCode(PostCode newPostCode)
            => new AddressBuilder(this.street, this.city, newPostCode);

        public AddressBuilder WithNoPostcode()
            => new AddressBuilder(this.street, this.city, new PostCode());

        public Address Build()
        {
            return new Address(this.street, this.city, this.postCode);
        }
    }
}
