using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todoapp.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; } = "";
        public string Otp { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
    }
}
