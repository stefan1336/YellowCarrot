using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrotStefanJohansson.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [MaxLength(30)]
        public string RecipeName { get; set; } = null!;

        public TimeSpan RecipeTime { get; set; } 

        public List<Ingredient> Ingridients { get; set; } = new();
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
