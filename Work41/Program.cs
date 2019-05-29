using System;

namespace Work41
{
    class Program
    {
        static void Main(string[] args)
        {
            OnlineShop shop = new OnlineShop();
            Customer customer1 = new Customer("Vasiy");
            Customer customer2 = new Customer("Petiy");
            
            shop.AddGoodsEvent += customer1.GotNewGoods;
            shop.AddGoodsEvent += customer2.GotNewGoods;
            shop.RemoveGoodsEvent += customer1.MissingOldGoods;
            shop.RemoveGoodsEvent += customer2.MissingOldGoods;

            Console.WriteLine("\nWe want to add a \"Book\"");
            shop.AddNewGoods("Book");
            Console.WriteLine("\nWe want to add a \"Book\"");
            shop.AddNewGoods("Book");
            Console.WriteLine("\nWe want to add a \"Pen\"");
            shop.AddNewGoods("Pen");
            Console.WriteLine("\nWe want to remove the \"Book\"");
            shop.RemoveOldGoods("Book");
            Console.WriteLine("\nWe want to remove the \"Penсil\"");
            shop.RemoveOldGoods("Penсil");

            Console.Write("\nFinish, press any key...");
            Console.ReadLine();
        }
    }
}
