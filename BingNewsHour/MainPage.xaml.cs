using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BingNewsHour.Resources;

namespace BingNewsHour
{
    public partial class MainPage : PhoneApplicationPage
    {
        public List<Constants.Category> CategorySelected;
        public List<string> categoryList = new List<string>();
        // Constructor
        public MainPage()
        {
            InitializeComponent(); 
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            this.CategorySelected = new List<Constants.Category>();
        }

        private void Button_Go_Click(object sender, RoutedEventArgs e)
        {
            if (this.chkTech.IsChecked == true)
            {
                CategorySelected.Add(Constants.Category.Technology);
                categoryList.Add("2");
            }
            if (this.chkPol.IsChecked == true)
            {
                CategorySelected.Add(Constants.Category.Politics);
                categoryList.Add("3");
            }
            if (this.chkEnt.IsChecked == true)
            {
                CategorySelected.Add(Constants.Category.Entertainment);
                categoryList.Add("4");
            }
            if (this.chkFin.IsChecked == true)
            {
                CategorySelected.Add(Constants.Category.Finance);
                categoryList.Add("5");
            }
            if (this.chkSci.IsChecked == true)
            {
                CategorySelected.Add(Constants.Category.Science);
                categoryList.Add("6");
            }
            if (this.chkSport.IsChecked == true)
            {
                CategorySelected.Add(Constants.Category.Sports);
                categoryList.Add("7");
            }
            if (categoryList.Count == 0)
            {
                MessageBox.Show("No Category Selected, Let me read out Trending News");
                CategorySelected.Add(Constants.Category.TopNews);
                categoryList.Add("1");
            }
            
            string idList = String.Join(",",categoryList);

            NavigationService.Navigate(new Uri(String.Format("/ContentDisplayPage.xaml?ids={0}",idList), UriKind.Relative));
        }

        private void chk_Checked(object sender, RoutedEventArgs e)
        {
            Go.Content = "Go!";
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}