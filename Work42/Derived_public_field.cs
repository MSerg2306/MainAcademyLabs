using System;

namespace Work42
{
    public sealed class Derived_public_field : Base_public_field<Derived_public_field>
    {
        public Derived_public_field()
        {
            Console.WriteLine("An instance of the Derived_public_field class has been created.");
        }
    }
}
