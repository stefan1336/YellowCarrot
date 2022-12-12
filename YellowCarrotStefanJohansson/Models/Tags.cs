using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrotStefanJohansson.Models
{
    internal class Tag
    {
        public int TagId { get; set; }
        public string Categories { get; set; } = null!;

        public List<Recipe> Recipes { get; set; } = null!;
    }
}
