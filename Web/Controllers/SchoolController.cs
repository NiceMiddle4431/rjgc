using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School
        /// <summary>
        /// index主页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取学生信息给主页面表格
        /// </summary>
        /// <param name="pageSize">一页显示数目</param>
        /// <param name="pageNumber">当前页码</param>
        /// <returns></returns>
        public JsonResult GetList(int pageSize, int pageNumber,string SchoolName,string Address)
        {
            BLL.School bll = new BLL.School();
            int count = bll.GetCount();
            return Json(new { total = count, rows = bll.GetList(pageSize, pageNumber,SchoolName,Address) });
        }
        /// <summary>
        /// 学校详情页
        /// </summary>
        /// <returns></returns>
       [Authorize]
        public ActionResult ClassIndex(int SchoolId)
        {
            ViewBag.SchoolId = SchoolId;
            return View();
        }

        /// <summary>
        /// 获取该学校所有的班级信息
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public JsonResult GetClass(int SchoolId)
        {
            BLL.School bll = new BLL.School();
            return Json(bll.GetClass(SchoolId));
        }
        /// <summary>
        /// 保存添加的班级信息
        /// </summary>
        /// <param name="AddClassName"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public JsonResult AddClassSave(string AddClassName,int SchoolId)
        {
            BLL.School bll = new BLL.School();
            int result = bll.AddClassSave(AddClassName, SchoolId);
            if (result == 1)
            {
                return Json("添加成功");
            }else
            {
                return Json("添加失败");
            }
        }

        /// <summary>
        /// 保存修改后的班级信息
        /// </summary>
        /// <param name="EditClassName"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public JsonResult EditClassSave(int ClassId,string EditClassName)
        {
            BLL.School bll = new BLL.School();
            int result = bll.EditClassSave(ClassId,EditClassName);
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
        /// 删除选择的学校信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public JsonResult DeleteClass(string[] Ids)
        {
            BLL.School bll = new BLL.School();
            int result = bll.DeleteClass(Ids);
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
        /// 保存添加的学校信息
        /// </summary>
        /// <param name="AddSchoolName"></param>
        /// <param name="AddSchoolNum"></param>
        /// <returns></returns>
        public JsonResult AddSchoolSave(string AddSchoolName, string AddSchoolNum,string AddAddress)
        {
            BLL.School bll = new BLL.School();
            int result = bll.AddSchoolSave(AddSchoolName, AddSchoolNum, AddAddress);
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
        /// 保存编辑后的学校信息
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="EditSchoolName"></param>
        /// <param name="EditSchoolNum"></param>
        /// <returns></returns>
        public JsonResult EditSchoolSave(int SchoolId, string EditSchoolNum, string EditSchoolName,string EditAddress)
        {
            BLL.School bll = new BLL.School();
            int result = bll.EditSchoolSave(SchoolId, EditSchoolNum, EditSchoolName, EditAddress);
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
        /// 删除选择的学校信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public JsonResult DeleteSchool(string[] Ids)
        {
            BLL.School bll = new BLL.School();
            int result = bll.DeleteSchool(Ids);
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