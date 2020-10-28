namespace FPKatas.Immutability
{
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public PostCode postCode { get; set; }
        public Address(string street, string city, PostCode postCode)
        {
            this.street = street;
            this.city = city;
            this.postCode = postCode;
        }
    }
}
