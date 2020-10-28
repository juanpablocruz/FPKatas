using SGS.Framework.FunK;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.OOP
{
    using static F;
    public class ValidatedConstructor
    {
        public string Name { get; }
        public int Age { get; }

        private ValidatedConstructor(string name, int age)
        {
            Name = name;
            Age = age;
        }
        
        public static Maybe<ValidatedConstructor> Of(string name, int age)
            => nameIsValid(name)
                    .Bind(validName =>
               ageIsValid(age)
                    .Map( validAge  => new ValidatedConstructor(validName, validAge)
               ));

        public static Validation<ValidatedConstructor> OfValidated(string name, int age)
        {
            Func<string, int, ValidatedConstructor> Create = (n, a) => new ValidatedConstructor(n, a);
            
            return Valid(Create)
                .Apply(ValidateName(name))
                .Apply(ValidateAge(age));
        }



        internal static Validation<int> ValidateAge(int age)
            => ageIsValid(age).Match(
                Nothing: () => Error("Invalid age"),
                Just: t => Valid(t));

        internal static Validation<string> ValidateName(string name)
            => nameIsValid(name).Match(
                Nothing: () => Error("Invalid name"),
                Just: t => Valid(t));

        internal static Maybe<int> ageIsValid(int age)
            => age > 18 && age < 73 ? Just(age) : Nothing;

        internal static Maybe<string> nameIsValid(string name)
            => string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name) ? Nothing : Just(name);
    }
}
