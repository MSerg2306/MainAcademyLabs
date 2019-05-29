using System;
using System.Collections.Generic;

namespace Work41
{
    class OnlineShop
    {
        public delegate void OnlineShopStateHandler(object sender, GoodsInfoEventArgs e);
        public event OnlineShopStateHandler AddGoodsEvent;
        public event OnlineShopStateHandler RemoveGoodsEvent;

        private static List<string> _goodsName;

        public OnlineShop()
        {
            _goodsName = new List<string>();
        }
        public void AddNewGoods (string goodsName)
        {
            if (!_goodsName.Contains(goodsName))
            {
                Console.WriteLine($"Add {goodsName} in shop...");
                _goodsName.Add(goodsName);
                AddGoodsEvent?.Invoke(this, new GoodsInfoEventArgs(goodsName));
            }
            else
            {
                Console.WriteLine($"{goodsName} not added, this product is already there!");
            }
        }
        public void RemoveOldGoods(string goodsName)
        {
            if (_goodsName.Contains(goodsName))
            {
                Console.WriteLine($"{goodsName} remove from shop...");
                _goodsName.Remove(goodsName);
                RemoveGoodsEvent?.Invoke(this, new GoodsInfoEventArgs(goodsName));
            }
            else
            {
                Console.WriteLine($"{goodsName} cannot be deleted because there is none!");
            }
        }
    }
}
