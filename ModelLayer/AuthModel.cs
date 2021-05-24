using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class AuthModel : IdentityUser
    {
        public string FullName { get; set; }
    }
}
