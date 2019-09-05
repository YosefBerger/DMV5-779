using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    //factor dal is an empty instance of dal interface
    public class FactoryDal
    {
        public static IDal getInstance()
        {
            //return new MyDal();
            return new XML_IDAL();
        }
    }
}
