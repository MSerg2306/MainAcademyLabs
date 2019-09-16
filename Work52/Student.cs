using System;

namespace Work52
{
    [Serializable]
    public class Student
    {
        [System.Xml.Serialization.XmlIgnore]
        public string FirstName;
        public string LastName;
        [NonSerialized]
        public string Nationality;
        public string Address;
        public string Code;

        public Student() { }            

        public void SetAddress(string address, string code)
        {
            Address = address;
            Code = code;
        }

        public override string ToString()
        {
            return $"FirstName-{FirstName}, LastName-{LastName}, Nationality-{Nationality}, Address-{Address}, Code-{Code}";
        }
    }  
}
