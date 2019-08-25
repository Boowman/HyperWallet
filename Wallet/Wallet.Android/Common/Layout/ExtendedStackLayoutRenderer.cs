using System;
using Wallet;
using Wallet.Droid;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedStackLayout), typeof(ExtendedStackLayoutRenderer))]
namespace Wallet.Droid
{
    public class ExtendedStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }

        public ExtendedStackLayoutRenderer(Context context) : base(context) { }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height, StartColor.ToAndroid(), EndColor.ToAndroid(), Android.Graphics.Shader.TileMode.Mirror);

            var paint = new Android.Graphics.Paint() { Dither = true, };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var scrollView = e.NewElement as ExtendedStackLayout;

                // Assign the colors chosen when the ScrollView is created
                StartColor = Color.FromHex(scrollView.StartColor);
                EndColor = Color.FromHex(scrollView.EndColor);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}