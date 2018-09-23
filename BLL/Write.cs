using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class Write
    {
        //获取该学生老师名字列表
        public List<Model.Teacher> getTeacherName(int StudentId)
        {
            DAL.Write dal = new DAL.Write();
            return dal.getTeacherName(StudentId);
        }
        //获取该学生所在班级的所有问卷
        public List<Model.QuestionnaireHead> getNaireList(int StudentId)
        {
            DAL.Write dal = new DAL.Write();
            return dal.getNaireList(StudentId);
        }
        //获取该问卷的表头
        public List<Model.QuestionnaireHead> getNaireHead(int HeadId)
        {
            DAL.Write dal = new DAL.Write();
            return dal.getNaireHead(HeadId);
        }
        //classId转ClassName
        public string getClassName(int ClassId)
        {
            DAL.Write dal = new DAL.Write();
            return dal.getClassName(ClassId);
        }

        //SchoolId转SchoolName
        public string getSchoolName(int SchoolId)
        {
            DAL.Write dal = new DAL.Write();
            return dal.getSchoolName(SchoolId);
        }

        //获取该问卷的表体

        public List<Model.Question> getNaireBody(int HeadId)
        {
            DAL.Question dal = new DAL.Question();
            return dal.showNaireQuestion(HeadId);
        }

        //TeacherId转TeacherName
        public string toTeacherName(int TeacherId)
        {
            DAL.Write dal = new DAL.Write();
            return dal.toTeacherName(TeacherId);
        }


        //保存填入的数据
        public int saveWriteResult(int StudentId, int QuestionnaireHeadId, int TeacherId, string tenScore)
        {
            int Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10;
            //拆分是个分数
            string[] sArray = Regex.Split(tenScore, ",", RegexOptions.IgnoreCase);
            Q1 = Convert.ToInt32(sArray[0]);
            Q2 = Convert.ToInt32(sArray[1]);
            Q3 = Convert.ToInt32(sArray[2]);
            Q4 = Convert.ToInt32(sArray[3]);
            Q5 = Convert.ToInt32(sArray[4]);
            Q6 = Convert.ToInt32(sArray[5]);
            Q7 = Convert.ToInt32(sArray[6]);
            Q8 = Convert.ToInt32(sArray[7]);
            Q9 = Convert.ToInt32(sArray[8]);
            Q10 = Convert.ToInt32(sArray[9]);



            DAL.Write dal = new DAL.Write();
            return dal.saveWriteResult(StudentId, QuestionnaireHeadId, TeacherId, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10);
        }
        //是否已经填写
        public int CompletedNaire(int TeacherId, int StudentId, int QuestionnaireHeadId)
        {
            DAL.Write dal = new DAL.Write();
            return dal.CompletedNaire(TeacherId, StudentId, QuestionnaireHeadId);
        }
    }
}