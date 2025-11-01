using System.Collections.Generic;

namespace OnlineCinemaApp.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";

        public List<MovieActor> MovieActors { get; set; } = new();
    }
}
