using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work22
{
    class Box
    {
        //1.  Implement public  auto-implement properties for start position (point position)
        //auto-implement properties for width and height of the box
        //and auto-implement properties for a symbol of a given set of valid characters (*, + ,.) to be used for the border 
        //and a message inside the box

        private int height;
        public dynamic Height
        {
            get
            {
                return height;
            }
            set
            {
                height = inputParameters(value, "Высота", "высоту", MaxHeight);
            }
        }
        private int width;
        public dynamic Width
        {
            get
            {
                return width;
            }
            set
            {
                width = inputParameters(value, "Ширина", "ширину", MaxWidth);
            }
        }
        const char CornerChar = '+';
        const char LineChar = '*';
        public const int MaxHeight = 40; //высота
        public const int MaxWidth = 50; //ширина

        public Box()
        {
            width = 5;
            height = 5;
            draw(height, width);
        }

        //2.  Implement public Draw() method
        //to define start position, width and height, symbol, message  according to properties
        //Use Math.Min() and Math.Max() methods
        //Use draw() to draw the box with message
        public void Draw()
        {
            draw(this.height, this.width);
        }

        //3.  Implement private method draw() with parameters 
        //for start position, width and height, symbol, message
        //Change the message in the method to return the Box square
        //Use Console.SetCursorPosition() method
        //Trim the message if necessary
        private void draw(int height, int width)
        {
            //width - Ширина
            //height - Высота
            Console.Clear();
            Console.WindowHeight = MaxHeight + 2;
            Console.WindowWidth = MaxWidth + 2;

            Console.WindowLeft = 0;
            Console.WindowTop = 0;
            Console.SetCursorPosition(MaxWidth / 2 - width / 2, MaxHeight / 2 - height / 2);
            Console.Write(CornerChar + new String(LineChar, width - 2) + CornerChar);
            for (int i = 1; i <= height; i++)
            {
                Console.SetCursorPosition(MaxWidth / 2 - width / 2, MaxHeight / 2 - height / 2 + i);
                Console.Write(LineChar + new String(' ', width - 2) + LineChar);
            }
            Console.SetCursorPosition(MaxWidth / 2 - width / 2, MaxHeight / 2 - height / 2 + height);
            Console.Write(CornerChar + new String(LineChar, width - 2) + CornerChar);
            setCursorForInput();
        }
        private int inputParameters(dynamic v, string NameParam1, string NameParam2, int MaxParam)
        {
            for (; ; )
            {
                if (int.TryParse(v, out int number))
                {
                    if (int.Parse(v) <= MaxParam)
                    {
                        setCursorForInput();
                        return number;
                    }
                    else
                    {
                        setCursorForInput();
                        Console.Write($"{NameParam1} превышает допустимое значение (макимум - {MaxParam}). ");
                        Console.Write($"Введите {NameParam2}: ");
                        v = Console.ReadLine();
                    }
                }
                else
                {
                    setCursorForInput();
                    Console.Write("Вы ввели что-то не то. ");
                    Console.Write($"Введите {NameParam2}: ");
                    v = Console.ReadLine();
                }
            }
        }
        private void setCursorForInput()
        {
            Console.SetCursorPosition(0, MaxHeight);
            Console.Write(new String(' ', MaxWidth));
            Console.SetCursorPosition(0, MaxHeight);
        }
    }
}
