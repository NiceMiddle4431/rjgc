using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
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
        public JsonResult GetList(int pageSize, int pageNumber, string ClassName, string StudentNum, string StudentName)
        {
            BLL.Student bll = new BLL.Student();
            int count = bll.GetCount();
            return Json(new { total = count, rows = bll.GetList(pageSize, pageNumber, ClassName, StudentNum, StudentName) });
        }
        /// <summary>
        /// 获取单个学生信息给编辑界面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult GetStudent(int Id)
        {
            BLL.Student bll = new BLL.Student();
            return Json(bll.GetStudent(Id));
        }
        /// <summary>
        /// 获取该学校所有的班级信息
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public JsonResult GetClass(int SchoolId)
        {
            BLL.Student bll = new BLL.Student();
            return Json(bll.GetClass(SchoolId));
        }
        /// <summary>
        /// 获取该地区所有学校信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSchool()
        {
            BLL.Student bll = new BLL.Student();
            return Json(bll.GetSchool());
        }
        /// <summary>
        /// 保存编辑后的学生信息
        /// </summary>
        /// <param name="EditId"></param>
        /// <param name="EditStudentNum"></param>
        /// <param name="EditStudentName"></param>
        /// <param name="EditSex"></param>
        /// <param name="EditClass"></param>
        /// <param name="EditSchool"></param>
        /// <param name="EditStudentPWD"></param>
        /// <returns></returns>
        public JsonResult EditSave(int EditId, string EditStudentNum, string EditStudentName,
            int EditSex, int EditClass, int EditSchool, string EditStudentPWD)
        {
            BLL.Student bll = new BLL.Student();
            if (bll.EditSave(EditId, EditStudentNum, EditStudentName,
                EditSex, EditClass, EditSchool, EditStudentPWD) > 0)
            {
                return Json("修改成功");
            }
            else
            {
                return Json("修改失败");
            }

        }
        /// <summary>
        /// 保存添加的学生信息
        /// </summary>
        /// <param name="AddStudentNum"></param>
        /// <param name="AddStudentName"></param>
        /// <param name="AddSex"></param>
        /// <param name="AddClass"></param>
        /// <param name="AddSchool"></param>
        /// <param name="AddStudentPWD"></param>
        /// <returns></returns>
        public JsonResult AddSave(string AddStudentNum, string AddStudentName,
            int AddSex, int AddClass, string AddSchool, string AddStudentPWD)
        {
            BLL.Student bll = new BLL.Student();
            if (bll.AddSave(AddStudentNum, AddStudentName,
                AddSex, AddClass, AddSchool, AddStudentPWD) > 0)
            {
                return Json("添加成功");
            }
            else
            {
                return Json("添加失败");
            }
        }
        /// <summary>
        /// 删除选择的学生信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public JsonResult Delete(string[] Ids)
        {
            BLL.Student bll = new BLL.Student();
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
        /// 保存Excel导入学生信息
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public int ExcelToDS(string fillPath)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fillPath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;

            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet(); myCommand.Fill(ds, "table1");
            DataTable dt = ds.Tables[0];
            List<Model.Student> list = new List<Model.Student>();
            try
            {
                for (int i = 0; i >= 0; i++)
                {
                    Model.Student student = new Model.Student();
                    Model.Class tempClass = new Model.Class();
                    Model.School school = new Model.School();
                    student.StudentNum = dt.Rows[i][0].ToString();
                    student.StudentPWD = dt.Rows[i][0].ToString();
                    student.StudentName = dt.Rows[i][1].ToString();
                    var sex = dt.Rows[i][2].ToString();
                    if (sex.Equals("男"))
                    {
                        student.Sex = 1;
                    }
                    else if(sex.Equals("女"))
                    {
                        student.Sex = 0;
                    }
                    tempClass.ClassName = dt.Rows[i][3].ToString();
                    school.SchoolNum = dt.Rows[i][4].ToString();
                    school.SchoolName = dt.Rows[i][5].ToString();
                    school.Address = dt.Rows[i][6].ToString();
                    student.Class = tempClass;
                    student.School = school;
                    list.Add(student);
                }
            }
            catch (Exception)
            {

            }
            BLL.Student bll = new BLL.Student();
            int result = bll.AddExcel(list);
            conn.Close();
            return result;

        }
        public JsonResult AddExcel()
        {
            //文件保存
            var file = Request.Files["txt_file"];
            string path = Server.MapPath("\\upLoad\\");
            string fileName = Guid.NewGuid().ToString() + ".xlx";
            string fillPath = path + fileName;
            file.SaveAs(fillPath);

            if (ExcelToDS(fillPath) >= 1)
            {
                return Json("添加成功");

            }
            else
            {
                return Json("添加失败");
            }
        }
    }
}