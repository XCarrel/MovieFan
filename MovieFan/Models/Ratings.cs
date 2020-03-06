using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFan.Models
{
    [ModelMetadataType(typeof(RatingMetadata))]
    public partial class Ratings
    {
    }
    public partial class RatingMetadata
    {
        [Display(Name ="Rating")]
        public string Name { get; set; }

        [Display(Name = "Films dans ce rating")]
        public virtual ICollection<Movies> Movies { get; set; }
    }
}
