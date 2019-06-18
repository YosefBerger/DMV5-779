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

        public List<Trainee> allTrainees()
        {
            //some verifications validations and more...
            //anything else
            //and so on....
            return dal.getAllTrainees();
        }

        public bool addTester(Tester tester)
        {
            if (tester.)
        }
    }
}
