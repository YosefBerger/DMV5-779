using BE;
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
    public class MyBL : IBL
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
            if(trainee.getAge() >= Configuration.TRAINEE_MIN_AGE)
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
            if(trainee.getAge() >= Configuration.TRAINEE_MIN_AGE && trainee.NumDrivingLessons >= Configuration.TRAINEE_MIN_LESSONS)
            {
                return dal.updateTrainee(trainee);
            }
            return false;
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
            bool flag = true;

            // Make sure test is not within 7 days of another test.
            // Subtracting two DateTime.Date from each other returns a TimeSpan object representing how many days are between the two dates.
            // We take the abs so that if it's date is after tests's date we can still get if it within 7 days, and also so that it doesn't
            // just return false if it is more than 7 days after. We then and if we have any tests that are within 7 days with flag to continue.
            flag = flag && (getAllTests(new Func<Test, bool>(it => Math.Abs((test.DateTime.Date - it.DateTime.Date).Days) 
            <= Configuration.DAYS_FROM_TEST)).Count() == 0);

            // Make sure there is a tester available at that time, if not sugest alternate times
            

            return false;
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
        public List<Tester> testersForTime(DateTime dateTime)
        {
           
            // it is of type Tester
            List<Tester> working = getAllTesters(new Func<Tester, bool>(it => it.getIfWorking(dateTime)));

            // all tests taking place at the specific time
            List<Test> testAtTime = getAllTests(new Func<Test, bool>(it => it.DateTime.Date == dateTime.Date && it.DateTime.Hour == dateTime.Hour));

            // create new list called available which will contain all Testers available at the specific time
            List<Tester> available = new List<Tester>();
            foreach(Tester t in working)
            {
                foreach(Test item in testAtTime)
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
        public List<Tester> testersForVehicle(VehicleType vehicleType)
        {
            List<Tester> specVehicle = (getAllTesters(new Func<Tester, bool>(it => it.VehicleType == vehicleType)));
            return specVehicle;
        }
        // check if tester exceeded number of tests for the week
        public bool exceededNumberOfTests(Tester tester, DateTime dateTime)
        {
            if(getNumTestsPerWeekForTester(tester, dateTime) > tester.MaxWeeklyTests)
            {
                return false;
            }
            return true;
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

        public int numTestsTaken(Trainee trainee)
        {
            return traineeTests(trainee).Count();
            
        }
        private List<Test> traineeTests(Trainee trainee)
        {
            // if the trainee ID on the test is equal to the passed in trainee id, put the test in the list
            List<Test> traineeTests = getAllTests(new Func<Test, bool>(it => it.TraineerId == trainee.ID && it.DateTime.CompareTo(DateTime.Now) >= 0));
            return traineeTests;
        }





        public List<Trainee> traineesInRange(Address center, double km)
        {
            return getAllTrainees(new Func<Trainee, bool>(it => getDistance(center, it.Address) <= km));
        }

        public List<Tester> testersForAddress(Address address)
        {
            return getAllTesters(new Func<Tester, bool>(it => getDistance(it.Address, address) <= it.MaxDistance));
        }

        public bool passedTest(String ID)
        {
            return getAllTests(new Func<Test, bool>(it => it.TraineeId == ID && it.Result)).Count > 0;
        }

        public double getDistance(Address add1, Address add2/*, RunWorkerCompletedEventHandler completed*/)
        {
            //BackgroundWorker backgroundworker = new BackgroundWorker();
            //backgroundworker.DoWork += Backgroundworker_DoWork;
            //backgroundworker.RunWorkerCompleted += completed;
            //backgroundworker.RunWorkerAsync(new List<Address> { add1, add2 });

            double num1 = add1.ToString().GetHashCode() % 100;
            double num2 = add2.ToString().GetHashCode() % 100;

            return Math.Abs(num2 - num1);
        }

        //private void Backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    double distinKm;

        //    List<Address> objs = e.Argument as List<Address>;

        //    //List<BE.Address> addr = e.Argument as List<BE.Address>;
        //    Address startAddress = objs[0];
        //    Address destAddress = objs[1];


        //    string KEY = API_KEY;

        //    string origin = startAddress.Street + " " + startAddress.Number + " st. " + startAddress.City;
        //    string destination = destAddress.Street + " " + destAddress.Number + " st. " + destAddress.City;

        //    string url = @"https://www.mapquestapi.com/directions/v2/route" +
        //    @"?key=" + KEY +
        //    @"&from=" + origin +
        //    @"&to=" + destination +
        //    @"&outFormat=xml" +
        //    @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
        //    @"&enhancedNarrative=false&avoidTimedConditions=false";
        //    //request from MapQuest service the distance between the 2 addresses
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    WebResponse response = request.GetResponse();
        //    Stream dataStream = response.GetResponseStream();
        //    StreamReader sreader = new StreamReader(dataStream);
        //    string responsereader = sreader.ReadToEnd();
        //    response.Close();
        //    //the response is given in an XML format
        //    System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
        //    xmldoc.LoadXml(responsereader);
        //    if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
        //    //we have the expected answer
        //    {
        //        //display the returned distance
        //        XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
        //        double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
        //        // Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);

        //        distinKm = (distInMiles * 1.609344);
        //        e.Result = distinKm;
        //        //if (distinKm <= currentTester.MaxDistance)
        //        //{
        //        //    e.Result = currentTester;
        //        //}
        //        //else
        //        //{
        //        //    e.Result = null;
        //        //}
        //        //display the returned driving time
        //        // XmlNodeList formattedTime = xmldoc.GetElementsByTagName("formattedTime");
        //        // string fTime = formattedTime[0].ChildNodes[0].InnerText;
        //        //  Console.WriteLine("Driving Time: " + fTime);
        //    }
        //    //else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
        //    ////we have an answer that an error occurred, one of the addresses is not found
        //    //{
        //    //    // Console.WriteLine("an error occurred, one of the addresses is not found. try again.");
        //    //    throw new Exception("an error occurred, one of the addresses is not found. try again.");
        //    //    //MessageBox.Show("an error occurred, one of the addresses is not found. try again.");
        //    //}
        //    //else //busy network or other error...
        //    //{
        //    //    // Console.WriteLine("We have'nt got an answer, maybe the net is busy...");
        //    //    throw new Exception("We have'nt got an answer, maybe the net is busy...");
        //    //    //MessageBox.Show("We have'nt got an answer, maybe the net is busy...");
        //    //}
        //}
    }
}
