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
    public class NumericUpDown : Control
    {
        static NumericUpDown()
        {
            InitializeCommands();

            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
            BackgroundProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(Brushes.LightGray));
            BorderBrushProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(Brushes.Gray));
        }

        private static void InitializeCommands()
        {
            //  create static instances
            IncreaseCommand = new RoutedCommand("IncreaseCommand", typeof(NumericUpDown));
            DecreaseCommand = new RoutedCommand("DecreaseCommand", typeof(NumericUpDown));

            //  register the command bindings - if the buttons get clicked, call these methods.
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown),
                           new CommandBinding(IncreaseCommand, OnIncreaseCommand));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown),
                           new CommandBinding(DecreaseCommand, OnDecreaseCommand));
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }
        public double TextWidth
        {
            get { return (double)GetValue(TextWidthProperty); }
            set { SetValue(TextWidthProperty, value); }
        }
        public Brush TextBackground
        {
            get { return (Brush)GetValue(TextBackgroundProperty); }
            set { SetValue(TextBackgroundProperty, value); }
        }
        public Brush BorderBackground
        {
            get { return (Brush)GetValue(BorderBackgroundProperty); }
            set { SetValue(BorderBackgroundProperty, value); }
        }

        [Category("NumericUpDown")]
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));
        [Category("NumericUpDown")]
        public static readonly DependencyProperty ButtonWidthProperty = 
            DependencyProperty.Register("ButtonWidth", typeof(double), typeof(NumericUpDown));
        [Category("NumericUpDown")]
        public static readonly DependencyProperty TextWidthProperty =
            DependencyProperty.Register("TextWidth", typeof(double), typeof(NumericUpDown));
        public static readonly DependencyProperty TextBackgroundProperty =
            DependencyProperty.Register("TextBackground", typeof(Brush), typeof(NumericUpDown), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty BorderBackgroundProperty =
            DependencyProperty.Register("BorderBackground", typeof(Brush), typeof(NumericUpDown), new PropertyMetadata(Brushes.White));

        public static RoutedCommand IncreaseCommand { get; set; }
        public static RoutedCommand DecreaseCommand { get; set; }

        public static void OnIncreaseCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDown copy = sender as NumericUpDown;
            if (copy != null)
            {
                copy.OnUpdateValue(1);
            }
        }

        public static void OnDecreaseCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            NumericUpDown copy = sender as NumericUpDown;
            if (copy != null)
            {
                copy.OnUpdateValue(-1);
            }
        }

        protected virtual void OnUpdateValue(int change)
        {
            Value += change;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == WidthProperty)
            {
                ButtonWidth = Math.Floor(((double)e.NewValue - 10) * 0.25);
                TextWidth = (double)e.NewValue - 12 - (Math.Floor(((double)e.NewValue - 10) * 0.25) * 2);
            }
            base.OnPropertyChanged(e);
        }
    }

    public enum TimeProperty:int 
    { Days = 4, Hours = 3, Minutes = 2, Seconds = 1, Milliseconds = 0 };

    public class TimeSpanUpDown : NumericUpDown
    {
        static TimeSpanUpDown()
        {
            InitializeCommands();

            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeSpanUpDown), new FrameworkPropertyMetadata(typeof(TimeSpanUpDown)));
            BackgroundProperty.OverrideMetadata(typeof(TimeSpanUpDown), new FrameworkPropertyMetadata(Brushes.LightGray));
            BorderBrushProperty.OverrideMetadata(typeof(TimeSpanUpDown), new FrameworkPropertyMetadata(Brushes.Gray));
        }

        private static void InitializeCommands()
        {
            //  create static instances
            NextPositionCommand = new RoutedCommand("NextPositionCommand", typeof(TimeSpanUpDown));
            LastPositionCommand = new RoutedCommand("LastPositionCommand", typeof(TimeSpanUpDown));

            //  register the command bindings - if the buttons get clicked, call these methods.
            CommandManager.RegisterClassCommandBinding(typeof(TimeSpanUpDown),
                           new CommandBinding(NextPositionCommand, OnNextPositionCommand));
            CommandManager.RegisterClassCommandBinding(typeof(TimeSpanUpDown),
                           new CommandBinding(LastPositionCommand, OnLastPositionCommand));
        }

        [Category("TimeSpanUpDown")]
        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        [Category("TimeSpanUpDown")]
        new public TimeSpan Value
        {
            get { return (TimeSpan)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        [Category("TimeSpanUpDown")]
        public TimeProperty Minimum
        {
            get { return (TimeProperty)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        [Category("TimeSpanUpDown")]
        public TimeProperty Maximum
        {
            get { return (TimeProperty)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public Brush SelectedForeground
        {
            get { return (Brush)GetValue(SelectedForegroundProperty); }
            set { SetValue(SelectedForegroundProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(int), typeof(TimeSpanUpDown), new FrameworkPropertyMetadata(0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, CoerceValue));
        new public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(TimeSpan), typeof(TimeSpanUpDown), new PropertyMetadata(new TimeSpan(0,0,0,0,0)));
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(TimeProperty), typeof(TimeSpanUpDown), new FrameworkPropertyMetadata(TimeProperty.Seconds, OnUpdateMinMax));
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(TimeProperty), typeof(TimeSpanUpDown), new FrameworkPropertyMetadata(TimeProperty.Hours,OnUpdateMinMax));
        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register("SelectedForeground", typeof(Brush), typeof(TimeSpanUpDown), new PropertyMetadata(Brushes.Blue));

        public static RoutedCommand NextPositionCommand { get; set; }
        public static RoutedCommand LastPositionCommand { get; set; }

        private static void OnUpdateMinMax(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeSpanUpDown copy = d as TimeSpanUpDown;
            if (copy != null)
            { copy.Position = 0; }
        }

        public static void OnNextPositionCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            TimeSpanUpDown copy = sender as TimeSpanUpDown;
            if (copy != null)
            {
                copy.OnUpdatePostion(1);
            }
        }

        public static void OnLastPositionCommand(Object sender, ExecutedRoutedEventArgs e)
        {
            TimeSpanUpDown copy = sender as TimeSpanUpDown;
            if (copy != null)
            {
                copy.OnUpdatePostion(-1);
            }
        }

        private static object CoerceValue(DependencyObject obj, object value)
        {
            if (value.GetType() == typeof(int) && value != null)
            {
                TimeSpanUpDown copy = obj as TimeSpanUpDown;
                if (copy != null)
                {
                    if ((int)value > (int)copy.Maximum)
                    { return (int)copy.Minimum; }
                    else if ((int)value < (int)copy.Minimum)
                    { return (int)copy.Maximum; }
                    return value;
                }
            }
            return 0;
        }

        public void OnUpdatePostion(int direction)
        {
            Position += direction;
        }

        protected override void OnUpdateValue(int change)
        {
            switch ((int)GetValue(PositionProperty))
            {
                case 0:
                    Value = ((TimeSpan)GetValue(ValueProperty)) + TimeSpan.FromMilliseconds(change);
                    break;
                case 1:
                    Value = ((TimeSpan)GetValue(ValueProperty)) + TimeSpan.FromSeconds(change);
                    break;
                case 2:
                    Value = ((TimeSpan)GetValue(ValueProperty)) + TimeSpan.FromMinutes(change);
                    break;
                case 3:
                    Value = ((TimeSpan)GetValue(ValueProperty)) + TimeSpan.FromHours(change);
                    break;
                case 4:
                    Value = ((TimeSpan)GetValue(ValueProperty)) + TimeSpan.FromDays(change);
                    break;
                default:
                    break;
            }   
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == WidthProperty)
            {
                ButtonWidth = Math.Floor(((double)e.NewValue - 10) * 0.125);
                TextWidth = (double)e.NewValue - 12 - (Math.Floor(((double)e.NewValue - 10) * 0.25) * 2);
                
            }
            else
            { base.OnPropertyChanged(e); }
        }
    }
}
