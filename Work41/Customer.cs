using System;

namespace Work41
{
    class Customer
    {
        private string _customerName { get; set; }
        public Customer (string customerName)
        {
            _customerName = customerName;
        }
        public void GotNewGoods(object sender, GoodsInfoEventArgs e)
        {
            Console.WriteLine($"{_customerName} said: I want a {e.GoodsName}.");
        }
        public void MissingOldGoods(object sender, GoodsInfoEventArgs e)
        {
            Console.WriteLine($"{_customerName} said: bad, I so wanted a {e.GoodsName}.");
        }
    }
}
