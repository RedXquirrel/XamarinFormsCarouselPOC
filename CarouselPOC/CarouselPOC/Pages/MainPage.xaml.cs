using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselPOC.ViewModels;
using Com.Xamtastic.Patterns.SmallestMvvm;
using Xamarin.Forms;

namespace CarouselPOC.Pages
{
    [ViewModelType(typeof(MainPageViewModel))]
    public partial class MainPage : PageBase
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
