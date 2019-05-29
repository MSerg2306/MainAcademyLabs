using System;

namespace Work41
{
    class Customer
    {
        private string CustomerName;
        public Customer (string customerName)
        {
            CustomerName = customerName;
        }
        public void GotNewGoods(object sender, GoodsInfoEventArgs e)
        {
            Console.WriteLine($"{CustomerName} said: I want a {e.GoodsName}.");
        }
        public void MissingOldGoods(object sender, GoodsInfoEventArgs e)
        {
            Console.WriteLine($"{CustomerName} said: bad, I so wanted a {e.GoodsName}.");
        }
    }
}
