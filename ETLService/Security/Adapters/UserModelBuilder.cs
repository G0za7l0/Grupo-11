using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPCORE.Security;
using ETLService.Security.Model;

namespace ETLService.Security.Adapters
{
    public class UserModelBuilder
    {
        public static UserModel build(Security_Users user)
        {
            return new UserModel
            {
                username = user.Mail,
                password = user.Password,
                password_Expiration_Date = user.Password_Expiration_Date
            };
        }

        public static  Security_Users build(UserModel user)
        {
            return new Security_Users
            {
                Mail = user.username,
                Password = user.password,
                Password_Expiration_Date = user.password_Expiration_Date
            };
        }
    }
}