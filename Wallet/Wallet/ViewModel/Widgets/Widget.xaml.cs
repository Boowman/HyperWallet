using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Widget : ContentView
	{
        protected object Page { get; set; }
        protected Card Card { get; set; }

        public virtual void SetupWidget(object page) { Log.Warning("Parent", "Setting The Page"); }

        protected virtual void SendWidget(object page) { Log.Warning("Parent", "Sending The Widget"); }

        protected virtual void TakeWidget(object page) { Log.Warning("Parent", "Taking The Widget"); }

    }
}