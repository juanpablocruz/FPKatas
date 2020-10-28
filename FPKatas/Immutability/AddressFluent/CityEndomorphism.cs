namespace FPKatas.Immutability
{
    public class CityEndomorphism : IEndomorphism<AddressBuilder>
    {
        private readonly string city;

        public CityEndomorphism(string city)
        {
            this.city = city;
        }

        public AddressBuilder Run(AddressBuilder x)
            => x.WithCity(city);
    }
}
