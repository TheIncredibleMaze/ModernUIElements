using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernUIElements
{
    public enum StringFormat { C, D, E, F, G, N, P, R, X, x };

    public class NumericTextBox : TextBox
    {
        static NumericTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericTextBox), new FrameworkPropertyMetadata(typeof(NumericTextBox)));
        }
        [Category("Numeric TextBox")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        [Category("Numeric TextBox")]
        public StringFormat Format
        {
            get { return (StringFormat)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }
        [Category("Numeric TextBox")]
        public int Accurancy
        {
            get { return (int)GetValue(AccurancyProperty); }
            set { SetValue(AccurancyProperty, value); }
        }
        [Category("Numeric TextBox")]
        public bool? OnlyNumbersAllowed
        {
            get { return (bool?)GetValue(OnlyNumbersAllowedProperty); }
            set { SetValue(OnlyNumbersAllowedProperty, value); }
        }
        [Category("Numeric TextBox")]
        public List<Key> AllowedKeys
        {
            get { return (List<Key>)GetValue(AllowedKeysProperty); }
            set { SetValue(AllowedKeysProperty, value); }
        }
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericTextBox), new PropertyMetadata(0.0, PropertyChanged));
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(StringFormat), typeof(NumericTextBox), new PropertyMetadata(StringFormat.D));
        public static readonly DependencyProperty AccurancyProperty =
            DependencyProperty.Register("Accurancy", typeof(int), typeof(NumericTextBox), new PropertyMetadata(0));
        public static readonly DependencyProperty OnlyNumbersAllowedProperty =
            DependencyProperty.Register("OnlyNumbersAllowed", typeof(bool?), typeof(NumericTextBox), new PropertyMetadata(true));
        public static readonly DependencyProperty AllowedKeysProperty =
            DependencyProperty.Register("AllowedKeys", typeof(List<Key>), typeof(NumericTextBox), new PropertyMetadata(new List<Key>() { Key.D0, Key.D1,
            Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, 
            Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9, Key.Left, Key.Right, Key.Oem4, Key.Oem6, Key.Oem1, Key.Oem7,
            Key.Back, Key.Enter, Key.OemClear, Key.OemComma, Key.OemPeriod, Key.Home, Key.End, Key.Tab, Key.LeftCtrl, Key.RightCtrl, Key.LeftAlt,
            Key.RightAlt, Key.LeftShift, Key.RightShift, Key.V, Key.C, Key.X}));
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(NumericTextBox), new PropertyMetadata(Brushes.White));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ValueProperty && (string)GetValue(TextProperty) != e.NewValue.ToString())
            {
                string format = GetValue(FormatProperty).ToString() + GetValue(AccurancyProperty).ToString();
                if ((StringFormat)GetValue(FormatProperty) == StringFormat.D || (StringFormat)GetValue(FormatProperty) == StringFormat.X
                    || (StringFormat)GetValue(FormatProperty) == StringFormat.x)
                {
                    int value = Convert.ToInt32((double)e.NewValue);
                    Text = value.ToString(format);
                }
                else
                {
                    Text = ((double)e.NewValue).ToString(format);
                }
            }
            base.OnPropertyChanged(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if ((bool?)GetValue(OnlyNumbersAllowedProperty) == true)
            {
                if (((List<Key>)GetValue(AllowedKeysProperty)).Contains(e.Key))
                {
                    base.OnPreviewKeyDown(e);
                }
            }
            else
            { base.OnPreviewKeyDown(e); }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            double result = 0.0;
            if (double.TryParse((string)GetValue(TextProperty), out result) && !((StringFormat)GetValue(FormatProperty) == StringFormat.X || (StringFormat)GetValue(FormatProperty) == StringFormat.x))
            { Value = result; }
            else if ((StringFormat)GetValue(FormatProperty) == StringFormat.X || (StringFormat)GetValue(FormatProperty) == StringFormat.x)
            { Value = Convert.ToUInt32((string)GetValue(TextProperty), 16); }
            base.OnTextChanged(e);
        }
    }
}
