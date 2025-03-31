using System;
using System.Collections.Generic;

namespace gestionproduit.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Released { get; set; }
        public string background_image { get; set; }
        public double Rating { get; set; }
        public int RatingsCount { get; set; }
        public int Metacritic { get; set; }
        public List<Genre> Genres { get; set; }
        public List<PlatformInfo> Platforms { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PlatformInfo
    {
        public Platform Platform { get; set; }
    }

    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ApiResponse
    {
        public List<Game> Results { get; set; }
    }
}
