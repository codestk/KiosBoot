using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Product : Page
    {
        public Product()
        {
            this.InitializeComponent();

            myStoryboard.Begin();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Menu), null, new DrillInNavigationTransitionInfo());
        }


        #region Effecft

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimationService.GetForCurrentView()
                    .PrepareToAnimate("backAnimation", circle);

                // Use the recommended configuration for back animation.
                //animation.Configuration = new DirectConnectedAnimationConfiguration();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //    EntranceAnimation.Edge = EdgeTransitionLocation.Top;

            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(circle);
            }


            base.OnNavigatedTo(e);
           
        }




        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            DemoRedirect();
        }

        void DemoRedirect()
        {
            this.Frame.Navigate(typeof(MasterDetailSelection), null, new DrillInNavigationTransitionInfo());

        }

    }
}
