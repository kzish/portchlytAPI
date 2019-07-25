using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portchlytAPI.Models
{
    public class mUsers
    {
        public string _id { get; set; } = Guid.NewGuid().ToString();
        public string email { get; set; }
        public string password { get; set; }
        public UserRole uer_role { get; set; }
        public void hash_password() {
            this.password = globals.getmd5(password);
        }
    }

    public enum UserRole {
        admin,
        back_office

    }

}
