using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Immutability
{
    public class StreetEndomorphism : IEndomorphism<AddressBuilder>
    {
        private readonly string street;

        public StreetEndomorphism(string street)
        {
            this.street = street;
        }

        public AddressBuilder Run(AddressBuilder x)
            => x.WithStreet(street);
    }
}
