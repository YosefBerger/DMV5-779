using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public class MyBL : IBL
    {
        IDal dal = FactoryDal.getInstance();

        public List<Trainee> getAllTrainees()
        {
            //some verifications validations and more...
            //anything else
            //and so on....
            return dal.getAllTrainees();
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
        public List<Tester> getAllTesters()
        {
            return dal.getAllTesters();
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
            flag = flag && (getAllTests(new Func<Test, bool>(it => Math.Abs((test.DateTime.Date - it.DateTime.Date).Days) <= Configuration.DAYS_FROM_TEST)).Count() == 0);

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

    }
}
