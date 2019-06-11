using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester : Person
    {
        public int YearsExperience { get; set; }    // Number of years as a tester
        public int MaxWeeklyTests { get; set; }     // Max number of tests that the tester can preform per-week

        // This is going to hold a bool array of hours worked, 5 days x 7 hours a day
        private bool[][] WorkingHours = new bool[5][];

        // To-Do getters and setters for the hours worked

        public double MaxDistance = 7.5;    // A radius of the max distance from the testers home that they are willing to work


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
            return new Tester
            {
                Address = this.Address,
                BirthDay = this.BirthDay,
                Email = this.Email,
                FirstName = this.FirstName,
                Gender = this.Gender,
                ID = this.ID,
                LastName = this.LastName,
                VehicleType = this.VehicleType,
                YearsExperience = this.YearsExperience,
                MaxWeeklyTests = this.MaxWeeklyTests,
                MaxDistance = this.MaxDistance,
                WorkingHours = this.WorkingHours
            };
        }
    }
}
}
