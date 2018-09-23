using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class QuestionnaireHead
    {
        DAL.QuestionnaireHead dal = new DAL.QuestionnaireHead();
        public List<Model.QuestionnaireHead> GetList(int CurrentPage, int PageSize, int TeacherId, string keyWord)
        {
            return dal.GetList(CurrentPage, PageSize, TeacherId, keyWord);
        }
        public int GetCount(int TeacherId)
        {

            return dal.GetCount(TeacherId);
        }
        public Model.QuestionnaireHead GetModel(int HeadId, int TeacherId)
        {
            return dal.GetModel(HeadId,TeacherId);
        }
        public int GetWritenTimes(int HeadId,int TeacherId)
        {
            return dal.GetWritenTimes(HeadId,TeacherId);
        }
    }
}
