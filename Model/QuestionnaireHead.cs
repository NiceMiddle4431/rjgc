using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class QuestionnaireHead
    {
        /// <summary>
        /// 问题Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 问卷标题
        /// </summary>
        public string QuestionnaireName { get; set; }

      
        /// <summary>
        /// 问卷可被评价的起始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 问卷可被评价的结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 教师所教班级
        /// </summary>
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public List<Model.SelectQuestion> Item { get; set; }
        public int TotalScore { get; set; }
        public int WritenTimes { get; set; }
        public List<int> ScoreList { get; set; }
    }
}
