using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
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

        public List<Trainee> getAllTrainees()
        {
            List<Trainee> result = new List<Trainee>();
            foreach (var item in DataSource.Trainees)
            {
                result.Add(item.Clone());
            }
            return result;
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
                    item.update(tr);
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
                    item.update(tester);
                }
            }
            return false;
        }
        public List<Tester> getAllTesters()
        {
            List<Tester> result = new List<Tester>();
            foreach (var item in DataSource.Testers)
            {
                result.Add(item.Clone());
            }
            return result;
        }
    }
}
