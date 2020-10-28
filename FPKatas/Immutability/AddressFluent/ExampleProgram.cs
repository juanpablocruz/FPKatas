namespace FPKatas.Immutability.AddressFluent
{
    public class ExampleProgram
    {
        public void Main()
        {
            IEndomorphism<AddressBuilder> morphism = new CityEndomorphism("Paris");
            Address address = morphism.Run(new AddressBuilder()).Build();
        }
    }
}
