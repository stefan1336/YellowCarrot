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
using System.Windows.Navigation;
using System.Windows.Shapes;
using YellowCarrotStefanJohansson.Data;
using YellowCarrotStefanJohansson.Models;

namespace YellowCarrotStefanJohansson
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btnDetails.IsEnabled = false;
            btnDeleteRecipe.IsEnabled = false;

            UpdateUi();

        }

        // Uppdatera mitt ui för recepten som finns
        private void UpdateUi()
        {
            lvRecipe.Items.Clear();

            using(AppDbContext context = new())
            {
                List<Recipe> recipes = new RecipeRepository(context).GetRecipes();

                foreach(Recipe recipe in recipes)
                {
                    ListViewItem item = new();

                    item.Content =  recipe.RecipeName;
                    item.Tag = recipe;

                    lvRecipe.Items.Add(item);
                }
            }
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Lätta till ett recipe
            // öppna fönstret för att lägga till ett recept
            AddRecipeWindow addRecipeWindow = new();
            addRecipeWindow.Show();
            // Close();
        }


        // Går till detailswindow. skickar med valt recept för att visa fullständig information om det valda receptet.
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if(lvRecipe.SelectedItem != null)
            {
                    ListViewItem selectedItem = (ListViewItem)lvRecipe.SelectedItem;

                    Recipe selectedRecipe =(Recipe)selectedItem.Tag;

                    // Om ett recept är valt ska knappen synas
                    DetailsWindow detailsWindow = new(selectedRecipe.RecipeId);
                    detailsWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a recipe");
            }
   
            // Close();
        }

        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Om ett recept är valt ska det gå att deleta
            ButtonError();
        }

        // Om inget recept är valt ska ett varningsmedelande printas Ja/Nej 

        private void ButtonError()
        {
            if(lvRecipe.SelectedItems != null)
            {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to remove this recipe?", "Are you sure?", MessageBoxButton.YesNo);

                if (dr == MessageBoxResult.Yes)
                {
                    // Ta bort receptet
                    ListViewItem selectedListViewItem = lvRecipe.SelectedItem as ListViewItem;

                    Recipe selectedRecipe = selectedListViewItem.Tag as Recipe;

                    using (AppDbContext context = new())
                    {
                        context.Recipes.Remove(selectedRecipe);
                        context.SaveChanges();
                    }
                    UpdateUi();
                }
                else
                {
                    // Gör ingenting

                }
            }
            else
            {
                MessageBox.Show("Please pick a recipe to delete");
            }


        }
    }
}
