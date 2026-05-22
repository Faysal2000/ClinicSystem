using Microsoft.AspNetCore.Identity;

namespace ClinicSystem.Models
{
    public class User  : IdentityUser<int>
    {

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public String? Address { get; set; }




        //public string Email { get; set; } = string.Empty;

        //public string Password { get; set; } = string.Empty;

        //public int RoleId { get; set; } 

        //public String? Phone { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public Role Role { get; set; } = null!;

        public Doctor? Doctor { get; set; }
         
    }
}
