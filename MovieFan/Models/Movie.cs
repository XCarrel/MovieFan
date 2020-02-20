using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFan.Models
{
    public class Movie
    {
        private int id;
        private string title;
        private string synopsis;

        public Movie(int id, string title, string synopsis)
        {
            this.id = id;
            this.title = title;
            this.synopsis = synopsis;
        }

        public string Title { get => this.title; }

        public int Id { get => this.id; }

        public string Synopsis { get => this.synopsis; set => this.synopsis = value; }
    }
}
