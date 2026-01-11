using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Documents;
using System.Text;
using EventTrackerWPF.Librarbies;

namespace EventTrackerWPF.CustomElements
{
    public class DrawTextOutlined : FrameworkElement
    {
        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata("Text", FrameworkPropertyMetadataOptions.AffectsRender));

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public static readonly DependencyProperty FontSizeProperty = 
            TextElement.FontSizeProperty.AddOwner(typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(36.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }

        public static readonly DependencyProperty FontFamilyProperty =
            TextElement.FontFamilyProperty.AddOwner(typeof(DrawTextOutlined),
                new FrameworkPropertyMetadata(new FontFamily("sans-serif"), FrameworkPropertyMetadataOptions.AffectsRender));

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
            CacheMode = new BitmapCache();
        }

        protected override void OnRender(DrawingContext DrawingContext)
        {
            double X = 0, Y = 0;

            if (string.IsNullOrEmpty(Text)) return;

            var (PlainText, ColorSpans) = ParseTextWithColorTags(Text, Fill);

            var FormattedText = new FormattedText(
                PlainText,
                CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretches.Normal),
                FontSize, Fill,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            FormattedText.MaxTextWidth = ActualWidth;
            FormattedText.TextAlignment = HorizontalContentAlignment;

            foreach (var Spanm in ColorSpans)
            {
                if (Spanm.Length > 0)
                    FormattedText.SetForegroundBrush(Spanm.Brush, Spanm.Start, Spanm.Length);
            }

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
                DrawingContext.DrawText(FormattedText, new Point(X, Y));
            }
        }

        protected override Size MeasureOverride(Size AvailableSize)
        {
            var PlainText = GetPlainText(Text);

            if (string.IsNullOrEmpty(Text))
                return new Size(0, 0);

            var FormattedText = new FormattedText(
                PlainText,
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

        public Size MeasureTextSize()
        {
            var PlainText = GetPlainText(Text);

            var FormattedText = new FormattedText(
                PlainText,
                CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretches.Normal),
                FontSize, Fill,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            return new Size(FormattedText.Width, FormattedText.Height);
        }

        private static string GetPlainText(string Input)
        {
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            
            var (PlainText, _) = ParseTextWithColorTags(Input, Brushes.White);
            return PlainText;
        }

        private static (string PlainText, List<ColorSpan> Spans) ParseTextWithColorTags(string Input, Brush DefaultBruh)
        {
            var StrBuilder = new StringBuilder(Input?.Length ?? 00);
            var ColorSpans = new List<ColorSpan>();

            if (string.IsNullOrEmpty(Input)) return (string.Empty, ColorSpans);

            int Indx = 0;
            while (Indx < Input.Length)
            {
                if (Input[Indx] == '<' && Indx + 2 < Input.Length && Input[Indx + 1] == 'c')
                {
                    int RightBracketIndx = Input.IndexOf('>', Indx + 2);
                    if (RightBracketIndx == -1)
                    {
                        StrBuilder.Append(Input[Indx]);
                        Indx++;
                        continue;
                    }
                    var ColorToken = Input.Substring(Indx + 2, RightBracketIndx - (Indx + 2)).Trim();

                    int ClosingTagIndx = Input.IndexOf("</c>", RightBracketIndx + 1);
                    if (ClosingTagIndx == -1)
                    {
                        StrBuilder.Append(Input[Indx]);
                        Indx++;
                        continue;
                    }

                    string InnerText = Input.Substring(RightBracketIndx + 1, ClosingTagIndx - (RightBracketIndx + 1));
                    int StartIndx = StrBuilder.Length;
                    StrBuilder.Append(InnerText);
                    int Length = InnerText.Length;

                    Brush Bruh = DefaultBruh;
                    if (!string.IsNullOrEmpty(ColorToken))
                    {
                        if (TryParseBrushFromColorToken(ColorToken, out Brush ParsedBruh))
                            Bruh = ParsedBruh;
                    }

                    ColorSpans.Add(new ColorSpan()
                    {
                        Start = StartIndx,
                        Length = Length,
                        Brush = Bruh
                    });

                    Indx = ClosingTagIndx + 4;
                }
                else
                {
                    StrBuilder.Append(Input[Indx]);
                    Indx++;
                }
            }

            return (StrBuilder.ToString(), ColorSpans);
        }

        private static bool TryParseBrushFromColorToken(string Token, out Brush Brush)
        {
            // THIS IS NORMAL OKAY?
            if (!Token.StartsWith('#'))
                Token = '#' + Token;

            Brush = new SolidColorBrush(Colors.Black);
            if (string.IsNullOrWhiteSpace(Token)) return false;

            try
            {
                var Converter = new BrushConverter();
                var Converted = Converter.ConvertFromString(Token);
                if (Converted is Brush Bruh)
                {
                    Brush = Bruh;
                    if (Brush.CanFreeze) Brush.Freeze();
                    return true;
                }

                var ConvertedFallback = ColorConverter.ConvertFromString(Token);
                if (ConvertedFallback is Color Coler)
                {
                    var SolidBruh = new SolidColorBrush(Coler);
                    SolidBruh.Freeze();
                    Bruh = SolidBruh;
                    return true;
                }
            }
            catch
            {
                // Don't need anything here.
            }
            return false;
        }

        private struct ColorSpan
        {
            public int Start;
            public int Length;
            public Brush Brush;
        }
    }
}
