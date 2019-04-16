using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    enum CityName
    {
        Stanbul = 1,
        Kiev,
        Athens,
        Rome,
        Paris,
        Berlin,
        Prague,
        Warsaw,
    }
    enum AirlineName
    {
        May = 1,
        TurkishAirlines,
        Lufthansa,
        YanAir,
        AeroJet,
    }
    enum TerminalName
    {
        A = 1,
        B,
    }
    enum FlightStatusName
    {
        CheckIn,
        GateClosed,
        Arrived,
        Departed,
        Unknown,
        Canceled,
        Expected,
        Delayed,
        InFlight
    }
    struct Flight // структура информации о рейсе
    {
        public bool InOut; // in - true, out - false
        public DateTime DTFlight;
        public int Number;
        public CityName City;
        public AirlineName Airline;
        public TerminalName Terminal;
        public int Gate;
        public FlightStatusName Status;

        public void DisplayInfo(int left, int top) // построение строки информации о рейсе
        {
            string s = null;
            Console.SetCursorPosition(left, top);

            s += "| " + String.Format("{0:d3}", Number) + " ";
            s += "| " + String.Format("{0:g}", DTFlight) + " ";
            s += "| " + String.Format(City + new string(' ', 7 - City.ToString("g").Length)) + " ";
            s += "| " + String.Format(Airline + new string(' ', 15 - Airline.ToString("g").Length)) + " ";
            s += "| " + Program.CentrStr(Terminal.ToString("g"), 7);
            s += "| " + String.Format("{0:d3}", Gate) + " ";
            s += "| " + String.Format(Status + new string(' ', 10 - Status.ToString("g").Length)) + " ";
            s += "| ";

            Console.Write(s);
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            Flight[] FlightList = new Flight[20];

            Console.WindowHeight = 42;
            Console.WindowWidth = 165;
            Console.WindowLeft = 0;
            Console.WindowTop = 0;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(CentrStr("Information panel", 164));
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, 40);
            Console.Write("MENU: 1-ViewList 2-AddFlight 3-DeliteFlight 4-EditFlight 5-FindFlight 6-SpecialFunction Q-Quit");

            while (true)
            { 
                switch (Console.ReadKey().KeyChar)
                {
                    case '1': // вывод таблицы рейсов
                        {
                            int j = 0;
                            Console.Clear();
                            TableHead();
                            int left = 0; int top = 6;
                            int qin = 0; int qout = 0;
                            for (int i=0; i<= FlightList.Count()-1;i++)
                            {
                                if (FlightList[i].Number!=0)
                                {
                                    if (FlightList[i].InOut)
                                    {
                                        FlightList[i].DisplayInfo(left, top + qin);
                                        ++qin;

                                    }
                                    else
                                    {
                                        FlightList[i].DisplayInfo(left+82, top + qout);
                                        ++qout;
                                    }
                                    ++j;
                                }
                            }
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(0, 40);
                            Console.WriteLine("MENU: 1-ViewList 2-AddFlight 3-DeliteFlight 4-EditFlight 5-FindFlight 6-SpecialFunction Q-Quit");
                            break;
                        }
                    case '2': // добавление нового рейса
                        {
                            int j = 0;
                            foreach (Flight fl in FlightList)
                            {
                                if (fl.Number != 0)
                                {
                                    ++j;
                                }
                            }
                            if (j <= 19) FlightList[j] = AddFlight();
                            break;
                        }
                    case 'q':
                    case 'Q': // закрытие приложения
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        break;
                }
            }

        }

        public static Flight AddFlight()
        {
            string s;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(CentrStr("Add flight info:", 164));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string('-', 164));
            Console.WriteLine("| Input type of flight (In/Out)                            |");
            Console.WriteLine("| Input number of flight (1 to 999)                        |");
            Console.WriteLine("| Input date/time of flight (dd.mm.yyyy hh:mm)             |");
            Console.WriteLine("| Input target city (help at the bottom of the screen)     |");
            Console.WriteLine("| Input airline company (help at the bottom of the screen) |");
            s = $"Input terminal code ({(TerminalName)1}/";
            for (int i = 2; i <= Enum.GetNames(typeof(TerminalName)).Length-1; i++)
            {
                s += (CityName)i + "/" + i + " ";
            }
            s += $"{(TerminalName)Enum.GetNames(typeof(TerminalName)).Length})";
            Console.WriteLine($"| {s}" + new string(' ', 56 - s.Length) + " |");
            Console.WriteLine("| Input gate in the terminal (1 to 999)                    |");
            Console.WriteLine("| Input flight status (help at the bottom of the screen)   |");
            Console.ForegroundColor = ConsoleColor.White;

            Flight flight = new Flight
            {

                InOut = InputInOut(61, 2),
                Number = InputNumberGate(61, 3),
                DTFlight = InputDT(61, 4),
                City = InputCity(61, 5),
                Airline = InputAirline(61, 6),
                Terminal = InputTerminal(61, 7),
                Gate = InputNumberGate(61, 8),
                Status = InputStatus(61, 9),
            };

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            Console.SetCursorPosition(0, 40);
            Console.WriteLine("MENU: 1-ViewList 2-AddFlight 3-DeliteFlight 4-EditFlight 5-FindFlight 6-SpecialFunction Q-Quit");

            return flight;
        }
        public static void TableHead()
        {
            string s = null;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(CentrStr("Information panel", 164));
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(new string('-', 164));
            s += "|" + CentrStr("In", 80);
            s += "||" + CentrStr("Out", 80) + "|";
            Console.WriteLine(s);
            Console.WriteLine(new string('-', 164));
            s = "|" + CentrStr("№",5);
            s += "|" + CentrStr("Date/time", 18);
            s += "|" + CentrStr("City", 9);
            s += "|" + CentrStr("Airline", 17);
            s += "|" + CentrStr("Terminal", 8);
            s += "|" + CentrStr("Gate", 5);
            s += "|" + CentrStr("Status", 12) + "|";
            s = s + s;
            Console.WriteLine(s);
            Console.WriteLine(new string('-', 164));
        }
        public static string CentrStr(string s,int l)
        {
            string result;
            if ((l - s.Length) % 2 == 0)
            {
                result = new string(' ', (l - s.Length) / 2) + s + new string(' ', (l - s.Length) / 2);
            }
            else
            {
                result = new string(' ', (l - s.Length) / 2) + s + new string(' ', (l - s.Length) / 2 + 1);
            }
            return (result);
        }
        public static bool InputInOut(int left, int top)
        {
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            Console.SetCursorPosition(left, top);
            int i = -1;
            string s = null;
            while (i == -1)
            {
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "In":
                    case "in":
                        {
                            s = "true";
                            i = 1;
                            break;
                        }
                    case "Out":
                    case "out":
                        {
                            s = "false";
                            i = 0;
                            break;
                        }
                    default:
                        {
                            Console.SetCursorPosition(0, 40);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("Укажите, пожалуйста правильно, \"In\" или \"Out\"... ");
                            Console.SetCursorPosition(left, top);
                            break;
                        }
                }
            }
            return bool.Parse(s);
        }
        private static int InputNumberGate(int left, int top)
        {
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            Console.SetCursorPosition(left, top);
            int number;
            for (; ; )
            {
                string answer = Console.ReadLine();
                if (int.TryParse(answer, out number))
                {
                    if (int.Parse(answer) < 1 || int.Parse(answer) > 999)
                    {
                        Console.SetCursorPosition(0, 40);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Номер рейса должен лежать в пределах от 1 до 999... ");
                        Console.SetCursorPosition(left, top);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 40);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Вы ввели что-то не то...");
                    Console.SetCursorPosition(left, top);
                }
            }
            return number;
        }
        private static DateTime InputDT(int left, int top)
        {
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            DateTime inputDT;
            Console.SetCursorPosition(left, top);
            for (; ; )
            {
                string answer = Console.ReadLine();
                if (DateTime.TryParse(answer, out inputDT))
                {
                    break;
                }
                else
                {
                    Console.SetCursorPosition(0, 40);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Введите, пожалуйста в правильном формате: dd.mm.yyyy hh:mm... ");
                    Console.SetCursorPosition(left, top);
                }
            }
            return inputDT;
        }
        private static CityName InputCity(int left, int top)
        {
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            string s = "Help list: ";
            for (int i=1; i<= Enum.GetNames(typeof(CityName)).Length; i++)
            {
                s += (CityName)i + "-" + i + " ";
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 40);
            Console.WriteLine(s);

            Console.SetCursorPosition(left, top);
            int number;
            for (; ; )
            {
                string answer = Console.ReadLine();
                if (int.TryParse(answer, out number))
                {
                    if (int.Parse(answer) < 1 || int.Parse(answer) > Enum.GetNames(typeof(CityName)).Length)
                    {
                        Console.SetCursorPosition(0, 40);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Please enter correctly. " + s);
                        Console.SetCursorPosition(left, top);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 40);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Please enter correctly. " + s);
                    Console.SetCursorPosition(left, top);
                }
            }
            return (CityName)number;

        }
        private static AirlineName InputAirline(int left, int top)
        {
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            string s = "Help list: ";
            for (int i = 1; i <= Enum.GetNames(typeof(AirlineName)).Length; i++)
            {
                s += (AirlineName)i + "-" + i + " ";
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 40);
            Console.WriteLine(s);

            Console.SetCursorPosition(left, top);
            int number;
            for (; ; )
            {
                string answer = Console.ReadLine();
                if (int.TryParse(answer, out number))
                {
                    if (int.Parse(answer) < 1 || int.Parse(answer) > Enum.GetNames(typeof(AirlineName)).Length)
                    {
                        Console.SetCursorPosition(0, 40);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Please enter correctly. " + s);
                        Console.SetCursorPosition(left, top);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 40);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Please enter correctly. " + s);
                    Console.SetCursorPosition(left, top);
                }
            }
            return (AirlineName)number;

        }
        private static TerminalName InputTerminal(int left, int top)
        {
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            Console.SetCursorPosition(left, top);
            TerminalName terminalName;
            string s;
            for (; ; )
            {
                string answer = Console.ReadLine();
                if (TerminalName.TryParse(answer.ToUpper(), out terminalName))
                {
                    break;
                }
                else
                {
                    Console.SetCursorPosition(0, 40);
                    Console.BackgroundColor = ConsoleColor.Black;

                    s = $"Please enter correctly: {(TerminalName)1}/";
                    for (int i = 2; i <= Enum.GetNames(typeof(TerminalName)).Length - 1; i++)
                    {
                        s += (CityName)i + "/" + i + " ";
                    }
                    s = $"{(TerminalName)Enum.GetNames(typeof(TerminalName)).Length}";
                    Console.Write(s);
                    Console.SetCursorPosition(left, top);
                }
            }
            return terminalName;
        }
        private static FlightStatusName InputStatus(int left, int top)
        {
            Console.SetCursorPosition(0, 40);
            Console.Write(new string(' ', 164));
            string s = "Help list: ";
            for (int i = 1; i <= Enum.GetNames(typeof(FlightStatusName)).Length; i++)
            {
                s += (FlightStatusName)i + "-" + i + " ";
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 40);
            Console.WriteLine(s);

            Console.SetCursorPosition(left, top);
            int number;
            for (; ; )
            {
                string answer = Console.ReadLine();
                if (int.TryParse(answer, out number))
                {
                    if (int.Parse(answer) < 1 || int.Parse(answer) > Enum.GetNames(typeof(FlightStatusName)).Length)
                    {
                        Console.SetCursorPosition(0, 40);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Please enter correctly. " + s);
                        Console.SetCursorPosition(left, top);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 40);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("Please enter correctly. " + s);
                    Console.SetCursorPosition(left, top);
                }
            }
            return (FlightStatusName)number;
        }
    }
}
