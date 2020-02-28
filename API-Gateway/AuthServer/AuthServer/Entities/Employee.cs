using System;

namespace AuthServer.Entities
{
    public class Employee
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Birthday { get; set; }
        public virtual int Gender { get; set; }
        public virtual string Username { get; set; }
        private string _password;
        public virtual string Password
        {
            get { return _password; }
            set
            {
                _password = value.Length == 128 ? value : ChangeSha512(value);
            }
        }
        public static String ChangeSha512(String strSoure)
        {
            System.Security.Cryptography.SHA512CryptoServiceProvider x = new System.Security.Cryptography.SHA512CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(strSoure);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        public virtual string Email { get; set; }
        public virtual bool IsLeader { get; set; }
        public virtual DateTime Create_at { get; set; }
        public virtual bool Status { get; set; }
        public virtual string Image { get; set; }
        public virtual Guid PermissionGroupId { get; set; }
    }
}
