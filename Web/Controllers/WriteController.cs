using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Globalization;
using System.Runtime.Serialization;
using BLL;
using Question = Model.Question;
using QuestionnaireHead = Model.QuestionnaireHead;

namespace Web.Controllers
{
    public class WriteController : Controller
    {
       
        // GET: Write
        [Authorize]
   
        public ActionResult Index(int studentId=0)
        {
            ViewBag.StudentId = studentId;
            return View();
        }

        //问卷列表
        [Authorize]
        public ActionResult NaireList(int Id, int TeacherId)
        {
            ViewBag.Id = Id;
            ViewBag.TeacherId = TeacherId;
            return View();
        }
        //真正问卷
        [Authorize]
        public ActionResult Questionnaire(int HeadId, int TeacherId, int StudentId)
        {
            //根据问卷Id获取这份问卷所有内容
            BLL.Write bll = new Write();
            List<Model.QuestionnaireHead> lstHead = new List<QuestionnaireHead>();
            lstHead = bll.getNaireHead(HeadId);
            ViewBag.lstHead = lstHead;
            string className = bll.getClassName(lstHead[0].ClassId);
            ViewBag.className = className;
            string schoolName = bll.getSchoolName(lstHead[0].SchoolId);
            ViewBag.schoolName = schoolName;
            List<Model.Question> lstBody = new List<Question>();
            lstBody = bll.getNaireBody(HeadId);
            ViewBag.lstBody = lstBody;
            string TeacherName = bll.toTeacherName(TeacherId);
            ViewBag.TeacherName = TeacherName;
            ViewBag.StudentId = StudentId;

            ViewBag.TeacherId = TeacherId;

            return View();
        }


        //获取老师名字
        public JsonResult getTeacherName(int StudentId)
        {
            BLL.Write bll = new Write();
            return Json(bll.getTeacherName(StudentId));
        }
        //获取该班级的问卷列表
        public JsonResult getNaireList(int StudentId)
        {
            BLL.Write bll = new Write();
            return Json(bll.getNaireList(StudentId));
        }

        //保存填入的数据
        public JsonResult saveWriteResult(int StudentId, int QuestionnaireHeadId, int TeacherId, string tenScore)
        {
            BLL.Write bll = new Write();
            if (bll.saveWriteResult(StudentId, QuestionnaireHeadId, TeacherId, tenScore) > 0)
            {
                return Json("提交成功！");
            }
            else
            {
                return Json("提交失败！");
            }
        }

        //是否已经填写
        public JsonResult CompletedNaire(int TeacherId, int StudentId, int QuestionnaireHeadId)
        {
            BLL.Write bll = new Write();
            int result = bll.CompletedNaire(TeacherId, StudentId, QuestionnaireHeadId);

            return Json(result);


        }

        //是否过期
        public JsonResult TimeoutNaire(int QuestionnaireHeadId)
        {
            BLL.Write bll = new Write();
            List<Model.QuestionnaireHead> lstHead = new List<QuestionnaireHead>();
            lstHead = bll.getNaireHead(QuestionnaireHeadId);
            int result;
            if (DateTime.Now < lstHead[0].StartTime)
            {
                result = -1;
                return Json(result);

            }
            else if (DateTime.Now > lstHead[0].EndTime)
            {
                result = 1;
                return Json(result);

            }
            else
            {
                result = 0;
                return Json(result);
            }

        }
        //获取该问卷题目数量
        public JsonResult getQuestionCount(int QuestionnaireHeadId)
        {
            BLL.Question bll = new BLL.Question();

            int result = bll.getQuestionCount(QuestionnaireHeadId);
            return Json(result);
        }
    }
}