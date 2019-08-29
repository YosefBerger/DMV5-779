using System;

namespace BE
{
    [Serializable]
    public class Trainee : Person
    {
        public Trainee()
        {
            VehicleType = VehicleType.PRIVATE;
            GearBox = GearBox.AUTOMATIC;
        }

        // specify gearbox that the trainee learned, must be set
        public GearBox GearBox { get; set; }
        // driving school that the trainee attended
        private String _DrivingSchool;
        public String DrivingSchool
        {
            get
            {
                return _DrivingSchool;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Driving School Name cannot be empty");
                }

                _DrivingSchool = value;
            }
        }
        // name of instructor wth whom the trainee studied
        private String _InstrucotrName;
        public String InstructorName
        {
            get
            {
                return _InstrucotrName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Instructor Name cannot be blank");
                }

                _InstrucotrName = value;
            }
        }
        // number of lessons
        private int _NumDrivingLessons;
        public int NumDrivingLessons
        {
            get
            {
                return _NumDrivingLessons;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Invalid number of lessons");
                }

                _NumDrivingLessons = value;
            }
        }

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
                Address = this.Address.clone(),//
                BirthDay = this.BirthDay, //
                Email = this.Email,//
                FirstName = this.FirstName,//
                Gender = this.Gender,//
                ID = this.ID, //
                LastName = this.LastName, //
                VehicleType = this.VehicleType,//
                GearBox = this.GearBox,//
                DrivingSchool = this.DrivingSchool,
                InstructorName = this.InstructorName,
                NumDrivingLessons = this.NumDrivingLessons

            };
        }

        public void Update(Trainee trainee)
        {
            this.Update((Person)trainee);
            this.InstructorName = trainee.InstructorName;
            this.NumDrivingLessons = trainee.NumDrivingLessons;
            this.GearBox = trainee.GearBox;
            this.DrivingSchool = trainee.DrivingSchool;
        }
    }
}
