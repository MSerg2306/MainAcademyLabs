using System;

namespace Work42
{
    public sealed class Derived_publ : Base_publ<Derived_publ>
    {
        public Derived_publ()
        {
            Console.WriteLine("An instance of the Derived_publ class has been created.");
        }
    }
}
