﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFan.Models
{
    public class Movie
    {
        private int id;
        private string title;

        public Movie(int id, string title)
        {
            this.id = id;
            this.title = title;
        }

        public string Title { get => this.title; }

        public int Id { get => this.id; }
    }
}
