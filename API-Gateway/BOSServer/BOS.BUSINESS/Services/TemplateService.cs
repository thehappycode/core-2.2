using NHibernate;
using BOS.BUSINESS.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOS.BUSINESS.Services
{
    public class TemplateService : ITemplateService
    {
        public bool Delete<T>(ISession ss, Guid id)
        {
            try
            {
                ss.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public dynamic Get<T>(ISession ss, int pageIndex, int itemsPerPage)
        {
            dynamic result = null;
            var datas = ss.Query<T>().ToList();
            var count = datas.Count();
            var total = count % itemsPerPage == 0 ? count / itemsPerPage : count / itemsPerPage + 1;
            var data = datas.Skip(itemsPerPage * pageIndex).Take(itemsPerPage);
            result.total = total;
            result.data = data;
            return result;
        }
        public List<T> GetAll<T>(ISession ss)
        {
            var results = ss.Query<T>().ToList();
            return results;
        }
        public T GetById<T>(ISession ss, Guid id)
        {
            var result = ss.Get<T>(id);
            return result;

        }
        public dynamic Validate<T>(ISession ss, T item)
        {
            throw new NotImplementedException();
        }
        public bool SaveOrUpdate<T>(ISession ss, T item)
        {
            try
            {
                ss.SaveOrUpdate(item);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
