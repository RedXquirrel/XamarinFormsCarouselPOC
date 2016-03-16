using System;
using System.Collections.Generic;
using System.Diagnostics;
using Com.Xamtastic.Xamarin.Forms.CarouselView.Managers;
using Com.Xamtastic.Xamarin.Forms.CarouselView.ViewModels;
using Com.Xamtastic.Xamarin.Forms.CarouselView.Views;

using Xamarin.Forms;

namespace Com.Xamtastic.Xamarin.Forms.CarouselView
{
        public class CarouselControl : ContentView
        {
            public static void Init(Type typeInViewsAssembly)
            {
                ViewLayoutManager.ViewAssemblyType = typeInViewsAssembly;
            }
            /// <summary>
            /// The view container.
            /// </summary>
            private RelativeLayout _relativeLayout;

            /// <summary>
            /// The BindableProperty backing for the view collection that will swipe left and right in the carousel. 
            /// 
            /// It is this declaration that makes the property visible in Xaml.
            /// </summary>
            public static BindableProperty ItemViewModelsProperty =
             BindableProperty.Create<CarouselControl, IEnumerable<object>>(
                x => x.ItemViewModels,
                   null
                 );

            /// <summary>
            /// The view collection that will swipe left and right in the carousel.
            /// </summary>
            public IEnumerable<object> ItemViewModels
            {
                get { return (IEnumerable<object>)GetValue(ItemViewModelsProperty); }
                set
                {
                    SetValue(ItemViewModelsProperty, value);
                }
            }

            public static BindableProperty PagingIndicatorsSelectedColorProperty =
                 BindableProperty.Create<CarouselControl, Color>(
                    x => x.PagingIndicatorsSelectedColor,
                       Color.Blue
                     );

            public Color PagingIndicatorsSelectedColor
            {
                get { return (Color)GetValue(PagingIndicatorsSelectedColorProperty); }
                set
                {
                    SetValue(PagingIndicatorsSelectedColorProperty, value);
                }
            }

            public static BindableProperty PagingIndicatorsUnselectedColorProperty =
                 BindableProperty.Create<CarouselControl, Color>(
                    x => x.PagingIndicatorsUnselectedColor,
                       Color.Yellow
                     );

            public Color PagingIndicatorsUnselectedColor
            {
                get { return (Color)GetValue(PagingIndicatorsUnselectedColorProperty); }
                set
                {
                    SetValue(PagingIndicatorsUnselectedColorProperty, value);
                }
            }

            public static BindableProperty PagingIndicatorsDiscSizeProperty =
                 BindableProperty.Create<CarouselControl, double>(
                    x => x.PagingIndicatorsDiscSize,
                       10
                     );

            public double PagingIndicatorsDiscSize
            {
                get { return (double)GetValue(PagingIndicatorsDiscSizeProperty); }
                set
                {
                    SetValue(PagingIndicatorsDiscSizeProperty, value);
                }
            }

            CarouselViewViewModel viewModel;

            public CarouselControl()
            {
                viewModel = new CarouselViewViewModel();
                BindingContext = viewModel;

                this.PropertyChanged += (
                (s, e) =>
                {
                    if (e.PropertyName == CarouselControl.ItemViewModelsProperty.PropertyName)
                    {
                        Debug.WriteLine($"Property Name: {CarouselControl.ItemViewModelsProperty.PropertyName}");
                        viewModel.Views = ItemViewModels;
                    }

                    if (e.PropertyName == CarouselControl.PagingIndicatorsDiscSizeProperty.PropertyName)
                    {
                        Debug.WriteLine($"Property Name: {CarouselControl.PagingIndicatorsDiscSizeProperty.PropertyName}");
                        viewModel.PagingIndicatorsDiscSize = PagingIndicatorsDiscSize;
                    }

                    if (e.PropertyName == CarouselControl.PagingIndicatorsSelectedColorProperty.PropertyName)
                    {
                        Debug.WriteLine($"Property Name: {CarouselControl.PagingIndicatorsSelectedColorProperty.PropertyName}");
                        viewModel.PagingIndicatorSelectedColor = PagingIndicatorsSelectedColor;
                    }

                    if (e.PropertyName == CarouselControl.PagingIndicatorsUnselectedColorProperty.PropertyName)
                    {
                        Debug.WriteLine($"Property Name: {CarouselControl.PagingIndicatorsUnselectedColorProperty.PropertyName}");
                        viewModel.PagingIndicatorUnselectedColor = PagingIndicatorsUnselectedColor;
                    }
                });

                CreateControl();
            }

            private void CreateControl()
            {
                _relativeLayout = new RelativeLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                var pagesCarousel = CreateViewsCarousel();
                var dots = CreatePagingIndicatorContainer();

                _relativeLayout.Children.Add(pagesCarousel,
                    Constraint.RelativeToParent((parent) => parent.X),
                    Constraint.RelativeToParent((parent) => parent.Y),
                    Constraint.RelativeToParent((parent) => parent.Width),
                    Constraint.RelativeToParent((parent) => parent.Height)
                    );

                _relativeLayout.Children.Add(dots,
                    Constraint.Constant(0),
                    Constraint.RelativeToView(pagesCarousel,
                        (parent, sibling) => sibling.Height - 18),
                    Constraint.RelativeToParent(parent => parent.Width),
                    Constraint.Constant(18)
                    );

                Content = _relativeLayout;
            }

            ViewLayoutManager CreateViewsCarousel()
            {
                var carousel = new ViewLayoutManager
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    //ItemTemplate = new DataTemplate(typeof(HomeView))
                };
                carousel.SetBinding(ViewLayoutManager.ItemsSourceProperty, "Views");
                carousel.SetBinding(ViewLayoutManager.SelectedItemProperty, "CurrentView", BindingMode.TwoWay);

                return carousel;
            }

            View CreatePagingIndicatorContainer()
            {
                return new StackLayout
                {
                    Children = { CreatePagingIndicators() }
                };
            }

            View CreatePagingIndicators()
            {
                var pagerIndicator = new PagerIndicatorView()
                {
                    //DotSize = PagingIndicatorsDiscSize,
                    //DotSelectedColor = PagingIndicatorsSelectedColor,
                    //DotUnselectedColor = PagingIndicatorsUnselectedColor
                };
                pagerIndicator.SetBinding(PagerIndicatorView.ItemsSourceProperty, "Views");
                pagerIndicator.SetBinding(PagerIndicatorView.SelectedItemProperty, "CurrentView");
                pagerIndicator.SetBinding(PagerIndicatorView.PagingIndicatorsDiscSizeProperty, "PagingIndicatorsDiscSize");
                pagerIndicator.SetBinding(PagerIndicatorView.PagingIndicatorsSelectedColorProperty, "PagingIndicatorSelectedColor");
                pagerIndicator.SetBinding(PagerIndicatorView.PagingIndicatorsUnselectedColorProperty, "PagingIndicatorUnselectedColor");
                return pagerIndicator;
            }
        }
}
