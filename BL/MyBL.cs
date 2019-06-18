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
            if(trainee.getAge() >= Configuration.TRAINEE_MIN_AGE)
            {
                return dal.updateTrainee(trainee);
            }
            return false;
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

    }
}
