using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardManager : Page
    {
        public List<Widget> WidgetsAdded = new List<Widget>();

        protected virtual async void GoToWidgetPage()
        {
            Log.Warning("Base", "Go To Widget Page");
            await Navigation.PushAsync(new WidgetsView(this));
        }
	}
}