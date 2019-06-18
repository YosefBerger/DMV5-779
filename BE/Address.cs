using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct Address
    {
        public String Street { get; set; }
        public int Number { get; set; }
        public String City { get; set; }

        public Address clone()
        {
            return new Address
            {
                Street = this.Street,
                Number = this.Number,
                City = this.City
            };
        }

        public override string ToString()
        {
            String result = "";

            result += String.Format("{0} {1}, {2}\t", Street, Number, City);
            //result += String.Format("Street Name: {0}", Street);
            //result += String.Format("Street Number: {0}", Number);
            //result += String.Format("City: {0}", City);
            return result;
        }
    }
}
