using System.ComponentModel.DataAnnotations;

namespace PagesCommon.DTOs
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
