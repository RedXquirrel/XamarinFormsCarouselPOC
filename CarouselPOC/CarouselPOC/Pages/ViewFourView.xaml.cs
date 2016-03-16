using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CarouselPOC.Pages
{
    public partial class ViewFourView : ContentView
    {
        public ViewFourView()
        {
            InitializeComponent();

            InputEntry.TextChanged += (s, e) =>
            {
                InputLabel.Text = InputEntry.Text;
                ClearButton.IsEnabled = InputEntry.Text.Length != 0;
            };

            ClearButton.Clicked += (s, e) =>
            {
                InputEntry.Text = string.Empty;
                InputLabel.Text = "Cleared!";
            };

            ClearButton.IsEnabled = false;
        }
    }
}
