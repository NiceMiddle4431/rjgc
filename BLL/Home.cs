using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Home
    {
        public List<Model.Menu> GetList(int RoleId)
        {
            DAL.Home dal = new DAL.Home();
            return dal.GetList(RoleId);
        }
        public Object Check(int RoleId, int SchoolId, string LoginName, string Password)
        {
            DAL.Home dal = new DAL.Home();
            if (RoleId == 5)
            {
                return dal.CheckAdmin(RoleId, LoginName, Password);
            }
            else if (RoleId == 4)
            {
                return dal.CheckStudent(RoleId, SchoolId, LoginName, Password);
            }
            else
            {
                return dal.CheckTeacher(RoleId, SchoolId, LoginName, Password);
            }
        }

        public List<Model.User> GetRole()
        {
            DAL.Home dal = new DAL.Home();
            return dal.GetRole();
        }
        public Model.User GetUser(int Id)
        {
            DAL.Home dal = new DAL.Home();
            return dal.GetUser(Id);
        }
    }
}
