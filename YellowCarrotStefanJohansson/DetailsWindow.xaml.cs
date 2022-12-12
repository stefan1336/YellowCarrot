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
            txtQuantityName.IsEnabled = false;
            txtTagName.IsEnabled = false;
            txtTimeName.IsEnabled = false;
            btnAddIngredient.IsEnabled = false;
            btnRemoveIngredient.IsEnabled = false;
            btnSave.IsEnabled = false;

            GetRecipe(recipeId);
        }

        private void GetRecipe(int recipeId)
        {
            using(AppDbContext context = new())
            {
                Recipe? recipe = new RecipeRepository(context).GetRecipe(recipeId);

                txtRecipeName.Text = recipe.RecipeName;
                txtTimeName.Text = $"{recipe.RecipeTime.TotalMinutes.ToString()} minutes";

                foreach(Ingredient ingredient in recipe.Ingridients)
                {
                    lvAddIngredient.Items.Add($"{ingredient.Name} / {ingredient.Quantity}");
                }

            }
        }

        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            // Låser upp knappar och textboxes för att möjliggöra ändringar
            
            btnAddIngredient.IsEnabled = true;
            btnRemoveIngredient.IsEnabled = true;
            btnSave.IsEnabled = true;

            txtTagName.IsEnabled = true;
            txtRecipeName.IsEnabled = true;
            txtIngredientName.IsEnabled = true;
            txtQuantityName.IsEnabled = true;



        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Spara eventuella ändringar
            string newRecipeName = txtRecipeName.Text;
            string listViewItem = lvAddIngredient.SelectedItems.ToString();
            string newTag = txtTagName.Text;
        }

        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Ta bort en ingridiens
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Lägga till en ingridiens till ett recept

            string newIngredientName = txtIngredientName.Text;
            string newTagName = txtQuantityName.Text;

            if(string.IsNullOrEmpty(newIngredientName) && string.IsNullOrEmpty(newTagName))
            {
                MessageBox.Show("Please make a full uppdate");
            }
            else
            {
                Ingredient ingredient = new();

                ingredient.Name = newIngredientName;
                ingredient.Quantity = newTagName;

                lvAddIngredient.Items.Add($"{ingredient.Name} / {ingredient.Quantity}");
            }
       
        }

    }
}
