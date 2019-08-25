using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Wallet;
using Wallet.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendEntryRenderer))]
namespace Wallet.Droid
{ 
    class ExtendEntryRenderer : EntryRenderer
    {
        public ExtendEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                var entry = e.NewElement as ExtendedEntry;

                if(entry.VerticalTextAlignment == Xamarin.Forms.TextAlignment.Start)
                    Control.Gravity = GravityFlags.Top;
                else if (entry.VerticalTextAlignment == Xamarin.Forms.TextAlignment.End)
                    Control.Gravity = GravityFlags.Bottom;
                else if(entry.VerticalTextAlignment == Xamarin.Forms.TextAlignment.End)
                    Control.Gravity = GravityFlags.CenterVertical;

                if(entry.Underline)
                {
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                        Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
                    else
                        Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
                }
                else
                {
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                        Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                    else
                        Control.Background.SetColorFilter(Android.Graphics.Color.Transparent, PorterDuff.Mode.SrcAtop);
                }


                Control.SetPadding((int)entry.Padding.Left, (int)entry.Padding.Top, (int)entry.Padding.Right, (int)entry.Padding.Bottom);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == "IsPassword")
                Control.Typeface = Typeface.CreateFromAsset(Context.Assets, "KeepCalm-Medium.ttf");
        }
    }
}