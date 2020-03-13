using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECM.BUSINESS.Infrastructures
{
    interface ITemplateService
    {
        bool Delte<T>(ISession ss, Guid id);
        dynamic Get<T>(ISession ss, int pageIndex, int itemsPerPage);
        T GetById<T>(ISession ss, Guid id);
        List<T> GetAll<T>(ISession ss);
        dynamic Validate<T>(ISession ss, T item);
        bool SaveOrUpdate<T>(ISession ss, T item);
    }
}
