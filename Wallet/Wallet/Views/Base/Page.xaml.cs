using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page : ContentPage
	{
        public virtual void AddWidget(object widgetSent)     { Log.Warning("Base", "Adding Widget"); }
        public virtual void RemoveWidget(object widgetSent)  { Log.Warning("Base", "Removing Widget"); }
    }
}