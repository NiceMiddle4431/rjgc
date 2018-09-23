using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取数据库中的课程信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public JsonResult GetList(int pageSize, int pageNumber)
        {
            BLL.Course bll = new BLL.Course();
            int count = bll.GetCount();
            return Json(new { total = count, rows = bll.GetList(pageSize, pageNumber) });
        }
        /// <summary>
        /// 保存修改后的课程信息
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="EditCourseName"></param>
        /// <returns></returns>
        public JsonResult EditCourseSave(int CourseId, string EditCourseName)
        {
            BLL.Course bll = new BLL.Course();
            int result = bll.EditCourseSave(CourseId, EditCourseName);
            if (result == 1)
            {
                return Json("修改成功");
            }
            else
            {
                return Json("修改失败");
            }
        }
        /// <summary>
        /// 保存添加后的课程信息
        /// </summary>
        /// <param name="CourseId"></param>
        /// <param name="EditCourseName"></param>
        /// <returns></returns>
        public JsonResult AddCourseSave(string AddCourseName)
        {
            BLL.Course bll = new BLL.Course();
            int result = bll.AddCourseSave(AddCourseName);
            if (result == 1)
            {
                return Json("添加成功");
            }
            else
            {
                return Json("添加失败");
            }
        }
        /// <summary>
        /// 删除选择的课程信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public JsonResult Delete(string[] Ids)
        {
            BLL.Course bll = new BLL.Course();
            int result = bll.Delete(Ids);
            if (result <= 0)
            {
                return Json("删除失败");
            }
            else
            {
                return Json("删除" + result + "记录");
            }
        }
        /// <summary>
        /// 获取全部课程信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCourse()
        {
            BLL.Course bll = new BLL.Course();
            return Json(bll.GetCourse());
        }
    }
}