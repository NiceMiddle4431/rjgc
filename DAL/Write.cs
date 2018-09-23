using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Write
    {
        string url = "server=212.64.18.220;uid=rjgc;pwd=rjgc;database=rjgc";
        //获取该学生老师名字列表
        public List<Model.Teacher> getTeacherName(int StudentId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            cm.CommandText =
                "select Id,TeacherName from T_Base_Teacher where id in (select TeacherId from Class_Teacher where classId in(select classId from t_base_Student where Id=" +
                StudentId + "))";
            List<Model.Teacher> lst = new List<Model.Teacher>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.Teacher teacher = new Model.Teacher();
                teacher.Id = Convert.ToInt32(dr["Id"]);
                teacher.TeacherName = Convert.ToString(dr["TeacherName"]);

                lst.Add(teacher);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        //获取该学生所在班级问卷列表
        public List<Model.QuestionnaireHead> getNaireList(int StudentId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            cm.CommandText =
                "select * from T_Base_QuestionnaireHead where ClassId in (select ClassId from T_Base_Student where Id=" +
                StudentId + ")";
            List<Model.QuestionnaireHead> lst = new List<Model.QuestionnaireHead>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.QuestionnaireHead questionnaire = new Model.QuestionnaireHead();
                questionnaire.Id = Convert.ToInt32(dr["Id"]);
                questionnaire.QuestionnaireName = Convert.ToString(dr["QuestionnaireName"]);
                questionnaire.StartTime = Convert.ToDateTime(dr["StartTime"]);
                questionnaire.EndTime = Convert.ToDateTime(dr["EndTime"]);
                questionnaire.ClassId = Convert.ToInt32(dr["ClassId"]);
                questionnaire.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                lst.Add(questionnaire);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        //获取该问卷的表头
        public List<Model.QuestionnaireHead> getNaireHead(int HeadId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            cm.CommandText =
                "select * from T_Base_QuestionnaireHead where Id=" +
                HeadId;
            List<Model.QuestionnaireHead> lst = new List<Model.QuestionnaireHead>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.QuestionnaireHead questionnaire = new Model.QuestionnaireHead();
                questionnaire.Id = Convert.ToInt32(dr["Id"]);
                questionnaire.QuestionnaireName = Convert.ToString(dr["QuestionnaireName"]);
                questionnaire.StartTime = Convert.ToDateTime(dr["StartTime"]);
                questionnaire.EndTime = Convert.ToDateTime(dr["EndTime"]);
                questionnaire.ClassId = Convert.ToInt32(dr["ClassId"]);
                questionnaire.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                lst.Add(questionnaire);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        //classId转ClassName
        public string getClassName(int ClassId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            cm.CommandText = "select ClassName from T_Base_Class where Id=" + ClassId;
            string result = cm.ExecuteScalar().ToString();
            co.Close();
            return result;

        }


        //SchoolId转SchoolName
        public string getSchoolName(int SchoolId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            cm.CommandText = "select SchoolName from T_Base_School where Id=" + SchoolId;
            string result = cm.ExecuteScalar().ToString();
            co.Close();
            return result;

        }

        //TeacherId转TeacherName
        public string toTeacherName(int TeacherId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;

            cm.CommandText = "select TeacherName from T_Base_Teacher where Id=" + TeacherId;
            string result = cm.ExecuteScalar().ToString();
            co.Close();
            return result;

        }
        //保存填入的数据
        public int saveWriteResult(int StudentId, int QuestionnaireHeadId, int TeacherId, int Q1, int Q2, int Q3, int Q4, int Q5, int Q6, int Q7, int Q8, int Q9, int Q10)
        {

            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "insert into T_Base_Write(StudentId,QuestionnaireHeadId,TeacherId,Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10) values ('" + StudentId + "','" +
                 QuestionnaireHeadId + "','" + TeacherId + "'," + Q1 + "," + Q2 + "," + Q3 + "," + Q4 + "," + Q5 + "," + Q6 + "," + Q7 + "," + Q8 + "," + Q9 + "," + Q10 + ")";
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        //是否已经填写
        public int CompletedNaire(int TeacherId, int StudentId, int QuestionnaireHeadId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from t_base_write where StudentId=" + StudentId + " and QuestionnaireHeadId=" + QuestionnaireHeadId + " and TeacherId=" + TeacherId;
            object flag = cm.ExecuteScalar();
            if (flag == null)
            {
                co.Close();
                return 0;
            }
            else
            {
                co.Close();
                int result = (int)flag;
                return result;
            }



        }
    }
}