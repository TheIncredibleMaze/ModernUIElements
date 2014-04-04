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
    public class ExtendedProgressBar : ProgressBar
    {
        static ExtendedProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedProgressBar), new FrameworkPropertyMetadata(typeof(ExtendedProgressBar)));
            BackgroundProperty.OverrideMetadata(typeof(ExtendedProgressBar), new FrameworkPropertyMetadata(Brushes.LightGray));
            BorderBrushProperty.OverrideMetadata(typeof(ExtendedProgressBar), new FrameworkPropertyMetadata(Brushes.Gray));
            ValueProperty.OverrideMetadata(typeof(ExtendedProgressBar), new FrameworkPropertyMetadata(0.0));
            MaximumProperty.OverrideMetadata(typeof(ExtendedProgressBar), new FrameworkPropertyMetadata(1.0));
        }
        [Category("Extended ProgressBar")]
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }
        public SolidColorBrush ProgressBarBackground
        {
            get { return (SolidColorBrush)GetValue(ProgressBarBackgroundProperty); }
            set { SetValue(ProgressBarBackgroundProperty, value); }
        }
        public SolidColorBrush ProgressBarForeground
        {
            get { return (SolidColorBrush)GetValue(ProgressBarForegroundProperty); }
            set { SetValue(ProgressBarForegroundProperty, value); }
        }
        private LinearGradientBrush ProgressBarFill
        {
            get { return (LinearGradientBrush)GetValue(ProgressBarFillProperty); }
            set { SetValue(ProgressBarFillProperty, value); }
        }
        [Category("Extended ProgressBar")]
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        [Category("Extended ProgressBar")]
        public string StringFormat
        {
            get { return (string)GetValue(StringFormatProperty); }
            set { SetValue(StringFormatProperty, value); }
        }
        [Category("Extended ProgressBar")]
        public PointCollection Points
        {
            get { return (PointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        private PointCollection ProgressBarPoints
        {
            get { return (PointCollection)GetValue(ProgressBarPointsProperty); }
            set { SetValue(ProgressBarPointsProperty, value); }
        }
        [Category("Extended ProgressBar")]
        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }
        [Category("Extended ProgressBar")]
        public double ProgressBarWidth
        {
            get { return (double)GetValue(ProgressBarWidthProperty); }
            set { SetValue(ProgressBarWidthProperty, value); }
        }

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(ExtendedProgressBar), new PropertyMetadata(new CornerRadius(0)));
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(ExtendedProgressBar), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty ProgressBarBackgroundProperty =
            DependencyProperty.Register("ProgressBarBackground", typeof(SolidColorBrush), typeof(ExtendedProgressBar), new PropertyMetadata(new SolidColorBrush(Colors.LightGray), OnProgressBarPropertyChanged));
        public static readonly DependencyProperty ProgressBarForegroundProperty =
            DependencyProperty.Register("ProgressBarForeground", typeof(SolidColorBrush), typeof(ExtendedProgressBar), new PropertyMetadata(new SolidColorBrush(Colors.Green), OnProgressBarPropertyChanged));
        public static readonly DependencyProperty ProgressBarFillProperty =
            DependencyProperty.Register("ProgressBarFill", typeof(LinearGradientBrush), typeof(ExtendedProgressBar), new PropertyMetadata());
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ExtendedProgressBar), new PropertyMetadata(""));
        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(double), typeof(ExtendedProgressBar), new PropertyMetadata(17.0));
        public static readonly DependencyProperty ProgressBarWidthProperty =
            DependencyProperty.Register("ProgressBarWidth", typeof(double), typeof(ExtendedProgressBar), new PropertyMetadata(51.0));
        public static readonly DependencyProperty StringFormatProperty =
            DependencyProperty.Register("StringFormat", typeof(string), typeof(ExtendedProgressBar), new PropertyMetadata(""));
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(PointCollection), typeof(ExtendedProgressBar), new PropertyMetadata(null, OnProgressBarPropertyChanged));
        public static readonly DependencyProperty ProgressBarPointsProperty =
            DependencyProperty.Register("ProgressBarPoints", typeof(PointCollection), typeof(ExtendedProgressBar), new PropertyMetadata(null));

        private static void OnProgressBarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == PointsProperty)
            {
                setPointCollection(d, e);
            }
            else if (e.Property == ProgressBarBackgroundProperty || e.Property == ProgressBarForegroundProperty)
            {
                ExtendedProgressBar copy = d as ExtendedProgressBar;
                if (copy != null)
                {
                    if (copy.ProgressBarFill == null)
                    {
                        copy.ProgressBarFill = new LinearGradientBrush();
                        copy.ProgressBarFill.StartPoint = new Point(0,0);
                        copy.ProgressBarFill.EndPoint = new Point(1,0);
                    }
                    copy.ProgressBarFill.GradientStops.Clear();
                    copy.ProgressBarFill.GradientStops.Add(new GradientStop(copy.ProgressBarForeground.Color, 0));
                    copy.ProgressBarFill.GradientStops.Add(new GradientStop(copy.ProgressBarForeground.Color, copy.Value / copy.Maximum));
                    copy.ProgressBarFill.GradientStops.Add(new GradientStop(copy.ProgressBarBackground.Color, copy.Value/copy.Maximum));
                }
            }
        }

        private static void setPointCollection(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExtendedProgressBar copy = d as ExtendedProgressBar;
            if (copy != null && copy.ProgressBarWidth != 0.0)
            {
                double _height = copy.Height;
                if (double.IsNaN(_height) || _height == 0)
                { _height = copy.ActualHeight; }
                PointCollection result = new PointCollection();
                if (copy.Points == null)
                { copy.Points = new PointCollection(new List<Point>() { new Point(0, 1), new Point(0, 0.99), new Point(1, 0), new Point(1, 1) }); }
                foreach (Point item in copy.Points)
                {
                    result.Add(new Point(item.X * copy.ProgressBarWidth, item.Y * (_height - 12)));
                }
                copy.ProgressBarPoints = result;
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ActualHeightProperty)
            {
                setPointCollection(this, e);
            }
            else if (e.Property == WidthProperty)
            {
                ContentWidth = ((double)e.NewValue - 10) * 0.25;
                ProgressBarWidth = (double)e.NewValue - 10 - (double)GetValue(ContentWidthProperty);
                /* Update Points */
                setPointCollection(this, e);
            }
            else if (e.Property == WidthProperty)
            {
                ContentWidth = ((double)e.NewValue - 10) * 0.25;
                ProgressBarWidth = (double)e.NewValue - 10 - (double)GetValue(ContentWidthProperty);
                /* Update Points */
                setPointCollection(this, e);
            }
            else if (e.Property == ValueProperty)
            {
                if (ProgressBarFill == null)
                {
                    ProgressBarFill = new LinearGradientBrush();
                    ProgressBarFill.StartPoint = new Point(0, 0);
                    ProgressBarFill.EndPoint = new Point(1, 0);
                }
                ProgressBarFill.GradientStops.Clear();
                ProgressBarFill.GradientStops.Add(new GradientStop(ProgressBarForeground.Color, 0));
                ProgressBarFill.GradientStops.Add(new GradientStop(ProgressBarForeground.Color, (double)e.NewValue / Maximum));
                ProgressBarFill.GradientStops.Add(new GradientStop(ProgressBarBackground.Color, (double)e.NewValue / Maximum));
            }
            base.OnPropertyChanged(e);
        }
    }
}
