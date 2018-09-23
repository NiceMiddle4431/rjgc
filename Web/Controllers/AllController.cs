using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AllController : Controller
    {
        // GET: All
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        int tempSchoolId;
        int tempTeacherId;
        int tempClassId;
        public JsonResult GetSchoolScore()
        {
            BLL.All bll = new BLL.All();
            return Json(bll.GetSchoolScore());
        }
        [Authorize]
        public ActionResult SchoolTeacher(int SchoolId)
        {
            tempSchoolId = SchoolId;
            ViewBag.SchoolId = SchoolId;
            return View();
        }
        public JsonResult GetSchoolTeacherScore(int SchoolId)
        {
            BLL.All bll = new BLL.All();
            return Json(bll.GetSchoolTeacherScore(SchoolId));
        }
        [Authorize]
        public ActionResult TeacherClass(int SchoolId, int TeacherId, int ClassId)
        {
            tempTeacherId = TeacherId;
            tempClassId = ClassId;
            ViewBag.TeacherId = TeacherId;
            ViewBag.SchoolId = SchoolId;
            ViewBag.ClassId = ClassId;
            return View();
        }
        public JsonResult GetTeacherClassScore(int TeacherId, int ClassId)
        {
            BLL.All bll = new BLL.All();
            return Json(bll.GetTeacherClassScore(TeacherId, ClassId));
        }
    }
}