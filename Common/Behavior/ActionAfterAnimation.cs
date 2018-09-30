using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Common.Behaviors
{
    public class ActionAfterAnimation : DependencyObject
    {
        public static bool GetCanAnimationStart(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanAnimationStartProperty);
        }


        public static void SetCanAnimationStart(DependencyObject obj, bool value)
        {
            obj.SetValue(CanAnimationStartProperty, value);
        }


        public static readonly DependencyProperty CanAnimationStartProperty =
            DependencyProperty.RegisterAttached(
                "CanAnimationStart",
                typeof(bool),
                typeof(ActionAfterAnimation),
                new PropertyMetadata(false, new PropertyChangedCallback(OnCanAnimationStartPropertyChange))
            );


        private static void OnCanAnimationStartPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement el)) return;

            if ((bool)e.NewValue != true) {
                return;
            }

            Storyboard sb = GetStoryboard(d);
            if (sb == null) {
                throw new Exception("A Storyboard must be delcared!");
            }

            ICommand c = GetPerformAction(d);
            if (c == null) {
                throw new Exception("An action (ICommand) must be declared");
            }

            if (sb.IsSealed || sb.IsFrozen) {
                sb = sb.Clone();
            }

            Storyboard.SetTarget(sb, el);
            sb.Completed += (o, ea) => {
                var vm = el.DataContext;
                if (!c.CanExecute(vm)) {
                    return;
                }
                c.Execute(vm);
            };

            sb.Begin();
        }
        

        // -----
        

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
                typeof(ActionAfterAnimation),
                null
            );


        // -----


        public static ICommand GetPerformAction(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PerformActionProperty);
        }


        public static void SetPerformAction(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PerformActionProperty, value);
        }

        
        public static readonly DependencyProperty PerformActionProperty =
            DependencyProperty.RegisterAttached(
                "PerformAction",
                typeof(ICommand),
                typeof(ActionAfterAnimation),
                null
            );


    }
}
