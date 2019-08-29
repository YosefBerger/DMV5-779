using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BL
{
    internal class MyBL : IBL
    {
        IDal dal = FactoryDal.getInstance();

        public List<Trainee> getAllTrainees(Func<Trainee, bool> condition = null)
        {
            //some verifications validations and more...
            //anything else
            //and so on....
            return dal.getAllTrainees(condition);
        }

        public bool addTrainee(Trainee trainee)
        {
            //if(trainee.getAge() >= Configuration.TRAINEE_MIN_AGE)
            //{
            //    return dal.addTrainee(trainee);
            //}

            //return false;
            if (trainee.getAge() >= Configuration.TRAINEE_MIN_AGE)
            {
                return dal.addTrainee(trainee);
            }

            return false;
        }

        public bool removeTrainee(Trainee trainee)
        {
            return dal.removeTrainee(trainee);
        }

        public bool updateTrainee(Trainee trainee)
        {
            //if(trainee.getAge() >= Configuration.TRAINEE_MIN_AGE && trainee.NumDrivingLessons >= Configuration.TRAINEE_MIN_LESSONS)
            //{
            //    return dal.updateTrainee(trainee);
            //}
            //return false;
            return dal.updateTrainee(trainee);
        }

        public List<Tester> getAllTesters(Func<Tester, bool> condition = null)
        {

            return dal.getAllTesters(condition);
        }

        public bool addTester(Tester tester)
        {
            
            if (tester.getAge() >= Configuration.TESTER_MIN_AGE)
            {
                return dal.addTester(tester);
            }
            
            return false;
        }

        public bool removeTester(Tester tester)
        {
            return dal.removeTester(tester);
        }

        public bool updateTester(Tester tester)
        {
            if(tester.getAge() >= Configuration.TESTER_MIN_AGE)
            {
                return dal.updateTester(tester);
            }

            return false;
        }

        public List<Test> getAllTests(Func<Test, bool> condition = null)
        {
            return dal.getAllTests(condition);
        }

        public bool addTest(Test test)
        {
            // Make sure that the Trainee exists
            Trainee trainee = GetTraineeByID(test.TraineeId);
            if (trainee == null)
            {
                throw new Exception("Invalid Trainee ID");
            }

            // Make sure that the trainee had not already passed a test for this vehicle type
            /* Right now Vehicle type is tied to the traineee because that is what the sheet said to do so we are only going to check if they passed a test at all */
            List<Test> testsTakenByTrainee = traineeTests(trainee);
            List<Test> passedTests = (from t in testsTakenByTrainee
                                      where t.Result
                                      select t).ToList();
            if (passedTests.Count != 0)
            {
                throw new Exception("The Trainee has already passed a test");
            }

            // Make sure StartAddress exists
            if (test.StartAddress == null || test.StartAddress.City == null || test.StartAddress.Street == null || test.StartAddress.Number < 0)
            {
                throw new Exception("Invalid start address");
            }

            // Make sure test is not within 7 days of another test.
            List<Test> conflictingTests = getAllTests(new Func<Test, bool>
                (it => it.TraineeId == test.TraineeId && Math.Abs((test.DateTime.Date - it.DateTime.Date).Days) <= Configuration.DAYS_FROM_TEST));
            if (conflictingTests.Count() != 0)
            {
                String dates = "";
                for(int i = 0; i < conflictingTests.Count(); i++)
                {
                    if (i > 0)
                    {
                        dates += " and a test on ";
                    }
                    dates += conflictingTests.ElementAt(i).ToString();
                }
                throw new Exception("The test date is within " + Configuration.DAYS_FROM_TEST
                    + " day(s) of a schedualed test on " + dates + ". Try " + GetNonConflictingDate(test));
            }


            // Make sure the Tester is valid
            Tester tester = GetTesterByID(test.TesterId);
            if (tester == null || !tester.getIfWorking(test.DateTime) || exceededNumberOfTests(tester, test.DateTime))
            {
                DateTime possibleDate = NewValidDateTime(test);

                throw new Exception("The specified Tester is unvailable at the specified time, the next time there is a Tester available is "
                                    + possibleDate.ToString("MM/dd/yyyy HH") + ":00");
            }

            return dal.addTest(test);
        }

        public Tester GetTesterByID(String ID)
        {
            return dal.GetTesterByID(ID);
        }

        public DateTime NewValidDateTime(Test t)
        {
            // Clone the test to make sure we don't screw anything up outside
            // This test will also hold our new DateTime.
            Test test = t.Clone();
            while (true)
            {
                Console.WriteLine("In NewValidDateTime loop");
                // Make sure that there are not any conflicting dates
                test.DateTime = GetNonConflictingDate(test);

                // For each hour of the working day
                for (int i = Configuration.DAY_START; i < Configuration.DAY_END; i++)
                {
                    Console.WriteLine("In Hour Loop");
                    // Reste the Date and then add i hours
                    test.DateTime = test.DateTime.Date.AddHours(i);

                    // Check if there are any testers available at the specified date and time
                    if (TestersAvailableAtDateTime(test))
                    {
                        Console.WriteLine("Found DateTime" + test.DateTime.ToString("mm/dd/yyyy"));
                        // If there is a Tester, congrats, we have a date time, return it
                        return test.DateTime;
                    }
                }

                // If we don't have a good date, try the next working day
                do
                {
                    test.DateTime = test.DateTime.Date.AddDays(1);
                } while ((int)test.DateTime.DayOfWeek > (int)DayOfWeek.Thursday);
            }
        }

        public Test GetTestByNumber(String Number)
        {
            return dal.GetTestByNumber(Number);
        }

        public Trainee GetTraineeByID(String ID)
        {
            return dal.GetTraineeByID(ID);
        }

        /// <summary>
        /// Returns if there is a tester available at the date and time of the passed test.
        /// </summary>
        /// <param name="test">The test with the DateTime to compare against.
        /// If StartAddress != null it will also attempt to check distance, this may take some time.</param>
        /// <returns></returns>
        private bool TestersAvailableAtDateTime(Test test)
        {
            // Get the trainee specified in the test, used for vehicle type
            Trainee trainee = GetTraineeByID(test.TraineeId);

            // Get the testers who work during the date and time of the test who are free at that time and
            // will not be over their tests per week if they were to take this test
            List<Tester> availableTesters = testersForTime(test.DateTime);
            
            // If the passed trainee exists then we will narrow down by VehicleType as well
            if (trainee != null)
            {
                availableTesters = testersForVehicle(trainee.VehicleType);
            }
            
            // If there is a specified address
            if (test.StartAddress != null)
            {
                // Get the testers that can service that address, this may take some time
                availableTesters = testersForAddress(test.StartAddress, availableTesters);
            }

            return availableTesters.Count != 0;
        }

        /// <summary>
        /// Recommends a date that is not too close to any other tests for the Trainee and date of the passed test
        /// </summary>
        /// <param name="test">Test with the Trainee and date to compare against</param>
        /// <returns>DateTime that is not conflicting with any other tests for that trainee</returns>
        public DateTime GetNonConflictingDate(Test test)
        {
            // Hold the date to recommend, ensure the starting date both exists and isn't in the past
            DateTime newDate = (test.DateTime != null || test.DateTime.CompareTo(DateTime.Today) < 0) ? test.DateTime : DateTime.Today;

            // Hold all the tests that the trainee is scheduald to take
            List<Test> TraineeTests = getAllTests(new Func<Test, bool>(it => it.TraineeId == test.TraineeId));

            // Get the tests that are on dates that conflict with the passed test
            List<Test> conflictingTests = (from t in TraineeTests
                                               // Get a TimeSpan object representing the span between the two dates and compare to config
                                           where (Math.Abs((newDate - t.DateTime.Date).Days) <= Configuration.DAYS_FROM_TEST)
                                           select t).ToList();


            // While there are still test that have conflicting dates
            while (conflictingTests.Count != 0)
            {
                // Get the latest date by reducing the list of conflicting tests
                DateTime LatestDate = conflictingTests.Aggregate((Test x, Test y) => (x.DateTime.CompareTo(y.DateTime) > 0) ? x : y).DateTime;

                // Add enough days to make it not conflicting with the latest date
                newDate = LatestDate.AddDays(Configuration.DAYS_FROM_TEST + 1);
                // Make sure this new date is not on a Friday or Saturday
                if ((int)LatestDate.DayOfWeek > (int)DayOfWeek.Thursday)
                {
                    // If it is add enough days to make it the next Sunday
                    newDate = newDate.AddDays(Math.Abs((int)DayOfWeek.Thursday - (int)newDate.DayOfWeek));
                }
                // Find any tests that conflict with this new date, and if there are any, repete the process
                conflictingTests = (from t in TraineeTests
                                        // Get a TimeSpan object representing the span between the two dates and compare to config
                                    where (Math.Abs((newDate - t.DateTime.Date).Days) <= Configuration.DAYS_FROM_TEST)
                                    select t).ToList();
            }

            // Now that we are out of the while loop we know we have a date with no conflicting tests, so retrun it
            return newDate;
        }

        public bool removeTest(Test test)
        {
            return dal.removeTest(test);
        }

        public bool updateTest(Test test)
        {
            return dal.updateTest(test);
        }

        // a list of all testers who are working at a specific time and are not testing anyone
        public List<Tester> testersForTime(DateTime dateTime, List<Tester> list = null)
        {
            List<Tester> working;
           if (list == null)
            {
                working = getAllTesters(new Func<Tester, bool>(it => it.getIfWorking(dateTime) && !exceededNumberOfTests(it, dateTime)));
            }
           else
            {
                working = (from t in list
                           where (t.getIfWorking(dateTime) && !exceededNumberOfTests(t, dateTime))
                           select t.Clone()).ToList();
            }

            // all tests taking place at the specific date and time, ignore hour
            List<Test> testAtTime = getAllTests(new Func<Test, bool>(it => it.DateTime.Date == dateTime.Date && it.DateTime.Hour == dateTime.Hour));
            if (testAtTime == null || testAtTime.Count == 0)
            {
                return working;
            }
            // create new list called available which will contain all Testers available at the specific time
            List<Tester> available = new List<Tester>();
            foreach(Tester t in working)
            {
                foreach (Test item in testAtTime)
                {
                    
                    if(item.TesterId != t.ID) // check that the IDs are not equal, which means that the tester is free
                    {
                        available.Add(t.Clone());
                    }
                }
            }
            return available;    
        }

        // Returns a list of Testers who can test for the specified vehicle type
        public List<Tester> testersForVehicle(VehicleType vehicleType, List<Tester> list = null)
        {
            List<Tester> specVehicle;
            if (list == null)
            {
                specVehicle = (getAllTesters(new Func<Tester, bool>(it => it.VehicleType == vehicleType)));
            }
            else
            {
                specVehicle = (from t in list
                              where t.VehicleType == vehicleType
                              select t.Clone()).ToList();
            }
            
            return specVehicle;
        }

        // Check if tester is ar capacity for number of tests for the week
        public bool exceededNumberOfTests(Tester tester, DateTime dateTime)
        {
            if(getNumTestsPerWeekForTester(tester, dateTime) >= tester.MaxWeeklyTests)
            {
                return true;
            }
            return false;
        }
        private int getNumTestsPerWeekForTester(Tester tester, DateTime dateTime)
        {
            // date of sunday
            DateTime SundayDate = dateTime.AddDays(-(int)dateTime.DayOfWeek);

            // testsOfWeek is a list of all days within the week of the date passed in.
            // testsOfWeek uses time parameters to find the days.
            List<Test> testsOfWeek = getAllTests(new Func<Test, bool>(it => (it.DateTime - SundayDate).Days >= 0 && (it.DateTime - SundayDate).Days <= 7));

            // find number of times the tester tests during the week
            int counter = 0;
            foreach(Test test in testsOfWeek)
            {
                // if the tester ID of the test equals the tester ID, incrament the counter
                if(test.TesterId == tester.ID)
                {
                    counter += 1;
                }
            }
            return counter;
        }


        // return number of tests the trainee has taken
        public int numTestsTaken(Trainee trainee)
        {
            return traineeTests(trainee).Count();
            
        }
        public List<Test> traineeTests(Trainee trainee)
        {
            // if the trainee ID on the test is equal to the passed in trainee id, put the test in the list
            List<Test> traineeTests = getAllTests(new Func<Test, bool>(it => it.TraineeId == trainee.ID && it.DateTime.CompareTo(DateTime.Now) <= 0));
            return traineeTests;
        }


        public List<Trainee> traineesInRange(Address center, double km)
        {
            return getAllTrainees(new Func<Trainee, bool>(it => getDistance(center, it.Address) <= km));
        }

        public List<Tester> testersForAddress(Address address, List<Tester> list = null, BackgroundWorker backgroundWorker = null)
        {
            List<Tester> testers;
            if (list == null)
            {
                testers = getAllTesters(new Func<Tester, bool>(it => getDistance(it.Address, address) <= it.MaxDistance));
            }
            else
            {
                testers = new List<Tester>();
                int counter = 0;
                foreach (Tester t in list)
                {
                    double dist = getDistance(t.Address, address);
                    Console.WriteLine("" + t.FirstName + " " + dist + " " + (dist <= t.MaxDistance));
                    if (dist <= t.MaxDistance)
                    {
                        testers.Add(t.Clone());
                    }
                    if (backgroundWorker != null)
                    {
                        if (backgroundWorker.CancellationPending)
                        {
                            return null;
                        }
                        backgroundWorker.ReportProgress((counter++/list.Count) * 100);
                    }
                }
                //testers = (from t in list
                //           where (getDistance(t.Address, address) <= t.MaxDistance)
                //           select t.Clone()).ToList();
            }

            return testers;
        }

        public bool passedTest(String ID)
        {
            return getAllTests(new Func<Test, bool>(it => it.TraineeId == ID && it.Result)).Count > 0;
        }

        public double getDistance(Address add1, Address add2)
        {
            /*
             * Get consistant random distance based on the id numbers
            double num1 = add1.ToString().GetHashCode() % 100;
            double num2 = add2.ToString().GetHashCode() % 100;

            return Math.Abs(num2 - num1);
            */

            string start = add1.Number + " " + add1.Street + " " + add1.City + " Israel";
            string end = add2.Number + " " + add2.Street + " " + add2.City + " Israel";
            string KEY = "soKBpkSSlUtTb1dxSAXZx4exA0Z0xLRh";

            string url = @"https://www.mapquestapi.com/directions/v2/route" +
            @"?key=" + KEY +
            @"&from=" + start +
            @"&to=" + end +
            @"&outFormat=xml" +
            @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
            @"&enhancedNarrative=false&avoidTimedConditions=false";

            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            Stream dataStream = resp.GetResponseStream();
            StreamReader sread = new StreamReader(dataStream);
            string respString = sread.ReadToEnd();
            resp.Close();

            // Make the string an XML document
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(respString);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0") //We recived a valid responce
            {
                Console.WriteLine("Successfull route Call");
                // Get dispance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                return Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402") // Error occured
            {
                Console.WriteLine("An error occurred, Invalid Location.");
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "403") // Error occured
            {
                Console.WriteLine("An error occured, Something is wong with the API Key.");
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "604") // Error occured
            {
                Console.WriteLine("An error occured, Search took too long.");
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "605") // Error occured
            {
                Console.WriteLine("An error occured, distance too far for mapquest.");
            }
            else //busy network or other error...
            {
                Console.WriteLine("No responce recived, network may be busy.");
            }

            return int.MaxValue;
        }


        public List<Test> scheduledTestsDay(DateTime day)
        {
            List<Test> tests = getAllTests(new Func<Test, bool>(it => (it.DateTime - day).Days == 0));
            tests.Sort((test1, test2) => test1.DateTime.CompareTo(test2.DateTime));
            return tests;
        }

        public List<Test> scheduledTestsMonth(DateTime month)
        {
            List<Test> tests = getAllTests(new Func<Test, bool>(it => (it.DateTime.Month == month.Month) && (it.DateTime.Year == month.Year)));
            tests.Sort((test1, test2) => test1.DateTime.CompareTo(test2.DateTime));
            return tests;
        }


        


        public IEnumerable<IGrouping<VehicleType, Tester>> testerByVehicalType(bool sorted = false)
        {
            IEnumerable<IGrouping<VehicleType, Tester>> grouping = from item in getAllTesters()
                                                                   group item by item.VehicleType;
            return grouping;
        }

        public IEnumerable<IGrouping<String, Trainee>> traineesBySchool(bool sorted = false)
        {
            IEnumerable<IGrouping<String, Trainee>> grouping = from item in getAllTrainees()
                                                               group item by item.DrivingSchool;
            return grouping;
        }

        public IEnumerable<IGrouping<String, Trainee>> traineesByInstructor(bool sorted = false)
        {
            IEnumerable<IGrouping<String, Trainee>> grouping = from item in getAllTrainees()
                                                               group item by item.InstructorName;
            return grouping;
        }
    }
}
