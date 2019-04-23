using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work21
{
    class LibraryUser : ILibraryUser
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        private int Id = 0;
        public string Phone { get; set; }
        public int BookLimit { get; private set; }
        public string[] BookList;

        public LibraryUser (string firstName = "Иван", string lastName = "Иванов", string phone = "+3(XXX)XXX-XX-XX", int bookLimit = 10)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            BookLimit = bookLimit;
            BookList = new string[bookLimit];
        }
        public string this[int i]
        {
            get
            {
                string temp;
                if (i >= 0 && i <= Id)
                {
                    temp = BookList[i];
                }
                else
                {
                    temp = "";
                }
                return temp;
            }
            set
            {
                if (i >= 0 && i < BookLimit)
                {
                    BookList[i] = value;
                    ++Id;
                }
            }

        }
        public void AddBook(string bookName)
        {
            this[Id] = bookName;
        }

        public void RemoveBook(string bookName)
        {
            int i, j = 0;
            string[] temp = new string[BookLimit];
            for (i=0; i<=Id; i++)
            {
                if (BookList[i] == bookName)
                {
                    BookList[i] = null;
                    --Id;
                }
                else
                {
                    temp[j] = BookList[i];
                    BookList[i] = null;
                    j++;
                }
            }
            for (i = 0; i <= Id; i++)
            {
                BookList[i] = temp[i];
            }
        }

        public string BookInfo()
        {
            string s;
            s = $"User {FirstName} {LastName} has the following books: \n";
            foreach(string bl in BookList)
            {
                if (bl != null) s += new string(' ', 4) + bl + "\n";
            }

            return s;
        }

        public int BooksCount()
        {
            return Id;
        }
    }
}
