using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CarouselPOC.Pages
{
    public partial class ViewTwoView : ContentView
    {
        public ViewTwoView()
        {
            InitializeComponent();

            InputEntry.TextChanged += (s, e) =>
            {
                InputLabel.Text = InputEntry.Text;
            };
        }
    }
}
