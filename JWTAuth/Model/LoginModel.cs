using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuth.Model
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public static bool Verify(LoginModel model)
        {
            return (model.Email == "akash@gmail.com") && (model.Password == "123");

        }
    }
}
