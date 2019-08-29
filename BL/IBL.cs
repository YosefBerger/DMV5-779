using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;


namespace BL
{
    public interface IBL
    {
        bool addTrainee(Trainee tr);
        bool removeTrainee(Trainee tr);
        bool updateTrainee(Trainee tr);
        List<Trainee> getAllTrainees(Func<Trainee, bool> condition = null);

        bool addTest(Test test);
        bool removeTest(Test test);
        bool updateTest(Test test);
        List<Test> getAllTests(Func<Test, bool> condition = null);

        bool addTester(Tester tester);
        bool removeTester(Tester tester);
        bool updateTester(Tester tester);
        List<Tester> getAllTesters(Func<Tester, bool> condition = null);
        

        /// <summary>
        /// Returns a list of Testers who are available at a specified day and time.
        /// </summary>
        /// <param name="dateTime">The Date and hour to check.</param>
        /// <returns>A List<> of Testers who work at the specified date/time and are not already schedualed for that date/time</returns>
        List<Tester> testersForTime(DateTime dateTime, List<Tester> list = null);

        /// <summary>
        /// Retuns a list of Testers who are able to proctor a test starting at a specific address.
        /// </summary>
        /// <param name="address">The address at which a test will start.</param>
        /// <returns>A List<> of Testers for whom the Adress is whithin their working distance.</returns>
        List<Tester> testersForAddress(Address address, List<Tester> list = null, BackgroundWorker backgroundWorker = null);

        /// <summary>
        /// Returns a list of Testers who can test for the specified vehicle type.
        /// </summary>
        /// <param name="vehicleType">The type of vehicle to chek.</param>
        /// <returns>A List<> of Testers who specialize in the specified vehicle type</returns>
        List<Tester> testersForVehicle(VehicleType vehicleType, List<Tester> list = null);

        /// <summary>
        /// Returns if the passed Tester is at or has exceeded the maximum nuber of tests that they may proctor in the week of the passed date
        /// </summary>
        /// <param name="tester">The Tester to check</param>
        /// <param name="dateTime">A date that is within the week to be checked</param>
        /// <returns>A bool representing if the Tester cannot schedule more tests</returns>
        bool exceededNumberOfTests(Tester tester, DateTime dateTime);

        int numTestsTaken(Trainee trainee);
        bool passedTest(String ID);

        /// <summary>
        /// Returns an ordered list of all the tests on the passed day
        /// </summary>
        /// <param name="day">The day to get the tests from</param>
        /// <returns>Tests in the passed day ordered by hour</returns>
        List<Test> scheduledTestsDay(DateTime day);

        /// <summary>
        /// Returns an ordered list of all the tests in the month of the passed date
        /// </summary>
        /// <param name="month">A date in the month to check</param>
        /// <returns>Tests in the month of the passed date</returns>
        List<Test> scheduledTestsMonth(DateTime month);


        /// <summary>
        /// Returns a list of Trainees who live within a specified distance to a specified Address
        /// </summary>
        /// <param name="center">The address to measure all the students against.</param>
        /// <param name="km">The distance to check around</param>
        /// <returns></returns>
        List<Trainee> traineesInRange(Address center, double km);

        /// <summary>
        /// Gets the distance between two addresses using mapquest api.
        /// </summary>
        /// <param name="add1">The start address</param>
        /// <param name="add2">The destination address</param>
        /// <returns>Distance in km.</returns>
        double getDistance(Address add1, Address add2);

        /// <summary>
        /// Retruns a DateTime that would be able to accomodate the Trainee of the passed test.
        /// This takes in to account all potential issues.
        /// </summary>
        /// <param name="test">Test Containing the Trainee and DateTime to compare against</param>
        /// <returns>A DateTime that would be able to accomodate the Trainee of the passed test</returns>
        DateTime NewValidDateTime(Test test);

        /// <summary>
        /// Returns The Trainee with the passed ID or null if no such Trainee exists
        /// </summary>
        /// <param name="ID">ID of Trainee</param>
        /// <returns></returns>
        Trainee GetTraineeByID(String ID);

        /// <summary>
        /// Returns The Tester with the passed ID or null if no such Tester exists
        /// </summary>
        /// <param name="ID">ID of Trainee</param>
        /// <returns></returns>
        Tester GetTesterByID(String ID);

        /// <summary>
        /// Returns The Test with the passed Number or null if no such Test exists
        /// </summary>
        /// <param name="ID">Number of Test</param>
        /// <returns></returns>
        Test GetTestByNumber(String Number);

        /// <summary>
        /// Recommends a date that is not too close to any other tests for the Trainee and date of the passed test
        /// </summary>
        /// <param name="test">Test with the Trainee and date to compare against</param>
        /// <returns>DateTime that is not conflicting with any other tests for that trainee</returns>
        DateTime GetNonConflictingDate(Test test);

        /// <summary>
        /// All past tests that a trainee has already taken
        /// </summary>
        /// <param name="trainee"></param>
        /// <returns></returns>
        List<Test> traineeTests(Trainee trainee);

        IEnumerable<IGrouping<VehicleType, Tester>> testerByVehicalType(bool sorted = false);
        IEnumerable<IGrouping<String, Trainee>> traineesBySchool(bool sorted = false);
        IEnumerable<IGrouping<String, Trainee>> traineesByInstructor(bool sorted = false);
    }
}
