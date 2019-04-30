using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work23
{
    class Program
    {
        const int LengthDividingString = 80;
        static void Main(string[] args)
        {
            // 10) declare 2 objects
            Money.InputCource(CurrencyTypes.USD, 27.2M);
            Money.InputCource(CurrencyTypes.EU, 30.8M);
            Console.WriteLine($"Cource UAH/USD: {Money.Cource(CurrencyTypes.UAH, CurrencyTypes.USD)}");
            Console.WriteLine($"Cource USD/UAH: {Money.Cource(CurrencyTypes.USD, CurrencyTypes.UAH)}");
            Console.WriteLine($"Cource UAH/EU: {Money.Cource(CurrencyTypes.UAH, CurrencyTypes.EU)}");
            Console.WriteLine($"Cource EU/UAH: {Money.Cource(CurrencyTypes.EU, CurrencyTypes.UAH)}");
            Console.WriteLine($"Cource USD/EU: {Money.Cource(CurrencyTypes.USD, CurrencyTypes.EU)}");
            Console.WriteLine($"Cource EU/USD: {Money.Cource(CurrencyTypes.EU, CurrencyTypes.USD)}");
            Console.WriteLine(new string('-', LengthDividingString));

            Money moneyUAH1 = new Money();
            Console.WriteLine($"Create object via Default: {moneyUAH1}");
            Money moneyUSD = new Money(CurrencyTypes.USD, 10);
            Console.WriteLine($"Create object via Сonstructor: {moneyUSD}");
            Money moneyUAH2 = (Money)(double)20;
            Console.WriteLine($"Create object from Double: {moneyUAH2}");
            Money moneyEU = (Money)(String)"EU";
            Console.WriteLine($"Create object from String: {moneyEU}");
            Console.WriteLine($"About an object created using a constructor: we heve {(double)moneyUSD} in {(string)moneyUSD}.");
            Console.WriteLine(new string('-', LengthDividingString));

            // 11) do operations
            // add 2 objects of Money
            Money moneyAdd = moneyUAH2 + moneyUSD;
            Console.WriteLine($"Get new money with \"+\", get in UAH, {moneyUAH2} + {moneyUSD} = {moneyAdd}");
            moneyEU += moneyUAH2 + moneyUSD;
            Console.WriteLine($"Add to old object with \"+=\", get in EU, EU += {moneyUAH2} + {moneyUSD}, get {moneyEU}");
            Console.WriteLine(new string('-', LengthDividingString));
            
            // add 1st object of Money and double
            Console.Write($"moneyUAH1 = {moneyUAH1}. ");
            moneyUAH1 += 25.5;
            Console.WriteLine($"Add to moneyUAH1 25.5, get boost amount: {moneyUAH1}");
            Console.WriteLine(new string('-', LengthDividingString));
            
            // decrease 2nd object of Money by 1 
            Console.Write($"We will lower {moneyUSD} with the operator \"--\", we get ");
            moneyUSD--;
            Console.WriteLine($"{moneyUSD}");
            Console.WriteLine(new string('-', LengthDividingString));
            
            // compare 2 objects of Money
            Console.WriteLine($"Compare some object:");
            Console.WriteLine($"{moneyUAH1} > {moneyUAH2} ? --> {(moneyUAH1 > moneyUAH2)}");
            Console.WriteLine($"{moneyUSD} < {moneyUAH2} ? --> {(moneyUSD < moneyUAH2)}");
            Console.WriteLine($"{moneyUSD} = {moneyEU} ? --> {(moneyUSD == moneyEU)}");
            Console.WriteLine($"{moneyUSD} <> {moneyEU} ? --> {(moneyUSD != moneyEU)}");
            Console.WriteLine(new string('-', LengthDividingString));

            // check CurrencyType of every object
            Console.WriteLine($"{moneyUSD} is a UAH ? --> {(moneyUSD & CurrencyTypes.UAH)}");
            Console.WriteLine($"{moneyUSD} is a USD ? --> {(moneyUSD & CurrencyTypes.USD)}");
            Console.Write("Press any key..");
            Console.ReadLine();
        }
    }
}
