using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work21
{
    interface ILibraryUser
    {
        void AddBook(string bookName); // – add new book to array bookList
        void RemoveBook(string bookName); // – remove book from array bookList
        string BookInfo(); // – returns book info by index
        int BooksCount(); // – returns current count of books
    }
}
