using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Address
    {
        public Address()
        {
            _Number = 1;
        }
        private String _Street;
        public String Street
        {
            get
            {
                return _Street;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("The Street Name cannot be empty");
                }

                _Street = value;
            }
        }
        private int _Number;
        public int Number
        {
            get
            {
                return _Number;
            }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Invalid number");
                }

                _Number = value;
            }
        }
        private String _City;
        public String City
        {
            get
            {
                return _City;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("The City Name cannot be empty");
                }

                _City = value;
            }
        }

        public Address clone()
        {
            return new Address
            {
                _Street = this._Street,
                _Number = this._Number,
                _City = this._City
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
