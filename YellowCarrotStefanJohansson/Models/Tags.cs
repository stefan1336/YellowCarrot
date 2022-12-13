using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrotStefanJohansson.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        [MaxLength(20)]
        public string Categories { get; set; } = null!;

        public List<Recipe> Recipes { get; set; } = null!;


    }
}
