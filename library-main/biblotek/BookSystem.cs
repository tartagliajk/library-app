using FuzzySearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AdminHandlesBook
{
    public class BookSystem
    {
        private static BookSystem? instance = null;
        private static string booksFilePath = "C:\\Users\\jeonn\\OneDrive\\Desktop\\library-main\\biblotek\\books.txt";

        private List<Book> books = new List<Book>();

        public List<Book> GetBooks() { return books; }

        private BookSystem() {
            LoadBooks();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
            Save();
        }

        public void Save()
        {

            string[] booksStringArr = books.Select(book => $"{book.Id} {book.Author} {book.Name} {book.Year} {book.Availability}").ToArray();


            File.WriteAllLines(booksFilePath, booksStringArr);
        }

        public static BookSystem GetInstance()
        {
            if (instance == null)
            {
                instance = new BookSystem();
            }
            return instance;
        }

        void LoadBooks() {
            string[] booksFromDb = File.ReadAllLines(booksFilePath);

            for (var i = 0; i < booksFromDb.Length; i++)
            {
                string bookStr = booksFromDb[i];
                string[] bookLineTokens = bookStr.Split(" ");
                string bookId = bookLineTokens[0];
                string bookAuthor = bookLineTokens[1];
                string bookName = bookLineTokens[2];
                int bookYear = Int32.Parse(bookLineTokens[3]);
                int availability = Int32.Parse(bookLineTokens[4]);

                Book book = new Book(bookId, bookAuthor, bookName, bookYear, availability);
                books.Add(book);
            }
        }

        public List<Book> FindBook(string text)
        {
            var textLowerCase = text.ToLower();
            int maxDistance = 2;

            var result = new List<Book>();

            foreach (var book in books)
            {
                var authorDistance = LevenshteinDistance.GetDistance(book.Author.ToLower(), textLowerCase);
                var nameDistance = LevenshteinDistance.GetDistance(book.Name.ToLower(), textLowerCase);

                if (authorDistance <= maxDistance || nameDistance <= maxDistance)
                {
                    result.Add(book);
                }
            }

            return result;
        }
    }
}
