using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Course
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
        /// 获取数据库中所有课程信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Model.Course> GetList(int pageSize, int pageNumber)
        {
            List<Model.Course> list = new List<Model.Course>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select top " + pageSize + " * from T_Base_Course where Id not in " +
                "(select top " + (pageNumber - 1) * pageSize + " Id from T_Base_Course)";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Course course = new Model.Course();
                course.Id = Convert.ToInt32(reader["Id"]);
                course.CourseName = Convert.ToString(reader["CourseName"]);
                list.Add(course);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 获取数据库里课程的总数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select count(1) from T_Base_Course";
            int result = (int)cmd.ExecuteScalar();
            co.Close();
            return result;
        }
        /// <summary>
        /// 保存修改后的课程信息
        /// </summary>
        /// <param name="EditCourseName"></param>
        /// <param name="EditCourseName"></param>
        /// <returns></returns>
        public int EditCourseSave(int CourseId, string EditCourseName)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "update T_Base_Course set CourseName ='" + EditCourseName + "' where Id = " + CourseId;
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 保存添加后的课程信息
        /// </summary>
        /// <param name="AddCourseName"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public int AddCourseSave(string AddCourseName)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "insert into T_Base_Course values('" + AddCourseName + "')";
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "delete from T_Base_Course where " +
                "Id in (" + Ids + ")";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 获取全部课程
        /// </summary>
        /// <returns></returns>
        public List<Model.Course> GetCourse()
        {
            List<Model.Course> list = new List<Model.Course>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_Course";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Course course = new Model.Course();
                course.Id = Convert.ToInt32(reader["Id"]);
                course.CourseName = Convert.ToString(reader["CourseName"]);
                list.Add(course);
            }
            co.Close();
            return list;
        }
    }
}
