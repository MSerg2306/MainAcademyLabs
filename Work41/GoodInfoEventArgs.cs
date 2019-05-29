using System;

namespace Work41
{
    class GoodsInfoEventArgs : EventArgs
    {
        public string GoodsName { get; }
        public GoodsInfoEventArgs(string goodsName)
        {
            GoodsName = goodsName;
        }
    }
}
