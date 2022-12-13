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



        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Spara receptet
            // Om inte alla textboxes är ifyllda ska ett varningsmedelande printas
            string newRecipeName = txtNewRecipeName.Text;
            int newTime = int.Parse(txtNewTime.Text);
            string newTag = ((ComboBoxItem)cbTag.SelectedItem).Content.ToString();

            Console.WriteLine(newTag);
            //string addedIngredient = lvAddIngredient.Tag as string;

            if(string.IsNullOrEmpty(newRecipeName) || string.IsNullOrEmpty(newTag))
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

                    RecipeRepository recipeRepos = new(context);

                    Recipe newRecipe = new();

                    Tag tag = new();
                    tag.Categories = newTag;

                    newRecipe.RecipeName = newRecipeName;
                    newRecipe.RecipeTime = TimeSpan.FromMinutes(newTime);

                    newRecipe.Tag= tag;

                    newRecipe.Ingridients = ingredients;
                    
                    recipeRepos.AddRecipe(newRecipe);
                    context.SaveChanges();

                }

                MainWindow mainWindow = new();
                mainWindow.Show();
                Close();
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
//List<Ingredient> ingredients = context.Ingredients.ToList();

//foreach(Ingredient ingredient in ingredients)
//{
//    ListViewItem item = new();

//    item.Content = ingredient.Name;
//    item.Content = ingredient.Quantity;
//    item.Tag = ingredient;
//}
//Recipe? recipe = new();

//foreach(Ingredient ingredients in recipe.Ingridients)
//{
//    lvAddIngredient.Tag = ingredients;
//}

//ListViewItem selectedListViewItems = (ListViewItem)lvAddIngredient.SelectedItems;

//Ingredient selectedIngredient = (Ingredient)selectedListViewItems.Tag;

//Recipe newRecipe = new();
