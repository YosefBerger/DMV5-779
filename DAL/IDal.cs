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
        bool updateTrainee(Trainee tr);
        List<Trainee> getAllTrainees();

        bool addTest(Test test);
        bool removeTest(Test test);
        bool updateTest(Test test);
        List<Test> getAllTests(Func<Test, bool> condition);

        bool addTester(Tester tester);
        bool removeTester(Tester tester);
        bool updateTester(Tester tester);
        List<Tester> getAllTesters();
    }
}
