using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Class
    {
        /// <summary>
        /// 班级Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 班级名字
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 所属学校Id
        /// </summary>
        public int SchoolId { get; set; }
        /// <summary>
        /// 所属学校信息
        /// </summary>
        public Model.School School { get; set; }
    }
}
