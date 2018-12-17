using KiosBoot.Models;
using KiosBoot.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views.Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class gameLv16 : Page
    {
        public gameLv16()
        {
            this.InitializeComponent();
            DataContext = new StartMenuViewModel(this);



            ScrollSEt();



        }


        private void ScrollSEt()
        {

            ObservableCollection<ScrollInfo> sc = new ObservableCollection<ScrollInfo>();
            ScrollInfo s1 = new ScrollInfo { Scroll = 99, Name = "A", Image = "ms-appx:///Assets/black_background.jpg" };
            ScrollInfo s2 = new ScrollInfo { Scroll = 2, Name = "B", Image = "ms-appx:///Assets/black_background.jpg" };
            ScrollInfo s3 = new ScrollInfo { Scroll = 1, Name = "C", Image = "ms-appx:///Assets/black_background.jpg" };

            sc.Add(s1);
            sc.Add(s2);
            sc.Add(s3);

            var ScrollInfoReorder = sc.OrderByDescending(a => a.Scroll);

            int i = 1;
            foreach (var item in ScrollInfoReorder)
            {
                item.ranking = i;

                i++;
            }
            //var query = from item in GetContacts(numberOfContacts)
            //            group item by item.LastName.Substring(0, 1).ToUpper() into g
            //            orderby g.Key
            //            select new { GroupName = g.Key, Items = g };

            Scrollinfo.ItemsSource = ScrollInfoReorder;
            //  Frame.Navigate(typeof(MediaPlayerPage), null, new SuppressNavigationTransitionInfo());
            //itemGridView.ItemsSource = new StartMenuViewModel(this); 


        }


        private void Play_Clicked(object sender, RoutedEventArgs e)
        {
            var startMenu = DataContext as StartMenuViewModel;

            //startMenu.StartNewGame(categoryBox.SelectedIndex);




            Menu.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Visible;



        }



        private void Slide_Clicked(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;
            var button = sender as Button;
            game.ClickedSlide(button.DataContext);



        }

        private void PlayAgain_C(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;
            game.Restart();
        }
    }
}
