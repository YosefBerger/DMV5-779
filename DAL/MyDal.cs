using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    /// <summary>
    /// all of the add X methods chech to make sure the data is not already in the system
    /// all of the delete X methods and update X methods chech to make sure the data is in the system
    /// all of the getAll X methods return a list filtered by a condition of that type of data
    /// </summary>

    //implimentation of the dal interface
    internal class MyDal : IDal
    {
        public bool addTrainee(Trainee tr)
        {

            foreach (var item in DataSource.Trainees)
            {
                if(item.ID == tr.ID)
                    return false;
            }
            DataSource.Trainees.Add(tr.Clone());
            return true;
        }

        public List<Trainee> getAllTrainees(Func<Trainee, bool> condition = null)
        {
            IEnumerable<Trainee> result = null;

            if (condition != null)
            {
                result = from item in DataSource.Trainees
                         where (condition(item))
                         select item.Clone();
            }
            else
            {
                result = from item in DataSource.Trainees
                         select item.Clone();
            }
            return result.ToList();
        }

        public bool removeTrainee(Trainee tr)
        {
            foreach (var item in DataSource.Trainees)
            {
                if (item.ID == tr.ID)
                {
                    return DataSource.Trainees.Remove(item);
                }
            }
           return false;
        }
        public bool updateTrainee(Trainee tr)
        {
            foreach (var item in DataSource.Trainees)
            {
                if (item.ID == tr.ID)
                {
                    item.Update(tr);
                    return true;
                }
            }
            return false;
        }

        public bool addTest(Test test)
        {
            foreach(var item in DataSource.Tests)
            {
                if(item.TestNumber == test.TestNumber)
                {
                    return false;
                }
            }

            DataSource.Tests.Add(test.Clone());
            return true;
        }
        public bool removeTest(Test test)
        {
            foreach (var item in DataSource.Tests)
            {
                if (item.TestNumber == test.TestNumber)
                {
                    return DataSource.Tests.Remove(item);
                }
            }
            return false;
        }
        public bool updateTest(Test test)
        {
            foreach (var item in DataSource.Tests)
            {
                if (item.TestNumber == test.TestNumber)
                {
                    item.update(test);
                    return true;
                }
            }
            return false;
        }
        public List<Test> getAllTests(Func<Test, bool> condition = null)
        {
            IEnumerable<Test> result = null;

            if (condition != null)
            {
                result = from item in DataSource.Tests
                         where (condition(item))
                         select item.Clone();
            }
            else
            {
                result = from item in DataSource.Tests
                         select item.Clone();
            }
            return result.ToList();
        }

        public bool addTester(Tester tester)
        {
            foreach (var item in DataSource.Testers)
            {
                if (item.ID == tester.ID)
                    return false;
            }
            DataSource.Testers.Add(tester.Clone());
            return true;
        }
        public bool removeTester(Tester tester)
        {
            foreach (var item in DataSource.Testers)
            {
                if (item.ID == tester.ID)
                {
                    return DataSource.Testers.Remove(item);
                }
            }
            return false;
        }
        public bool updateTester(Tester tester)
        {
            foreach (var item in DataSource.Testers)
            {
                if (item.ID == tester.ID)
                {
                    item.Update(tester);
                }
            }
            return false;
        }
        public List<Tester> getAllTesters(Func<Tester, bool> condition = null)
        {
            IEnumerable<Tester> result = null;

            if (condition != null)
            {
                result = from item in DataSource.Testers
                         where (condition(item))
                         select item.Clone();
            }
            else
            {
                result = from item in DataSource.Testers
                         select item.Clone();
            }
            return result.ToList();
        }

        public Test GetTestByNumber(String Number)
        {
            List<Test> tests = getAllTests(new Func<Test, bool>(t => t.TestNumber == Number));
            return (tests.Count == 0) ? null : tests.FirstOrDefault();
        }

        public Tester GetTesterByID(String ID)
        {
            List<Tester> testers = getAllTesters(new Func<Tester, bool>(it => it.ID == ID));
            return (testers.Count == 0) ? null : testers.FirstOrDefault();
        }

        public Trainee GetTraineeByID(String ID)
        {
            List<Trainee> trainees = getAllTrainees(new Func<Trainee, bool>(t => t.ID == ID));
            return (trainees.Count == 0) ? null : trainees.FirstOrDefault();
        }
    }
}
