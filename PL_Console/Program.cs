using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Trainee atrainee = new Trainee
                {
                    Address = new Address { City = "JLM", Number = 21, Street = "Havaad haleumi" },
                    BirthDay = new DateTime(1907, 10, 25),
                    Email = new System.Net.Mail.MailAddress("kuku@nowhere.com"),
                    FirstName = "kuku",
                    LastName = "was here",
                    ID = "013680319"
                     
                };
                Console.WriteLine(atrainee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
    }
}
