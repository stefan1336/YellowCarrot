using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YellowCarrotStefanJohansson.Data;
using YellowCarrotStefanJohansson.Models;
using YellowCarrotStefanJohansson.Services;

namespace YellowCarrotStefanJohansson
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public DetailsWindow(int recipeId)
        {
            InitializeComponent();

            txtRecipeName.IsEnabled = false;
            txtIngredientName.IsEnabled = false;
            txtTagName.IsEnabled = false;
            txtTimeName.IsEnabled = false;

            GetRecipe(recipeId);
        }

        private void GetRecipe(int recipeId)
        {
            using(AppDbContext context = new())
            {
                Recipe? recipe = new RecipeRepository(context).GetRecipe(recipeId);

                txtRecipeName.Text = recipe.RecipeName;
                txtIngredientName.Text = recipe.Ingridients;



            }
        }

        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            // Låsa upp receptet för att möjliggöra ändringar
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Spara eventuella ändringar
        }

        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Ta bort en ingridiens
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Lägga till en ingridiens till ett recept
        }
    }
}
