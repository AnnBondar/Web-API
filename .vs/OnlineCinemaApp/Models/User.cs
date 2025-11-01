using System.Collections.Generic;

namespace OnlineCinemaApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";

        public UserProfile? Profile { get; set; }
    }
}
