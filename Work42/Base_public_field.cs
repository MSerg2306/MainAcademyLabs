using System;

namespace Work42
{
    public class Base_public_field<T> where T : new()
    {
        private T _instance;
        public T Instance
        {
            get
            {
                Console.WriteLine("Public field");
                _instance = new T();
                return _instance;
            }
        }
        static Base_public_field()
        {
            Console.WriteLine($"An instance of the Base_public_field class created at the {DateTime.Now}");
            T t = new T();
        }
    }
}
