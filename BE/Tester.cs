using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable()]
    public class Tester : Person, ISerializable
    {
        public int YearsExperience { get { return (DateTime.Today - StartYear).Days / 365; } }   // Number of years as a tester
        public int MaxWeeklyTests { get; set; }     // Max number of tests that the tester can preform per-week

        public Tester()
        {
            WorkingHours = new bool[5][];
            for(int i = 0; i < 5; i++)
            {
                WorkingHours[i] = new bool[7];
                for (int j = 0; j < 7; j++)
                {
                    WorkingHours[i][j] = false;
                }
            }
        }

        // This is going to hold a bool array of hours worked, 5 days x 7 hours a day
        public bool[][] WorkingHours { get; set; }
        
        // check if the tester is working
        public bool getIfWorking(DateTime dateTime)
        {
            if((int)dateTime.DayOfWeek < 6 && dateTime.Hour <= 15 && dateTime.Hour >= 9)
            {
                return WorkingHours[(int)dateTime.DayOfWeek][dateTime.Hour - 9];
            }
            return false;
        }
        // To-Do getters and setters for the hours worked

        public double MaxDistance { get; set; }

        public DateTime StartYear
        {
            get;
            set;
        }

        // To string
        public override string ToString()
        {
            String result = "";

            result += base.ToString();
            result += String.Format("Experience:   {0} years\n", YearsExperience);
            result += String.Format("Max Tests:    {0}\n", MaxWeeklyTests);
            result += String.Format("Max Distance: {0}\n", MaxDistance);

            return result;
        }

        // Copy constructor
        public Tester Clone()
        {
            bool[][] tmpWorkingHours = new bool[5][];
            for(int i = 0; i < 5; i++)
            {
                tmpWorkingHours[i] = new bool[7];
                for(int j = 0; j < 7; j++)
                {
                    tmpWorkingHours[i][j] = this.WorkingHours[i][j];
                }
            }
            return new Tester
            {
                Address = this.Address.clone(),
                BirthDay = this.BirthDay,
                Email = this.Email,
                FirstName = this.FirstName,
                Gender = this.Gender,
                ID = this.ID,
                LastName = this.LastName,
                VehicleType = this.VehicleType,
                StartYear = this.StartYear,
                MaxWeeklyTests = this.MaxWeeklyTests,
                MaxDistance = this.MaxDistance,
                WorkingHours = tmpWorkingHours
            };
        }

        // implement update for tester
        public void Update(Tester tester)
        {
            this.Update((Person)tester);
            this.MaxWeeklyTests = tester.MaxWeeklyTests;
            this.StartYear = tester.StartYear;
            this.MaxDistance = tester.MaxDistance;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    this.WorkingHours[i][j] = tester.WorkingHours[i][j];
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID);
            info.AddValue("FirstName", FirstName);
            info.AddValue("LastName", LastName);
            info.AddValue("Gender", Gender);
            info.AddValue("Email", Email);
            info.AddValue("VehicleType", VehicleType);
            info.AddValue("DOBYear", BirthDay.Year);
            info.AddValue("DOBMonth", BirthDay.Month);
            info.AddValue("DOBDay", BirthDay.Day);
            info.AddValue("DOBHour", BirthDay.Hour);
            info.AddValue("Street", Address.Street);
            info.AddValue("City", Address.City);
            info.AddValue("Number", Address.Number);
            info.AddValue("StartYear", StartYear.Year);
            info.AddValue("StartMonth", StartYear.Month);
            info.AddValue("StartDay", StartYear.Day);
            info.AddValue("StartHour", StartYear.Hour);
            info.AddValue("MaxWeeklyTests", MaxWeeklyTests);
            info.AddValue("MaxDistance", MaxDistance);
            info.AddValue("WorkingHours", WorkingHours);
        }

        /*
        public Tester(SerializationInfo info, StreamingContext context)
        {
            ID = (String)info.GetValue("ID", typeof(String));
            FirstName = (String)info.GetValue("FirstName", typeof(String));
            LastName = (String)info.GetValue("LastName", typeof(String));
            Gender = (Gender)info.GetValue("Gender", typeof(Gender));
            Email = (String)info.GetValue("Email", typeof(String));
            VehicleType = (VehicleType)info.GetValue("VehicleType", typeof(VehicleType));
            BirthDay = new DateTime(
                    (int)info.GetValue("DOBYear", typeof(int)),
                    (int)info.GetValue("DOBMonth", typeof(int)),
                    (int)info.GetValue("DOBDay", typeof(int)),
                    (int)info.GetValue("DOBHour", typeof(int)), 0, 0
                );
            Address = new Address
            {
                Street = (String)info.GetValue("Street", typeof(String)),
                City = (String)info.GetValue("City", typeof(String)),
                Number = (int)info.GetValue("Number", typeof(int))
            };
            StartYear = new DateTime(
                    (int)info.GetValue("StartYear", typeof(int)),
                    (int)info.GetValue("StartMonth", typeof(int)),
                    (int)info.GetValue("StartDay", typeof(int)),
                    (int)info.GetValue("StartHour", typeof(int)), 0, 0
                );
            MaxDistance = (double)info.GetValue("MaxDistance", typeof(double));
            MaxWeeklyTests = (int)info.GetValue("MaxWeeklyTests", typeof(int));
            WorkingHours = (bool[][])info.GetValue
        }*/
    }
}

