using System;

namespace Work42
{
    public sealed class Derived : Base<Derived>
    {
        public Derived()
        {
            Console.WriteLine("An instance of the Derived class has been created.");
        }
    }
}
