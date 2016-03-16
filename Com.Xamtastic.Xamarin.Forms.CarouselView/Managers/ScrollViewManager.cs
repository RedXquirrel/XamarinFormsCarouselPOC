using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Com.Xamtastic.Xamarin.Forms.CarouselView.Managers
{
    public class ScrollViewManager : ScrollView
    {
        readonly StackLayout _stack;

        int _selectedIndex;
        bool _timerflag = true;

        public ScrollViewManager()
        {
            Orientation = ScrollOrientation.Horizontal;

            _stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0
            };

            Content = _stack;
        }

        public IList<View> Children
        {
            get
            {
                return _stack.Children;
            }
        }

        private bool _layingOutChildren;
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            if (_layingOutChildren) return;

            _layingOutChildren = true;
            foreach (var child in Children) child.WidthRequest = width;
            _layingOutChildren = false;
        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create<ScrollViewManager, int>(
                carousel => carousel.SelectedIndex,
                0,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((ScrollViewManager)bindable).UpdateSelectedItem();
                }
            );

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        void UpdateSelectedItem()
        {
            _timerflag = false;

            _timerflag = true;

            global::Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 0, 0, 300),
                () =>
                {
                    SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;

                    return _timerflag;
                });
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<ScrollViewManager, IList>(
                view => view.ItemsSource,
                null,
                propertyChanging: (bindableObject, oldValue, newValue) =>
                {
                    ((ScrollViewManager)bindableObject).ItemsSourceChanging();
                },
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    ((ScrollViewManager)bindableObject).ItemsSourceChanged();
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

        void ItemsSourceChanging()
        {
            if (ItemsSource == null) return;
            _selectedIndex = ItemsSource.IndexOf(SelectedItem);
        }

        void ItemsSourceChanged()
        {
            _stack.Children.Clear();
            foreach (var item in ItemsSource)
            {
                var view = CreateView(item.GetType());

                var bindableObject = view as BindableObject;
                if (bindableObject != null)
                    bindableObject.BindingContext = item;
                _stack.Children.Add(view);
            }

            if (_selectedIndex >= 0) SelectedIndex = _selectedIndex;
        }

        public static View CreateView(Type viewModelType)
        {
            var viewModelName = viewModelType.Name;
            var viewName = viewModelName.Replace("ViewModel", "View");

            var viewType =
                ViewAssemblyType.GetTypeInfo().Assembly.ExportedTypes.FirstOrDefault(t => t.Name == viewName);

            if (viewType == null)
            {
                return null;
            }

            var view = Activator.CreateInstance(viewType) as View;
            if (view == null)
            {

            }
            return view;
        }

        public DataTemplate ItemTemplate
        {
            get;
            set;
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create<ScrollViewManager, object>(
                view => view.SelectedItem,
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((ScrollViewManager)bindable).UpdateSelectedIndex();
                }
            );

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

        public static Type ViewAssemblyType { get; internal set; }

        void UpdateSelectedIndex()
        {
            if (SelectedItem == BindingContext) return;

            SelectedIndex = Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(SelectedItem);
        }
    }
}
