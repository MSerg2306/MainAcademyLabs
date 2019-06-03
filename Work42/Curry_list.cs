using System;

namespace Work42
{
    public static class Curry_list
    {
        public static Func<TArg2, Func<TArgl, TResult>> Bnd<TArgl, TArg2, TResult>(this Func<TArgl, TArg2, TResult> f)
        {
            return (y) => ((x) => f(x, y));
        }
    }
}
