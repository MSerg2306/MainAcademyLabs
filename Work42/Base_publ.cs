using System;

namespace Work42
{
    public class Base_publ<T> where T : new()
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
        protected Base_publ()
        {
            Console.WriteLine($"An instance of the Base_publ class created at the {DateTime.Now}");
            T t = new T();
        }
    }
}
