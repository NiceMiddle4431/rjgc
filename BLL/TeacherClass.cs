using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TeacherClass
    {
        public List<Model.Teacher> GetList(int teacherId, int pageSize, int pageNumber)
        {
            DAL.TeacherClass dal = new DAL.TeacherClass();
            return dal.GetList(teacherId, pageSize, pageNumber);
        }
        public int GetCount(int TeacherId)
        {
            DAL.TeacherClass dal = new DAL.TeacherClass();
            return dal.GetCount(TeacherId);
        }
        public List<Model.Class> GetClass(int TeacherId)
        {
            DAL.TeacherClass dal = new DAL.TeacherClass();
            return dal.GetClass(TeacherId);
        }
        public int AddSave(int AddTeacherId, int AddClassId)
        {
            DAL.TeacherClass dal = new DAL.TeacherClass();
            return dal.AddSave(AddTeacherId, AddClassId);
        }
        public int Delete(int DeleteTeacherId, string[] DeleteClassIds)
        {
            //防止注入式漏洞
            string ids = string.Join(",", DeleteClassIds);
            DAL.TeacherClass dal = new DAL.TeacherClass();
            return dal.Delete(DeleteTeacherId, ids);
        }
    }
}
