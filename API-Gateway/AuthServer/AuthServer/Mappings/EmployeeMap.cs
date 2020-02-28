using AuthServer.Entities;
using FluentNHibernate.Mapping;

namespace AuthServer.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("[hrm.Employee]");
            Id(p => p.Id);
            Map(p => p.Name);
            Map(p => p.Code);
            Map(p => p.Birthday);
            Map(p => p.Gender);
            Map(p => p.Username);
            Map(p => p.Password);
            Map(p => p.Email);
            Map(p => p.IsLeader);
            Map(p => p.Create_at);
            Map(p => p.Status);
            Map(p => p.Image);
            Map(p => p.PermissionGroupId);
        }
    }
}
