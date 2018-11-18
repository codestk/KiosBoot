﻿
 
using KiosBoot.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KiosBoot.Views
{
    /// <summary>
    /// Interaction logic for StartMenuView.xaml
    /// </summary>
    public partial class StartMenuView : UserControl
    {
        public StartMenuView()
        {
            InitializeComponent();
        }

        private void Play_Clicked(object sender, RoutedEventArgs e)
        {
            var startMenu = DataContext as StartMenuViewModel;
        


            startMenu.StartNewGame(categoryBox.SelectedIndex);

         


        }
    }
}
