using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DAL
{
    public class Student
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
        /// 获取数据库中学生班级学校等基础信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Model.Student> GetList(int pageSize, int pageNumber, string ClassName, string StudentNum, string StudentName)
        {
            ClassName = "'%" + ClassName + "%'";
            StudentNum = "'%" + StudentNum + "%'";
            StudentName = "'%" + StudentName + "%'";
            List<Model.Student> list = new List<Model.Student>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select top " + pageSize + " * from [V_Student_Class_School] where Id not in (select top " + (pageNumber - 1) * pageSize + " Id from [V_Student_Class_School] where ClassName like " + ClassName + " and StudentNum like " + StudentNum + " and StudentName like " + StudentName + ")and ClassName like " + ClassName + " and StudentNum like " + StudentNum + " and StudentName like " + StudentName;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Student student = new Model.Student();
                Model.Class tempClass = new Model.Class();
                Model.School school = new Model.School();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.StudentName = Convert.ToString(reader["StudentName"]);
                student.Sex = Convert.ToInt32(reader["Sex"]);
                student.ClassId = Convert.ToInt32(reader["ClassId"]);
                student.StudentNum = Convert.ToString(reader["StudentNum"]);
                student.StudentPWD = Convert.ToString(reader["StudentPWD"]);
                tempClass.Id = Convert.ToInt32(reader["ClassId"]);
                tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                tempClass.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                school.Id = Convert.ToInt32(reader["SchoolId"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                student.Class = tempClass;
                student.School = school;
                list.Add(student);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 获取单个学生信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Model.Student GetStudent(int Id)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            Model.Student student = new Model.Student();
            cmd.CommandText = "select * from V_Student_Class_School where Id = " + Id;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Class tempClass = new Model.Class();
                Model.School school = new Model.School();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.StudentName = Convert.ToString(reader["StudentName"]);
                student.ClassId = Convert.ToInt32(reader["ClassId"]);
                student.StudentNum = Convert.ToString(reader["StudentNum"]);
                student.StudentPWD = Convert.ToString(reader["StudentPWD"]);
                student.Sex = Convert.ToInt32(reader["Sex"]);
                tempClass.Id = Convert.ToInt32(reader["ClassId"]);
                tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                tempClass.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                school.Id = Convert.ToInt32(reader["SchoolId"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                student.Class = tempClass;
                student.School = school;
            }
            reader.Close();
            co.Close();
            return student;
        }
        /// <summary>
        /// 根据学校不同获取班级信息
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public List<Model.Class> GetClass(int SchoolId)
        {
            List<Model.Class> list = new List<Model.Class>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_Class where SchoolId = " + SchoolId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Class tempClass = new Model.Class();
                tempClass.Id = Convert.ToInt32(reader["Id"]);
                tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                tempClass.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                list.Add(tempClass);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 获取学校信息
        /// </summary>
        /// <returns></returns>
        public List<Model.School> GetSchool()
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            List<Model.School> list = new List<Model.School>();
            cmd.CommandText = "select * from T_Base_School";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.School school = new Model.School();
                school.Id = Convert.ToInt32(reader["Id"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                list.Add(school);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 保存修改后的学生信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int EditSave(int EditId, string EditStudentNum, string EditStudentName,
           int EditSex, int EditClass, int EditSchool, string EditStudentPWD)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "update T_Base_Student set ClassId = " + EditClass + ",StudentNum = '" + EditStudentNum +
                "',StudentName = '" + EditStudentName + "',Sex = " + EditSex + ",StudentPwd = '" + EditStudentPWD +
                "' where Id = " + EditId;
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 保存添加的学生信息
        /// </summary>
        /// <param name="AddStudentNum"></param>
        /// <param name="AddStudentName"></param>
        /// <param name="AddSex"></param>
        /// <param name="AddClass"></param>
        /// <param name="AddSchool"></param>
        /// <param name="AddStudentPWD"></param>
        /// <returns></returns>
        public int AddSave(string AddStudentNum, string AddStudentName, int AddSex,
            int AddClass, string AddSchool, string AddStudentPWD)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "insert into T_Base_Student values ('" + AddStudentName + "'," +
                 AddClass + ",'" + AddStudentNum + "','" + AddStudentPWD + "'," + AddSex + ",4)";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;

        }
        /// <summary>
        /// 获取数据库里学生的总数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select count(1) from T_Base_Student";
            int result = (int)cmd.ExecuteScalar();
            co.Close();
            return result;
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "delete from T_Base_Student where " +
                "Id in (" + Ids + ")";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int AddExcel(List<Model.Student> list)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
        
            int schoolId = 0;
            string schoolName = "";
            string schoolNum = "";
            string address = "";
            int classId = 0;
            string className = "";
            int schoolCount = 0;
            int classCount = 0;
            foreach (Model.Student item in list)
            {
                className = item.Class.ClassName;
                schoolNum = item.School.SchoolNum;
                schoolName = item.School.SchoolName;
                address = item.School.Address;
            }
            //查询学生所在学校是否存在，不存在添加
            cmd.CommandText = "select count(Id) from T_Base_School where SchoolName = '" + schoolName + "'";
            schoolCount = (int)cmd.ExecuteScalar();

            if(schoolCount == 0)
            {
                co.Close();
                co = SQLSeverOpen();
                cmd.Connection = co;
                cmd.CommandText = "insert into T_Base_School values('"+schoolName+"','"+
                    schoolNum+"','"+address+"')";
                cmd.ExecuteNonQuery();    
            }
            ///查学校id
            co.Close();
            co = SQLSeverOpen();
            cmd.Connection = co;
            cmd.CommandText = "select Id from T_Base_School where SchoolName = '" + schoolName + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                schoolId = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();

            //查看班级是否存在
            co.Close();
            co = SQLSeverOpen();
            cmd.Connection = co;
            cmd.CommandText = "select count(Id) from T_Base_Class where ClassName = '" + className + "' and SchoolId = "+schoolId;
            classCount = (int)cmd.ExecuteScalar();
            
            //查询学生所在班级是否存在，不存在添加
            if (classCount == 0)
            {
                co.Close();
                co = SQLSeverOpen();
                cmd.Connection = co;
                cmd.CommandText = "insert into T_Base_Class values('" + className + "',"+schoolId+")";
                cmd.ExecuteNonQuery();
            }
            //读取班级id
            co.Close();
            co = SQLSeverOpen();
            cmd.Connection = co;
            cmd.CommandText = "select Id from T_Base_Class where ClassName = '" + className + "'";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                classId = Convert.ToInt32(reader["Id"]);
            }
            reader.Close();
            //插入学生
            co.Close();
            co = SQLSeverOpen();
            cmd.Connection = co;
            string cmdtext = "insert into T_Base_Student values";
            foreach (Model.Student student in list)
            {
                cmdtext += "('" + student.StudentName + "'," + classId + ",'" +
                    student.StudentNum + "','" + student.StudentPWD + "'," +
                    student.Sex + ",4),";
            }
            cmdtext = cmdtext.Substring(0, cmdtext.Length - 1);
            cmd.CommandText = cmdtext;
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
