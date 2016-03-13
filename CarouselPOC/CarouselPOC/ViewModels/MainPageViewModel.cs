using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Xamtastic.Patterns.SmallestMvvm;

namespace CarouselPOC.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private List<object> _carouselViewModels;

        public List<object> CarouselViewModels
        {
            get { return _carouselViewModels; }
            set { _carouselViewModels = value; RaisePropertyChanged(); }
        }

        public MainPageViewModel()
        {
            CarouselViewModels = new List<object>
            {
                new ViewOneViewModel(),
                new ViewTwoViewModel(),
                new ViewThreeViewModel(),
                new ViewFourViewModel(),
            };
        }
    }
}
