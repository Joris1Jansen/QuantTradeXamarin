using System;
using System.Globalization;
using Xamarin.Forms;

namespace QuantTrade.Utils.Converter
{
    public class IsNullConverterUtil : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
                if (!(value is string)) return true;
            return string.IsNullOrWhiteSpace(value as string) ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ImageConvertUtil : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ImageSource.FromResource($"QuantTrade.Assets.Images.{value}.png"); 
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        } 
    }
    
    public class CoinImageConvertUtil : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ImageSource.FromResource($"QuantTrade.Assets.Images.Coins.{value}.png"); 
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        } 
    }
}