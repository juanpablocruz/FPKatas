using System;

namespace FPKatas.Functor
{
    public interface IFunctor<T>
    {
        IFunctor<R> map<R>(Func<T, R> func);

        // [1,2,3] -> map(toString) -> ["1","2","3"]
        


        // in -> 2.... tryParse(in) -> Maybe<int>


        /*
         * 
         * Maybe =
         *  | Just t
         *  | Nothing
         *  
         *  
         *  maybe.map(fn)
         */
    }
}
