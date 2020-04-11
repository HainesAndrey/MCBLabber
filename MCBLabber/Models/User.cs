using System;

namespace MCBLabber.Models
{
    public class User
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public uint RoleId { get; set; }
        public Role Role { get; set; }

        public User(uint roleId, string name, string password = null)
        {
            RoleId = roleId;
            Name = name;
            Password = password ?? new Random().Next(100000, 999999).ToString();
        }
    }
}
