using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CarouselPOC.Pages
{
    public partial class ViewThreeView : ContentView
    {
        public ViewThreeView()
        {
            InitializeComponent();

            InputEntry.TextChanged += (s, e) =>
            {
                InputLabel.Text = InputEntry.Text;
            };
        }
    }
}
