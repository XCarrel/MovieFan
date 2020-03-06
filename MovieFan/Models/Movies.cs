﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieFan.Models
{
    [ModelMetadataType(typeof(MoviesMetadata))]
    public partial class Movies
    {
        [Display(Name = "Apprécié de")]
        public List<string> Likers
        {
            get => this.UserLikeMovie.Select(u => u.User.FullName).ToList();
        }

        [NotMapped]
        [DataType(DataType.Date)]
        [Display(Name = "Date de la première")]
        public DateTime? ReleaseDate
        {
            get => this.Release;
            set => this.Release = value;
        }

        public class MoviesMetadata
        {
            [Display(Name ="Résumé")]
            public string Synopsis { get; set; }
            [Display(Name = "Titre")]
            public string Title { get; set; }
            public DateTime? Release { get; set; }

        }
    }
}
