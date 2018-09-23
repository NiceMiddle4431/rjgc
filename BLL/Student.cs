using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Student
    {
        public List<Model.Student> GetList(int pageSize, int pageNumber, string ClassName, string StudentNum, string StudentName)
        {
            DAL.Student dal = new DAL.Student();
            return dal.GetList(pageSize, pageNumber, ClassName, StudentNum, StudentName);
        }
        public Model.Student GetStudent(int Id)
        {
            DAL.Student dal = new DAL.Student();
            return dal.GetStudent(Id);
        }
        public List<Model.Class> GetClass(int SchoolId)
        {
            DAL.Student dal = new DAL.Student();
            return dal.GetClass(SchoolId);
        }
        public List<Model.School> GetSchool()
        {
            DAL.Student dal = new DAL.Student();
            return dal.GetSchool();
        }

        public int EditSave(int EditId, string EditStudentNum, string EditStudentName,
             int EditSex, int EditClass, int EditSchool, string EditStudentPWD)
        {
            DAL.Student dal = new DAL.Student();
            return dal.EditSave(EditId, EditStudentNum, EditStudentName,
                EditSex, EditClass, EditSchool, EditStudentPWD);
        }

        public int AddSave(string AddStudentNum, string AddStudentName,
            int AddSex, int AddClass, string AddSchool, string AddStudentPWD)
        {
            DAL.Student dal = new DAL.Student();
            return dal.AddSave(AddStudentNum, AddStudentName,
                AddSex, AddClass, AddSchool, AddStudentPWD);
        }
        public int GetCount()
        {
            DAL.Student dal = new DAL.Student();
            return dal.GetCount();
        }
        public int Delete(string[] Ids)
        {
            //防止注入式漏洞
            string ids = string.Join(",", Ids);
            DAL.Student dal = new DAL.Student();
            return dal.Delete(ids);
        }
        public int AddExcel(List<Model.Student> list)
        {
            DAL.Student dal = new DAL.Student();
            return dal.AddExcel(list);
        }
    }
}
