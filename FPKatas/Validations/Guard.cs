using SGS.Framework.FunK;
using System;
using System.Linq.Expressions;

namespace FPKatas.Validations
{
    using static F;
    public static class Guard
    {
        public static Maybe<T> NotNull<T>(object value)
        {
            if (value is null)
            {
                return Nothing;
            }
            return Just((T)value);
        }

        public static Validation<TValue> Validate<TValue>(TValue arg, Expression<Func<TValue, bool>> expression)
        {
            var func = expression.Compile();
            var isValid = func(arg);
            if (!isValid)
            {
                return Error("Validation failed");
            }
            return Valid(arg);
        }

    }
}
