using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using AdminHandlesBook;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace biblotek
{
    internal class Program
    {
        static BookSystem system = BookSystem.GetInstance();
        static int memberKey;
        static void Main(string[] args)
        {
            selection();
        }

        static void selection()
        {
            Console.WriteLine("Welcome to the library");
            Console.WriteLine("Please enter your option");
            Console.WriteLine("Log - Login; Sign - Signup; C - Close");
            var Option = Console.ReadLine();
            Option = Option.ToLower();
            if (Option == "log")
            {
                login.LoginPage();
            }
            else if (Option == "sign")
            {
                signup.signupPage();
            }else if (Option == "c")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Enter a valid option");
                selection();
            }
        }

        static public void memberMenu()
        {
            Console.WriteLine("You are now loged in as a member, please pick ur next option");
            Console.WriteLine("List - List all books; Search - Search; C - close");
            memberKey = 1;
            var Option = Console.ReadLine();
            Option = Option.ToLower();
            if (Option == "list")
            {
                ListAllBooks();
            }
            else if (Option == "search")
            {
                Search();
            }
            else if (Option == "c")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Enter a valid option");
                memberMenu();
            }
        }

        static public void adminMenu()
        {
            Console.WriteLine("\nYou are now loged in as a admin, please pick ur next option");
            Console.WriteLine("Listb - List all books; Listm - List all members; Search - Search; Add - Add books; C - close");
            var Option = Console.ReadLine();
            Option = Option.ToLower();
            if (Option == "listb")
            {
                ListAllBooks();
            }else if (Option == "listm")
            {
                ListAllMembers();
            }
            else if (Option == "add")
            {
                AddBook();
            }
            else if (Option == "search")
            {
                Search();
            }
            else if (Option == "c")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Enter a valid option");
                adminMenu();
            }
        }
        static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("Add a new book");

            Console.WriteLine("Enter book information. Put a '-' between spaces.\n");

            var id = (system.GetBooks().Count + 1).ToString();

            Console.Write("Author: ");
            var author = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Year: ");
            var year = Int32.Parse(Console.ReadLine());

            Console.Write("Availability (1 - true; 0 - false): ");
            var availability = Int32.Parse(Console.ReadLine());

            Book newBook = new Book(id, author, name, year, availability);
            system.AddBook(newBook);

            adminMenu();
        }

        static void ListAllBooks()
        {
            Console.Clear();
            List<Book> books = system.GetBooks();

            for (var i = 0; i < books.Count; i++)
            {
                Book book = books[i];

                Console.WriteLine($"{book.Id} {book.Author} {book.Name} {book.Year} {book.Availability}");
            }

            if(memberKey == 1)
            {
                memberMenu();
            }
            else
            {
                adminMenu();
            }
            
        }

        static void ListAllMembers()
        {
            Console.Clear();
            string text = System.IO.File.ReadAllText(@"C:\Users\jeonn\OneDrive\Desktop\library-main\biblotek\details.txt");
            System.Console.WriteLine("All members = {0}\n", text);
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\jeonn\OneDrive\Desktop\library-main\biblotek\details.txt");

            adminMenu();
        }

        static void Search()
        {
            Console.Write("Search after a book: ");
            var query = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("\u001b[2J\u001b[3J");
            Console.Clear();

            Console.WriteLine($"Search after a car: {query}\n");

            var result = system.FindBook(query);

            for (var i = 0; i < result.Count; i++)
            {
                var book = result[i];

                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Name: {book.Name}");
                Console.WriteLine($"Year:{book.Year}");
            }

            if (memberKey == 1)
            {
                memberMenu();
            }
            else
            {
                adminMenu();
            }
        }
    }
}
