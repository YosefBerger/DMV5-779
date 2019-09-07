using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// A Person class is used by Trainee and Tester
    /// it holds all values they share which are properties of people
    /// </summary>
    public class Person
    {

        private String _ID;
        private String _FirstName;
        private String _LastName;
        private MailAddress _Email;
        public DateTime BirthDay { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public VehicleType VehicleType { get; set; }    // Both tester and trainee need this value, so we put it here

        public Person()
        {
            Address = new Address();
            BirthDay = new DateTime();
            Gender = Gender.MALE;
        }
        
        public String ID
        {
            get
            {
                return _ID;
            }
            set
            {
                //id cannot be empty
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new Exception("ID cannot be empty");
                }

                //id must be a valid number (see VALIDID(string value).)
                else if (!validID(value))
                {
                    throw new Exception("Invalid ID number");
                }

                _ID = value;
            }
        }
       
        public String FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                //the name must not be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("First Name cannot be empty");
                }

                _FirstName = value;
            }
        }
        
        public String LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                //same for last name
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Last Name cannot be empty");
                }

                _LastName = value;
            }
        }
        
        public String Email {
            get
            {
                //turns the email to a string needs to manually use null string if its null
                return (_Email == null) ? "" : _Email.ToString();
            }
            set
            {
                try
                {
                    _Email = new MailAddress(value);
                }
                catch
                {
                    throw new Exception("Invalid Email");
                }
            }
        }

        public int getAge()
        {
            // find today's date
            DateTime today = DateTime.Today;
            int year = today.Year - this.BirthDay.Year;
            // if person has not had their birthday yet this year
            if(today.Month < this.BirthDay.Month)
            {
                return year - 1;
            }
            if(today.Month == this.BirthDay.Month && today.Day < this.BirthDay.Day)
            {
                return year - 1;
            }
            return year;
        }

        public override string ToString()
        {
            String result = "";

            result += String.Format("ID:           {0}\n", ID);
            result += String.Format("FirstName:    {0}\n", FirstName);
            result += String.Format("LastName:     {0}\n", LastName);
            result += String.Format("Gender:       {0}\n", Gender);
            result += String.Format("Vehicle Tyep: {0}\n", VehicleType);
            result += String.Format("Email:        {0}\n", Email);
            result += String.Format("BirthDay:     {0}\n", BirthDay);
            result += String.Format("Address:      {0}\n", Address);

            return result;
        }

        // implement update for person, which will be used by Trainee and Tester
        public void Update(Person pr)
        {
            this.Address = pr.Address.clone();
            this.BirthDay = pr.BirthDay;
            this.Email = pr.Email;
            this.FirstName = pr.FirstName;
            this.Gender = pr.Gender;
            this.ID = pr.ID;
            this.LastName = pr.LastName;
            this.VehicleType = pr.VehicleType;
        }

        //this checks to make sure the id is valid
        public static bool validID(string value)
        {
            value = value.Replace(" ", "");
            long tmp;
            //id is invalid if it is empty has less then 9 digits and cannot be parsed
            if(string.IsNullOrWhiteSpace(value) || value.Length < 9 || !Int64.TryParse(value, out tmp))
            {
                return false;
            }
         
            string m_PERID = value;
            char[] digits = m_PERID.PadLeft(9, '0').ToCharArray();
            int[] oneTwo = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int[] multiply = new int[9];
            int[] oneDigit = new int[9];
            for (int i = 0; i < 9; i++)
                multiply[i] = Convert.ToInt32(digits[i].ToString()) * oneTwo[i];
            for (int i = 0; i < 9; i++)
                oneDigit[i] = (int)(multiply[i] / 10) + multiply[i] % 10;
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += oneDigit[i];
            if (sum % 10 == 0)
                return true;
            return false;
        }
    }
}
