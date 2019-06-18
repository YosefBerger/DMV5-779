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
        public bool

        public bool addTester(Tester tester)
        {
            
            if (getAge(tester) < Configuration.TESTER_MIN_AGE)
            {
                return false;
            }
        }
    }
}
