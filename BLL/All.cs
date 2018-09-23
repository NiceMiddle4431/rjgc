using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class All
    {
        public List<Model.Score> GetSchoolScore()
        {
            DAL.All dal = new DAL.All();
            return dal.GetSchoolScore();
        }
        public List<Model.Score> GetSchoolTeacherScore(int SchoolId)
        {
            DAL.All dal = new DAL.All();
            return dal.GetSchoolTeacherScore(SchoolId);
        }
        public List<Model.Score> GetTeacherClassScore(int TeacherId,int ClassId)
        {
            DAL.All dal = new DAL.All();
            return dal.GetTeacherClassScore(TeacherId, ClassId);
        }
    }
}
