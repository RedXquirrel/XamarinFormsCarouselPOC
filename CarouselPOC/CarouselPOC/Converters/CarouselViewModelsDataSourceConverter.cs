using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselPOC.ViewModels;
using Xamarin.Forms;

namespace CarouselPOC.Converters
{
    public class CarouselViewModelsDataSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new List<object>
            {
                new ViewOneViewModel(),
                new ViewTwoViewModel(),
                new ViewThreeViewModel(),
                new ViewFourViewModel(),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
