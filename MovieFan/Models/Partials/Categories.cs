using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFan.Models
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Categories
    {
    }
    public partial class CategoryMetadata
    {
        [Display(Name ="Catégorie")]
        public string Name { get; set; }

        [Display(Name = "Filma dans cette catégorie")]
        public virtual ICollection<Movies> Movies { get; set; }
    }
}
