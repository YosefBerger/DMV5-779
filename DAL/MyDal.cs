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
                    //return true;
                }
            }
           return false;
        }
        public bool updateTrainee(string id, Trainee tr)
        {
            return false;
        }
        public bool addTest(Test test)
        {
            foreach()

            return true;
        }
        public bool removeTest(Test test)
        {
            return false;
        }
        public bool updateTest(Test test)
        {
            return false;
        }
        public bool addTester(Tester tester)
        {
            return true;
        }
        public bool removeTester(Tester tester)
        {
            return false;
        }
        public bool updateTester(Tester tester)
        {
            return false;
        }
    }
}
