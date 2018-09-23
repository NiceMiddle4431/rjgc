using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Menu
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 控制器名字
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 控制器里函数名字
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 所对应的功能
        /// </summary>
        public string Display { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 父类菜单
        /// </summary>
        public int ParentId { get; set; }
    }
}
