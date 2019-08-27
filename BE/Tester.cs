using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester : Person
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
    }
}

