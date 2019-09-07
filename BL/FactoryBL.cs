using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A factory BL is an Empty instance of the BL interface
/// </summary>
namespace BL
{
    public class FactoryBL
    {
        public static IBL getInstance()
        {
            return new MyBL();
        }
    }
}
