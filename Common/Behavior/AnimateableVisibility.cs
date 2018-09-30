using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Common.Behaviors
{
    public class AnimateableVisibility : DependencyObject
    {
        private static DoubleAnimation _da = new DoubleAnimation();
        private static FrameworkElement _fe;

        private static void OnVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _da.Completed -= AnimationCompletedHandler;
            _fe = d as FrameworkElement;
            if (_fe == null) return;

            Visibility v = (Visibility)e.NewValue;
            _fe.Loaded -= _fe_Loaded;
            _fe.Loaded += _fe_Loaded;

            _da.Duration = TimeSpan.FromMilliseconds(GetDuration(_fe));
            if (v == Visibility.Visible) {
                _fe.Visibility = v;
                _da.From = 0.0;
                _da.To = 1.0;

            } else { // Hidden or Collapsed
                _da.From = 1.0;
                _da.To = 0.0;
                _da.Completed += AnimationCompletedHandler;
            }
            _fe.BeginAnimation(UIElement.OpacityProperty, _da);
        }


        private static void _fe_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement s = sender as FrameworkElement;
            if (GetVisibility(s) == Visibility.Hidden) {
                s.SetCurrentValue(UIElement.VisibilityProperty, Visibility.Hidden);
            }
        }


        private static void AnimationCompletedHandler(object sender, EventArgs e)
        {
            _fe.Visibility = Visibility.Hidden;
        }


        // -----


        public static Visibility GetVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(VisibilityProperty);
        }


        public static void SetVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(VisibilityProperty, value);
        }


        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.RegisterAttached("Visibility", typeof(Visibility), typeof(AnimateableVisibility), new PropertyMetadata(default(Visibility), OnVisibilityPropertyChanged));


        // -----
        

        public static int GetDuration(DependencyObject obj)
        {
            return (int)obj.GetValue(DurationProperty);
        }


        public static void SetDuration(DependencyObject obj, int value)
        {
            obj.SetValue(DurationProperty, value);
        }


        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.RegisterAttached("Duration", typeof(int), typeof(AnimateableVisibility), new PropertyMetadata(300));


    }
}
