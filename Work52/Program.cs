using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace Work52
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student
            {
                FirstName = "Jon",
                LastName = "Do",
                Nationality = "indian"
            };
            student.SetAddress("New York", "012567863");
            Console.Write("Create object of Student: ");
            Console.WriteLine(student);

            Student newStudent;

            Console.WriteLine(new string('-', 20));
            newStudent = BinaryFrm(student);
            Console.Write("New object after BinaryFormatter: ");
            Console.WriteLine(newStudent);

            Console.WriteLine(new string('-', 20));
            newStudent = SoapFrm(student);
            Console.Write("New object after SoapFormatter: ");
            Console.WriteLine(newStudent);

            Console.WriteLine(new string('-', 20));
            newStudent = XmlFrm(student);
            Console.Write("New object after XmlSerializer: ");
            Console.WriteLine(newStudent);

            Console.ReadLine();
        }
        public static Student BinaryFrm(Student student)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string pathFile = Directory.GetCurrentDirectory() + @"\" + "student.dat";
            Student newStudent;

            using (FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, student);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                newStudent = (Student)bf.Deserialize(fs);
                Console.WriteLine("Object deserialized");
            }

            return newStudent;
        }
        public static Student SoapFrm(Student student)
        {
            SoapFormatter sf = new SoapFormatter();
            string pathFile = Directory.GetCurrentDirectory() + @"\" + "student.soap";
            Student newStudent;

            using (FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                sf.Serialize(fs, student);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                newStudent = (Student)sf.Deserialize(fs);
                Console.WriteLine("Object deserialized");
            }

            return newStudent;
        }
        public static Student XmlFrm(Student student)
        { 
            XmlSerializer xf = new XmlSerializer(typeof(Student));
            string pathFile = Directory.GetCurrentDirectory() + @"\" + "student.xml";
            Student newStudent;

            using (FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                xf.Serialize(fs, student);
                Console.WriteLine("Object serialized");
            }

            using (FileStream fs = new FileStream(pathFile, FileMode.OpenOrCreate))
            {
                newStudent = (Student)xf.Deserialize(fs);
                Console.WriteLine("Object deserialized");
            }

            return newStudent;
        }
    }
}

