using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrotStefanJohansson.Models
{
    public class Ingredient
    {
        // Todo [MaxLength(50)] till namn
        public int IngredientId { get; set; }
        [MaxLength(20)]
        public string Name { get; set; } = null!;
        [MaxLength(20)]
        public string Quantity { get; set; } = null!;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

    }
}
