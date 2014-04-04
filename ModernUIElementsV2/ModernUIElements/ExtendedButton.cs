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
using System.Windows.Threading;

namespace ModernUIElements
{
    public class ExtendedButton : Button
    {
        static ExtendedButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedButton), new FrameworkPropertyMetadata(typeof(ExtendedButton)));
            BackgroundProperty.OverrideMetadata(typeof(ExtendedButton), new FrameworkPropertyMetadata(Brushes.LightGray));
            BorderBrushProperty.OverrideMetadata(typeof(ExtendedButton), new FrameworkPropertyMetadata(Brushes.Gray));
        }
        DispatcherTimer timer = new DispatcherTimer();
        int clicks = 0;

        [Category("Extended Button")]
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        [Category("Extended Button")]
        public bool? FastClick
        {
            get { return (bool?)GetValue(FastClickProperty); }
            set { SetValue(FastClickProperty, value); }
        }
        [Category("Extended Button")]
        public bool? PermanentClick
        {
            get { return (bool?)GetValue(PermanentClickProperty); }
            set { SetValue(PermanentClickProperty, value); }
        }
        [Category("Extended Button")]
        public double ClockValue
        {
            get { return (double)GetValue(ClockValueProperty); }
            set { SetValue(ClockValueProperty, value); }
        }
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(ExtendedButton), new PropertyMetadata(new CornerRadius(0)));
        public static readonly DependencyProperty FastClickProperty =
            DependencyProperty.Register("FastClick", typeof(bool?), typeof(ExtendedButton), new FrameworkPropertyMetadata(null, PropertyChanged));
        public static readonly DependencyProperty PermanentClickProperty =
            DependencyProperty.Register("PermanentClick", typeof(bool?), typeof(ExtendedButton), new FrameworkPropertyMetadata(null, PropertyChanged));
        public static readonly DependencyProperty ClockValueProperty =
            DependencyProperty.Register("ClockValue", typeof(double), typeof(ExtendedButton), new PropertyMetadata(0.0));
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(ExtendedButton), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(ExtendedButton), new PropertyMetadata(Brushes.LightBlue));
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(ExtendedButton), new PropertyMetadata(Brushes.CornflowerBlue));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExtendedButton copy = d as ExtendedButton;
            if (copy != null && e.Property == FastClickProperty && (bool?)e.NewValue == false)
            { copy.PermanentClick = false; }
            if (copy != null && e.Property == PermanentClickProperty && (bool?)e.NewValue == true)
            {
                copy.FastClick = true;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if ((bool?)GetValue(FastClickProperty) == false)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(10);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            else if ((bool?)GetValue(PermanentClickProperty) == true)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(200);
                timer.Tick += timer_Tick;
                timer.Start();
                base.OnClick();
            }
            else
            {
                base.OnClick();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if ((bool?)GetValue(FastClickProperty) == false)
            {
                if ((double)GetValue(ClockValueProperty) <= 1)
                { ClockValue += timer.Interval.TotalSeconds; }
                else
                { timer.Stop(); ClockValue = 0; base.OnClick(); }
            }
            else
            {
                if (clicks % 10 == 0)
                {
                    double timeValue = 200 - clicks * 2;
                    if (timeValue < 25)
                    { timeValue = 25; }
                    timer.Interval = TimeSpan.FromMilliseconds(timeValue); 
                }
                clicks++;
                base.OnClick();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            ClockValue = 0; timer.Stop(); clicks = 0;
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            /* Set Background to Hover-Values and restore the needed Values*/
            Brush restore = Background;
            Background = HoverBackground;
            HoverBackground = restore;

            restore = BorderBrush;
            BorderBrush = HoverBorderBrush;
            HoverBorderBrush = restore;

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            /* Set Background to Standard-Values and restore the needed Values*/
            Brush restore = Background;
            Background = HoverBackground;
            HoverBackground = restore;

            restore = BorderBrush;
            BorderBrush = HoverBorderBrush;
            HoverBorderBrush = restore;

            ClockValue = 0; timer.Stop(); clicks = 0;
            base.OnMouseLeave(e);
        }
    }

    public class ExtendedRadioButton : RadioButton
    {
        static ExtendedRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedRadioButton), new FrameworkPropertyMetadata(typeof(ExtendedRadioButton)));
            BackgroundProperty.OverrideMetadata(typeof(ExtendedRadioButton), new FrameworkPropertyMetadata(Brushes.LightGray));
            BorderBrushProperty.OverrideMetadata(typeof(ExtendedRadioButton), new FrameworkPropertyMetadata(Brushes.Gray));
        }

        Brush restoreOriginalBackground = null;
        Brush restoreOriginalBorderBrush = null;

        [Category("Extended Radiobutton")]
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        [Category("Extended Radiobutton")]
        public double ClockValue
        {
            get { return (double)GetValue(ClockValueProperty); }
            set { SetValue(ClockValueProperty, value); }
        }
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }
        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }
        public Brush CheckedBorderBrush
        {
            get { return (Brush)GetValue(CheckedBorderBrushProperty); }
            set { SetValue(CheckedBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(ExtendedRadioButton), new PropertyMetadata(new CornerRadius(0)));
        public static readonly DependencyProperty ClockValueProperty =
            DependencyProperty.Register("ClockValue", typeof(double), typeof(ExtendedRadioButton), new PropertyMetadata(0.0));
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(ExtendedRadioButton), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(ExtendedRadioButton), new PropertyMetadata(Brushes.LightBlue));
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(ExtendedRadioButton), new PropertyMetadata(Brushes.CornflowerBlue));
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(ExtendedRadioButton), new PropertyMetadata(Brushes.LightGreen));
        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.Register("CheckedBorderBrush", typeof(Brush), typeof(ExtendedRadioButton), new PropertyMetadata(Brushes.Green));

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (restoreOriginalBackground == null)
            { restoreOriginalBackground = (Brush)GetValue(BackgroundProperty); }
            if (restoreOriginalBorderBrush == null)
            { restoreOriginalBorderBrush = (Brush)GetValue(BorderBrushProperty); }

            Background = (Brush)GetValue(HoverBackgroundProperty);
            BorderBrush = (Brush)GetValue(HoverBorderBrushProperty);

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if ((bool?)GetValue(IsCheckedProperty) == true)
            {
                Background = (Brush)GetValue(CheckedBackgroundProperty);
                BorderBrush = (Brush)GetValue(CheckedBorderBrushProperty);
            }
            else
            {
                Background = restoreOriginalBackground;
                BorderBrush = restoreOriginalBorderBrush;
            }

            base.OnMouseLeave(e);
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            Background = (Brush)GetValue(CheckedBackgroundProperty);
            BorderBrush = (Brush)GetValue(CheckedBorderBrushProperty);

            base.OnChecked(e);
        }

        protected override void OnUnchecked(RoutedEventArgs e)
        {
            Background = restoreOriginalBackground;
            BorderBrush = restoreOriginalBorderBrush;

            base.OnUnchecked(e);
        }
    }

    public class ExtendedCheckBox : CheckBox
    { 
        static ExtendedCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedCheckBox), new FrameworkPropertyMetadata(typeof(ExtendedCheckBox)));
            BackgroundProperty.OverrideMetadata(typeof(ExtendedCheckBox), new FrameworkPropertyMetadata(Brushes.LightGray));
            BorderBrushProperty.OverrideMetadata(typeof(ExtendedCheckBox), new FrameworkPropertyMetadata(Brushes.Gray));
        }

        Brush restoreOriginalBackground = null;
        Brush restoreOriginalBorderBrush = null;

        [Category("Extended CheckBox")]
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        [Category("Extended CheckBox")]
        public double ClockValue
        {
            get { return (double)GetValue(ClockValueProperty); }
            set { SetValue(ClockValueProperty, value); }
        }
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }
        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }
        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }
        public Brush CheckedBorderBrush
        {
            get { return (Brush)GetValue(CheckedBorderBrushProperty); }
            set { SetValue(CheckedBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(ExtendedCheckBox), new PropertyMetadata(new CornerRadius(0)));
        public static readonly DependencyProperty ClockValueProperty =
            DependencyProperty.Register("ClockValue", typeof(double), typeof(ExtendedCheckBox), new PropertyMetadata(0.0));
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(ExtendedCheckBox), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(ExtendedCheckBox), new PropertyMetadata(Brushes.LightBlue));
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(ExtendedCheckBox), new PropertyMetadata(Brushes.CornflowerBlue));
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(ExtendedCheckBox), new PropertyMetadata(Brushes.LightGreen));
        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.Register("CheckedBorderBrush", typeof(Brush), typeof(ExtendedCheckBox), new PropertyMetadata(Brushes.Green));

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (restoreOriginalBackground == null)
            { restoreOriginalBackground = (Brush)GetValue(BackgroundProperty); }
            if (restoreOriginalBorderBrush == null)
            { restoreOriginalBorderBrush = (Brush)GetValue(BorderBrushProperty); }

            Background = (Brush)GetValue(HoverBackgroundProperty);
            BorderBrush = (Brush)GetValue(HoverBorderBrushProperty);

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if ((bool?)GetValue(IsCheckedProperty) == true)
            {
                Background = (Brush)GetValue(CheckedBackgroundProperty);
                BorderBrush = (Brush)GetValue(CheckedBorderBrushProperty);
            }
            else
            {
                Background = restoreOriginalBackground;
                BorderBrush = restoreOriginalBorderBrush;
            }

            base.OnMouseLeave(e);
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            if (restoreOriginalBackground == null)
            { restoreOriginalBackground = (Brush)GetValue(BackgroundProperty); }
            if (restoreOriginalBorderBrush == null)
            { restoreOriginalBorderBrush = (Brush)GetValue(BorderBrushProperty); }

            Background = (Brush)GetValue(CheckedBackgroundProperty);
            BorderBrush = (Brush)GetValue(CheckedBorderBrushProperty);

            base.OnChecked(e);
        }

        protected override void OnUnchecked(RoutedEventArgs e)
        {
            if (restoreOriginalBackground == null)
            { restoreOriginalBackground = (Brush)GetValue(BackgroundProperty); }
            if (restoreOriginalBorderBrush == null)
            { restoreOriginalBorderBrush = (Brush)GetValue(BorderBrushProperty); }

            Background = restoreOriginalBackground;
            BorderBrush = restoreOriginalBorderBrush;

            base.OnUnchecked(e);
        }
    }
}

