using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace EventTrackerWPF.Librarbies
{
    public class DrawTextShakyOutlined : FrameworkElement
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata("YOUR TAKING TOO LONG", FrameworkPropertyMetadataOptions.AffectsRender));

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public static readonly DependencyProperty FontSizeProperty =
            TextElement.FontSizeProperty.AddOwner(typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(36.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(new FontFamily("Arial"), FrameworkPropertyMetadataOptions.AffectsRender));

        public FontFamily FontFamily { get => (FontFamily)GetValue(FontFamilyProperty); set => SetValue(FontFamilyProperty, value); }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Fill { get => (Brush)GetValue(FillProperty); set => SetValue(FillProperty, value); }

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Brush), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Stroke { get => (Brush)GetValue(StrokeProperty); set => SetValue(StrokeProperty, value); }

        public static readonly DependencyProperty StrokeThiccnessProperty =
            DependencyProperty.Register(nameof(StrokeThiccness), typeof(double), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double StrokeThiccness { get => (double)GetValue(StrokeThiccnessProperty); set => SetValue(StrokeThiccnessProperty, value); }

        public static readonly DependencyProperty FontStyleProperty =
            DependencyProperty.Register(nameof(FontStyle), typeof(FontStyle), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(FontStyles.Normal, FrameworkPropertyMetadataOptions.AffectsRender));

        public FontStyle FontStyle { get => (FontStyle)GetValue(FontStyleProperty); set => SetValue(FontStyleProperty, value); }

        public static readonly DependencyProperty FontWeightProperty =
            DependencyProperty.Register(nameof(FontWeight), typeof(FontWeight), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(FontWeights.Normal, FrameworkPropertyMetadataOptions.AffectsRender));

        public FontWeight FontWeight { get => (FontWeight)GetValue(FontWeightProperty); set => SetValue(FontWeightProperty, value); }

        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register(nameof(ShadowColor), typeof(Color), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public Color ShadowColor { get => (Color)GetValue(ShadowColorProperty); set => SetValue(ShadowColorProperty, value); }

        public static readonly DependencyProperty ShadowOffsetProperty =
            DependencyProperty.Register(nameof(ShadowOffset), typeof(Vector), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(new Vector(0, 1), FrameworkPropertyMetadataOptions.AffectsRender));

        public Vector ShadowOffset { get => (Vector)GetValue(ShadowOffsetProperty); set => SetValue(ShadowOffsetProperty, value); }

        public static readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalContentAlignment), typeof(TextAlignment), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(TextAlignment.Left, FrameworkPropertyMetadataOptions.AffectsRender));

        public TextAlignment HorizontalContentAlignment { get => (TextAlignment)GetValue(HorizontalContentAlignmentProperty); set => SetValue(HorizontalContentAlignmentProperty, value); }

        public static readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalContentAlignment), typeof(VerticalAlignment), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(VerticalAlignment.Top, FrameworkPropertyMetadataOptions.AffectsRender));

        public VerticalAlignment VerticalContentAlignment { get => (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); set => SetValue(VerticalContentAlignmentProperty, value); }

        public static readonly DependencyProperty ShakeIntensityProperty =
            DependencyProperty.Register(nameof(ShakeIntensity), typeof(double), typeof(DrawTextShakyOutlined),
                new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double ShakeIntensity { get => (double)GetValue(ShakeIntensityProperty); set => SetValue(ShakeIntensityProperty, value); }

        private readonly DispatcherTimer ShakeTimer;
        private readonly List<Vector> ShakeOffsets = new();
        private readonly Random RNG = new();

        public DrawTextShakyOutlined()
        {
            ShakeTimer = new()
            {
                Interval = TimeSpan.FromMilliseconds(16 + 2 / 3)
            };
            ShakeTimer.Tick += (Sender, EventArgs) =>
            {
                UpdateShakeOffsets();
                InvalidateVisual();
            };

            ShakeTimer.Start();
        }

        private void UpdateShakeOffsets()
        {
            int TextLength = Text?.Length ?? 0;
            while (ShakeOffsets.Count < TextLength)
            {
                ShakeOffsets.Add(new Vector());
            }
            while (ShakeOffsets.Count > TextLength)
            {
                ShakeOffsets.RemoveAt(ShakeOffsets.Count - 1);
            }

            for (int Idx = 0; Idx < TextLength; Idx++)
            {
                double OffsetX = (RNG.NextDouble() - .5) * 2 * ShakeIntensity;
                double OffsetY = (RNG.NextDouble() - .5) * 2 * ShakeIntensity;

                ShakeOffsets[Idx] = new Vector(OffsetX, OffsetY);
            }
        }


        protected override void OnRender(DrawingContext DrawingContext)
        {
            double X = 0, Y = 0;

            if (string.IsNullOrEmpty(Text)) return;

            var FormattedText = new FormattedText(
                "Dummy text for determining line height",
                CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                FontSize, Fill,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            switch (VerticalContentAlignment)
            {
                case VerticalAlignment.Top:
                default:
                    Y = 0;
                    break;
                case VerticalAlignment.Center:
                    Y = (ActualHeight - FormattedText.Height) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    Y = ActualHeight - FormattedText.Height;
                    break;
                case VerticalAlignment.Stretch:
                    Console.WriteLine("Vertical alignment stretching is not supported yet... (Using top alignment instead.)");
                    break;
            }

            for (int Idx = 0; Idx < Text.Length; Idx++)
            {
                string Char = Text[Idx].ToString();
                var FormattedChar = new FormattedText(
                    Char, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                    new Typeface(FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                    FontSize, Fill, VisualTreeHelper.GetDpi(this).PixelsPerDip);

                Vector Offset = (Idx < ShakeOffsets.Count) ? ShakeOffsets[Idx] : new Vector();

                var CharGeometry = FormattedChar.BuildGeometry(new Point(X + Offset.X, Y + Offset.Y));

                if (ShadowColor.A > 0)
                {
                    var ShadowBrush = new SolidColorBrush(ShadowColor);
                    ShadowBrush.Freeze();
                    var ShadowPen = new Pen(ShadowBrush, StrokeThiccness);
                    DrawingContext.PushTransform(new TranslateTransform(ShadowOffset.X, ShadowOffset.Y));
                    DrawingContext.DrawGeometry(ShadowBrush, ShadowPen, CharGeometry);
                    DrawingContext.Pop();
                }

                if (Stroke != null && StrokeThiccness > 0)
                {
                    DrawingContext.DrawGeometry(null, new Pen(Stroke, StrokeThiccness), CharGeometry);
                }

                if (Fill != null)
                {
                    DrawingContext.DrawGeometry(Fill, null, CharGeometry);
                }

                X += FormattedChar.WidthIncludingTrailingWhitespace;
            }
        }
    }
}

