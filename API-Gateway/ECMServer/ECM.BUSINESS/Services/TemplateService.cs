using NHibernate;
using ECM.BUSINESS.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECM.BUSINESS.Services
{
    public class TemplateService : ITemplateService
    {
        public dynamic Delte<T>(ISession ss, Guid id)
        {
            try
            {
                ss.Delete(id);
                return new { status = 200, message = "success", data = new { } };
            }
            catch (Exception ex)
            {
                return new { status = 400, message = "error", data = ex.Message };
            }
        }

        public dynamic GetAll<T>(ISession ss)
        {
            try
            {
                var results = ss.Query<T>().ToList();
                return new { status = 200, message = "success", data = results };
            }
            catch (Exception ex)
            {
                return new { status = 400, message = "error", data = ex.Message };
            }
        }

        public dynamic GetById<T>(ISession ss, Guid id)
        {
            try
            {
                var result = ss.Get<T>(id);
                return new { status = 200, message = "success", data = result };
            }
            catch (Exception ex)
            {
                return new { status = 400, message = "error", data = ex.Message };
            }
        }
        public dynamic SaveOrUpdate<T>(ISession ss, T item)
        {
            try
            {
                ss.SaveOrUpdate(item);
                return new { status = 200, message = "success", data = new { } };
            }
            catch (Exception ex)
            {
                return new { status = 400, message = "error", data = ex.Message };
            }
        }

        public dynamic Validate<T>(ISession ss, T item)
        {
            throw new NotImplementedException();
        }
    }
}
