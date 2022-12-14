using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        private List<Ingredient> ingredients = new();

       
        public AddRecipeWindow()
        {
            InitializeComponent();

            UpdateUi();
        }

        // Uppdatera ui för att printa ut mina ingredienser
        private void UpdateUi()
        {
            cbTag.Items.Clear();

            using (AppDbContext context = new())
            {

                List<Tag> tags = context.Tags.ToList();

                foreach (Tag tag in tags)
                {
                    ComboBoxItem item = new();

                    item.Content = $"{tag.Categories}";
                    item.Tag = tag;

                    cbTag.Items.Add(item);
                }

            }
        }


        // Spara receptet
        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            
            // Om inte alla textboxes är ifyllda ska ett varningsmedelande printas
            string newRecipeName = txtNewRecipeName.Text;
            int newTime = int.Parse(txtNewTime.Text);
            Tag? newTag = (Tag)((ComboBoxItem)cbTag.SelectedItem).Tag;
            
            if(string.IsNullOrEmpty(newRecipeName) || newTag == null)
            {
                MessageBox.Show("Please make a full registration of your new recipe");

                if(newTime == 0)
                {
                    MessageBox.Show("Please enter timespan for your recipe");
                }
            }
            else
            {              
                using (AppDbContext context = new())
                {
                    RecipeRepository recipeRepos = new(context);
                    IngredientRepository ingredientRepos = new(context);

                    Recipe newRecipe = new();

                    newRecipe.RecipeName = newRecipeName;
                    newRecipe.RecipeTime = TimeSpan.FromMinutes(newTime);

                    newRecipe.TagId = newTag.TagId;

                    newRecipe.Ingridients = ingredients;
                    
                    recipeRepos.AddRecipe(newRecipe);
                    context.SaveChanges();

                }

                MainWindow mainWindow = new();
                mainWindow.Show();
                Close();
            }
       
        }
        // Lägga till ingrediens till listan
        private void btnAddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            
            string newIngredientName = txtNewIngredientName.Text;
            string newQuantityName = txtNewQuantity.Text;

            if (string.IsNullOrEmpty(newIngredientName) && string.IsNullOrEmpty(newQuantityName))
            {
                MessageBox.Show("Please enter all ingredient details");
            }
            else
            {
                Ingredient ingredient = new();

                ingredient.Name = newIngredientName;
                ingredient.Quantity = newQuantityName;
                ingredients.Add(ingredient);
                
                lvAddIngredient.Items.Add($"{ingredient.Name} / {ingredient.Quantity}");
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }

    //private bool CheckInputs()
    //{

    //}
}

