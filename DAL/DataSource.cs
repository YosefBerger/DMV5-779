using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DataSource
    {
        private static List<Trainee> trainees = new List<Trainee>();

        public static List<Trainee> Trainees
        {
            get { return trainees; }
        }

    }
}
