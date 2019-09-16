using System;
using System.Threading;
using System.IO;

namespace Work51
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Computer computerForWrite = new Computer
                {
                    Cores = 7,
                    Frequency = 3.8,
                    Hdd = 2000,
                    Memory = 16,
                };
                InOutOperation inOut = new InOutOperation();

                Console.WriteLine(new string('-', 20));

                inOut.СurrentFile = "computer.dat";
                InOutOperation.WriteData(computerForWrite, inOut.СurrentFile);
                Computer computerForReadDat = InOutOperation.ReadData(inOut.СurrentFile);
                Console.WriteLine($"Object from file: {computerForReadDat}");

                Console.WriteLine(new string('-', 20));
                InOutOperation.ChangeLocation("Zip");
                Console.WriteLine(new string('-', 20));

                inOut.СurrentFile = "computer.gz";
                InOutOperation.WriteZip(computerForWrite, inOut.СurrentFile);
                Computer computerForReadZip = InOutOperation.ReadZip(inOut.СurrentFile);
                Console.WriteLine($"Object from file: {computerForReadZip}");

                Console.WriteLine(new string('-', 20));
                InOutOperation.ChangeLocation("Temp");
                Console.WriteLine(new string('-', 20));

                inOut.СurrentFile = "computer_memory.dat";
                using (MemoryStream memStream = new MemoryStream())
                { 
                    InOutOperation.WriteToFileFromMemoryStream(InOutOperation.WriteToMemoryStream(memStream, computerForWrite), inOut.СurrentFile);
                }
                Computer computerForReadDatFromMemory = InOutOperation.ReadData(inOut.СurrentFile);
                Console.WriteLine($"Object from file: {computerForReadDat}");

                Console.WriteLine(new string('-', 20));

                inOut.СurrentFile = "computer.dat";
                ReadAsync(inOut.СurrentFile);
                for (int i=0; i<19; i++)
                {
                    Console.Write("* ");
                    Thread.Sleep(500);
                }
                Console.WriteLine("\n");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
        static async void ReadAsync(string fileName)
        {
            Computer computerForReadDatAsync = await InOutOperation.ReadDataAsync(fileName);
            Console.WriteLine($"Object from file: {computerForReadDatAsync}");
            Console.WriteLine(new string('-', 20));
        }
    }
}
