using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Com.Xamtastic.Xamarin.Forms.CarouselView.ViewModels
{
    public class CarouselControlViewModel : ViewModelBase
    {
        public CarouselControlViewModel()
        {
        }

        IEnumerable<object> _views;
        /// <summary>
        /// 
        /// Note: The CarouselView binds the ItemsSource property of the ViewLayoutManager, 
        /// and the ItemsSource property of the ViewLayoutManager,
        /// to this property in the method CreateViewsCarousel().
        /// </summary>
        public IEnumerable<object> Views
        {
            get
            {
                return _views;
            }
            set
            {
                SetObservableProperty(ref _views, value);
                CurrentView = Views.ToList().FirstOrDefault();
            }
        }

        object _currentView;
        /// <summary>
        /// 
        /// 
        /// Note: The CarouselView binds the SelectedItem property of the ViewLayoutManager, 
        /// and the ItemsSource property of the ViewLayoutManager,
        /// to this property in the method CreateViewsCarousel().
        /// </summary>
        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                SetObservableProperty(ref _currentView, value);
            }
        }

        private Color _pagingIndicatorSelectedColor;
        public Color PagingIndicatorSelectedColor
        {
            get { return _pagingIndicatorSelectedColor; }
            set
            {
                SetObservableProperty(ref _pagingIndicatorSelectedColor, value);
            }
        }

        private Color _pagingIndicatorUnselectedColor;
        public Color PagingIndicatorUnselectedColor
        {
            get { return _pagingIndicatorUnselectedColor; }
            set
            {
                SetObservableProperty(ref _pagingIndicatorUnselectedColor, value);
            }
        }

        private double _pagingIndicatorsDiscSize;
        public double PagingIndicatorsDiscSize
        {
            get { return _pagingIndicatorsDiscSize; }
            set
            {
                SetObservableProperty(ref _pagingIndicatorsDiscSize, value);
            }
        }



    }
}
