using Microsoft.AspNetCore.Identity;

namespace ClinicSystem.Models;

public class Role : IdentityUser<int>
{ 
        //public int Id { get; set; }

        //public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public ICollection<User> Users { get; set; } = new List<User>();

    }

