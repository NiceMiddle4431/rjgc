using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL;

namespace Web.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        /// <summary>
        /// 问卷列表
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult QuestionList()
        {
            return View();
        }

        public ActionResult AddNaireQuestions(int Id, string QuestionnaireName)
        {
            ViewBag.Id = Id;
            ViewBag.QuestionnaireName = QuestionnaireName;
            return View();
        }
        /// <summary>
        /// 增加问题的保存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int saveQuestion(string data)
        {
            BLL.Question bll = new Question();
            int result = bll.saveQuestion(data);
            return result;
        }
        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public JsonResult GetQuestionList(int pageSize, int pageIndex, string keyWord = "")
        {
            BLL.Question bll = new BLL.Question();
            List<Model.Question> lst = new List<Model.Question>();
            lst = bll.GetQuestionList(pageIndex, pageSize, keyWord);
            int count = bll.Count();
            return Json(new { total = count, rows = lst });
        }
        /// <summary>
        /// 获取单个问题
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetQuestion(int Id)
        {
            BLL.Question bll = new Question();
            return Json(bll.GetQustion(Id));
        }

        public JsonResult GetQuestionnaire(int Id)
        {
            BLL.Question bll = new Question();
            return Json(bll.GetQuestionnaire(Id));
        }
        public JsonResult saveEditContent(int Id, string Content)
        {
            BLL.Question bll = new Question();
            if (bll.saveEditContent(Id, Content) > 0)
            {
                return Json("修改成功！");
            }
            else
            {
                return Json("修改失败！");
            }
        }

        public JsonResult saveEditNaireQuestion(int Id, string EditQuestionnaireName, DateTime EditStartTime, DateTime EditEndTime, int EditSchool,
           /*int EditteacherName,*/ int EditclassName)
        {
            BLL.Question bll = new Question();
            if (bll.saveEditNaireQuestion(Id, EditQuestionnaireName, EditStartTime, EditEndTime, EditSchool, /*EditteacherName,*/ EditclassName) > 0)
            {
                return Json("修改成功！");
            }
            else
            {
                return Json("修改失败！");
            }
        }

        public JsonResult DeleteQuestion(string[] ids)
        {
            BLL.Question bll = new BLL.Question();
            int result = bll.DeleteQuestion(ids);
            if (result <= 0)
            {
                return Json("删除失败");
            }
            else
            {
                return Json("删除" + result + "记录");
            }
        }

        public JsonResult Deletenaire(string[] ids)
        {
            BLL.Question bll = new BLL.Question();
            int result = bll.Deletenaire(ids);
            if (result <= 0)
            {
                return Json("删除失败");
            }
            else
            {
                return Json("删除" + result + "记录");
            }
        }

        //public JsonResult GetTeacher(int SchoolId)
        //{
        //    BLL.Question bll = new BLL.Question();
        //    return Json(bll.GetTeacher(SchoolId));
        //}

        public JsonResult AddSaveNaire(string QuestionnaireName, DateTime StartTime, DateTime EndTime, int SchoolName,
          /* int teacherName,*/ int className)
        {
            BLL.Question bll = new BLL.Question();
            if (bll.AddSaveNaire(QuestionnaireName, StartTime,
                EndTime, SchoolName, /*teacherName, */className) > 0)
            {
                return Json("添加成功");
            }
            else
            {
                return Json("添加失败");
            }
        }

        public JsonResult GetNaireList(int pageSize, int pageIndex, string keyWord = "")
        {
            BLL.Question bll = new BLL.Question();
            List<Model.QuestionnaireHead> lst = new List<Model.QuestionnaireHead>();
            lst = bll.GetNaireList(pageIndex, pageSize, keyWord);
            int count = bll.NaireCount();
            return Json(new { total = count, rows = lst });
        }

        public JsonResult saveNaireQuestions(string[] ids, string HeadId)
        {
            BLL.Question bll = new Question();
            if (bll.saveNaireQuestions(ids, HeadId) > 0)
            {
                return Json("问卷题目成功！");
            }
            else
            {
                return Json("问卷题目失败！");
            }
        }


        public JsonResult showNaireQuestion(int HeadId)
        {
            BLL.Question bll = new Question();

            return Json(bll.showNaireQuestion(HeadId));

        }

        public JsonResult deleteNaireQuestion(int Id)
        {
            BLL.Question bll = new BLL.Question();
            int result = bll.deleteNaireQuestion(Id);
            if (result <= 0)
            {
                return Json("删除失败");
            }
            else
            {
                return Json("删除" + result + "记录");
            }
        }
        //获取该问卷题目数量
        public JsonResult getQuestionCount(int HeadId)
        {
            BLL.Question bll = new Question();
            int result = bll.getQuestionCount(HeadId);
            return Json(result);
        }

    }
}