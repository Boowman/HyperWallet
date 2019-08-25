using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System;
using Wallet;
using Wallet.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(ExtendedPickerRenderer))]
namespace Wallet.Droid
{
    public class ExtendedPickerRenderer : PickerRenderer
    {
        public ExtendedPickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                var picker = e.NewElement as ExtendedPicker;

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
                else
                    Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);

                Control.Gravity = GravityFlags.Bottom | GravityFlags.CenterHorizontal;
                Control.SetPadding((int)picker.Padding.Left * 10, (int)picker.Padding.Top * 10, (int)picker.Padding.Right * 10, (int)picker.Padding.Bottom * 10);
                Control.SetTextColor(Android.Graphics.Color.White);
                Control.SetHintTextColor(Android.Graphics.Color.White);
            }
        }
    }
}