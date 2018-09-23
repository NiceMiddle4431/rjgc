using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Score
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int QuestionnaireHeadId { get; set; }
        public int TeacherId { get; set; }
        public List<int> QScore { get; set; }
        //public int Q0Score { get; set; }
        //public int Q1Score { get; set; }
        //public int Q2Score { get; set; }
        //public int Q3Score { get; set; }
        //public int Q4Score { get; set; }
        //public int Q5Score { get; set; }
        //public int Q6Score { get; set; }
        //public int Q7Score { get; set; }
        //public int Q8Score { get; set; }
        //public int Q9Score { get; set; }
        public int TotalScore { get; set; }
        public int ClassId { get; set; }
        public int SchoolId { get; set; }
        public string QuestionnaireName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTiem { get; set; }
        public Model.School School { get; set; }
        public Model.Class Class { get; set; }
        public Model.Teacher Teacher { get; set; }
        public List<Model.Question> Question { get; set; }

    }
}
