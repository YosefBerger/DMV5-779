using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
    /// <summary>
    /// Address is a class that will be used by a person
    /// it has a string for the city and street name and a number for the address
    /// </summary>
    public class Address
    {
        //members
        private String _Street;
        private int _Number;
        private String _City;

        //constuctor
        public Address() { _Number = 1; }

        //copy constuctor
        public Address clone()
        {
            return new Address
            {
                _Street = this._Street,
                _Number = this._Number,
                _City = this._City
            };
        }

        //getters and setters for members
        public String Street
        {
            get
            {
                return _Street;
            }
            set
            {
                //address can't be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("The Street Name cannot be empty");
                }

                _Street = value;
            }
        }
       
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
                    //number can't be negative
                    throw new Exception("Invalid number");
                }

                _Number = value;
            }
        }
        
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

        //overiding to string method
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
