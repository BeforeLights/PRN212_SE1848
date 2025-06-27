using System;
using System.Globalization;
using System.Windows.Data;
using MaiQuocAnhWPF.Business;

namespace MaiQuocAnhWPF.Converters
{
    public class ProductNameConverter : IValueConverter
    {
        private static readonly ProductService _productService = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int productId)
            {
                var product = _productService.GetProductById(productId);
                return product?.ProductName ?? "Unknown Product";
            }
            return "Unknown Product";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}