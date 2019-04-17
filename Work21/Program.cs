using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work21
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryUser user1 = new LibraryUser(), user2 = new LibraryUser("Maria", "Ivanenko", "+380447777777", 2);
            Console.WriteLine("User1 " + user1.FirstName + " " + user1.LastName);
            Console.WriteLine("User2 " + user2.FirstName + " " + user2.LastName);

            Console.WriteLine("User 1: add Harry Potter");
            user1.AddBook("Harry Potter");

            Console.WriteLine("User 2: add Sherlock Holmes");
            user2.AddBook("Sherlock Holmes");

            Console.WriteLine("user1.BooksCount = " + user1.BooksCount() + "; user2.BooksCount = " + user2.BooksCount());

            Console.Write("user2 : ");
            Console.WriteLine("add Kobzar");
            user2.AddBook("Kobzar");

            Console.WriteLine("user2.BooksCount = " + user2.BooksCount());

            Console.WriteLine("User 2: add Dorian Gray");
            user2.AddBook("Dorian Gray");

            Console.WriteLine("user2.BooksCount = " + user2.BooksCount());
            Console.WriteLine("user2 books: \n" + new string(' ',4) + user2[0] + "\n" + new string(' ', 4) + user2[1]);

            Console.WriteLine("user2 : remove Sherlock Holmes");
            user2.RemoveBook("Sherlock Holmes");

            Console.WriteLine("user2.BooksCount " + user2.BooksCount());

            Console.WriteLine(new string('-', 10));
            Console.WriteLine(user1.BookInfo());
            Console.WriteLine(user2.BookInfo());

            Console.ReadLine();
        }
    }
}
