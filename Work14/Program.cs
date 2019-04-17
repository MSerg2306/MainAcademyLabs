using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work4
{
    enum NumberToWord
    {
        первое,
        второе,
        третье,
        четвертое,
        пятое,
        шестое,
    }

    class Gamer
    {
        public string Name;
        public int HendMoney;
        public int[,] GamerHistory = new int[100, 6];
        public Gamer(int hendMoney)
        {
            HendMoney = hendMoney;
        }
    }
    class Game
    {
        public static int[] CurrentThrow()
        {
            int[] NewThrow=new int[6];
            Random rnd = new Random();
            
            //заполнение массива текущего розыгрыша
            NewThrow[0] = rnd.Next(1, 36); //первый шар

            for (int i=1; i<=5; i++)
            {
                int NextBall;
                bool next = true;
                do
                {
                    NextBall = rnd.Next(1, 36);
                    next = true;
                    foreach (int t in NewThrow)
                    {
                        if (t==NextBall)
                        {
                            next = false;
                            break;
                        }
                    }

                } while (next == false);
                NewThrow[i] = NextBall;
            }
            return NewThrow;
        }
        public static int[] CurrentSelection(string name)
        {
            int[] cs = new int[6];
            Console.WriteLine($"{name}, введите 6 цифр от 1 до 36:");
            for (int i = 0; i <= 5; i++)
            {
                for (; ; )
                {
                    Console.Write($"Введите {(NumberToWord)i} число: ");
                    var currentnumber = Console.ReadLine();
                    if (int.TryParse(currentnumber, out int number))
                    {
                        if (int.Parse(currentnumber) >= 1 & int.Parse(currentnumber) <= 36)
                        {
                            cs[i] = int.Parse(currentnumber);
                            break;
                        }
                        else
                        {
                            Console.Write("Вы ввели что-то не то...");
                        }
                    }
                    else
                    {
                        Console.Write("Вы ввели что-то не то...");
                    }
                }
            }
            return cs;
        }
        public static void GameSpecification(string name)
        {
            Console.WriteLine();
            Console.WriteLine($"{name}, правила игры:");
            Console.WriteLine("Нужно вспомнить старое, доброе спорт-лото - угадать 6 цифр из 36.");
            Console.WriteLine("Вам дается 100 кредитов. Каждая попытка стоит 10 кредитов.");
            Console.WriteLine("Награда:");
            Console.WriteLine("- 1 угаданная цифра - 1 кредит");
            Console.WriteLine("- 2 угаданные цифры - 10 кредитов");
            Console.WriteLine("- 3 угаданные цифры - 100 кредитов");
            Console.WriteLine("- 4 угаданные цифры - 1.000 кредитов");
            Console.WriteLine("- 5 угаданные цифры - 10.000 кредитов");
            Console.WriteLine("- 6 угаданные цифры - 100.000 кредитов");
            Console.WriteLine("Играть можно пока есть кредиты или уйти в любой момент с солидным выиграшем)");
            Console.WriteLine("Приятной игры и пусть фартуна Вам улыбнется!!!");
            Console.WriteLine(new string('-', 40));
        }
        public static int GameExit()
        {
            int i = -1;
            Console.Write("Хотите продолжить (да/нет)? ");
            while (i==-1)
            { 
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "да":
                        {
                            i=1;
                            break;
                        }
                    case "нет":
                        {
                            i=0;
                            break;
                        }
                    default:
                        {
                            Console.Write("Ответьте, пожалуйста правильно, \"да\" или \"нет\"... ");
                            break;
                        }
                }
            }
            return i;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[,] GameHistory = new int[100, 6];
            Gamer gamer = new Gamer(100);
            Console.Write("Представтесь, пожалуйста... - ");
            gamer.Name = Console.ReadLine();

            Game.GameSpecification(gamer.Name);

            if (Game.GameExit()==1)
            {
                for (int gameId=0; gameId<=99; gameId++)
                { 
                    if (gamer.HendMoney >= 10)
                    {
                        //ставка
                        gamer.HendMoney = gamer.HendMoney - 10;

                        //заполнение билетика
                        int[] CurrentSelection = Game.CurrentSelection(gamer.Name);
                        for (int i=0; i<=5; i++)
                        {
                            gamer.GamerHistory[gameId, i] = CurrentSelection[i];
                        }
                        Console.WriteLine(new string('-', 40));

                        Console.WriteLine("Ваши цифры:");
                        for (int i = 0; i <= 5; i++)
                        {
                            Console.Write(gamer.GamerHistory[gameId, i] + " ");
                        }
                        Console.Write("\r\n");

                        //генерация розыгрыша
                        int [] CurrentGame = Game.CurrentThrow();
                        for (int i = 0; i <= 5; i++)
                        {
                            GameHistory[gameId, i] = CurrentGame[i];
                        }
                        Console.WriteLine("Лототрон выбросил такие шары:");
                        for (int i = 0; i <= 5; i++)
                        {
                            Console.Write(GameHistory[gameId, i] + " ");
                        }
                        Console.Write("\r\n");
                        
                        //проверка результата
                        int guessed = 0;
                        for (int i=0; i<=5; i++)
                        {
                            for (int j=0; j<=5; j++)
                            {
                                if (gamer.GamerHistory[gameId, i] == GameHistory[gameId, j])
                                {
                                    ++guessed;
                                    break;
                                }
                            }
                        }
                        if (guessed>0)
                        {
                            string s;
                            switch (guessed)
                            {
                                default:
                                    {
                                        s = "шар";
                                        gamer.HendMoney = gamer.HendMoney + 1;
                                        break;
                                    }
                                case 2:
                                    {
                                        s = "шара";
                                        gamer.HendMoney = gamer.HendMoney + 10;
                                        break;
                                    }
                                case 3:
                                    {
                                        s = "шара";
                                        gamer.HendMoney = gamer.HendMoney + 100;
                                        break;
                                    }
                                case 4:
                                    {
                                        s = "шара";
                                        gamer.HendMoney = gamer.HendMoney + 1000;
                                        break;
                                    }
                                case 5:
                                    {
                                        s = "шаров";
                                        gamer.HendMoney = gamer.HendMoney + 10000;
                                        break;
                                    }
                                case 6:
                                    {
                                        s = "шаров";
                                        gamer.HendMoney = gamer.HendMoney + 100000;
                                        break;
                                    }
                            }

                            Console.Write($"Вы угадали {guessed} {s}! ");
                            Console.WriteLine($"Ваш капитал - {gamer.HendMoney} кредитов.");
                        }
                        else
                        {
                            Console.Write($"Вы ничего не угадали, ");
                            Console.WriteLine($"Ваш капитал - {gamer.HendMoney} кредитов.");
                        }
                        Console.WriteLine(new string('-', 40));
                    }
                    else
                    {
                        break;
                    }
                    if (Game.GameExit() == 0) break;
                }
            }
            Console.WriteLine($"{gamer.Name}, Ваш капитал - {gamer.HendMoney}.");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadLine();
        }
    }
}
