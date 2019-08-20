using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PL_Console
{
    class Program
    {
        private static IBL bl = FactoryBL.getInstance();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Press a to deal with trainees, or q to quit: ");
                String temp = Console.ReadLine();
                if(temp.CompareTo("a") == 0)
                {
                    Console.Write("Press a to add a trainee, r to remove one, u to update one, or q to quit: ");
                    temp = Console.ReadLine();
                    if (temp.CompareTo("a") == 0)
                    {
                        addTraineeTest();
                    }
                    else if (temp.CompareTo("r") == 0)
                    {
                        removeTraineeTest();
                    }
                    else if (temp.CompareTo("u") == 0)
                    {

                    }
                }
                if (temp.CompareTo("q") == 0)
                {
                    break;
                }

                
            }
        }

        public static void addTraineeTest()
        {
            Console.WriteLine("Adding a trainee");
            Console.WriteLine("----------------");

            bool flag = true;

            String id;
            do
            {
                flag = false;
                Console.Write("ID: ");
                id = Console.ReadLine();
                if (!Person.validID(id))
                {
                    Console.WriteLine("Invalid ID number, try again..");
                    flag = true;
                }
            } while (flag);

            Console.Write("First Name: ");
            String firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            String lastName = Console.ReadLine();

            Gender gender = new Gender();
            do
            {
                flag = false;
                Console.Write("Gender: ");
                String gend = Console.ReadLine().ToUpper();

                if (gend.CompareTo("M") == 0 || gend.CompareTo("MALE") == 0)
                {
                    gender = BE.Gender.MALE;
                }
                else if (gend.CompareTo("F") == 0 || gend.CompareTo("FEMALE") == 0)
                {
                    gender = BE.Gender.FEMALE;
                }
                else
                {
                    Console.WriteLine("Invalid gender, please try again (M/F or MALE/FEMALE).");
                    flag = true;
                }
            } while (flag);

            DateTime dob;
            do
            {
                flag = false;
                Console.Write("Enter DOB (mm/dd/yyy): ");
                if (!(DateTime.TryParse(Console.ReadLine(), out dob)))
                {
                    Console.WriteLine("Invalid Date, try again.");
                    flag = true;
                }
            } while (flag);

            Address address = new Address();
            do
            {
                flag = false;
                Console.Write("Address Number: ");

                try
                {
                    address.Number = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid number, try again.");
                    flag = true;
                }
            } while (flag);
            Console.Write("Address Street: ");
            address.Street = Console.ReadLine();
            Console.Write("Address City: ");
            address.City = Console.ReadLine();

            MailAddress email = new MailAddress("example@domain.com");
            do
            {
                flag = false;
                Console.Write("Email Address: ");
                try
                {
                    email = new MailAddress(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid Email, try again");
                    flag = true;
                }
            } while (flag);

            VehicleType vehicleType;
            do
            {
                flag = false;

                Console.Write("Vehicle Type (PRIVATE, TWO_WHEELED, MEDIUM_SIZED_TRUCK, HEAVY_TRUCK): ");
                if (!Enum.TryParse<VehicleType>(Console.ReadLine().ToUpper(), out vehicleType))
                {
                    Console.WriteLine("Invalid Vehicle Type, try again.");
                    flag = true;
                }
            } while (flag);

            GearBox gearBox;
            do
            {
                flag = false;

                Console.Write("Gear Box (AUTOMATIC, MANUAL): ");
                if (!Enum.TryParse<GearBox>(Console.ReadLine().ToUpper(), out gearBox))
                {
                    Console.WriteLine("Invalid Gear Box, try again.");
                    flag = true;
                }
            } while (flag);

            Console.Write("Driving School: ");
            String drivingSchool = Console.ReadLine();

            Console.Write("Instructor Name: ");
            String instructorName = Console.ReadLine();

            int numLessons = 0;
            do
            {
                flag = false;
                Console.Write("Number of driving Lessons: ");

                try
                {
                    numLessons = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid number, try again.");
                    flag = true;
                }
                if (numLessons < 0)
                {
                    Console.WriteLine("Invalid number, try again.");
                    flag = true;
                }
            } while (flag);

            Trainee trainee = new Trainee{
                ID = id,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                BirthDay = dob,
                Address = address,
                Email = email,
                VehicleType = vehicleType,
                GearBox = gearBox,
                DrivingSchool = drivingSchool,
                InstructorName = instructorName,
                NumDrivingLessons = numLessons
            };

            bl.addTrainee(trainee);

            printTrainees();
        }

        private static void printTrainees(Func<Trainee, bool> condition = null)
        {
            List<Trainee> trainees = bl.getAllTrainees(condition);

            foreach (Trainee t in trainees)
            {
                Console.WriteLine(t);
            }
        }

        private static void printTraineesNames(Func<Trainee, bool> condition = null)
        {
            List<Trainee> trainees = bl.getAllTrainees(condition);

            foreach (Trainee t in trainees)
            {
                Console.WriteLine("ID: " + t.ID + "\t" + t.FirstName + " " + t.LastName);
            }
        }


        public static void removeTraineeTest()
        {
            Console.WriteLine("Trainees available to remove:");
            printTraineesNames();
            
            Trainee trainee;
            do
            {
                Console.Write("Enter the ID of the trainee to remove: ");
                String ID = Console.ReadLine();
                trainee = bl.getAllTrainees(new Func<Trainee, bool>(t => t.ID.CompareTo(ID) == 0)).FirstOrDefault();
                if (trainee != null)
                {
                    break;
                }

                Console.WriteLine("No student with that ID found, try again.");
            } while (true);

            bl.removeTrainee(trainee);
            printTraineesNames();
        }
    }
}
