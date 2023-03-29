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
        //Gives a key to member so admin and member dont have the same menu
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
            //makes it lowercase
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
                //closes the application
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
            Console.WriteLine("Listb - List all books; Listm - List all members; Search - Search; Add - Add books; Change - Change Password; C - close");
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
            else if(Option == "change")
            {
                changePassword();
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

            //sets index as i which represents the lines
            //checks lines in file and add 1+ everytime it prints
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

            adminMenu();
        }

        static void Search()
        {
            Console.Write("Search after a book: ");
            var query = Console.ReadLine();
            Console.WriteLine("\n");

            //two ways to console clear
            Console.WriteLine("\u001b[2J\u001b[3J");
            Console.Clear();

            Console.WriteLine($"Search after a book: {query}\n");

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

        static void changePassword()
        {
            Console.WriteLine("Write your SSN");
            var writtenSSN = Console.ReadLine();
            Console.WriteLine("\nWrite your new password");
            var writtenPassword = Console.ReadLine();

            //reads the file and spilts ","
            var users = File.ReadAllText(@"C:\Users\jeonn\OneDrive\Desktop\library-main\biblotek\details.txt").Split(',').ToList().Where(x => !String.IsNullOrWhiteSpace(x));

            List<User> myUsers = new List<User>();
            foreach (var user in users)
            {
                var information = user.Split(' ');
                User temp = new User();
                temp.Name = information[0].Trim();
                temp.lastName = information[1].Trim();
                temp.SSN = information[2].Trim();
                temp.Password = information[3].Trim();
                temp.status = information[4].Trim();
                myUsers.Add(temp);
            }

            //checks if it match and then change the password
            var selectedUser = myUsers.Where(x => x.SSN == writtenSSN).SingleOrDefault();
            myUsers.Remove(selectedUser);
            selectedUser.Password = writtenPassword;
            myUsers.Add(selectedUser);

            List<string> formatForFile = new List<string>();
            foreach (var item in myUsers)
            {
                formatForFile.Add(item.PrepareForFile());
            }
            File.WriteAllLines(@"C:\Users\jeonn\OneDrive\Desktop\library-main\biblotek\details.txt", formatForFile.ToArray());

            Console.WriteLine("The password has been changed");
        }
            

        private class User
        {
            public string Name { get; set; }
            public string lastName { get; set; }
            public string SSN { get; set; }
            public string Password { get; set; }
            public string status { get; set; }

            public string PrepareForFile()
            {
                return Name + " " + lastName + " " + SSN + " " + Password + " " + status + " ,";
            }
        }
        
    }
}
