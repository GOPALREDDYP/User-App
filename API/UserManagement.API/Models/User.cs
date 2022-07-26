using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Models
{
    public class User
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
      
    }
}
