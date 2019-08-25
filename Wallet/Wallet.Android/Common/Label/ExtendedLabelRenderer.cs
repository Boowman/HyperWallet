using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Wallet;
using Wallet.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace Wallet.Droid
{
    class ExtendedLabelRenderer : LabelRenderer
    {
        public ExtendedLabelRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                var entry = e.NewElement as ExtendedLabel;

                SetTheControlGravity(entry);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
                else
                    Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);

                Control.SetPadding((int)entry.Padding.Left, (int)entry.Padding.Top, (int)entry.Padding.Right, (int)entry.Padding.Bottom);
            }
        }

        /// <summary>
        /// Simply moved the entire code block into it's own method to keep it more organized.
        /// Issue: 
        /// VerticalTextAlignment.Start has Bottom assigned and End has Top is because in Xamarin it seems to be the other way around.
        /// Instead of pushing the object to the top of the container it pushes it to the bottom.
        /// </summary>
        private void SetTheControlGravity(ExtendedLabel element)
        {
            if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Start            && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.Start)
            {
                //Log.Warning("Alignment", "Left - Top");
                Control.Gravity = GravityFlags.Left | GravityFlags.Bottom;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Start       && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.End)
            {
                //Log.Warning("Alignment", "Left - Bottom");
                Control.Gravity = GravityFlags.Left | GravityFlags.Top;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Start       && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.Center)
            {
                //Log.Warning("Alignment", "Left - VCenter");
                Control.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.End         && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.Start)
            {
                //Log.Warning("Alignment", "Right - Top");
                Control.Gravity = GravityFlags.Right | GravityFlags.Bottom;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.End         && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.End)
            {
                //Log.Warning("Alignment", "Right - Bottom");
                Control.Gravity = GravityFlags.Right | GravityFlags.Top;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.End         && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.Center)
            {
                //Log.Warning("Alignment", "Right - VCenter");
                Control.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center      && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.Start)
            {
                //Log.Warning("Alignment", "HCenter - Top");
                Control.Gravity = GravityFlags.CenterHorizontal | GravityFlags.Bottom;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center      && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.End)
            {
                //Log.Warning("Alignment", "HCenter - Bottom");
                Control.Gravity = GravityFlags.CenterHorizontal | GravityFlags.Top;
            }
            else if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center      && element.VerticalTextAlignment == Xamarin.Forms.TextAlignment.Center)
            {
                //Log.Warning("Alignment", "HCenter - VCenter");
                Control.Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical;
            }
            else
            {
                //Log.Warning("Alignment", "Center");
                Control.Gravity = GravityFlags.Center;
            }
        }
    }
}