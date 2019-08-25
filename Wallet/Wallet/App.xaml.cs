using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Wallet
{
	public partial class App : Application
	{
        public static List<Card> CardsStorred = new List<Card>()
        {
            new Card()
            {
                ID = 0,
                Type = ECardType.DebitCard,
                Label = "Student Card",
                Balance = 500000000000.59,
                CardEnd = 4653,
                CurrencySymbol = "$",
                RecentTransaction = new STransactions
                {
                    Label = "Asos",
                    Value = 30,
                    Date = DateTime.Now,
                },
                MonthlyTransactions = new List<SMonthlyTransactions>()
                {
                    new SMonthlyTransactions()
                    {
                        Label = "Sky",
                        Value = 36.78,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                    },
                    new SMonthlyTransactions()
                    {
                        Label = "Three",
                        Value = 42.65,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                    }
                },
                AllTransactions = new List<STransactions>()
                {
                    new STransactions()
                    {
                        Label = "Asos",
                        Value = 30,
                        Date = DateTime.Now,
                    },
                    new STransactions()
                    {
                        Label = "Asda",
                        Value = 18.50,
                        Date = DateTime.Now,
                    },
                }
            },
        };

        public static bool LoginChecked = false;

        public static Animations Animations         = new Animations();

        public App()
        {
            InitializeComponent();

            MainPage = new UserAccountChoice();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static void ShowNavMenu()
        {
            if(Current.MainPage is MasterDetailPage)
                (Current.MainPage as MasterDetailPage).IsPresented = true;
            else
                Log.Warning("Check", "Not a master detail page");
        }

        public static void HideNavMenu()
        {
            if (Current.MainPage is MasterDetailPage)
                (Current.MainPage as MasterDetailPage).IsPresented = false;
            else
                Log.Warning("Check", "Not a master detail page");
        }
    }
}
