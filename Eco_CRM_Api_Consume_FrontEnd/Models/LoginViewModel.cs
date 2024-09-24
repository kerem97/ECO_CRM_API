using System.ComponentModel.DataAnnotations;

namespace Eco_CRM_Api_Consume_FrontEnd.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
