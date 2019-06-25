using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;


namespace BL
{
    interface IBL
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
        List<Tester> testersForTime(DateTime dateTime);

        /// <summary>
        /// Retuns a list of Testers who are able to proctor a test starting at a specific address.
        /// </summary>
        /// <param name="address">The address at which a test will start.</param>
        /// <returns>A List<> of Testers for whom the Adress is whithin their working readius.</returns>
        List<Tester> testersForAddress(Address address);

        /// <summary>
        /// Returns a list of Testers who can test for the specified vehicle type.
        /// </summary>
        /// <param name="vehicleType">The type of vehicle to chek.</param>
        /// <returns>A List<> of Testers who specialize in the specified vehicle type</returns>
        List<Tester> testersForVehical(VehicleType vehicleType);

        /// <summary>
        /// Returns if the passed Tester is at or has exceeded the maximum nuber of tests that they may proctor in the week of the passed date
        /// </summary>
        /// <param name="tester">The Tester to check</param>
        /// <param name="dateTime">A date that is within the week to be checked</param>
        /// <returns>A bool representing if the Tester cannot schedule more tests</returns>
        bool exceededNumberOfTests(Tester tester, DateTime dateTime);

        // int numTestsTaken(Trainee trainee);
        // bool passedTest(int ID);

        // list of all scheduled tests by day and month
        List<Test> scheduledTests();
    }
}
