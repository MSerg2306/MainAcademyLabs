using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work22
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Implement start position, width and height, symbol, message input
                Box box = new Box();

                //Create Box class instance
                Console.Write("Введите высоту: ");
                box.Height = Console.ReadLine();
                Console.Write("Введите ширину: ");
                box.Width = Console.ReadLine();

                //Use  Box.Draw() method
                box.Draw();

                Console.Write("Press any key...");
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
                Console.ReadLine();
            }
        }
    }
}
