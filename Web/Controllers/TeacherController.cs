using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取教师信息给主页面表格
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public JsonResult GetList(int pageSize, int pageNumber, string TeacherWorkNum, string TeacherName, string  CourseName)
        {
            BLL.Teacher bll = new BLL.Teacher();
            int count = bll.GetCount();
            return Json(new { total = count, rows = bll.GetList(pageSize, pageNumber,TeacherWorkNum,TeacherName,CourseName) });
        }
        public JsonResult AddSave(string AddTeacherName, string AddTeacherWorkNum,
           int AddSex, int AddIsLeader, int AddSchool, int AddCourse, string AddTeacherPWD)
        {
            BLL.Teacher bll = new BLL.Teacher();
            int result = bll.AddSave(AddTeacherName, AddTeacherWorkNum,
                AddSex, AddIsLeader, AddSchool, AddCourse, AddTeacherPWD);
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
        ///  获取编辑时单个教师的信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetTeacher(int Id)
        {
            BLL.Teacher bll = new BLL.Teacher();
            return Json(bll.GetTeacher(Id));
        }
    
        /// <summary>
        /// 保存修改后教师信息
        /// </summary>
        /// <param name="EditTeacherName"></param>
        /// <param name="EditTeacherWorkNum"></param>
        /// <param name="EditSex"></param>
        /// <param name="EditIsLeader"></param>
        /// <param name="EditSchool"></param>
        /// <param name="EditCourse"></param>
        /// <param name="EditTeacherPWD"></param>
        /// <returns></returns>
        public JsonResult EditSave(int EditId, string EditTeacherName, string EditTeacherWorkNum,
            int EditSex, int EditIsLeader, int EditSchool, int EditCourse, string EditTeacherPWD)
        {
            BLL.Teacher bll = new BLL.Teacher();
            int result = bll.EditSave(EditId, EditTeacherName, EditTeacherWorkNum,
                EditSex, EditIsLeader, EditSchool, EditCourse, EditTeacherPWD);
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
        /// 删除选择的教师信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public JsonResult Delete(string[] Ids)
        {
            BLL.Teacher bll = new BLL.Teacher();
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
    }
}