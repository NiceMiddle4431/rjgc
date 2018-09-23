using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class School
    {
        /// <summary>
        /// 学校Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学校名字
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNum { get; set; }
        public string Address { get; set; }
    }
}
