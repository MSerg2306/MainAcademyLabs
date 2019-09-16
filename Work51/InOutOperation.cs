using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Work51
{
    class InOutOperation
    {
        private static string СurrentPath { get; set; }
        public static string CurrentDirectory = "Temp";
        public string СurrentFile { get; set; }
        const string tempFile = "temp.dat";
        static InOutOperation()
        {
            СurrentPath = Directory.GetCurrentDirectory() + @"\";
            CreateDirectory(СurrentPath, CurrentDirectory);
            Console.WriteLine($"The default directory is set:\n{СurrentPath + CurrentDirectory}");
        }
        public InOutOperation() { }

        public static void ChangeLocation(string newDirectory)
        {
            try
            {
                //string currentPath = Path.GetDirectoryName(newPath) + @"\";
                //string currentDirectory = newPath.Substring(СurrentPath.Length);

                CreateDirectory(СurrentPath, newDirectory);

                //СurrentPath = currentPath;
                CurrentDirectory = newDirectory;
                Console.WriteLine($"New directory installed:\n{СurrentPath + CurrentDirectory}");
            }
            catch
            {
                Exception MyExeptions = new Exception($"Wrong path specified. The default path is set: {СurrentPath + CurrentDirectory}");
                throw MyExeptions;
            }
        }
        static public void WriteData(Computer computer, string fileName)
        {
            try
            {
                string fullPath = СurrentPath + CurrentDirectory + @"\" + fileName;
                using (BinaryWriter writer = new BinaryWriter(File.Open(fullPath, FileMode.Create)))
                {
                    writer.Write(computer.Cores);
                    writer.Write(computer.Frequency);
                    writer.Write(computer.Hdd);
                    writer.Write(computer.Memory);
                }
                Console.WriteLine($"Object successfully written to file {fileName}.");
            }
            catch
            {
                Exception MyExeptions = new Exception("File writing failed...");
                throw MyExeptions;
            }
        }
        static public Computer ReadData(string fileName)
        {
            Computer computer = new Computer();
            try
            {
                string fullPath = СurrentPath + CurrentDirectory + @"\" + fileName;
                using (BinaryReader reader = new BinaryReader(File.Open(fullPath, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        computer.Cores = reader.ReadInt32();
                        computer.Frequency = reader.ReadDouble();
                        computer.Hdd = reader.ReadInt32();
                        computer.Memory = reader.ReadInt32();
                    }
                }
                Console.WriteLine($"Object successfully read from file {fileName}.");
            }
            catch
            {
                Exception MyExeptions = new Exception("File writing failed...");
                throw MyExeptions;
            }
            return computer;
        }

        static public void WriteZip(Computer computer, string fileName)
        {
            WriteData(computer, tempFile);

            string tempPath = СurrentPath + CurrentDirectory + @"\" + tempFile;
            string targetPath = СurrentPath + CurrentDirectory + @"\" + fileName;

            try
            {
                using (FileStream sourceStream = new FileStream(tempPath, FileMode.Open))
                {
                    using (FileStream targetStream = File.Create(targetPath))
                    {
                        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream);
                        }
                    }
                }
                Console.WriteLine($"Object successfully written and compressed to file {targetPath}.");
            }
            catch
            {
                Exception MyExeptions = new Exception("Error creating archive...");
                throw MyExeptions;
            }
        }
        static public Computer ReadZip(string fileName)
        {
            string tempPath = СurrentPath + CurrentDirectory + @"\" + tempFile;
            string targetPath = СurrentPath + CurrentDirectory + @"\" + fileName;

            try
            {
                using (FileStream sourceStream = new FileStream(targetPath, FileMode.Open))
                {
                    using (FileStream targetStream = File.Create(tempPath))
                    {
                        using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(targetStream);
                        }
                    }
                }
                return ReadData(tempFile);
            }
            catch
            {
                Exception MyExeptions = new Exception("Error at the archive reading stage...");
                throw MyExeptions;
            }
        }
        public static async Task<Computer> ReadDataAsync(string fileName)
        {
            string fullPath = СurrentPath + CurrentDirectory + @"\" + fileName;
            byte[] result;

            using (FileStream SourceStream = File.Open(fullPath, FileMode.Open))
            {
                result = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            }

            Thread.Sleep(10000);

            Console.WriteLine($"Object successfully async read from file {fileName}.");

            return ArrayByteToComputer(result);
        }
        public static MemoryStream WriteToMemoryStream(MemoryStream memStream, Computer computer)
        {
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            byte[] arrayComputer = uniEncoding.GetBytes(computer.ToString());
            memStream = new MemoryStream(arrayComputer.Length);

            memStream.Write(arrayComputer, 0, arrayComputer.Length);
            Console.WriteLine($"Object successfully written to memory.");
            return memStream;
        }
        public static void WriteToFileFromMemoryStream(MemoryStream memStream, string fileName)
        {
            //object formation from MemoryStream
            byte[] byteArray = new byte[memStream.Length];
            byteArray = memStream.ToArray();
            //int i = 0;
            //while (i < memStream.Length)
            //{
            //    byteArray[i++] =
            //        Convert.ToByte(memStream.ReadByte()); //getting byte array
            //}
            Computer computer = ArrayByteToComputer(byteArray);// getting object

            //save object to file
            Console.WriteLine($"Object successfully written from memory to object.");
            WriteData(computer, fileName);
        }

        private static void CreateDirectory(string directoryPath, string directoryName)
        {
            if (!Directory.Exists(directoryPath + directoryName))
                Directory.CreateDirectory(directoryPath + directoryName);
        }
        private static Computer ArrayByteToComputer (byte[] computerArray)
        {
            Computer computer = new Computer();

            computer.Cores = BitConverter.ToInt32(computerArray, 0);
            computer.Frequency = BitConverter.ToDouble(computerArray, 4);
            computer.Hdd = BitConverter.ToInt32(computerArray, 12);
            computer.Memory = BitConverter.ToInt32(computerArray, 16);

            return computer;
        }
    }
}
