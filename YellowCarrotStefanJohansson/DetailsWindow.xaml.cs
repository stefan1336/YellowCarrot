using Azure;
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
        private List<Ingredient> ingredients = new();
        public DetailsWindow(int recipeId)
        {
            InitializeComponent();

            LockButton();

            GetRecipe(recipeId);
            //using (AppDbContext context = new())
            //{


            //}


        }
        // Hämtar mina recept och printar ut dom i mitt DetailsWindow
        private void GetRecipe(int recipeId)
        {
            using(AppDbContext context = new())
            {
                Recipe? recipe = new RecipeRepository(context).GetRecipe(recipeId);

                txtRecipeName.Text = recipe.RecipeName;
                txtTimeName.Text = $"{recipe.RecipeTime.TotalMinutes.ToString()} minutes";
                txtTagName.Text = $"{recipe.Tag.Categories}";
                    
                foreach(Ingredient ingredient in recipe.Ingridients)
                {
                    lvAddIngredient.Items.Add($"{ingredient.Name} / {ingredient.Quantity}");
                }

            }
        }

        private void LockButton()
        {
            txtRecipeName.IsEnabled = false;
            txtIngredientName.IsEnabled = false;
            txtQuantityName.IsEnabled = false;
            txtTagName.IsEnabled = false;
            txtTimeName.IsEnabled = false;
            btnAddIngredient.IsEnabled = false;
            btnRemoveIngredient.IsEnabled = false;
            btnSave.IsEnabled = false;
            lvAddIngredient.IsEnabled = false;
        }
        // Låser upp knappar och textboxes för att möjliggöra ändringar
        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            
            
            btnAddIngredient.IsEnabled = true;
            btnRemoveIngredient.IsEnabled = true;
            btnSave.IsEnabled = true;
            txtTimeName.IsEnabled = true;
            txtTagName.IsEnabled = true;
            txtRecipeName.IsEnabled = true;
            txtIngredientName.IsEnabled = true;
            txtQuantityName.IsEnabled = true;

        }

        // Spara eventuella ändringar
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            string newRecipeName = txtRecipeName.Text.Trim();
            int newTime = int.Parse(txtTimeName.Text);
            string newTag = txtTagName.Text.Trim();

            if (string.IsNullOrEmpty(newRecipeName))
            {

                using (AppDbContext context = new())
                {
                    Recipe updateRecipe = new();
                    Tag updateTag = new();

                    updateRecipe.RecipeName = newRecipeName;
                    updateRecipe.RecipeTime = TimeSpan.FromMinutes(newTime);
                    updateRecipe.Tag = updateTag;
                    updateTag.Categories = newTag;
                    updateRecipe.Ingridients = ingredients;

                    context.Recipes.Update(updateRecipe);
                    context.SaveChanges();
                }

                MainWindow mainWindow = new();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please make a full details change");
            }

        }

        // Ta bort en ingridiens
        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if(lvAddIngredient.SelectedItem != null)
            {
                ListViewItem selectedListViewItem = lvAddIngredient.SelectedItem as ListViewItem;

                Ingredient ingredientToRemove = selectedListViewItem.Tag as Ingredient;

                using (AppDbContext context = new())
                {
                    new RecipeRepository(context).RemoveIngredient(ingredientToRemove);
                    context.SaveChanges();
                }

            }
            else
            {
                MessageBox.Show("Please pick an ingredient to remove");
            }

                
  
        }

        // Lägga till en ingridiens till ett recept
        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
           

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

        // Återgå till tidigare fönster
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        private void lvAddIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
