using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Course
    {
        public List<Model.Course> GetList(int pageSize, int pageNumber)
        {
            DAL.Course dal = new DAL.Course();
            return dal.GetList(pageSize, pageNumber);
        }
        public int GetCount()
        {
            DAL.Course dal = new DAL.Course();
            return dal.GetCount(); ;
        }
        public int EditCourseSave(int CourseId, string EditCourseName)
        {
            DAL.Course dal = new DAL.Course();
            return dal.EditCourseSave(CourseId, EditCourseName);
        }
        public int AddCourseSave(string AddCourseName)
        {
            DAL.Course dal = new DAL.Course();
            return dal.AddCourseSave(AddCourseName);
        }
        public int Delete(string[] Ids)
        {
            //防止注入式漏洞
            string ids = string.Join(",", Ids);
            DAL.Course dal = new DAL.Course();
            return dal.Delete(ids);
        }
        public List<Model.Course> GetCourse()
        {
            DAL.Course dal = new DAL.Course();
            return dal.GetCourse();
        }
    }
}
