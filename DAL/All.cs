using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class All
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
        public List<Model.Score> GetSchoolScore()
        {
            List<Model.Score> list = new List<Model.Score>();
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select SchoolId,SchoolName,SUM(TotalScore) as TotalScore from V_Write_Questionnaire_Class_School group by SchoolId,SchoolName,SchoolName";
            SqlDataReader reader = cmd.ExecuteReader();
            List<Model.Score> tempList = new List<Model.Score>();
            while (reader.Read())
            {
                Model.Score score = new Model.Score();
                Model.School school = new Model.School();
                score.SchoolId = Convert.ToInt32(reader["SchoolId"]);
                score.TotalScore = Convert.ToInt32(reader["TotalScore"]);
                school.Id = Convert.ToInt32(reader["SchoolId"]);
                school.SchoolName = Convert.ToString(reader["SchoolName"]);
                score.School = school;
                tempList.Add(score);
            }
            reader.Close();
            co.Close();
            co = SQLSeverOpen();
            cmd.Connection = co;
            foreach (Model.Score item in tempList)
            {
                cmd.CommandText = "select COUNT(1) from V_Write_Questionnaire_Class_School where SchoolId = " + item.SchoolId;
                int count = (int)cmd.ExecuteScalar();
                item.TotalScore = item.TotalScore / count;
                list.Add(item);
            }

            reader.Close();
            co.Close();
            return list;
        }

        public List<Model.Score> GetSchoolTeacherScore(int SchoolId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            List<Model.Score> list = new List<Model.Score>();
            cmd.CommandText = "select TeacherId,ClassId,ClassName,SUM(TotalScore)as TotalScore"+
                ",TeacherName from V_Write_Questionnaire_Class_School where SchoolId = " +
                 SchoolId+ " group by TeacherId,ClassId,ClassName,TeacherName";
            SqlDataReader reader = cmd.ExecuteReader();
            List<Model.Score> tempList = new List<Model.Score>();
            while (reader.Read())
            {
                Model.Score score = new Model.Score();
                Model.Teacher teacher = new Model.Teacher();
                Model.Class tempClass = new Model.Class();
                score.TeacherId = Convert.ToInt32(reader["TeacherId"]);
                score.ClassId = Convert.ToInt32(reader["ClassId"]);
                score.TotalScore = Convert.ToInt32(reader["TotalScore"]);
                score.TeacherId = Convert.ToInt32(reader["TeacherId"]);
                tempClass.Id = Convert.ToInt32(reader["ClassId"]);
                tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                teacher.Id = Convert.ToInt32(reader["TeacherId"]);
                teacher.TeacherName = Convert.ToString(reader["TeacherName"]);
                score.Teacher = teacher;
                score.Class = tempClass;
                tempList.Add(score);
            }
            reader.Close();
            co.Close();
            co = SQLSeverOpen();
            cmd.Connection = co;
            foreach (Model.Score item in tempList)
            {
                cmd.CommandText = "select COUNT(1) from V_Write_Questionnaire_Class_School where teacherId = " + item.TeacherId;
                int count = (int)cmd.ExecuteScalar();
                item.TotalScore = item.TotalScore / count;
                list.Add(item);
            }

            reader.Close();
            co.Close();
            return list;



        }
        //private int getCount(int TeacherId, int ClassId)
        //{
        //    SqlConnection co = SQLSeverOpen();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = co;
        //    List<Model.Score> list = new List<Model.Score>();
        //    cmd.CommandText = "select COUNT(1) from V_QuestionHead_Select where TeacherId = " + TeacherId + " and ClassId = " + ClassId;

        //    return (int)cmd.ExecuteScalar();
        //}
        public List<Model.Score> GetTeacherClassScore(int TeacherId, int ClassId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            List<Model.Score> list = new List<Model.Score>();
            cmd.CommandText = "select  top 10 * from V_QuestionHead_Select where TeacherId = " + TeacherId + " and ClassId = " + ClassId;
            SqlDataReader reader = cmd.ExecuteReader();
            List<Model.Question> questionList = new List<Model.Question>();
       
            Model.Score score = new Model.Score();
           
            while (reader.Read())
            {
                try
                {
                    Model.Teacher teacher = new Model.Teacher();
                    Model.Class tempClass = new Model.Class();
                    Model.Question question = new Model.Question();
                    score.TeacherId = Convert.ToInt32(reader["TeacherId"]);
                    score.ClassId = Convert.ToInt32(reader["ClassId"]);
                    score.TotalScore = Convert.ToInt32(reader["TotalScore"]);
                    score.TeacherId = Convert.ToInt32(reader["TeacherId"]);
                    List<int> scoreList = GetTotalScore(TeacherId, ClassId);
                    score.QScore = scoreList;
                    question.Content = Convert.ToString(reader["Content"]);
                    questionList.Add(question);
         ;
                    tempClass.Id = Convert.ToInt32(reader["ClassId"]);
                    tempClass.ClassName = Convert.ToString(reader["ClassName"]);
                    teacher.Id = Convert.ToInt32(reader["TeacherId"]);
                    teacher.TeacherName = Convert.ToString(reader["TeacherName"]);
                    score.Teacher = teacher;
                    score.Class = tempClass;

                    score.Question = questionList;
                    list.Add(score);
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
        public List<int>  GetTotalScore(int TeacherId,int ClassId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            List<int> list = new List<int>();
            cmd.CommandText = "select sum(Q1) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q1,sum(Q2) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q2,sum(Q3) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q3,sum(Q4) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q4,sum(Q5) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q5, sum(Q6) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q6,sum(Q7) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q7, sum(Q8) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q8,sum(Q9) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q9, sum(Q10) / (select COUNT(1)  from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId+")as Q10 from V_QuestionHead_Select where TeacherId = "+TeacherId+" and ClassId = "+ClassId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(Convert.ToInt32(reader["Q1"]));
                list.Add(Convert.ToInt32(reader["Q2"]));
                list.Add(Convert.ToInt32(reader["Q3"]));
                list.Add(Convert.ToInt32(reader["Q4"]));
                list.Add(Convert.ToInt32(reader["Q5"]));
                list.Add(Convert.ToInt32(reader["Q6"]));
                list.Add(Convert.ToInt32(reader["Q7"]));
                list.Add(Convert.ToInt32(reader["Q8"]));
                list.Add(Convert.ToInt32(reader["Q9"]));
                list.Add(Convert.ToInt32(reader["Q10"]));
            }
            return list;
        }

    }
}
