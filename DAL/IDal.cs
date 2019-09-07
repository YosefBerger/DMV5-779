using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    /// <summary>
    /// the DAL Layer accesses all of the raw data in the DS Layer
    /// it can add new data to it, delete current data, and update current data
    /// it can also view the data filtered by various conditions
    /// </summary>
    public interface IDal
    {
        bool addTrainee(Trainee tr);
        bool removeTrainee(Trainee tr);
        bool updateTrainee(Trainee tr);
        List<Trainee> getAllTrainees(Func<Trainee, bool> condition = null);
        Trainee GetTraineeByID(String ID);

        bool addTest(Test test);
        bool removeTest(Test test);
        bool updateTest(Test test);
        List<Test> getAllTests(Func<Test, bool> condition = null);

        /// <summary>
        /// Returns The Test with the passed Number or null if no such Test exists
        /// </summary>
        /// <param name="ID">Number of Test</param>
        /// <returns></returns>
        Test GetTestByNumber(String Number);

        bool addTester(Tester tester);
        bool removeTester(Tester tester);
        bool updateTester(Tester tester);
        List<Tester> getAllTesters(Func<Tester, bool> condition = null);
        Tester GetTesterByID(String ID);
    }
}
