using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using MaiQuocAnhWPF.Data.models;

namespace MaiQuocAnhWPF.Converters
{
    // Converter for calculating order detail totals
    public class OrderDetailTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OrderDetail detail)
            {
                return detail.UnitPrice * detail.Quantity * (1 - (decimal)detail.Discount);
            }
            return 0m;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Converter for calculating order totals
    public class OrderTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<OrderDetail> details)
            {
                decimal total = 0;
                foreach (var detail in details)
                {
                    total += detail.UnitPrice * detail.Quantity * (1 - (decimal)detail.Discount);
                }
                return total;
            }
            return 0m;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}