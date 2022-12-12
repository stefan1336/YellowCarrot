using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();

            using(AppDbContext context = new())
            {
           
                
            }
        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Spara receptet
            // Om inte alla textboxes är ifyllda ska ett varningsmedelande printas
            string newRecipeName = txtNewRecipeName.Text;
            int newTime = int.Parse(txtNewTime.Text);
            string newTag = txtNewTag.Text;

            if(string.IsNullOrEmpty(newRecipeName) && string.IsNullOrEmpty(newTag))
            {
                MessageBox.Show("Please make a full registration of your new recipe");

                if(newTime == null)
                {
                    MessageBox.Show("Please enter timespan for your recipe");
                }
            }
            else
            {
                using (AppDbContext context = new())
                {
                    Recipe newRecipe = new();
                    var recipeRepo = new RecipeRepository(context);

                    newRecipe.RecipeName = newRecipeName;
                    newRecipe.RecipeTime = TimeSpan.FromMinutes(newTime);
                    newRecipe.Tag = newTag;


                    ListViewItem? selectedItem = lvAddIngredient.SelectedItems as ListViewItem;

                    recipeRepo.AddRecipe(newRecipe);

                }
            }


            
        }

        private void btnAddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Lägga till ingrediens till listan
            // Lägga till en ingridiens till ett recept
            
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

                lvAddIngredient.Items.Add($"{ingredient.Name} / {ingredient.Quantity}");
            }
        }
    }
}
