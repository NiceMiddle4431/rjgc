using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class QuestionnaireHead
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
        /// 获取数据库中所有问卷信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>    
        public List<Model.QuestionnaireHead> GetList(int CurrentPage, int PageSize, int TeacherId, string keyWord)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            keyWord = "'%" + keyWord + "%'";
            cm.CommandText = "select top " + PageSize + " * from [V_Write_Questionnaire] where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from [V_Write_Questionnaire]  where QuestionnaireName like " + keyWord + " and TeacherId=" + TeacherId + ")   and  QuestionnaireName like " + keyWord + "and TeacherId=" + TeacherId;
            List<Model.QuestionnaireHead> lst = new List<Model.QuestionnaireHead>();

            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.QuestionnaireHead head = new Model.QuestionnaireHead();
                head.Id = Convert.ToInt32(dr["QuestionnaireHeadId"]);
                head.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                head.ClassId = Convert.ToInt32(dr["ClassId"]);
                head.QuestionnaireName = Convert.ToString(dr["QuestionnaireName"]);
                head.StartTime = Convert.ToDateTime(dr["StartTime"]);
                head.EndTime = Convert.ToDateTime(dr["EndTime"]);
                head.WritenTimes = GetWritenTimes(head.Id,TeacherId);
                int score = GetTotalScore(head.Id);
                head.TotalScore = score / head.WritenTimes;
                lst.Add(head);
            }
            dr.Close();
            co.Close();
            return lst;
        }
        public int GetCount(int TeacherId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select count(QuestionnaireHeadId) from T_Base_Write where TeacherId=" + TeacherId;
            int result = (int)cmd.ExecuteScalar();
            co.Close();
            return result;
        }

        public Model.QuestionnaireHead GetModel(int HeadId,int TeacherId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            Model.QuestionnaireHead Questionnaire = new Model.QuestionnaireHead();
            Questionnaire.Item = new List<Model.SelectQuestion>();
            cmd.CommandText = "select * from [V_SelectQuestion_Question] where QuestionnaireHeadId=@QuestionnaireHeadId";
            cmd.Parameters.AddWithValue("@QuestionnaireHeadId", HeadId);
            cmd.Connection = co;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Model.SelectQuestion item = new Model.SelectQuestion();
                item.QuestionHeadId = HeadId;
                item.QuestionId = Convert.ToInt32(dr["QuestionId"]);
                Model.Question question = new Model.Question();
                question.Id = item.QuestionId;
                question.Content = Convert.ToString(dr["content"]);
                item.Question = question;
                Questionnaire.Item.Add(item);
                Questionnaire.ScoreList = GetQuestionScore(HeadId,TeacherId);
            }
            dr.Close();
            co.Close();
            return Questionnaire;

        }
        public int GetWritenTimes(int HeadId, int TeacherId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select count(1) from [V_Write_Questionnaire] where TeacherId=" + TeacherId + "and QuestionnaireHeadId=" + HeadId;
            int result = (int)cmd.ExecuteScalar();
            return result;
        }
        //public int GetWritenTimes(int HeadId)
        //{
        //    SqlConnection co = SQLSeverOpen();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = co;
        //    cmd.CommandText = "select count(1) from T_Base_Write where QuestionnaireHeadId=" + HeadId;
        //    int result = (int)cmd.ExecuteScalar();
        //    return result;
        //}
        public int GetTotalScore(int HeadId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select SUM(TotalScore) from [V_Write_Questionnaire] where QuestionnaireHeadId=" + HeadId;
            int result = (int)cmd.ExecuteScalar();
            return result;
        }
        public List<int> GetQuestionScore(int HeadId,int TeacherId)
        {
            SqlConnection co = SQLSeverOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select SUM(Q1) as Q1, SUM(Q2) as Q2, SUM(Q3) as Q3, SUM(Q4) as Q4, SUM(Q5) as Q5, SUM(Q6) as Q6, SUM(Q7) as Q7, SUM(Q8) as Q8, SUM(Q9) as Q9, SUM(Q10) as Q10 from[V_Write_Questionnaire] where QuestionnaireHeadId=" + HeadId+"and TeacherId="+TeacherId;
            SqlDataReader dr = cmd.ExecuteReader();
            List<int> scoreList = new List<int>();
            while (dr.Read())
            {
                int score1 = Convert.ToInt32(dr["Q1"]);
                int score2 = Convert.ToInt32(dr["Q2"]);
                int score3 = Convert.ToInt32(dr["Q3"]);
                int score4 = Convert.ToInt32(dr["Q4"]);
                int score5 = Convert.ToInt32(dr["Q5"]);
                int score6 = Convert.ToInt32(dr["Q6"]);
                int score7 = Convert.ToInt32(dr["Q7"]);
                int score8 = Convert.ToInt32(dr["Q8"]);
                int score9 = Convert.ToInt32(dr["Q9"]);
                int score10 = Convert.ToInt32(dr["Q10"]);
                scoreList.Add(score1);
                scoreList.Add(score2);
                scoreList.Add(score3);
                scoreList.Add(score4);
                scoreList.Add(score5);
                scoreList.Add(score6);
                scoreList.Add(score7);
                scoreList.Add(score8);
                scoreList.Add(score9);
                scoreList.Add(score10);
            }

            return scoreList;
        }

    }
}
