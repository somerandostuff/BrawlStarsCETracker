using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Documents;

namespace EventTrackerWPF.CustomElements
{
    public class DrawTextOutlined : FrameworkElement
    {
        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata("Hello world!!!", FrameworkPropertyMetadataOptions.AffectsRender));

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public static readonly DependencyProperty FontSizeProperty = 
            TextElement.FontSizeProperty.AddOwner(typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(36.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(new FontFamily("Arial"), FrameworkPropertyMetadataOptions.AffectsRender));

        public FontFamily FontFamily { get => (FontFamily)GetValue(FontFamilyProperty); set => SetValue(FontFamilyProperty, value); }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Fill { get => (Brush)GetValue(FillProperty); set => SetValue(FillProperty, value); }

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Brush), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Stroke { get => (Brush)GetValue(StrokeProperty); set => SetValue(StrokeProperty, value); }

        public static readonly DependencyProperty StrokeThiccnessProperty =
            DependencyProperty.Register(nameof(StrokeThiccness), typeof(double), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(2.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double StrokeThiccness { get => (double)GetValue(StrokeThiccnessProperty); set => SetValue(StrokeThiccnessProperty, value); }

        public static readonly DependencyProperty FontStyleProperty =
            DependencyProperty.Register(nameof(FontStyle), typeof(FontStyle), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(FontStyles.Normal, FrameworkPropertyMetadataOptions.AffectsRender));

        public FontStyle FontStyle { get => (FontStyle)GetValue(FontStyleProperty); set => SetValue(FontStyleProperty, value); }

        public static readonly DependencyProperty FontWeightProperty =
            DependencyProperty.Register(nameof(FontWeight), typeof(FontWeight), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(FontWeights.Normal, FrameworkPropertyMetadataOptions.AffectsRender));

        public FontWeight FontWeight { get => (FontWeight)GetValue(FontWeightProperty); set => SetValue(FontWeightProperty, value); }

        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register(nameof(ShadowColor), typeof(Color), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public Color ShadowColor { get => (Color)GetValue(ShadowColorProperty); set => SetValue(ShadowColorProperty, value); }

        public static readonly DependencyProperty ShadowOffsetProperty =
            DependencyProperty.Register(nameof(ShadowOffset), typeof(Vector), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(new Vector(0, 1), FrameworkPropertyMetadataOptions.AffectsRender));

        public Vector ShadowOffset { get => (Vector)GetValue(ShadowOffsetProperty); set => SetValue(ShadowOffsetProperty, value); }

        public static readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalContentAlignment), typeof(TextAlignment), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(TextAlignment.Left, FrameworkPropertyMetadataOptions.AffectsRender));

        public TextAlignment HorizontalContentAlignment { get => (TextAlignment)GetValue(HorizontalContentAlignmentProperty); set => SetValue(HorizontalContentAlignmentProperty, value); }

        public static readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalContentAlignment), typeof(VerticalAlignment), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(VerticalAlignment.Top, FrameworkPropertyMetadataOptions.AffectsRender));

        public VerticalAlignment VerticalContentAlignment { get => (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); set => SetValue(VerticalContentAlignmentProperty, value); }

        public DrawTextOutlined()
        {
            CacheMode = null;
        }

        protected override void OnRender(DrawingContext DrawingContext)
        {
            double X = 0, Y = 0;

            if (string.IsNullOrEmpty(Text)) return;

            var FormattedText = new FormattedText(
                Text,
                CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                FontSize, Fill,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            FormattedText.MaxTextWidth = ActualWidth;
            FormattedText.TextAlignment = HorizontalContentAlignment;

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

            var Geometry = FormattedText.BuildGeometry(new Point(X, Y));

            if (ShadowColor.A > 0)
            {
                var ShadowBrush = new SolidColorBrush(ShadowColor);
                ShadowBrush.Freeze();
                var ShadowPen = new Pen(ShadowBrush, StrokeThiccness);
                DrawingContext.PushTransform(new TranslateTransform(ShadowOffset.X, ShadowOffset.Y));
                DrawingContext.DrawGeometry(ShadowBrush, ShadowPen, Geometry);
                DrawingContext.Pop();
            }

            if (Stroke != null && StrokeThiccness > 0)
            {
                DrawingContext.DrawGeometry(null, new Pen(Stroke, StrokeThiccness), Geometry);
            }

            if (Fill != null)
            {
                DrawingContext.DrawGeometry(Fill, null, Geometry);
            }
        }

        protected override Size MeasureOverride(Size AvailableSize)
        {
            if (string.IsNullOrEmpty(Text))
                return new Size(0, 0);

            var FormattedText = new FormattedText(
                Text,
                CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretches.Normal),
                FontSize, Fill,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            FormattedText.MaxTextWidth = AvailableSize.Width;

            double TxtWidth = Math.Min(FormattedText.Width, AvailableSize.Width);
            double TxtHeight = Math.Min(FormattedText.Height, AvailableSize.Height);

            if (double.IsInfinity(AvailableSize.Width))   TxtWidth = FormattedText.Width;
            if (double.IsInfinity(AvailableSize.Height))  TxtHeight = FormattedText.Height;

            return new Size(TxtWidth, TxtHeight);
        }
    }
}
