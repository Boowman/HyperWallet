using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Wallet;
using Wallet.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace Wallet.Droid
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        public ExtendedButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if(this.Control != null)
            {
                var button = e.NewElement as ExtendedButton;

                Control.SetAllCaps(false);
                Control.Gravity = Android.Views.GravityFlags.Center;
                Control.SetPadding((int)button.Padding.Left * 10, (int)button.Padding.Top * 10, (int)button.Padding.Right * 10, (int)button.Padding.Bottom * 10);
            }
        }
    }
}