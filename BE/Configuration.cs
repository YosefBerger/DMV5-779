using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// the configurations class is to hold the values of important things used throughout the program
    /// </summary>
    // We think this should be in BL but the PDF says to put it here, so that's fun I guess
    public static class Configuration
    {
        public static int TESTER_MIN_AGE = 40;  // Cannot add tester under this age
        public static int TRAINEE_MIN_AGE = 18; // Cannot add trainee under this age
        public static int DAYS_FROM_TEST = 7;   // Cannot take test untill DAYS_FROM_TEST days after previus test
        public static int TRAINEE_MIN_LESSONS = 20; // Cannot take test without having done at least this many lessons
        public static int DAY_START = 9;    // Start hour of day
        public static int DAY_END = 15;     // End hour of day
        
        // To-Do: add requestTestDate()
        // To-Do: add public static int TESTER_MAX_TESTS = ???; // The maximum amount of tests a tester can preform in a week
        // To-Do: 
    }
}
