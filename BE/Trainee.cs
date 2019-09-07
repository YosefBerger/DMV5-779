using System;

namespace BE
{
    [Serializable]
    public class Trainee : Person
    {

        private String _InstrucotrName;  // name of instructor wth whom the trainee studied
        public GearBox GearBox { get; set; }  // specify gearbox that the trainee learned, must be set
        private String _DrivingSchool;  // driving school that the trainee attended
        private int _NumDrivingLessons; // number of lessons

        public Trainee()
        {
            VehicleType = VehicleType.PRIVATE;
            GearBox = GearBox.AUTOMATIC;
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
    }
}
