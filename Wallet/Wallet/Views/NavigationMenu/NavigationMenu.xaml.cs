/*---------------------------------------------------------
* 		 Name: Lenard Denisz Pop
* 	     Date: 
* Description:
* 
* 
* 
	Copyright © 2018 - HyperVoid LTD, All Rights Reserved
---------------------------------------------------------*/

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationMenu : ContentPage
	{
		public NavigationMenu ()
		{
            InitializeComponent();

            username.Text = "Boowman";
            usernameID.Text = "@boowman";

            viewProfile.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => GoToViewProfile()), });
        }

        public void GoToViewProfile()
        {
            Log.Warning("Page", "View Profile");
        }
	}
}