using System;

namespace Work42
{
    public class Base<T> where T : new()
    {
        static Base()
        {
            Console.WriteLine($"An instance of the Base class created at the {DateTime.Now}");
            T t = new T();
        }
    }
}
