using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Teacher
    {
        public List<Model.Teacher> GetList(int pageSize, int pageNumber, string TeacherWorkNum, string TeacherName, string CourseName)
        {
            DAL.Teacher dal = new DAL.Teacher();
            return dal.GetList(pageSize, pageNumber,TeacherWorkNum,TeacherName,CourseName);
        }
        public int GetCount()
        {
            DAL.Teacher dal = new DAL.Teacher();
            return dal.GetCount();
        }
        public int AddSave(string AddTeacherName, string AddTeacherWorkNum,
            int AddSex, int AddIsLeader, int AddSchool, int AddCourse, string AddTeacherPWD)
        {
            DAL.Teacher dal = new DAL.Teacher();
            return dal.AddSave(AddTeacherName, AddTeacherWorkNum, AddSex, AddIsLeader, AddSchool, AddCourse, AddTeacherPWD);
        }
        public Model.Teacher GetTeacher(int Id)
        {
            DAL.Teacher dal = new DAL.Teacher();
            return dal.GetTeacher(Id);
        }
    
        public int EditSave(int EditId, string EditTeacherName, string EditTeacherWorkNum,
            int EditSex, int EditIsLeader, int EditSchool, int EditCourse, string EditTeacherPWD)
        {
            DAL.Teacher dal = new DAL.Teacher();
            return dal.EditSave(EditId, EditTeacherName, EditTeacherWorkNum, EditSex, EditIsLeader, EditSchool, EditCourse, EditTeacherPWD);
        }
        public int Delete(string[] Ids)
        {
            //防止注入式漏洞
            string ids = string.Join(",", Ids);
            DAL.Teacher dal = new DAL.Teacher();
            return dal.Delete(ids);
        }
    }
}
