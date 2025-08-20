using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EventTrackerWPF.Librarbies
{
    public class GridButton : Grid
    {
        public static readonly DependencyProperty ShrinkScaleProperty =
            DependencyProperty.Register(nameof(ShrinkScale), typeof(double), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(0.95, FrameworkPropertyMetadataOptions.AffectsRender));

        public double ShrinkScale { get => (double)GetValue(ShrinkScaleProperty); set => SetValue(ShrinkScaleProperty, value); }

        public static readonly DependencyProperty BounceScaleProperty =
            DependencyProperty.Register(nameof(BounceScale), typeof(double), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(1.05, FrameworkPropertyMetadataOptions.AffectsRender));

        public double BounceScale { get => (double)GetValue(BounceScaleProperty); set => SetValue(BounceScaleProperty, value); }

        public static readonly DependencyProperty AnimationDurationMsProperty =
            DependencyProperty.Register(nameof(AnimationDurationMs), typeof(double), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double AnimationDurationMs { get => (double)GetValue(AnimationDurationMsProperty); set => SetValue(AnimationDurationMsProperty, value); }

        private readonly double NormalScale = 1.0;

        public GridButton()
        {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseLeftButtonUp += OnMouseLeftButtonUp;
            MouseLeave += OnMouseLeave;
        }

        private void AnimateButtonPress(double Scale)
        {
            foreach (UIElement ChildElem in Children)
            {
                var Transform = ChildElem.RenderTransform as ScaleTransform;
                if (Transform == null)
                {
                    Transform = new ScaleTransform(1, 1);
                    ChildElem.RenderTransform = Transform;
                    ChildElem.RenderTransformOrigin = new Point(.5, .5);
                }

                var AnimX = new DoubleAnimation(Scale, TimeSpan.FromMilliseconds(AnimationDurationMs));
                var AnimY = new DoubleAnimation(Scale, TimeSpan.FromMilliseconds(AnimationDurationMs));

                Transform.BeginAnimation(ScaleTransform.ScaleXProperty, AnimX);
                Transform.BeginAnimation(ScaleTransform.ScaleYProperty, AnimY);
            }
        }

        private void AnimateButtonUnpressBounce(double Scale)
        {
            foreach (UIElement ChildElem in Children)
            {
                var Transform = ChildElem.RenderTransform as ScaleTransform;
                if (Transform == null)
                {
                    Transform = new ScaleTransform(1, 1);
                    ChildElem.RenderTransform = Transform;
                    ChildElem.RenderTransformOrigin = new Point(.5, .5);
                }

                var KeyframesX = new DoubleAnimationUsingKeyFrames();
                KeyframesX.KeyFrames.Add(new EasingDoubleKeyFrame(ShrinkScale, KeyTime.FromPercent(0.0)));
                KeyframesX.KeyFrames.Add(new EasingDoubleKeyFrame(BounceScale, KeyTime.FromPercent(0.5)) { EasingFunction = new BackEase { Amplitude = .5, EasingMode = EasingMode.EaseOut} });
                KeyframesX.KeyFrames.Add(new EasingDoubleKeyFrame(NormalScale, KeyTime.FromPercent(1.0)) { EasingFunction = new BounceEase { Bounces = 1, Bounciness = 2, EasingMode = EasingMode.EaseOut} });
                KeyframesX.Duration = TimeSpan.FromMilliseconds(AnimationDurationMs);

                var KeyframesY = new DoubleAnimationUsingKeyFrames();
                KeyframesY.KeyFrames.Add(new EasingDoubleKeyFrame(ShrinkScale, KeyTime.FromPercent(0.0)));
                KeyframesY.KeyFrames.Add(new EasingDoubleKeyFrame(BounceScale, KeyTime.FromPercent(0.5)) { EasingFunction = new BackEase { Amplitude = .5, EasingMode = EasingMode.EaseOut } });
                KeyframesY.KeyFrames.Add(new EasingDoubleKeyFrame(NormalScale, KeyTime.FromPercent(1.0)) { EasingFunction = new BounceEase { Bounces = 1, Bounciness = 2, EasingMode = EasingMode.EaseOut } });
                KeyframesY.Duration = TimeSpan.FromMilliseconds(AnimationDurationMs);

                Transform.BeginAnimation(ScaleTransform.ScaleXProperty, KeyframesX);
                Transform.BeginAnimation(ScaleTransform.ScaleYProperty, KeyframesY);
            }
        }

        private void OnMouseLeftButtonDown(object Sender, MouseButtonEventArgs Event)
        {
            AnimateButtonPress(ShrinkScale);
            
        }

        private void OnMouseLeftButtonUp(object Sender, MouseButtonEventArgs Event)
        {
            AnimateButtonUnpressBounce(BounceScale);
        }

        private void OnMouseLeave(object Sender, MouseEventArgs Event)
        {
            AnimateButtonPress(NormalScale);
        }
    }
}
