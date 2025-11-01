using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineCinemaApp.Models;

namespace OnlineCinemaApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new CinemaContext();

            // create DB if not exists
            db.Database.EnsureCreated();

            // seed if empty
            if (!db.Users.Any())
            {
                Console.WriteLine("Seeding database with test data (15+ records per table)...");

                // Genres (15)
                var genres = Enumerable.Range(1, 15)
                    .Select(i => new Genre { Name = $"Genre {i}" })
                    .ToArray();
                db.Genres.AddRange(genres);

                // Actors (15)
                var actors = Enumerable.Range(1, 15)
                    .Select(i => new Actor { FullName = $"Actor {i}" })
                    .ToArray();
                db.Actors.AddRange(actors);

                // Movies (15) - assign genres round-robin
                var movies = Enumerable.Range(1, 15)
                    .Select(i => new Movie { Title = $"Movie {i}", ReleaseYear = 2000 + (i % 20), Genre = genres[(i - 1) % genres.Length] })
                    .ToArray();
                db.Movies.AddRange(movies);

                // MovieActors (many-to-many) - each movie gets 3 actors
                foreach (var m in movies)
                {
                    var idx = (m.Id == 0) ? Array.IndexOf(movies, m) : m.Id - 1;
                    // pick 3 actors by index
                    for (int k = 0; k < 3; k++)
                    {
                        var actor = actors[( (Array.IndexOf(movies, m) + k) ) % actors.Length];
                        db.MovieActors.Add(new MovieActor { Movie = m, Actor = actor });
                    }
                }

                // Users (15)
                var users = Enumerable.Range(1, 15)
                    .Select(i => new User { Username = $"user{i}", Email = $"user{i}@example.com" })
                    .ToArray();
                db.Users.AddRange(users);
                db.SaveChanges(); // save to get generated IDs

                // UserProfiles (15) - one-to-one
                for (int i = 0; i < users.Length; i++)
                {
                    var u = users[i];
                    db.UserProfiles.Add(new UserProfile
                    {
                        UserId = u.Id,
                        FullName = $"User Fullname {i + 1}",
                        BirthDate = new DateTime(1990, 1, 1).AddDays(i * 30)
                    });
                }

                db.SaveChanges();
                Console.WriteLine("Seeding finished."); 
            }
            else
            {
                Console.WriteLine("Database already seeded."); 
            }

            // show some data
            var list = db.Movies.Include(m => m.Genre).Take(20).ToList();
            Console.WriteLine($"Movies in DB: {db.Movies.Count()}"); 
            foreach (var m in list)
            {
                Console.WriteLine($"{m.Title} ({m.ReleaseYear}) - Genre: {m.Genre?.Name}"); 
            }
        }
    }
}
