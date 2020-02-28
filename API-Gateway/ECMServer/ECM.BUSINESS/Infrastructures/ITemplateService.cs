using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECM.BUSINESS.Infrastructures
{
    interface ITemplateService
    {
        dynamic GetById<T>(ISession ss, Guid id);
        dynamic GetAll<T>(ISession ss);
        dynamic Validate<T>(ISession ss, T item);
        dynamic SaveOrUpdate<T>(ISession ss, T item);
        dynamic Delte<T>(ISession ss, Guid id);
    }
}
