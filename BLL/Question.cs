using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Question
    {
        /// <summary>
        /// 保存增加的问题
        /// </summary>
        /// <param name="Question"></param>
        /// <returns></returns>
        public int saveQuestion(string Question)
        {
            Model.Question question = new Model.Question();
            question.Content = Question;
            DAL.Question dal = new DAL.Question();
            return dal.saveQuestion(question);
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
            return new DAL.Question().GetQuestionList(CurrentPage, PageSize, keyWord);
        }

        public int Count()
        {
            return new DAL.Question().Count();
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
            return new DAL.Question().GetNaireList(CurrentPage, PageSize, keyWord);
        }

        public int NaireCount()
        {
            return new DAL.Question().NaireCount();
        }
        public Model.Question GetQustion(int Id)
        {
            DAL.Question dal = new DAL.Question();
            return dal.GetQustion(Id);
        }


        public Model.QuestionnaireHead GetQuestionnaire(int Id)
        {
            DAL.Question dal = new DAL.Question();
            return dal.GetQuestionnaire(Id);
        }
        /// <summary>
        /// 保存修改的信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public int saveEditContent(int Id, string Content)
        {
            DAL.Question dal = new DAL.Question();
            return (dal.saveEditContent(Id, Content));
        }
        public int saveEditNaireQuestion(int Id, string EditQuestionnaireName, DateTime EditStartTime, DateTime EditEndTime, int EditSchool,
          /* int EditteacherName,*/ int EditclassName)
        {
            DAL.Question dal = new DAL.Question();
            return (dal.saveEditNaireQuestion(Id, EditQuestionnaireName, EditStartTime, EditEndTime, EditSchool/*, EditteacherName*/, EditclassName));
        }

        public int DeleteQuestion(string[] Ids)
        {
            //防止注入式漏洞
            string ids = string.Join(",", Ids);
            DAL.Question dal = new DAL.Question();
            return dal.DeleteQuestion(ids);
        }

        public int Deletenaire(string[] Ids)
        {
            //防止注入式漏洞
            string ids = string.Join(",", Ids);
            DAL.Question dal = new DAL.Question();
            return dal.Deletenaire(ids);
        }

        //public List<Model.Teacher> GetTeacher(int SchoolId)
        //{
        //    DAL.Question dal = new DAL.Question();
        //    return dal.GetTeacher(SchoolId);
        //}


        public int AddSaveNaire(string QuestionnaireName, DateTime StartTime, DateTime EndTime, int SchoolName,
      /* int teacherName,*/ int className)
        {
            DAL.Question dal = new DAL.Question();
           // int CourseId = dal.GetCourseId(teacherName);
            return dal.AddSaveNaire(QuestionnaireName, StartTime,
                EndTime, SchoolName, /*teacherName,*/ className/*, CourseId*/);
        }
        public int saveNaireQuestions(string[] Ids, string HeadId)
        {
            //string ids = string.Join(",", Ids);
            DAL.Question dal = new DAL.Question();
            return dal.saveNaireQuestions(Ids, HeadId);
        }

        public List<Model.Question> showNaireQuestion(int HeadId)
        {
            DAL.Question dal = new DAL.Question();
            return dal.showNaireQuestion(HeadId);
        }

        public int deleteNaireQuestion(int Id)
        {

            DAL.Question dal = new DAL.Question();
            return dal.deleteNaireQuestion(Id);
        }
        //获取该问卷题目数量
        public int getQuestionCount(int HeadId)
        {
            DAL.Question dal = new DAL.Question();
            return dal.getQuestionCount(HeadId);
        }

    }
}
