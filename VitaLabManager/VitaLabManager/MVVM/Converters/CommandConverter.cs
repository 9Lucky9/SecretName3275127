using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VitaLabManager.MVVM.Models;

namespace VitaLabManager.MVVM.Converters
{
    public class CommandConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;
            var productId = (int)values[0];
            var quantityString = (string)values[1];
            if(string.IsNullOrEmpty(quantityString))
            {
                return DependencyProperty.UnsetValue;
            }

            var test = new AddToBasketModel()
            {
                ProductId = productId,
                Quantity = System.Convert.ToInt32((string)values[1])
            };
            return test;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
