using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Question
    {
        string url = "server=212.64.18.220;uid=rjgc;pwd=rjgc;database=rjgc";
        /// <summary>
        /// 保存增加的问题
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public int saveQuestion(Model.Question question)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();

            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "insert into t_base_question(content) values(@Content)";
            cm.Parameters.AddWithValue("@Content", question.Content);

            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Model.Question> GetQuestionList(int CurrentPage, int PageSize, string keyWord)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            keyWord = "'%" + keyWord + "%'";
            cm.CommandText = "select top " + PageSize + " * from t_base_question where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from t_base_question  where content like " + keyWord + ")     and  content like " + keyWord;
            cm.Connection = co;

            List<Model.Question> lst = new List<Model.Question>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.Question question = new Model.Question();
                question.Id = Convert.ToInt32(dr["Id"]);
                question.Content = Convert.ToString(dr["Content"]);

                lst.Add(question);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        public int Count()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from t_base_question";
            cm.Connection = co;

            int result = (int)cm.ExecuteScalar();
            co.Close();
            return result;
        }
        /// <summary>
        /// 获取单个问题
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Model.Question GetQustion(int Id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from t_base_question where Id = " + Id;
            SqlDataReader reader = cm.ExecuteReader();
            Model.Question question = new Model.Question();
            while (reader.Read())
            {

                question.Id = Convert.ToInt32(reader["Id"]);
                question.Content = Convert.ToString(reader["Content"]);

            }
            reader.Close();
            co.Close();
            return question;
        }
        public Model.QuestionnaireHead GetQuestionnaire(int Id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "select * from t_base_questionnaireHead where Id = " + Id;
            SqlDataReader reader = cm.ExecuteReader();
            Model.QuestionnaireHead question = new Model.QuestionnaireHead();
            while (reader.Read())
            {

                question.Id = Convert.ToInt32(reader["Id"]);
                question.QuestionnaireName = Convert.ToString(reader["QuestionnaireName"]);
               // question.TeacherId = Convert.ToInt32(reader["TeacherId"]);
                //question.TotalScore = Convert.ToInt32(reader["TotalScore"]);
                question.StartTime = Convert.ToDateTime(reader["StartTime"]);
                question.EndTime = Convert.ToDateTime(reader["EndTime"]);
                //question.CourseId = Convert.ToInt32(reader["CourseId"]);
                question.ClassId = Convert.ToInt32(reader["ClassId"]);
                question.SchoolId = Convert.ToInt32(reader["SchoolId"]);


            }
            reader.Close();
            co.Close();
            return question;
        }
        /// <summary>
        /// 保存修改的信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public int saveEditContent(int Id, string Content)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "update t_base_Question set Content = '" + Content + "' where Id = " + Id;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int saveEditNaireQuestion(int Id, string EditQuestionnaireName, DateTime EditStartTime, DateTime EditEndTime, int EditSchool,
           /*int EditteacherName,*/ int EditclassName)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "update t_base_QuestionnaireHead set QuestionnaireName = '" + EditQuestionnaireName + "',StartTime='" + EditStartTime + "',EndTime='" + EditEndTime + "',SchoolId=" + EditSchool + /*",TeacherId=" + EditteacherName +*/ ",ClassId=" + EditclassName + " where Id = " + Id;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
        public int DeleteQuestion(string Ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "delete from T_Base_Question where " +
                "Id in (" + Ids + ")";
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public int Deletenaire(string Ids)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "delete from T_Base_QuestionnaireHead where " +
                "Id in (" + Ids + ");delete from t_base_selectquestion where QuestionnaireHeadId in (" + Ids + ") ;delete from t_base_write where QuestionnaireHeadId in (" + Ids + ")";
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        //获取该问卷题目数量
        public int getQuestionCount(int HeadId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "Select count(*) from t_base_SelectQuestion where QuestionnaireHeadId=" + HeadId;
            int result = (int)cm.ExecuteScalar();
            co.Close();
            return result;
        }

        /// <summary>
        /// 插入新的问卷表头信息
        /// </summary>
        /// <param name="QuestionnairName"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="SchoolName"></param>
        /// <param name="teacherName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public int AddSaveNaire(string QuestionnaireName, DateTime StartTime, DateTime EndTime, int SchoolName,
      /* int teacherName,*/ int className/*, int courseId*/)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "insert into T_Base_QuestionnaireHead(QuestionnaireName,StartTime,EndTime,SchoolId,classId) values ('" + QuestionnaireName + "','" +
                 StartTime + "','" + EndTime + "'," + SchoolName /*+ "," + teacherName*/ + "," + className /*+ "," + courseId*/ + ")";
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;

        }
        /// <summary>
        /// 获取问卷列表
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<Model.QuestionnaireHead> GetNaireList(int CurrentPage, int PageSize, string keyWord)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            keyWord = "'%" + keyWord + "%'";
            cm.CommandText = "select top " + PageSize + " * from t_base_questionnaireHead where  id not in (select top " + PageSize * (CurrentPage - 1) + " id from t_base_questionnaireHead  where QuestionnaireName like " + keyWord + ")     and  QuestionnaireName like " + keyWord;
            cm.Connection = co;

            List<Model.QuestionnaireHead> lst = new List<Model.QuestionnaireHead>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.QuestionnaireHead naire = new Model.QuestionnaireHead();
                naire.Id = Convert.ToInt32(dr["Id"]);
                naire.QuestionnaireName = Convert.ToString(dr["QuestionnaireName"]);
                naire.ClassId = Convert.ToInt32(dr["ClassId"]);
                //naire.CourseId = Convert.ToInt32(dr["CourseId"]);
                lst.Add(naire);
            }
            dr.Close();
            co.Close();
            return lst;
        }

        public int NaireCount()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "select count(*) from t_base_QuestionnaireHead";
            cm.Connection = co;

            int result = (int)cm.ExecuteScalar();
            co.Close();
            return result;
        }
        /// <summary>
        /// 根据教师Id获取
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        //public int GetCourseId(int teacherId)
        //{

        //    SqlConnection co = new SqlConnection();
        //    co.ConnectionString = url;
        //    co.Open();
        //    SqlCommand cm = new SqlCommand();
        //    cm.CommandText = "select courseId from t_base_teacher where Id= " + teacherId;
        //    cm.Connection = co;

        //    int result = (int)cm.ExecuteScalar();
        //    co.Close();
        //    return result;
        //}
        public int saveNaireQuestions(string[] Ids, string HeadId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            var CmText = "insert into t_base_selectQuestion(questionnaireHeadId,QuestionId) ";
            CmText += "VALUES(" + HeadId + "," + Ids[0] + ")";
            for (int i = 1; i < Ids.Length; i++)
            {
                CmText += ",(" + HeadId + "," + Ids[i] + ")";
            }
            cm.CommandText = CmText;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }

        public List<Model.Question> showNaireQuestion(int HeadId)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();

            string SqlWords = "select t_base_selectquestion.Id Id, content Content from t_base_Question , t_base_selectquestion  where t_base_Question.Id=t_base_selectquestion.QuestionId and QuestionnaireHeadId=" + HeadId;
            SqlCommand cm = new SqlCommand(SqlWords, co);
            List<Model.Question> lst = new List<Model.Question>();
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                Model.Question naire = new Model.Question();
                naire.Id = Convert.ToInt32(dr["Id"]);
                naire.Content = Convert.ToString(dr["Content"]);
                lst.Add(naire);
            }
            dr.Close();
            co.Close();
            return lst;

        }

        public int deleteNaireQuestion(int Id)
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = url;
            co.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = co;
            cm.CommandText = "delete from T_Base_selectQuestion where " +
                "Id =" + Id;
            int result = cm.ExecuteNonQuery();
            co.Close();
            return result;
        }
    }
}
