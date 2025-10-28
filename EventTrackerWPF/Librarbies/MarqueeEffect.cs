using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EventTrackerWPF.Librarbies
{
    public class MarqueeEffect
    {
        public static void StartMarquee(UIElement Elem, double ElemWidth, double SpeedSeconds)
        {
            if (Elem == null) return;

            double TextWidth = (Elem as FrameworkElement)?.ActualWidth ?? 0;

            var Transform = Elem.RenderTransform as TranslateTransform;
            if (Transform == null)
            {
                Transform = new TranslateTransform();
                Elem.RenderTransform = Transform;
            }

            var MarqueeAnimation = new DoubleAnimation
            {
                From = ElemWidth,
                To = -TextWidth * 2,
                Duration = new Duration(TimeSpan.FromSeconds(SpeedSeconds)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            Transform.BeginAnimation(TranslateTransform.XProperty, MarqueeAnimation);
        }
    }
}
