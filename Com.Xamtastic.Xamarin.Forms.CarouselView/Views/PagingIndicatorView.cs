using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;

namespace Com.Xamtastic.Xamarin.Forms.CarouselView.Views
{
    public class PagingIndicatorView : StackLayout
    {
        int dotCount = 1;
        int _selectedIndex;

        public Color DotSelectedColor { get; set; }
        public Color DotUnselectedColor { get; set; }
        public double DotSize { get; set; }

        public PagingIndicatorView()
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.Center;
            Orientation = StackOrientation.Horizontal;
            DotSelectedColor = Color.Black;
        }

        private void CreateDot()
        {
            //Make one button and add it to the dotLayout
            var dot = new Button
            {
                BorderRadius = Convert.ToInt32(10 / 2),
                HeightRequest = 10,
                WidthRequest = 10,
                BackgroundColor = DotSelectedColor, BorderWidth = 2, BorderColor = Color.White
                
            };
            Children.Add(dot);
        }

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<PagingIndicatorView, IList>(
                pi => pi.ItemsSource,
                null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).ItemsSourceChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).ItemsSourceChanged();
                }
        );

        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<PagingIndicatorView, object>(
                pi => pi.SelectedItem,
                null,
                BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).SelectedItemChangedChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).SelectedItemChanged();
                });

        private void SelectedItemChangedChanging()
        {
        }

        private void SelectedItemChanged()
        {
            RestyleDiscs();
        }

        private void RestyleDiscs()
        {
            if(ItemsSource == null || Children == null) return;

            var selectedIndex = ItemsSource.IndexOf(SelectedItem);
            var pagerIndicators = Children.Cast<Button>().ToList();

            foreach (var pi in pagerIndicators)
            {
                UnselectDot(pi);
            }

            if (selectedIndex > -1)
            {
                SelectDot(pagerIndicators[selectedIndex]);
            }
        }

        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        // *******

        public static BindableProperty PagingIndicatorsSelectedColorProperty =
             BindableProperty.Create<PagingIndicatorView, Color>(
                x => x.PagingIndicatorsSelectedColor,
                   Color.Blue,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).PagingIndicatorsSelectedColorChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).PagingIndicatorsSelectedColorChanged();
                }
                 );

        public Color PagingIndicatorsSelectedColor
        {
            get { return (Color)GetValue(PagingIndicatorsSelectedColorProperty); }
            set
            {
                SetValue(PagingIndicatorsSelectedColorProperty, value);
            }
        }

        private void PagingIndicatorsSelectedColorChanged()
        {
            RestyleDiscs();
        }

        private void PagingIndicatorsSelectedColorChanging()
        {
            
        }

        public static BindableProperty PagingIndicatorsUnselectedColorProperty =
             BindableProperty.Create<PagingIndicatorView, Color>(
                x => x.PagingIndicatorsUnselectedColor,
                   Color.Yellow,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).PagingIndicatorsUnselectedColorChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).PagingIndicatorsUnselectedColorChanged();
                }
                 );

        public Color PagingIndicatorsUnselectedColor
        {
            get { return (Color)GetValue(PagingIndicatorsUnselectedColorProperty); }
            set
            {
                SetValue(PagingIndicatorsUnselectedColorProperty, value);
            }
        }

        private void PagingIndicatorsUnselectedColorChanged()
        {
            RestyleDiscs();
        }

        private void PagingIndicatorsUnselectedColorChanging()
        {

        }

        public static BindableProperty PagingIndicatorsDiscSizeProperty =
             BindableProperty.Create<PagingIndicatorView, double>(
                x => x.PagingIndicatorsDiscSize,
                   10,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).PagingIndicatorsDiscSizeChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) => {
                    ((PagingIndicatorView)bindable).PagingIndicatorsDiscSizeChanged();
                }
                 );

        public double PagingIndicatorsDiscSize
        {
            get { return (double)GetValue(PagingIndicatorsDiscSizeProperty); }
            set
            {
                SetValue(PagingIndicatorsDiscSizeProperty, value);
            }
        }

        private void PagingIndicatorsDiscSizeChanged()
        {
            RestyleDiscs();
        }

        private void PagingIndicatorsDiscSizeChanging()
        {

        }

        void ItemsSourceChanging()
        {
            if (ItemsSource != null)
                _selectedIndex = ItemsSource.IndexOf(SelectedItem);
        }

        void ItemsSourceChanged()
        {
            if (ItemsSource == null) return;

            // Dots *************************************
            GenerateDiscs();
            //*******************************************
        }

        private void GenerateDiscs()
        {
            if (ItemsSource == null) return;

            var countDelta = ItemsSource.Count - Children.Count;

            if (countDelta > 0)
            {
                for (var i = 0; i < countDelta; i++)
                {
                    CreateDot();
                }
            }
            else if (countDelta < 0)
            {
                for (var i = 0; i < -countDelta; i++)
                {
                    Children.RemoveAt(0);
                }
            }
        }

        void UnselectDot(Button dot)
        {
            dot.BorderRadius = Convert.ToInt32(PagingIndicatorsDiscSize / 2);
            dot.Opacity = 1.0;
            dot.BackgroundColor = PagingIndicatorsUnselectedColor;
            dot.HeightRequest = PagingIndicatorsDiscSize;
            dot.WidthRequest = PagingIndicatorsDiscSize;
        }

        void SelectDot(Button dot)
        {
            dot.BorderRadius = Convert.ToInt32(PagingIndicatorsDiscSize / 2);
            dot.Opacity = 1.0;
            dot.BackgroundColor = PagingIndicatorsSelectedColor;
            dot.HeightRequest = PagingIndicatorsDiscSize;
            dot.WidthRequest = PagingIndicatorsDiscSize;
        }
    }
}
