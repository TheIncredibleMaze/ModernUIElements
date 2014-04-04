using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ModernUIElements
{
    class TimeSpanSinglePropertyConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null)
            {
                try
                {
                    string property = (string)values[0];
                    TimeSpan toConvert = (TimeSpan)values[1];
                    TimeProperty max = (TimeProperty)values[2];
                    switch (property)
                    {
                        case "Days":
                            return toConvert.Days.ToString("D1");
                        case "Hours":
                            if (max == TimeProperty.Hours)
                                return (toConvert.Days * 24 + toConvert.Hours).ToString("D2");
                            else
                                return toConvert.Hours.ToString("D2");
                        case "Minutes":
                            if (max == TimeProperty.Minutes)
                                return (toConvert.Days * 24 * 60 + toConvert.Hours * 60 + toConvert.Minutes).ToString("D2");
                            else
                                return toConvert.Minutes.ToString("D2");
                        case "Seconds":
                            if (max == TimeProperty.Seconds)
                                return (toConvert.Days * 24 * 60 * 60 + toConvert.Hours * 60 * 60 + toConvert.Minutes * 60 + toConvert.Seconds).ToString("D2");
                            else
                                return toConvert.Seconds.ToString("D2");
                        case "Milliseconds":
                            if (max == TimeProperty.Milliseconds)
                                return (toConvert.Days * 24 * 60 * 60 * 1000 + toConvert.Hours * 60 * 60 * 1000 + toConvert.Minutes * 60 * 1000 + toConvert.Seconds * 1000).ToString("D3");
                            else
                                return toConvert.Milliseconds.ToString("D1");
                        default:
                            return "0";
                    }
                }
                catch
                { return "0"; }
            }
            return "0";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class TimeSpanVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null)
            {
                try
                {
                    string property = (string)values[0];
                    TimeProperty min = (TimeProperty)values[1];
                    TimeProperty max = (TimeProperty)values[2];
                    switch (property)
                    {
                        case "Days":
                            if (max == TimeProperty.Days)
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "Hours":
                            if ((max == TimeProperty.Hours || max == TimeProperty.Days) && min != TimeProperty.Days)
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "Minutes":
                            if ((max != TimeProperty.Seconds && max != TimeProperty.Milliseconds) && 
                                (min == TimeProperty.Minutes || min == TimeProperty.Seconds || min == TimeProperty.Milliseconds))
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "Seconds":
                            if (max != TimeProperty.Milliseconds && (min == TimeProperty.Milliseconds || min == TimeProperty.Seconds))
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "Milliseconds":
                            if (min == TimeProperty.Milliseconds)
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "DaysMark":
                            if (max == TimeProperty.Days && min != TimeProperty.Days)
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "HoursMark":
                            if ((max == TimeProperty.Hours || max == TimeProperty.Days) && (min != TimeProperty.Days && min != TimeProperty.Hours))
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "MinutesMark":
                            if ((max != TimeProperty.Seconds && max != TimeProperty.Milliseconds) &&
                                (min == TimeProperty.Seconds || min == TimeProperty.Milliseconds))
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        case "SecondsMark":
                            if (max != TimeProperty.Milliseconds && min == TimeProperty.Milliseconds)
                                return Visibility.Visible;
                            return Visibility.Collapsed;
                        default:
                            break;
                    }
                }
                catch
                { throw new ArgumentNullException(); }
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class PositionForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null)
            {
                try
                {
                    string property = (string)values[0];
                    int position = (int)values[1];
                    switch (property)
                    {
                        case "Days":
                            if (position == (int)TimeProperty.Days)
                                return values[2];
                            return values[3];
                        case "Hours":
                            if (position == (int)TimeProperty.Hours)
                                return values[2];
                            return values[3];
                        case "Minutes":
                            if (position == (int)TimeProperty.Minutes)
                                return values[2];
                            return values[3];
                        case "Seconds":
                            if (position == (int)TimeProperty.Seconds)
                                return values[2];
                            return values[3];
                        case "Milliseconds":
                            if (position == (int)TimeProperty.Milliseconds)
                                return values[2];
                            return values[3];
                        default:
                            return values[3];
                    }
                }
                catch
                { throw new ArgumentNullException(); }
            }
            return System.Windows.Media.Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
