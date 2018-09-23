using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 获取全部的角色
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRole()
        {
            BLL.Home bll = new BLL.Home();
            return Json(bll.GetRole());
        }
        public JsonResult Check(int RoleId, string LoginName, string Password, int SchoolId = -1)
        {
            BLL.Home bll = new BLL.Home();
            int roleId = -1;
            int loginId = -1;
            if (RoleId == 2)
            {
                Model.Teacher teacher = (Model.Teacher)bll.Check(RoleId, SchoolId, LoginName, Password);
                roleId = teacher.RoleId;
                loginId = teacher.Id;
            }
            else if (RoleId == 4)
            {
                Model.Student student = (Model.Student)bll.Check(RoleId, SchoolId, LoginName, Password);
                roleId = student.RoleId;
                loginId = student.Id;
            }
            else if (RoleId == 5)
            {
                Model.User user = (Model.User)bll.Check(RoleId, SchoolId, LoginName, Password);
                roleId = user.RoleId;
                loginId = user.Id;
            }
            if (roleId != -1)
            {
                //记录票据
                FormsAuthentication.SetAuthCookie(LoginName, false);//简单授权
                var authTicket = new FormsAuthenticationTicket(
                    roleId,        //角色
                    "" + loginId,          //登录用户Id
                    DateTime.Now,       //当前时间
                    DateTime.Now.AddMinutes(5),//保存时间
                    true,// 如果为 true，则创建持久 Cookie（跨浏览器会话保存的 Cookie）；否则为 false。
                    ""      //存储在票证中的用户特定的数据
                    );
                HttpCookie authCookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(authTicket));

                Response.Cookies.Add(authCookie);
                return Json("登录成功");
            }
            else
            {
                return Json("登录失败，账号密码错误");
            }


        }
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "home");
        }
    }
}