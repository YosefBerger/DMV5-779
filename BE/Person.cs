using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Person
    {
        private String myID;

      

        public static bool validID(string value)
        {
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

        public String ID
        {
            get { return myID; }
            set
            {
                if (!validID(value))
                {
                    throw new Exception("not valid ID");
                }
                myID = value;
            }
        }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public MailAddress Email { get; set; }
        public DateTime BirthDay { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public VehicleType VehicleType { get; set; }    // Both tester and trainee need this value, so we put it here
        public override string ToString()
        {
            String result = "";

            result += String.Format("ID:           {0}\n", ID);
            result += String.Format("FirstName:    {0}\n", FirstName);
            result += String.Format("LastName:     {0}\n", LastName);
            result += String.Format("Gender:       {0}\n", Gender);
            result += String.Format("Vehicle Tyep: {0}\n", VehicleType);
            result += String.Format("Phone Number: {0}\n", PhoneNumber);
            result += String.Format("Email:        {0}\n", Email);
            result += String.Format("BirthDay:     {0}\n", BirthDay);
            result += String.Format("Address:      {0}\n", Address);
          
            return result;
        }

        // implement update for person, which will be used by Trainee and Tester
        public void update(Person pr)
        {
            this.Address = pr.Address.clone();
            this.BirthDay = pr.BirthDay;
            this.Email = pr.Email;
            this.FirstName = pr.FirstName;
            this.Gender = pr.Gender;
            this.ID = pr.ID;
            this.LastName = pr.LastName;
            this.PhoneNumber = pr.PhoneNumber;
            this.VehicleType = pr.VehicleType;
        }
    }
}
