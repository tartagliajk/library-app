using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace biblotek
{
    internal class login
    {
        static public void LoginPage()
        {
            Console.Clear();

            Console.WriteLine("Welcome");

            Console.WriteLine("To log in, please insert ssn and password");
            Console.WriteLine("");

            Console.Write("Ssn: ");
            var personalNumber = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            bool loginSuccessful = false;
            bool adminSuccessful = false;

            using (StreamReader sr = new StreamReader(File.Open(@"C:\Users\jeonn\source\repos\biblotek\biblotek\details.txt", FileMode.Open)))
            {
                string line;
                // Read one line at a time from file until end of file.
                while ((line = sr.ReadLine()) != null)
                {
                    // split the line by comma.
                    var creds = line.Split(' ');

                    // if the values match, set login to true and exit the while loop.  
                    if (personalNumber == creds[2] && password == creds[3])
                    {
                        loginSuccessful = true;
                        if (creds[4] == "admin")
                        {
                            adminSuccessful = true;
                        }
                        break;
                    }
                }
                sr.Close();
            }

            if (loginSuccessful & adminSuccessful == true)
            {
                Console.Clear();
                Program.adminMenu();

            }else if (loginSuccessful)
            {
                Console.Clear();
                Program.memberMenu();
            }
            else
            {
                Console.WriteLine("Username or password is incorrect. Try again");
                Console.ReadKey();
                Console.Clear();
                LoginPage();
            }
            Console.Read();
        }
    }
}
