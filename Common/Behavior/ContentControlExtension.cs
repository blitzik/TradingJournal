using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Common.Behaviors
{
    public class ContentControlExtension : DependencyObject
    {
        public static Storyboard GetStoryboard(DependencyObject obj)
        {
            return (Storyboard)obj.GetValue(StoryboardProperty);
        }


        public static void SetStoryboard(DependencyObject obj, Storyboard value)
        {
            obj.SetValue(StoryboardProperty, value);
        }


        public static readonly DependencyProperty StoryboardProperty =
            DependencyProperty.RegisterAttached(
                "Storyboard",
                typeof(Storyboard),
                typeof(ContentControlExtension),
                new PropertyMetadata(default(Storyboard), OnStoryboardPropertyChanged)
            );


        private static void OnStoryboardPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contentControl = d as ContentControl;
            if (contentControl == null)
                throw new Exception("Can only be applied to a ContentControl");

            DependencyPropertyDescriptor propertyDescriptor = DependencyPropertyDescriptor.FromProperty(
                ContentControl.ContentProperty,
                typeof(ContentControl)
            );

            propertyDescriptor.RemoveValueChanged(contentControl, ContentChangedHandler);
            propertyDescriptor.AddValueChanged(contentControl, ContentChangedHandler);
        }


        private static void ContentChangedHandler(object sender, EventArgs eventArgs)
        {
            var animateObject = (FrameworkElement)sender;
            var storyboard = GetStoryboard(animateObject);
            storyboard.Begin(animateObject);
        }
    }
}
