using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Teacher
    {
        /// <summary>
        /// 教师Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 教师姓名
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 教师工号
        /// </summary>
        public string TeacherWorkNum { get; set; }
        /// <summary>
        /// 教师登录密码
        /// </summary>
        public string TeacherPWD { get; set; }
        /// <summary>
        /// 教师所属学校Id
        /// </summary>
        public int SchoolId { get; set; }
        /// <summary>
        /// 是否为领导
        /// </summary>
        public int IsLeader { get; set; }
        /// <summary>
        /// 教师性别 0为女性 1为男性
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 所教科目Id
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 所教科目信息
        /// </summary>
        public Model.Course Course { get; set; }
        /// <summary>
        /// 所教班级信息
        /// </summary>
        public Model.Class TempClass { get; set; }

        public Model.School School { get; set; }
        public int RoleId { get; set; }
    }
}
