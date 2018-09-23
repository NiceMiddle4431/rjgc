using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class School
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
        /// 获取数据库中所有学校基础信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<Model.School> GetList(int pageSize, int pageNumber,string SchoolName,string Address)
        {
            SchoolName = "'%" + SchoolName + "%'";
            Address = "'%" + Address + "%'";
            List<Model.School> list = new List<Model.School>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select top " + pageSize + " * from T_Base_School where Id not in (select top " + (pageNumber - 1) * pageSize + " Id from T_Base_School where SchoolName like " + SchoolName + " and Address like " + Address + ") and SchoolName like " + SchoolName + " and Address like " + Address ;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.School school = new Model.School();
                school.Id = Convert.ToInt32(reader["Id"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                school.Address = Convert.ToString(reader["Address"]);
                list.Add(school);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 保存修改后的班级信息
        /// </summary>
        /// <param name="editClassName"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public int EditClassSave(int ClassId,string EditClassName)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "update T_Base_Class set ClassName ='"+EditClassName+"' where Id = "+ClassId;
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
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
            cmd.CommandText = "select * from V_Class_School where SchoolId = " + SchoolId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.Class tempClass = new Model.Class();
                Model.School school = new Model.School();
                tempClass.Id = Convert.ToInt32(reader["Id"]);
                tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                tempClass.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                school.Id = Convert.ToInt32(reader["SchoolId"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                school.SchoolNum = Convert.ToString(reader["SchoolNum"]);
                tempClass.School = school;
                list.Add(tempClass);
            }
            reader.Close();
            co.Close();
            return list;
        }
        /// <summary>
        /// 保存添加班级信息
        /// </summary>
        /// <param name="AddClassName"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public int AddClassSave(string AddClassName,int SchoolId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "insert into T_Base_Class values('" + AddClassName + "'," +SchoolId + ")";
            int result = cmd.ExecuteNonQuery();
            return result;
        }



        /// <summary>
        /// 获取数据库里学校的总数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select count(1) from T_Base_School";
            int result = (int)cmd.ExecuteScalar();
            co.Close();
            return result;
        }
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int DeleteClass(string Ids)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "delete from T_Base_Class where " +
                "Id in (" + Ids + ")";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 保存添加学校信息
        /// </summary>
        /// <param name="AddSchoolName"></param>
        /// <param name="AddSchoolNum"></param>
        /// <returns></returns>
        public int AddSchoolSave(string AddSchoolName, string AddSchoolNum,string AddAddress)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "insert into T_Base_School values('" + AddSchoolName + "','" + AddSchoolNum + "','"+ AddAddress+"','')";
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        /// <summary>
        /// 保存修改学校信息
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="EditClassName"></param>
        /// <returns></returns>
        public int EditSchoolSave(int SchoolId, string EditSchoolNum, string EditSchoolName, string EditAddress)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "update T_Base_School set SchoolNum ='" + EditSchoolNum + "',SchoolName = '" + EditSchoolName + "',Address = '" + EditAddress + "' where Id = " + SchoolId;
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 删除学校
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int DeleteSchool(string Ids)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            //            select* from T_Base_Student where T_Base_Student.ClassId in 
            //(select Id from T_Base_Class where T_Base_Class.SchoolId = 2)
            //查询要删除的学校下属班级
            cmd.CommandText = "select Id from T_Base_Class where T_Base_Class.SchoolId in (" + Ids + ")";
            SqlDataReader reader = cmd.ExecuteReader();
            List<int> classList = new List<int>();
            while (reader.Read())
            {
                classList.Add(Convert.ToInt32(reader["Id"]));
            }
            string classId = string.Join(",", classList.ToArray());
            co.Close();
            co = SQLSeverOpen();
            cmd = new SqlCommand();
            cmd.Connection = co;
            //删除下属班级下的学生
            try
            {
                cmd.CommandText = "delete from T_Base_Student where ClassId in (" + classId + ")";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

            co.Close();
            //删除班级
            co = SQLSeverOpen();
            cmd = new SqlCommand();
            cmd.Connection = co;
            try
            {
                cmd.CommandText = "delete from T_Base_Class where SchoolId in (" + Ids + ")";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

            co.Close();
            //删除学校
            co = SQLSeverOpen();
            cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "delete from T_Base_School where " +
                "Id in (" + Ids + ")";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
