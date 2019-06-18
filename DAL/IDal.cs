using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDal
    {
        bool addTrainee(Trainee tr);
        bool removeTrainee(Trainee tr);
        bool updateTrainee(string id, Trainee tr);  // Replace the exisisting trainee of the id with the passed trainee
        List<Trainee> getAllTrainees();

        bool addTest(Test test);
        bool removeTest(Test test);
        bool updateTest(String num, Test test); // Replace the test corresponding to num with the passed test
        List<Test> getAllTests();

        bool addTester(Tester tester);
        bool removeTester(Tester tester);
        bool updateTester(String id, Tester tester);    // Replace the tester corresponding to the id with the passed tester
        List<Tester> getAllTesters();
    }
}
