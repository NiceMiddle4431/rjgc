using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Write
    {
        /// <summary>
        /// 填写问卷Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 填写问卷学生Id
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 所填写问卷内容
        /// </summary>
        public int QuestionnaireHeadId { get; set; }
        /// <summary>
        /// 被评价教师Id
        /// </summary>
        public int TeacherId { get; set; }
        /// <summary>
        /// 总分
        /// </summary>
        public int TotalScore { get; set; }
        public int Q1 { get; set; }
        public int Q2 { get; set; }
        public int Q3 { get; set; }
        public int Q4 { get; set; }
        public int Q5 { get; set; }
        public int Q6 { get; set; }
        public int Q7 { get; set; }
        public int Q8 { get; set; }
        public int Q9 { get; set; }
        public int Q10 { get; set; }

    }
}
