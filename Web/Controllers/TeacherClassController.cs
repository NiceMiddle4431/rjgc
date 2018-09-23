using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TeacherClassController : Controller
    {
        // GET: TeacherClass
        [Authorize]
        public ActionResult index(int TeacherId)
        {
            ViewBag.teacherId = TeacherId;
            return View();
        }
        public JsonResult GetList(int teacherId, int pageSize, int pageNumber)
        {
            BLL.TeacherClass bll = new BLL.TeacherClass();
            return Json(new { total = bll.GetCount(teacherId), rows = bll.GetList(teacherId, pageSize, pageNumber) });
        }
        public JsonResult GetClass(int TeacherId)
        {
            BLL.TeacherClass bll = new BLL.TeacherClass();
            return Json(bll.GetClass(TeacherId));
        }
        public JsonResult AddSave(int AddTeacherId, int AddClassId)
        {
            BLL.TeacherClass bll = new BLL.TeacherClass();
            int result = bll.AddSave(AddTeacherId, AddClassId);
            if (result == 1)
            {
                return Json("添加成功");
            }
            else
            {
                return Json("添加失败");
            }
        }

        public JsonResult Delete(int DeleteTeacherId, string[] DeleteClassIds)
        {
            BLL.TeacherClass bll = new BLL.TeacherClass();
            int result = bll.Delete(DeleteTeacherId, DeleteClassIds);
            if (result >= 1)
            {
                return Json("删除" + result + "条记录");
            }
            else
            {
                return Json("删除失败");
            }
        }

    }
}