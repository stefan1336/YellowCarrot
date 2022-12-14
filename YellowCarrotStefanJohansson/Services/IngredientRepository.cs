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
    internal class IngredientRepository
    {
        private readonly AppDbContext _context;
        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public void RemoveIngredient(Ingredient ingredientToRemove)
        {
            _context.Ingredients.Remove(ingredientToRemove);
        }

        public void AddIngredient(Ingredient addIngredient)
        {
            _context.Ingredients.Add(addIngredient);
        }

        public Ingredient? GetIngredient(int id)
        {
            return _context.Ingredients.FirstOrDefault(r => r.RecipeId == id);
        }
    }
}
