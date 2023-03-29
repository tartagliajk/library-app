using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblotek
{
    internal class signup
    {
        static public void signupPage()
        {
            Console.WriteLine("\nTo signup insert First name, Last name, Ssn and password.");
            Console.WriteLine("");

            Console.Write("First Name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();

            Console.Write("Ssn: ");
            var personalNumber = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            Console.WriteLine("");

            //if any of the values are null:
            if (UserInfoIncomplete(firstName, lastName, personalNumber, password))
            {
                Console.Clear();
                Console.WriteLine("Insert real info to signup.");
                Console.WriteLine("");
                signupPage();
            }//if the ssn already exist:
            else if (UserRegistered(personalNumber))
            {
                Console.Clear();
                Console.WriteLine("You are already registerd, please insert correct infomartion.");
                Console.WriteLine("");
                signupPage();
            }

            var line = $"{firstName} {lastName} {personalNumber} {password} {"medlem"} {","}";
            string[] lines = { line };

            File.AppendAllLines(@"C:\Users\jeonn\OneDrive\Desktop\library-main\biblotek\details.txt", lines);

            Console.WriteLine("You are now signed up and can login. Please wait while we redirect you.");

            Thread.Sleep(3000);

            login.LoginPage();
        }


        static bool UserRegistered(string personalNumber)
        {
            string[] users = System.IO.File.ReadAllLines(@"C:\Users\jeonn\OneDrive\Desktop\library-main\biblotek\details.txt");

            for (int i = 0; i < users.Length; i++)
            {
                var line = users[i].Trim();
                string[] parts = line.Split(' ');

                var currentPersonalNumber = parts[2];

                if (currentPersonalNumber == personalNumber)
                {
                    return true;
                }
            }

            return false;
        }

        static bool UserInfoIncomplete(string? firstName, string? lastName, string? personalNumber, string? password)
        {
            if (firstName == null || firstName == "") return true;
            if (lastName == null || lastName == "") return true;
            if (personalNumber == null || personalNumber == "") return true;
            if (password == null || password == "") return true;

            return false;
        }
    }
}
