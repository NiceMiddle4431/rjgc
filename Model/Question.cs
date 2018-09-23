using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Question
    { 
        /// <summary>
        /// 问题Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 问题内容
        /// </summary>
        public string Content { get; set; }
    }
}
