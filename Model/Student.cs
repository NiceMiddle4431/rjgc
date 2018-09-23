using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Student
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 所属班级Id
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 所属班级信息
        /// </summary>
        public Model.Class Class { get; set; }
        /// <summary>
        /// 学生学号
        /// </summary>
        public string StudentNum { get; set; }
        /// <summary>
        /// 学生登录密码
        /// </summary>
        public string StudentPWD { get; set; }
        /// <summary>
        /// 所属学校信息
        /// </summary>
        public Model.School School { get; set; }
        /// <summary>
        /// 学生性别，0为女，1为男
        /// </summary>
        public int Sex { get; set; }
        public int RoleId { get; set; }
    }
}
