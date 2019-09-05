using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    class XML_IDAL : IDal
    {
        #region Paths

        XElement TestsRoot;
        XElement ConfigRoot;
        string TestersPath = @"Testers.xml";
        string TraineesPath = @"Trainees.xml";
        string TestsPath = @"Tests.xml";
        string ConfigPath = @"Config.xml";

        #endregion

        public XML_IDAL()
        {
            // Varify that the Tests file exists
            if (!File.Exists(TestsPath))
            {
                // If file does not exists, create a new one
                TestsRoot = new XElement("Tests");
                TestsRoot.Save(TestsPath);
            }
            else
            {
                TestsRoot = XElement.Load(TestsPath);
            }

            // Verify that the Cofig file exists
            if (!File.Exists(ConfigPath))
            {
                // If file does not exists, create a new one
                XElement TestNumber = new XElement("TestNumber", 0);
                ConfigRoot = new XElement("Configs", TestNumber);
                ConfigRoot.Save(ConfigPath);
            }
            else
            {
                ConfigRoot = XElement.Load(ConfigPath);
            }

            // Varify that Trainee file exists
            if (!File.Exists(TraineesPath))
            {
                FileStream TraineeFile = new FileStream(TraineesPath, FileMode.Create);
                TraineeFile.Close();
                DataSource.Trainees = new List<Trainee>();
                saveListToXML<Trainee>(new List<Trainee>(), TraineesPath);
            }
            else
            {
                DataSource.Trainees = (LoadListFromXML<Trainee>(TraineesPath));
            }

            // Varify that Tester file exists
            if (!File.Exists(TestersPath))
            {
                FileStream TesterFile = new FileStream(TestersPath, FileMode.Create);
                TesterFile.Close();
                DataSource.Testers = new List<Tester>();
                saveListToXML<Tester>(new List<Tester>(), TestersPath);
            }
            else
            {
                DataSource.Testers = (LoadListFromXML<Tester>(TestersPath));
            }
            //saveListToXML<Trainee>(DataSource.Trainees, TraineesPath);
            //saveListToXML<Test>(DataSource.Tests, TestsPath);
        }

        protected static XML_IDAL instance = null;
        public static XML_IDAL GetInstance()
        {
            if (instance == null)
                instance = new XML_IDAL();
            return instance;
        }
        #region Adition Convertors

        /// <summary>
        /// Converts form BE.address to an XML element
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public XElement ConvertAddress(BE.Address address)
        {
            XElement addressElement = new XElement("Address");

            XElement street = new XElement("Street", address.Street);
            XElement city = new XElement("City", address.City);
            XElement number = new XElement("Number", address.Number);

            addressElement.Add(street, city, number);

            return addressElement;
        }

        /// <summary>
        /// Convert an XML Address to an Address object
        /// </summary>
        /// <param name="addressElement">XElement with a Street, City, and Number</param>
        /// <returns></returns>
        public Address ConvertAddress(XElement addressElement)
        {
            Address address = new Address();

            address.Street = addressElement.Element("Street").Value;
            address.City = addressElement.Element("City").Value;
            address.Number = int.Parse(addressElement.Element("Number").Value);

            return address;
        }


        /// <summary>
        /// Convert from DateTime to XML representation
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        XElement ConvertDateTime(DateTime dateTime)
        {
            XElement dateTimeElement = new XElement("DateTime");

            XElement monthElment = new XElement("Month", dateTime.Month);
            XElement dayElment = new XElement("Day", dateTime.Day);
            XElement yearElment = new XElement("Year", dateTime.Year);
            XElement hourElment = new XElement("Hour", dateTime.Hour);

            dateTimeElement.Add(monthElment, dayElment, yearElment, hourElment);

            return dateTimeElement;
        }

        /// <summary>
        /// Convert an XML DateTime to a DateTime object
        /// </summary>
        /// <param name="dateTimeElement">XElement with a Year, Month, Day, and Hour</param>
        /// <returns></returns>
        DateTime ConvertDateTime(XElement dateTimeElement)
        {
            int Year = int.Parse(dateTimeElement.Element("Year").Value);
            int Month = int.Parse(dateTimeElement.Element("Month").Value);
            int Day = int.Parse(dateTimeElement.Element("Day").Value);
            int Hour = int.Parse(dateTimeElement.Element("Hour").Value);

            return new DateTime(Year, Month, Day, Hour, 0, 0);
        }

        #endregion

        #region Test


        /// <summary>
        /// Convert a Test object to a XML element
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        XElement ConvertTest(Test test)
        {
            XElement testElement = new XElement("Test");

            foreach (PropertyInfo item in typeof(BE.Test).GetProperties())
            {
                // Convert Address
                if (item.Name == "StartAddress")
                {
                    XElement addressElement = ConvertAddress(test.StartAddress);
                    addressElement.Name = "StartAddress";
                    testElement.Add(addressElement);
                }
                // Convert Date
                else if (item.Name == "DateTime")
                {
                    testElement.Add(ConvertDateTime(test.DateTime));
                }
                // Everything else
                else
                {
                    XElement stringElement = new XElement(item.Name, item.GetValue(test, null).ToString());
                    testElement.Add(stringElement);
                }
            }


            return testElement;
        }


        /// <summary>
        /// Convert XML Test to a Test object
        /// </summary>
        /// <param name="testElement"></param>
        /// <returns></returns>
        Test ConvertTest(XElement testElement)
        {
            Test test = new Test();

            foreach (PropertyInfo item in typeof(BE.Test).GetRuntimeProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);

                if (item.Name == "StartAddress")
                {
                    test.StartAddress = ConvertAddress(testElement.Element("StartAddress"));
                }
                else if (item.Name == "DateTime")
                {
                    test.DateTime = ConvertDateTime(testElement.Element("DateTime"));
                }
                //else if (item.Name == )
                else
                {
                    item.SetValue(test, typeConverter.ConvertFrom(testElement.Element(item.Name).Value));
                }
            }

            return test;
        }


        public bool addTest(Test test)
        {
            ConfigRoot = XElement.Load(ConfigPath);

            //ConfigRoot.Element("TestNumber").Value = (int.Parse(ConfigRoot.Element("TestNumber").Value) + 1).ToString();
            //ConfigRoot.Save(ConfigPath);

            int TestNumber;

            // Load the most recently used TestNumber
            XElement numbElement = ConfigRoot.Element("TestNumber");
            // Make sure the element exists
            /*
            if (numbElement == null)
            {
                TestNumber = 0;
            }
            // Get the value from the element
            else
            {
                string valString = numbElement.Value;
                TestNumber = (valString == null) ? 0 : int.Parse(valString);
            }*/

            TestNumber = int.Parse(ConfigRoot.Element("TestNumber").Value) + 1;

            // Save it to make sure we don't go around the loop too many times
            int counter = TestNumber;

            // Find an available valid number
            while (GetTestByNumber(String.Format("{0:D8}", TestNumber)) != null)
            {
                // Increas the number
                TestNumber = TestNumber + 1;

                // Avoid Int over flow by restricting to 8 digits
                if (TestNumber > 99999999)
                {
                    TestNumber = 0;
                }

                // Catch if we have gone around all the possible numbers and still have no answer
                if (TestNumber == counter)
                {
                    throw new Exception("No avialable Test Number");
                }
            }

            // Save the number to XML
            //ConfigRoot.Element("TestNumber").Value = int.Parse(ConfigRoot.Element("TestNumber").Value).ToString();
            ConfigRoot.Element("TestNumber").Value = TestNumber.ToString();
            ConfigRoot.Save(ConfigPath);

            // Add the number to the test and save
            test.TestNumber = String.Format("{0:D8}", TestNumber);
            TestsRoot.Add(ConvertTest(test));
            TestsRoot.Save(TestsPath);
            return true;
        }

        public bool removeTest(Test test)
        {
            XElement testElement = (from t in TestsRoot.Elements()
                                    where t.Element("TestNumber").Value == test.TestNumber
                                    select t).FirstOrDefault();

            if (testElement == null)
            {
                return false;
            }

            testElement.Remove();
            TestsRoot.Save(TestsPath);
            return true;
        }
        public bool updateTest(Test test)
        {
            Test toUpdate = null;

            // Find the test to dupdate
            foreach (XElement t in TestsRoot.Elements())
            {
                if (t.Element("TestNumber").Value.CompareTo(test.TestNumber) == 0)
                {
                    // Get it in order to update it
                    toUpdate = ConvertTest(t);

                    // Remove the XElement from TesstRoot
                    t.Remove();
                }
            }

            // If we didn't find one, just return
            if (toUpdate == null)
            {
                return false;
            }

            // Update the test we got from TestsRoot with the passed test
            toUpdate.update(test);

            // Add the updated Test to TestsRoot and save it
            TestsRoot.Add(ConvertTest(toUpdate));
            TestsRoot.Save(TestsPath);
            return true;
        }

        public List<Test> getAllTests(Func<Test, bool> condition = null)
        {
            IEnumerable<Test> result = null;

            if (condition != null)
            {
                result = from item in TestsRoot.Elements()
                         let t = ConvertTest(item)
                         where (condition(t))
                         select t;
            }
            else
            {
                result = from item in TestsRoot.Elements()
                         select ConvertTest(item);
            }
            return result.ToList();
        }

        /// <summary>
        /// Returns The Test with the passed Number or null if no such Test exists
        /// </summary>
        /// <param name="ID">Number of Test</param>
        /// <returns></returns>
        public Test GetTestByNumber(String Number)
        {
            XElement Test = null;

            try
            {
                Test = (from t in TestsRoot.Elements()
                        where t.Element("TestNumber").Value.CompareTo(Number) == 0
                        select t).FirstOrDefault();
            }
            catch
            {
                return null;
            }

            if (Test == null)
            {
                return null;
            }

            return ConvertTest(Test);
        }

        #endregion

        #region Trainee
        public bool addTrainee(Trainee tr)
        {
            
            Trainee trainee = (from t in DataSource.Trainees
                               where t.ID == tr.ID
                               select t).FirstOrDefault();
            if (trainee == null)
            {
                DataSource.Trainees.Add(tr);
                saveListToXML<Trainee>(DataSource.Trainees, TraineesPath);
                return true;
            }

            /*
            List<Trainee> trainees = LoadListFromXML<Trainee>(TraineesPath);
            Trainee tmp = (from t in trainees
                           where t.ID == tr.ID
                           select t).FirstOrDefault();
            if (tmp == null)
            {
                trainees.Add(tr);
                saveListToXML<Trainee>(trainees, TraineesPath);
                return true;
            }*/

            throw new Exception("DAL: This trainee ID already exists at the Misrad HaRishui");
        }

        public bool removeTrainee(Trainee tr)
        {
            
            Trainee tmp = (from t in DataSource.Trainees
                           where t.ID == tr.ID
                           select t).FirstOrDefault();
            if (tmp != null)
            {
                DataSource.Trainees.Remove(tmp);
                saveListToXML<Trainee>(DataSource.Trainees, TraineesPath);
                return true;
            }

            /*
            List<Trainee> trainees = LoadListFromXML<Trainee>(TraineesPath);
            if (trainees.Remove(tr))
            {
                saveListToXML<Trainee>(trainees, TraineesPath);
            }
            */

            throw new Exception("DAL: This trainee ID does not exist at the Misrad HaRishui");
        }

        public List<Trainee> getAllTrainees(Func<Trainee, bool> condition = null)
        {
            List<Trainee> result = null;


            if (condition != null)
            {
                
                result = (from item in DataSource.Trainees
                            where (condition(item))
                            select item.Clone()).ToList();
                //return LoadListFromXML<Trainee>(TraineesPath).Where(condition).ToList();
            }
            else
            {
                //return LoadListFromXML<Trainee>(TraineesPath);
                result = (from item in DataSource.Trainees
                            select item.Clone()).ToList();
            }
            return result;
        }
        public Trainee GetTraineeByID(String ID)
        {
            //DataSource.Trainees = LoadListFromXML<Trainee>(TraineesPath);
            Trainee trainee = (from t in DataSource.Trainees
                               where t.ID == ID
                               select t).FirstOrDefault();
            return trainee;
        }


        public bool updateTrainee(Trainee tr)
        {
            int index = DataSource.Trainees.FindIndex(x => x.ID == tr.ID);
            if (index != -1)
            {
                DataSource.Trainees[index] = tr;
                saveListToXML<Trainee>(DataSource.Trainees, TraineesPath);
                return true;
            }
            throw new Exception("DAL: This trainee ID does not exist at the Misrad HaRishui");
        }
        #endregion

        #region
        public bool addTester(Tester tester)
        {
            Tester tmp = (from t in DataSource.Testers
                          where t.ID == tester.ID
                          select t).FirstOrDefault();

            /*
            Tester tmp = GetTesterByID(tester.ID);*/
            if (tmp == null)
            {
                DataSource.Testers.Add(tester);
                saveListToXML<Tester>(DataSource.Testers, TestersPath);
                return true;
            }
            throw new Exception("DAL: This tester ID is already in the Misrad HaRishui");
        }

        public bool removeTester(Tester tester)
        {
            Tester tmp = (from t in DataSource.Testers
                          where t.ID == tester.ID
                          select t).FirstOrDefault();
            
            if (tmp != null)
            {
                DataSource.Testers.Remove(tmp);
                saveListToXML<Tester>(DataSource.Testers, TestersPath);
                return true;
            }

            /*
            List<Tester> testers = getAllTesters();
            if (testers.Remove(tester)){
                saveListToXML<Tester>(testers, TestersPath);
                return true;
            }*/

            throw new Exception("DAL: This tester ID is not in the Misrad HaRishui");
        }
        public bool updateTester(Tester tester)
        {
            int index = DataSource.Testers.FindIndex(x => x.ID == tester.ID);
            if (index != -1)
            {
                DataSource.Testers[index] = tester;
                saveListToXML<Tester>(DataSource.Testers, TestersPath);
                return true;
            }

            /*
            List<Tester> testers = getAllTesters();
            Tester toUopdate = (from t in testers
                         where t.ID == tester.ID
                         select t).FirstOrDefault();

            if (toUopdate != null)
            {
                toUopdate.Update(tester);
                saveListToXML<Tester>(testers, TestersPath);
                return true;
            }*/
            
            throw new Exception("DAL: This tester ID does not exist at the Misrad HaRishui");
        }
        public List<Tester> getAllTesters(Func<Tester, bool> condition = null)
        {
            IEnumerable<Tester> result = null;

            if (condition != null)
            {
                result = from item in DataSource.Testers
                            where (condition(item))
                            select item.Clone();
                //return LoadListFromXML<Tester>(TraineesPath).Where(condition).ToList();
            }
            else
            {
                //return LoadListFromXML<Tester>(TraineesPath).ToList();
                result = from item in DataSource.Testers
                            select item.Clone();
            }
            return result.ToList();
        }
        public Tester GetTesterByID(String ID)
        {
            return DataSource.Testers.Where(x => x.ID == ID).FirstOrDefault();
            //return LoadListFromXML<Tester>(TestersPath).FirstOrDefault(new Func<Tester, bool>(x => x.ID == ID));
        }
        #endregion

        #region Save and Load Lists to XML
        public static void saveListToXML<T>(List<T> list, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            //XmlSerializer serializer = new XmlSerializer(list.GetType());
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(file, list);
            file.Close();
        }
        public static List<T> LoadListFromXML<T>(string path)
        {
            List<T> list;
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            FileStream file = new FileStream(path, FileMode.Open);
            list = (List<T>)serializer.Deserialize(file);
            /* try
             {
                 list = (List<T>)serializer.Deserialize(file);
             }
             catch
             {
                 list = new List<T>();
             }
             */
            file.Close();
            return list;
        }
        #endregion
    }
}

