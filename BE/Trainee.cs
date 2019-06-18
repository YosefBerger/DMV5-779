using System;

namespace BE
{
    public class Trainee : Person
    {
        // specify gearbox that the trainee learned
        public GearBox GearBox { get; set; }
        // driving school that the trainee attended
        public String DrivingSchool { get; set; }
        // name of instructor wth whom the trainee studied
        public String InstructorName { get; set; }
        // number of lessons
        public int NumDrivingLessons { get; set; }

        // ToString
        public override string ToString()
        {
            String result = "";

            result += base.ToString();
            result += String.Format("GearBox: {0}\n", GearBox);
            result += String.Format("Driving School: {0}\n", DrivingSchool);
            result += String.Format("Instructor Name: {0}\n", InstructorName);
            result += String.Format("Number of Driving Lessons Passed: {0}\n", NumDrivingLessons);

            return result;
        }
        // clone copy constructor
        public Trainee Clone()
        {
            return new Trainee
            {
                Address = this.Address,
                BirthDay = this.BirthDay,
                Email = this.Email,
                FirstName = this.FirstName,
                Gender = this.Gender,
                ID = this.ID,
                LastName = this.LastName,
                VehicleType = this.VehicleType,
                GearBox = this.GearBox,
                DrivingSchool = this.DrivingSchool,
                InstructorName = this.InstructorName,
                NumDrivingLessons = this.NumDrivingLessons

            };
        }

        public void update(Trainee trainee)
        {
            this.update((Person)trainee);
            this.InstructorName = trainee.InstructorName;
            this.NumDrivingLessons = trainee.NumDrivingLessons;
            this.GearBox = trainee.GearBox;
            this.DrivingSchool = trainee.DrivingSchool;
        }
    }
}
