using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Transaction : ContentView
	{
		public Transaction()
		{
			InitializeComponent();

            description.Text    = "Payment to Asda";
            value.Text          = "£15000.00";
            date.Text           = "21/08/2018";
            balance.Text        = "£650.04";
		}

        public Transaction(string desc, string amount, string dateMade)
        {
            InitializeComponent();

            description.Text = desc;
            value.Text = "£" + amount;
            date.Text = dateMade;
            balance.Text = "£650.04";

            Console.WriteLine("Payment Made: " + desc + " - " + amount + " - " + dateMade);
        }
    }
}