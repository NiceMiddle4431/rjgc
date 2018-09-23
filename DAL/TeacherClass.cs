using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TeacherClass
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private SqlConnection SQLSeverOpen()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = "server=212.64.18.220;uid=rjgc;pwd=rjgc;database=rjgc";
            co.Open();
            return co;
        }
        /// <summary>
        /// 获取老师所教的所有课程
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Model.Teacher> GetList(int teacherId, int pageSize, int pageNumber)
        {
            List<Model.Teacher> list = new List<Model.Teacher>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from V_Teacher_Course_Class_School where Id = " + teacherId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Teacher teacher = new Model.Teacher();
                Model.Course course = new Model.Course();
                Model.Class tempClass = new Model.Class();
                Model.School school = new Model.School();

                teacher.Id = Convert.ToInt32(reader["Id"]);
                teacher.TeacherName = Convert.ToString(reader["TeacherName"]);
                teacher.TeacherWorkNum = Convert.ToString(reader["TeacherWorkNum"]);
                teacher.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                teacher.IsLeader = Convert.ToInt32(reader["IsLeader"]);
                teacher.Sex = Convert.ToInt32(reader["Sex"]);
                teacher.CourseId = Convert.ToInt32(reader["CourseId"]);
                course.Id = Convert.ToInt32(reader["CourseId"]);
                course.CourseName = Convert.ToString(reader["CourseName"]);
                try
                {
                    tempClass.Id = Convert.ToInt32(reader["ClassId"]);
                    tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                    tempClass.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                }
                catch (Exception)
                {
                    continue;
                }
                school.Id = Convert.ToInt32(reader["SchoolId"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                teacher.School = school;
                teacher.Course = course;
                teacher.TempClass = tempClass;
                list.Add(teacher);
            }
            return list;
        }

        /// <summary>
        /// 获取数据库里有教课的教师所教班级总数
        /// </summary>
        /// <returns></returns>
        public int GetCount(int TeacherId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select count(1) from V_Teacher_Course_Class_School where id = " + TeacherId;
            int result = (int)cmd.ExecuteScalar();
            co.Close();
            return result;
        }
        /// <summary>
        ///  获取教师所在学校所有班级
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <returns></returns>
        public List<Model.Class> GetClass(int TeacherId)
        {
            List<Model.Class> list = new List<Model.Class>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select SchoolId from T_Base_Teacher where Id = " + TeacherId;
            SqlDataReader reader = cmd.ExecuteReader();
            int schoolId = -1;
            while (reader.Read())
            {
                schoolId = Convert.ToInt32(reader["SchoolId"]);
            }
            co.Close();
            co = SQLSeverOpen();
            cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_Class where SchoolId = " + schoolId;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Class tempClass = new Model.Class();
                tempClass.Id = Convert.ToInt32(reader["Id"]);
                tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                tempClass.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                list.Add(tempClass);
            }
            co.Close();
            return list;
        }
        /// <summary>
        /// 保存教师所教班级
        /// </summary>
        /// <param name="AddTeacherId"></param>
        /// <param name="AddClassId"></param>
        /// <returns></returns>
        public int AddSave(int AddTeacherId, int AddClassId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "insert Class_Teacher values(" + AddClassId + "," + AddTeacherId + ")";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 删除老师所教的班级
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int Delete(int DeleteTeacherId, string DeleteClassIds)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "delete from Class_Teacher where " +
                "ClassId in (" + DeleteClassIds + ") and TeacherId = " + DeleteTeacherId;
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
