using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
        private Recipe _currentRecipe;

        private List<Ingredient> _newIngredients = new();

        private List<Ingredient> ingredients = new();
        public DetailsWindow(int recipeId)
        {
            InitializeComponent();

            LockButton();

            GetRecipe(recipeId);

        }

        private void GetRecipe(int recipeId)
        {
            using(AppDbContext context = new())
            {
                _currentRecipe = new RecipeRepository(context).GetRecipe(recipeId);
                              
                txtRecipeName.Text = _currentRecipe.RecipeName;
                txtTimeName.Text = $"{_currentRecipe.RecipeTime.TotalMinutes.ToString()} minutes";
                txtTagName.Text = $"{_currentRecipe.Tag.Categories}";
                    
                foreach(Ingredient ingredient in _currentRecipe.Ingridients)
                {
                    ListViewItem item = new();
                    item.Content = $"{ingredient.Name} / {ingredient.Quantity}";
                    item.Tag = ingredient;
                    lvAddIngredient.Items.Add(item);
                }

            }
        }

        private void UpdateUi()
        {
            lvAddIngredient.Items.Clear();

            using(AppDbContext context = new())
            {
                foreach (Ingredient ingredient in _currentRecipe.Ingridients)
                {
                    ListViewItem item = new();
                    item.Content = $"{ingredient.Name} / {ingredient.Quantity}";
                    item.Tag = ingredient;
                    lvAddIngredient.Items.Add(item);
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
            btnSave.IsEnabled = true;
            txtRecipeName.IsEnabled = true;
            txtIngredientName.IsEnabled = true;
            txtQuantityName.IsEnabled = true;
            lvAddIngredient.IsEnabled=true;
        }

        // Spara eventuella ändringar
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new())
            {
                RecipeRepository recipeRepos = new(context);

                Recipe? dbRecipe = recipeRepos.GetRecipe(_currentRecipe.RecipeId);

                if (dbRecipe != null)
                {
                    _newIngredients.ForEach(i => dbRecipe.Ingridients.Add(i));
                }

                if (dbRecipe.RecipeName != txtRecipeName.Text.Trim())
                {
                    dbRecipe.RecipeName = txtRecipeName.Text.Trim(); ;
                }

                recipeRepos.UpdateRecipe(dbRecipe);
                context.SaveChanges(); 
            }

            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
            
        }

        // Ta bort en ingridiens
        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if(lvAddIngredient != null)
            {
                //ListViewItem selectedListViewItem = (ListViewItem)lvAddIngredient.Tag;
                //Ingredient ingredientToRemove = (Ingredient)selectedListViewItem.Tag;
                ListViewItem selectedListViewItem = lvAddIngredient.SelectedItem as ListViewItem;
                Ingredient ingredientToRemove = selectedListViewItem.Tag as Ingredient;

                using (AppDbContext context = new())
                {
                    new IngredientRepository(context).RemoveIngredient(ingredientToRemove);
                    context.SaveChanges();
                }

                UpdateUi();
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
                _newIngredients.Add(ingredient);
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
            btnRemoveIngredient.IsEnabled = true;
        }
    }

}

