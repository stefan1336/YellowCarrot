using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrotStefanJohansson.Data;
using YellowCarrotStefanJohansson.Models;

namespace YellowCarrotStefanJohansson.Services
{
    internal class RecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get single recipe from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Recipe? GetRecipe(int id)
        {
            return _context.Recipes.Include(r => r.Ingridients).FirstOrDefault(r => r.RecipeId == id);
        }

        public List<Recipe> GetRecipes()
        {
            return _context.Recipes.Include(r => r.Ingridients).ToList();
        }
    }
}
