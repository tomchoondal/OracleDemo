using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace OracleCommunication_Demo.Controls
{
    public class ElasticScrollViewer :ScrollViewer
    {
        #region Private Properties

        private TranslateTransform Transform { get; set; }

        private double StretchLengthX { get; set; }

        private double StretchLengthY { get; set; }

        private static int _disableManipulationCount = 0;



        public bool CanRaiseParent
        {
            get { return (bool)GetValue(CanRaiseParentProperty); }
            set { SetValue(CanRaiseParentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanRaiseParent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanRaiseParentProperty =
            DependencyProperty.Register("CanRaiseParent", typeof(bool), typeof(ElasticScrollViewer), new PropertyMetadata(true));



        #endregion

        #region DependencyProperty

        public static bool GetDisableManipulation(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisableManipulationProperty);
        }

        public static void SetDisableManipulation(DependencyObject obj, bool value)
        {
            obj.SetValue(DisableManipulationProperty, value);
        }

        // Using a DependencyProperty as the backing store for Manipulation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisableManipulationProperty =
            DependencyProperty.RegisterAttached("DisableManipulation", typeof(bool), typeof(ElasticScrollViewer), new PropertyMetadata(false, new PropertyChangedCallback(OnDisableManipulationChanged)));

        /// <summary>
        /// Increase this to get more stretchiness and vice-versa; Default is 10
        /// It doesn't increase or decrease in a linear fashion though, since the underlying computing function is SquareRoot
        /// </summary>
        public int Elasticity
        {
            get { return (int)GetValue(ElasticityProperty); }
            set { SetValue(ElasticityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Elasticity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElasticityProperty =
            DependencyProperty.Register("Elasticity", typeof(int), typeof(ElasticScrollViewer), new PropertyMetadata(10));


        /// <summary>
        /// Time it takes to reset scroll to zero position (milliseconds); Default is 300
        /// </summary>
        public int SpringBackDuration
        {
            get { return (int)GetValue(SpringBackDurationProperty); }
            set { SetValue(SpringBackDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpringBackDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpringBackDurationProperty =
            DependencyProperty.Register("SpringBackDuration", typeof(int), typeof(ElasticScrollViewer), new PropertyMetadata(300));

        #endregion

        #region Constructor

        public ElasticScrollViewer()
        {
            Transform = new TranslateTransform();
        }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Called when content changes, usually only once per application run
        /// </summary>
        /// <param name="oldContent"></param>
        /// <param name="newContent"></param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            UIElement content = this.Content as UIElement;

            if (content != null && content.RenderTransform == System.Windows.Media.Transform.Identity)
                content.RenderTransform = Transform;
            base.OnContentChanged(oldContent, newContent);
        }

        private bool RaiseParent(RoutedEventArgs e)
        {
            if (!CanRaiseParent)
                return false;

            if (VerticalOffset >= ScrollableHeight || VerticalOffset <= 0)
            {
                e.Handled = false;
                var parent = this.Parent as UIElement;
                if (parent != null)
                {
                    parent.RaiseEvent(e);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// WPF provides us with ready to use data when we try to overscroll; Unfortunately MSDN documentation on this is a bit lacking
        /// e.BoundaryFeedback.Translation signifies the amount by which we are overscrolling
        /// </summary>
        /// <param name="e"></param>
        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            if (_disableManipulationCount > 0)
            {
                return;
            }

            //Square root is a simple function that gives a decreasing growth characteristic
            if (e.BoundaryFeedback.Translation.Y != 0)
                StretchLengthY = Math.Sqrt(Math.Abs(e.BoundaryFeedback.Translation.Y * Elasticity));
            else
                ResetY();

            if (e.BoundaryFeedback.Translation.X != 0)
                StretchLengthX = Math.Sqrt(Math.Abs(e.BoundaryFeedback.Translation.X * Elasticity));
            else
                ResetX();

            Transform.Y = StretchLengthY = e.BoundaryFeedback.Translation.Y > 0 ? StretchLengthY : -StretchLengthY;
            Transform.X = StretchLengthX = e.BoundaryFeedback.Translation.X > 0 ? StretchLengthX : -StretchLengthX;

            //If not handled, default WPF behaviour of window shake will occur in conjuction with this
            // if (!RaiseParent(e))
            {
                e.Handled = true;
            }
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (_disableManipulationCount > 0)
            {
                return;
            }

            base.OnManipulationDelta(e);
            RaiseParent(e);
        }

        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            if (_disableManipulationCount > 0)
            {
                return;
            }

            base.OnManipulationCompleted(e);
            RaiseParent(e);
        }

        protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
            if (_disableManipulationCount > 0)
            {
                return;
            }

            base.OnManipulationStarted(e);
            RaiseParent(e);
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            if (_disableManipulationCount > 0)
            {
                return;
            }

            base.OnManipulationStarting(e);
            RaiseParent(e);
        }
        protected override void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            if (_disableManipulationCount > 0)
            {
                return;
            }

            base.OnManipulationInertiaStarting(e);
            RaiseParent(e);
        }

        /// <summary>
        /// OnManipulationBoundaryFeedback DOES return a final zero when we release touch(and thereby releasing overscroll),
        /// but it doesn't always give this value :/ 
        /// Hence this override ensures scroll returns to resting position on release
        /// IMP: Keeping this function short and optimized is extremely important since this is invoked on every touch release
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTouchUp(TouchEventArgs e)
        {
            if (_disableManipulationCount > 0)
            {
                return;
            }

            base.OnTouchUp(e);

            ResetY();
            ResetX();
        }

        #endregion

        #region Methods

        private static void OnDisableManipulationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = (FrameworkElement)d;

            if ((bool)e.NewValue)
            {
                fe.Unloaded += Fe_Unloaded;
                fe.Loaded += Fe_Loaded;

                if (fe.IsLoaded)
                {
                    _disableManipulationCount++;
                }
            }
            else
            {
                _disableManipulationCount--;
                fe.Unloaded -= Fe_Unloaded;
                fe.Loaded -= Fe_Loaded;
            }
        }

        private static void Fe_Unloaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = (FrameworkElement)sender;

            if (GetDisableManipulation(fe))
            {
                _disableManipulationCount--;
            }
        }

        private static void Fe_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = (FrameworkElement)sender;

            if (GetDisableManipulation(fe))
            {
                _disableManipulationCount++;
            }
        }

        private void ResetY()
        {
            //No need to make animations if there is no overscroll in effect
            if (StretchLengthY != 0)
            {
                DoubleAnimation AnimY = new DoubleAnimation(StretchLengthY, 0, new Duration(new TimeSpan(0, 0, 0, 0, SpringBackDuration)), FillBehavior.Stop);
                AnimY.EasingFunction = new ExponentialEase { Exponent = 3 };
                AnimY.Completed += (sender, eventArg) =>
                {
                    Transform.Y = StretchLengthY = 0;
                };
                Transform.BeginAnimation(TranslateTransform.YProperty, AnimY);
            }
        }

        private void ResetX()
        {
            //No need to make animations if there is no overscroll in effect
            if (StretchLengthX != 0)
            {
                DoubleAnimation AnimX = new DoubleAnimation(StretchLengthX, 0, new Duration(new TimeSpan(0, 0, 0, 0, SpringBackDuration)), FillBehavior.Stop);
                AnimX.EasingFunction = new ExponentialEase { Exponent = 3 };
                AnimX.Completed += (sender, eventArg) =>
                {
                    Transform.X = StretchLengthX = 0;
                };
                Transform.BeginAnimation(TranslateTransform.XProperty, AnimX);
            }
        }

        #endregion
    }
}
