using System;
using System.ComponentModel.DataAnnotations;

namespace Serials.Core
{
    public class UserToLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
