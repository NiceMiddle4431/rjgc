using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Home
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
        /// 获取数据库中该角色全部的权限菜单
        /// </summary>
        /// <returns></returns>
        public List<Model.Menu> GetList(int RoleId)
        {
            List<Model.Menu> list = new List<Model.Menu>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from V_Role_Menu where roleId = " + RoleId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Menu menu = new Model.Menu();
                menu.Id = Convert.ToInt32(reader["Id"]);
                menu.Controller = Convert.ToString(reader["Controller"]);
                menu.Action = Convert.ToString(reader["Action"]);
                menu.Display = Convert.ToString(reader["Display"]);
                menu.Type = Convert.ToInt32(reader["Type"]);
                menu.ParentId = Convert.ToInt32(reader["ParentId"]);

                list.Add(menu);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 检查学生登录信息
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Model.Student CheckStudent(int RoleId, int SchoolId, string LoginName, string Password)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from V_Student_Class_School where SchoolId = " + SchoolId +
                " and StudentNum = '" + LoginName + "' and StudentPWD = '" + Password + "' and RoleId = " + RoleId;
            SqlDataReader reader = cmd.ExecuteReader();
            int result = 0;
            Model.Student student = new Model.Student();
            int roleId = -1;
            while (reader.Read())
            {
                student.Id = Convert.ToInt32(reader["Id"]);
                student.StudentName = Convert.ToString(reader["StudentName"]);
                student.ClassId = Convert.ToInt32(reader["ClassId"]);
                student.StudentNum = Convert.ToString(reader["StudentNum"]);
                student.StudentPWD = Convert.ToString(reader["StudentPWD"]);
                student.Sex = Convert.ToInt32(reader["Sex"]);
                student.RoleId = Convert.ToInt32(reader["RoleId"]);
                result++;
                roleId = Convert.ToInt32(reader["RoleId"]);
            }
            reader.Close();
            co.Close();
            if (result == 1)
            {
                return student;
            }
            else
            {
                student.RoleId = -1;
                return student;
            }
        }
        /// <summary>
        /// 检查教师登录信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Model.Teacher CheckTeacher(int RoleId, int SchoolId, string LoginName, string Password)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_Teacher where SchoolId = " + SchoolId +
                " and TeacherWorkNum = '" + LoginName + "' and TeacherPWD = '" + Password + "' and RoleId = " + RoleId;
            SqlDataReader reader = cmd.ExecuteReader();
            int result = 0;
            Model.Teacher teacher = new Model.Teacher();
            Model.User user = new Model.User();
            int roleId = -1;
            while (reader.Read())
            {
                result++;
                teacher.Id = Convert.ToInt32(reader["Id"]);
                teacher.TeacherName = Convert.ToString(reader["TeacherName"]);
                teacher.TeacherWorkNum = Convert.ToString(reader["TeacherWorkNum"]);
                teacher.TeacherPWD = Convert.ToString(reader["TeacherPWD"]);
                teacher.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                teacher.IsLeader = Convert.ToInt32(reader["IsLeader"]);
                teacher.Sex = Convert.ToInt32(reader["Sex"]);
                teacher.CourseId = Convert.ToInt32(reader["CourseId"]);
                teacher.RoleId = Convert.ToInt32(reader["RoleId"]);
                roleId = Convert.ToInt32(reader["RoleId"]);
            }
            reader.Close();
            co.Close();
            if (result == 1)
            {
                return teacher;
            }
            else
            {
                teacher.RoleId = -1;
                return teacher;
            }

        }
        /// <summary>
        /// 检查管理员登录信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="SchoolId"></param>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Model.User CheckAdmin(int RoleId, string LoginName, string Password)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_User where " +
                "UserName = '" + LoginName + "' and UserPWD = '" + Password + "' and RoleId = " + RoleId;
            SqlDataReader reader = cmd.ExecuteReader();
            int result = 0;
            Model.User user = new Model.User();
            int roleId = -1;
            while (reader.Read())
            {
                result++;
                user.Id = Convert.ToInt32(reader["Id"]);
                user.UserName = Convert.ToString(reader["UserName"]);
                user.UserPWD = Convert.ToString(reader["UserPWD"]);
                user.RoleId = Convert.ToInt32(reader["RoleId"]);
                roleId = Convert.ToInt32(reader["RoleId"]);
            }
            co.Close();
            if (result == 1)
            {
                return user;
            }
            else
            {
                user.RoleId = -1;
                return user;
            }

        }
        /// <summary>
        /// 获取全部的角色
        /// </summary>
        /// <returns></returns>
        public List<Model.User> GetRole()
        {
            List<Model.User> list = new List<Model.User>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_Role";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.User user = new Model.User();
                user.RoleId = Convert.ToInt32(reader["Id"]);
                user.RoleName = Convert.ToString(reader["RoleName"]);
                user.Memo = Convert.ToString(reader["Memo"]);
                list.Add(user);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 获取单个管理员信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Model.User GetUser(int Id)
        {
            Model.User user = new Model.User();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_User where Id = " + Id;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Id = Convert.ToInt32(reader["Id"]);
                user.UserName = Convert.ToString(reader["UserName"]);
                user.UserPWD = Convert.ToString(reader["UserPWD"]);
            }
            reader.Close();
            co.Close();
            return user;
        }
    }
}
