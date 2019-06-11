﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Test
    {
        private static int TestCounter = 0; // Allows us to have incramental test numbers

        private string Number;  // Hold actual test number
        public string TestNumber {
            get
            {
                return Number;
            }
            set
            {
                Number = String.Format("{0:D8}", TestCounter);

                TestCounter++;
                // Avoid integer over flow erros
                if(TestCounter >= 100000000)
                {
                    TestCounter = 0;
                }
            }
        }

        private string testerId;    // Hold the actualle tester ID
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

        private string traineeId;    // Hold the actualle tester ID
        public string TraineerId      // Get and set the tester ID
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

        private DateTime dateTime;
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

        public Address StartAddress { get; set; } // The address from which the test was/will be started

        // Aspects that they will be tested on
        public bool UseMirrors { get; set; }
        public bool MaintinaDistance { get; set; }
        public bool ParkInReverse { get; set; }
        public bool Signals { get; set; }
        public bool ParralellPraking { get; set; }
        public bool Awareness { get; set; }
        public bool StopSigns { get; set; }
        public bool SpeedLimit { get; set; }

        public bool Result { get; set; }    // Result of the test (if they passed or failed)

        public string TesterComment { get; set; }   // Hold any comments from the tester about the test

        public override string ToString()
        {
            string result = "";

            result += String.Format("Test Number:      {0}\n", Number);
            result += String.Format("Tester:           {0}\n", TesterId);
            result += String.Format("Trainee:          {0}\n", TraineerId);
            result += String.Format("Date/Time:        {0}\n", DateTime);
            result += String.Format("Address:          {0}\n", StartAddress);
            result += "----------------------------------\n";
            result += String.Format("Use Mirrors:      {0}\n", UseMirrors ? "PASS" : "FAIL");
            result += String.Format("Parke in Reverse: {0}\n", ParkInReverse ? "PASS" : "FAIL");
            result += String.Format("Signals:          {0}\n", Signals ? "PASS" : "FAIL");
            result += String.Format("Parralel Parking: {0}\n", ParralellPraking ? "PASS" : "FAIL");
            result += String.Format("Awareness:        {0}\n", Awareness ? "PASS" : "FAIL");
            result += String.Format("Stop Signs:       {0}\n", StopSigns ? "PASS" : "FAIL");
            result += String.Format("Speed Limit:      {0}\n", Signals ? "PASS" : "FAIL");
            result += "----------------------------------\n";
            result += String.Format("The trainee {0} the test.\n", Result ? "PASSED" : "FAILED");
            result += String.Format("Comment from the tester:\n\t\"{0}\"\n", TesterComment);

            return result;
        }
    }
}