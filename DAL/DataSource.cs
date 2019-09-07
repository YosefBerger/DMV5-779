using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// The DS Layer holds all of the 'raw data' in the program
    /// this program has three lists
    /// one for trainees, one for testers, and one for tests
    /// </summary>
    public static class DataSource
    {
        
        private static List<Trainee> trainees = new List<Trainee>();
        private static List<Test> tests = new List<Test>();
        private static List<Tester> testers = new List<Tester>();

        public static List<Trainee> Trainees
        {
            get { return trainees; }
            set { trainees = value; }
        }
        public static List<Test> Tests 
        {
            get { return tests; }
            set { tests = value; }
        }
        public static List<Tester> Testers
        {
            get { return testers; }
            set { testers = value;  }
        }

    }
}
