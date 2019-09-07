using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// A Driving Test will hold all info about a test for a license
    /// Things about the student, teacher, date of the test ect... 
    /// </summary>
    public class Test : IComparable
    {
        // private static int TestCounter = 0; // Allows us to have incramental test numbers
        //private string Number;  // Hold actual test number
        public string TestNumber { get; set; }
        private string testerId;    // Hold the actualle tester ID
        private string traineeId;    // Hold the actualle tester ID
        private DateTime dateTime;
        public Address StartAddress { get; set; } // The address from which the test was/will be started

        // Aspects that they will be tested on
        public bool UseMirrors { get; set; }
        public bool MaintinaDistance { get; set; }
        public bool ParkInReverse { get; set; }
        public bool Signals { get; set; }
        public bool ParralellParking { get; set; }
        public bool Awareness { get; set; }
        public bool StopSigns { get; set; }
        public bool SpeedLimit { get; set; }
        String testerComment;

        public Test()
        {
            dateTime = DateTime.Today;
            if(dateTime.DayOfWeek == DayOfWeek.Friday)
            {
                dateTime = dateTime.AddDays(2).Date;
            }
            else if(dateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                // This function would of course only be called after sundown
                dateTime = dateTime.AddDays(1).Date;
            }
            dateTime.AddHours(9);
            StartAddress = new Address();
            StartAddress.Number = 1;
            TesterComment = "";
        }

        // copy constructor
        public Test Clone()
        {
            return new Test
            {
                TestNumber = this.TestNumber,
                TesterId = this.TesterId,
                TraineeId = this.TraineeId,
                DateTime = this.DateTime,
                StartAddress = this.StartAddress.clone(),
                UseMirrors = this.UseMirrors,
                MaintinaDistance = this.MaintinaDistance,
                ParkInReverse = this.ParkInReverse,
                Signals = this.Signals,
                ParralellParking = this.ParralellParking,
                Awareness = this.Awareness,
                StopSigns = this.StopSigns,
                SpeedLimit = this.SpeedLimit,
                TesterComment = this.TesterComment,
            };
        }

        // update test
        public void update(Test test)
        {
            this.testerId = test.TesterId;
            this.traineeId = test.TraineeId;
            this.dateTime = test.dateTime;

            this.StartAddress = test.StartAddress.clone();
            this.UseMirrors = test.UseMirrors;
            this.MaintinaDistance = test.MaintinaDistance;
            this.ParkInReverse = test.ParkInReverse;
            this.Signals = test.Signals;
            this.ParralellParking = test.ParralellParking;
            this.Awareness = test.Awareness;
            this.StopSigns = test.StopSigns;
            this.SpeedLimit = test.SpeedLimit;

            this.TesterComment = test.TesterComment;
        }

        public string TesterId      // Get and set the tester ID
        {
            get
            {
                return testerId;
            }
            set
            {
                // Make sure the passed ID is valid
                if (!Tester.validID(value))
                {
                    throw new Exception("not valid ID");
                }
                testerId = value;
            }
        }
        
        public string TraineeId      // Get and set the tester ID
        {
            get
            {
                return traineeId;
            }
            set
            {
                // Make sure the passed ID is valid
                if (!Trainee.validID(value))
                {
                    throw new Exception("not valid ID");
                }
                traineeId = value;
            }
        }

        
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value;
                
                // Make sure the hour is valid
                if (dateTime.Hour > 15 || dateTime.Hour < 9)
                {
                    throw new Exception("Hour is out of bounds");
                }
                // Make sure the day of the week is valid
                if ((int)dateTime.DayOfWeek > 4)
                {
                    throw new Exception("Day is out of bounds");
                }
            }
        }
        
        public bool Result {
            get
            {
                return UseMirrors && MaintinaDistance && ParkInReverse && Signals && ParralellParking && Awareness && StopSigns && SpeedLimit;
            }
            set
            {
                
            }
        }    // Result of the test (if they passed or failed)
        
        public string TesterComment
        {
            get
            {
                return testerComment;
            }
            set
            {
                testerComment = (value == null) ? "" : value;
            }
        }   // Hold any comments from the tester about the test
        
        // override to String
        public override string ToString()
        {
            string result = "";

            result += String.Format("Test Number:      {0}\n", TestNumber);
            result += String.Format("Tester:           {0}\n", TesterId);
            result += String.Format("Trainee:          {0}\n", TraineeId);
            result += String.Format("Date/Time:        {0}\n", DateTime);
            result += String.Format("Address:          {0}\n", StartAddress);
            result += "----------------------------------\n";
            result += String.Format("Use Mirrors:      {0}\n", UseMirrors ? "PASS" : "FAIL");
            result += String.Format("Parke in Reverse: {0}\n", ParkInReverse ? "PASS" : "FAIL");
            result += String.Format("Signals:          {0}\n", Signals ? "PASS" : "FAIL");
            result += String.Format("Parralel Parking: {0}\n", ParralellParking ? "PASS" : "FAIL");
            result += String.Format("Awareness:        {0}\n", Awareness ? "PASS" : "FAIL");
            result += String.Format("Stop Signs:       {0}\n", StopSigns ? "PASS" : "FAIL");
            result += String.Format("Speed Limit:      {0}\n", Signals ? "PASS" : "FAIL");
            result += "----------------------------------\n";
            result += String.Format("The trainee {0} the test.\n", Result ? "PASSED" : "FAILED");
            result += String.Format("Comment from the tester:\n\t\"{0}\"\n", TesterComment);

            return result;
        }

        //this will enable objects to be campared with a Test
        public int CompareTo(object obj)
        {
            if (TestNumber == null)
            {
                return -1;
            }

            return TestNumber.CompareTo(((Test)obj).TestNumber);
        }

        //this will enable Tests to be campared with a Test
        public int CompareTo(Test obj)
        {
            if (TestNumber == null)
            {
                return -1;
            }

            return TestNumber.CompareTo(obj.TestNumber);
        }
    }
}
