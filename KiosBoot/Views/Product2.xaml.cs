using Microsoft.Toolkit.Uwp.UI.Controls;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Product2 : Page
    {
        public Product2()
        {
            this.InitializeComponent();


            //obit.ItemsSource = items;


            
        }


        private void CreateControls()
        {
            // Initialize a new Button control
            Button button1 = new Button();

            // Set button content
            button1.Content = "Click Me";

            // Set button height and width
            button1.Width = 200;
            button1.Height = 75;

            // Add a click event
            button1.Click += Button1_Click;

            // Add the newly created button control to the stack panel
      
        }

        // Handle the button click event
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            b.Content = "Clicked";
        }












    }
}
