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
    internal class RecipeRepository : IDisposable
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
            return _context.Recipes.Include(r => r.Ingridients).Include(r=>r.Tag).FirstOrDefault(r => r.RecipeId == id);
        }

        public List<Recipe> GetRecipes()
        {
            return _context.Recipes.Include(r => r.Ingridients).ToList();
        }

        public void AddRecipe(Recipe addrecipe)
        {
            _context.Recipes.Add(addrecipe);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void UpdateRecipe(Recipe updateRecipe)
        {
            _context.Recipes.Update(updateRecipe);
        }

        public void RemoveRecipe(Recipe recipeToRemove)
        {
            _context.Recipes.Remove(recipeToRemove);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
