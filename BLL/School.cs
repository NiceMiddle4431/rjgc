using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class School
    {
        /// <summary>
        /// 获取数据库中学校基础信息
        /// </summary>
        public List<Model.School> GetList(int pageSize, int pageNumber,string SchoolName,string Address)
        {
            DAL.School dal = new DAL.School();
            return dal.GetList(pageSize, pageNumber,SchoolName,Address);
        }

        public List<Model.Class> GetClass(int SchoolId)
        {
            DAL.School dal = new DAL.School();
            return dal.GetClass(SchoolId);
        }
        public int AddClassSave(string AddClassName,int SchoolId)
        {
            DAL.School dal = new DAL.School();
            return dal.AddClassSave(AddClassName, SchoolId);
        }

        /// <summary>
        /// 获取数据库里学校的总数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            DAL.School dal = new DAL.School();
            return dal.GetCount();
        }

        public int EditClassSave(int ClassId,string EditClassName)
        {
            DAL.School dal = new DAL.School();
            return dal.EditClassSave(ClassId,EditClassName);
        }

        public int DeleteClass(string[] Ids)
        {
            //防止注入式漏洞
            string ids = string.Join(",", Ids);
            DAL.School dal = new DAL.School();
            return dal.DeleteClass(ids);
        }
        /// <summary>
        /// 添加学校信息保存
        /// </summary>
        /// <param name="AddSchoolName"></param>
        /// <param name="AddSchoolNum"></param>
        /// <returns></returns>
        public int AddSchoolSave(string AddSchoolName, string AddSchoolNum,string AddAddress)
        {
            DAL.School dal = new DAL.School();
            return dal.AddSchoolSave(AddSchoolName, AddSchoolNum, AddAddress);
        }
        /// <summary>
        /// 修改学校信息保存
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="EditSchoolNum"></param>
        /// <param name="EditSchoolName"></param>
        /// <returns></returns>
        public int EditSchoolSave(int SchoolId, string EditSchoolNum, string EditSchoolName,string EditAddress)
        {
            DAL.School dal = new DAL.School();
            return dal.EditSchoolSave(SchoolId, EditSchoolNum, EditSchoolName, EditAddress);
        }
        /// <summary>
        /// 删除学校
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int DeleteSchool(string[] Ids)
        {
            //防止注入式漏洞
            string ids = string.Join(",", Ids);
            DAL.School dal = new DAL.School();
            return dal.DeleteSchool(ids);
        }
    }
}
