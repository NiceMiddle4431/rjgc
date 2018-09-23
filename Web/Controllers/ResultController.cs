using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ResultController : Controller
    {
        // GET: Result
        [Authorize]
        
        public ActionResult Index(int teacherId=0)
        {    
            ViewBag.TeacherId = teacherId;
            return View();
        }
        public JsonResult GetList(int pageSize, int pageIndex, int TeacherId, string keyWord = "")
        {
            BLL.QuestionnaireHead bll = new BLL.QuestionnaireHead();
            List<Model.QuestionnaireHead> lst = new List<Model.QuestionnaireHead>();
            lst = bll.GetList(pageIndex, pageSize, TeacherId, keyWord);
            int count = bll.GetCount(TeacherId);

            List<object> fullList = new List<object>();
            for (int i = 0; i <lst.Count; i ++)
            {
                Model.QuestionnaireHead qstnH = lst[i];
                bool isExisted = false;
                for (int j = 0; j < i; j ++)
                {
                    if(lst[j].Id == qstnH.Id)
                    {
                        isExisted = true;
                        break;
                    }
                }
                if (isExisted)
                    continue;
                //生成一个新obj
                object obj = new
                {
                    Id = qstnH.Id,
                    QuestionnaireName = qstnH.QuestionnaireName,
                    WritenTimes = qstnH.WritenTimes,
                    TotalScore = qstnH.TotalScore,
                    ClassId = qstnH.ClassId,
                    ClassName = new BLL.Write().getClassName(qstnH.ClassId)
                };
                fullList.Add(obj);
            }
            return Json(new { total = count, rows = fullList, prev = lst });
        }
        public JsonResult GetModel(int HeadId, int TeacherId)
        {
            BLL.QuestionnaireHead bll = new BLL.QuestionnaireHead();
            Model.QuestionnaireHead model = bll.GetModel(HeadId,TeacherId);

            List<object> fullItem = new List<object>();
            for (int i = 0; i < model.Item.Count; i++)
            {
                Model.SelectQuestion sq = model.Item[i];
                Model.Question question = new Model.Question();
                question.Id = sq.Question.Id;
                question.Content = sq.Question.Content;

                object obj = new
                {
                    Question = question,
                    QuestionHeadId = sq.QuestionHeadId,
                    AVGScore = model.ScoreList[i] / new BLL.QuestionnaireHead().GetWritenTimes(sq.QuestionHeadId,TeacherId)
                };
                fullItem.Add(obj);
            }

            return Json(new { Item = fullItem });
            //return Json(model);
        }


    }
}