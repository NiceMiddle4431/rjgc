using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SelectQuestion
    {
        /// <summary>
        /// 生成试卷Id
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        /// 存储评价成绩Id
        /// </summary>
        public int QuestionHeadId { get; set; }
        /// <summary>
        /// 评价问题Id
        /// </summary>
        public int QuestionId { get; set; }
        public Question Question { get; set; }
       

    }
}
