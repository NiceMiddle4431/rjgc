using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Teacher
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
        /// 获取数据库中教师班级学校等基础信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Model.Teacher> GetList(int pageSize, int pageNumber, string TeacherWorkNum, string TeacherName, string CourseName)
        {
            TeacherWorkNum = "'%" + TeacherWorkNum + "%'";
            TeacherName = "'%" + TeacherName + "%'";
            CourseName = "'%" + CourseName + "%'";
            List<Model.Teacher> list = new List<Model.Teacher>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select top " + pageSize + " * from V_Teacher_Course_School where Id not in (select top " + (pageNumber - 1) * pageSize + " Id from V_Teacher_Course_School where TeacherWorkNum like " + TeacherWorkNum + " and TeacherName like " + TeacherName + " and CourseName like " + CourseName + ") and TeacherWorkNum like " + TeacherWorkNum + " and TeacherName like " + TeacherName + " and CourseName like " + CourseName;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    Model.Teacher teacher = new Model.Teacher();
                    Model.Course course = new Model.Course();
                    Model.School school = new Model.School();
                    teacher.Id = Convert.ToInt32(reader["Id"]);
                    teacher.TeacherName = Convert.ToString(reader["TeacherName"]);
                    teacher.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                    teacher.Sex = Convert.ToInt32(reader["Sex"]);
                    teacher.IsLeader = Convert.ToInt32(reader["IsLeader"]);
                    teacher.TeacherWorkNum = Convert.ToString(reader["TeacherWorkNum"]);
                    teacher.TeacherPWD = Convert.ToString(reader["TeacherPWD"]);
                    course.Id = Convert.ToInt32(reader["CourseId"]);
                    course.CourseName = Convert.ToString(reader["CourseName"]);
                    school.Id = Convert.ToInt32(reader["SchoolId"]);
                    school.SchoolName = Convert.ToString(reader["SchoolName"]);
                    school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                    teacher.Course = course;
                    teacher.School = school;
                    list.Add(teacher);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            reader.Close();
            co.Close();
            return list;
        }

        /// <summary>
        /// 获取数据库里教师的总数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select count(1) from T_Base_Teacher";
            int result = (int)cmd.ExecuteScalar();
            co.Close();
            return result;
        }
        /// <summary>
        /// 保存添加教师信息
        /// </summary>
        /// <param name="AddTeacherName"></param>
        /// <param name="AddTeacherWorkNum"></param>
        /// <param name="AddSex"></param>
        /// <param name="AddIsLeader"></param>
        /// <param name="AddSchool"></param>
        /// <param name="AddCourse"></param>
        /// <param name="AddTeacherPWD"></param>
        /// <returns></returns>
        public int AddSave(string AddTeacherName, string AddTeacherWorkNum,
            int AddSex, int AddIsLeader, int AddSchool, int AddCourse, string AddTeacherPWD)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "insert T_Base_Teacher values('" + AddTeacherName + "','" +
                AddTeacherWorkNum + "','" + AddTeacherPWD + "'," + AddSchool + "," + AddIsLeader + "," +
                AddSex + "," + AddCourse + ",2)";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 获取编辑时单个教师的信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Model.Teacher GetTeacher(int Id)
        {
            Model.Teacher teacher = new Model.Teacher();
            Model.Course course = new Model.Course();
            Model.School school = new Model.School();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from V_Teacher_Course_School where Id = " + Id;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                teacher.Id = Convert.ToInt32(reader["Id"]);
                teacher.TeacherName = Convert.ToString(reader["TeacherName"]);
                teacher.TeacherWorkNum = Convert.ToString(reader["TeacherWorkNum"]);
                teacher.TeacherPWD = Convert.ToString(reader["TeacherPWD"]);
                teacher.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                school.Id = Convert.ToInt32(reader["SchoolId"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                teacher.IsLeader = Convert.ToInt32(reader["IsLeader"]);
                teacher.Sex = Convert.ToInt32(reader["Sex"]);
                teacher.CourseId = Convert.ToInt32(reader["CourseId"]);
                course.Id = Convert.ToInt32(reader["CourseId"]);
                course.CourseName = Convert.ToString(reader["CourseName"]);
                teacher.Course = course;
                teacher.School = school;
            }
            co.Close();
            return teacher;
        }
        /// <summary>
        /// 保存修改后的教师信息
        /// </summary>
        /// <param name="EditId"></param>
        /// <param name="EditTeacherName"></param>
        /// <param name="EditTeacherWorkNum"></param>
        /// <param name="EditSex"></param>
        /// <param name="EditIsLeader"></param>
        /// <param name="EditSchool"></param>
        /// <param name="EditCourse"></param>
        /// <param name="EditTeacherPWD"></param>
        /// <returns></returns>
        public int EditSave(int EditId, string EditTeacherName, string EditTeacherWorkNum,
           int EditSex, int EditIsLeader, int EditSchool, int EditCourse, string EditTeacherPWD)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "update T_Base_Teacher set TeacherName = '" + EditTeacherName +
                "',TeacherWorkNum = '" + EditTeacherWorkNum + "',TeacherPWD = '" + EditTeacherPWD +
                "',SchoolId = " + EditSchool + ",IsLeader = " + EditIsLeader + ",Sex = " + EditSex +
                ",CourseId = " + EditCourse + " where Id = " + EditId;
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "delete from T_Base_Teacher where " +
                "Id in (" + Ids + ")";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }






    }
}
