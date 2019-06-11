using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    internal class MyDal : IDal
    {
        public bool addTrainee(Trainee tr)
        {
            foreach (var item in DataSource.Trainees)
            {
                if(item.ID == tr.ID)
                    return false;
            }
            DataSource.Trainees.Add(tr.Clone());
            return true;
        }

        public List<Trainee> getAllTrainees()
        {
            List<Trainee> result = new List<Trainee>();
            foreach (var item in DataSource.Trainees)
            {
                result.Add(item.Clone());
            }
            return result;
        }

        public bool removeTrainee(Trainee tr)
        {
            foreach (var item in DataSource.Trainees)
            {
                if (item.ID == tr.ID)
                {
                    return DataSource.Trainees.Remove(item);
                    //return true;
                }
            }
           return false;
        }
    }
}
