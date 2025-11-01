using System.Collections.Generic;

namespace OnlineCinemaApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int ReleaseYear { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public List<MovieActor> MovieActors { get; set; } = new();
    }
}
