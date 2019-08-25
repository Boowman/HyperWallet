using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace Wallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsView : Widget
    {
        public int MaxTransactions = 5;
        private string multiplier = "s";

        public TransactionsView()
        {
            InitializeComponent();

            multiplier = transactionsList.Children.Count != 1 ? "s" : "";
            transactionsCount.Text = (transactionsList.Children.Count - 1).ToString() + " Transaction" + multiplier;

            addWidget.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => SendWidget(Page)), });
            removeWidget.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => TakeWidget(Page)), });
            addTransaction.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => TransactionPopUp()), });
        }

        private void TransactionPopUp()
        {
            PopupNavigation.Instance.PushAsync(new TransactionPopup(this));
        }

        public void AddTransaction(string desc, string amount, string dateMade)
        {
            if (transactionsList.Children.Count < MaxTransactions + 1)
            {
                transactionsList.Children.Insert(1, new Transaction(desc, amount, dateMade));

                multiplier = transactionsList.Children.Count != 1 ? "s" : "";
                transactionsCount.Text = (transactionsList.Children.Count - 1).ToString() + " Transaction" + multiplier;
            }
        }

        /// <summary>
        /// Check the page that we were one and if it's a match customize the current widget
        /// and pass it to the previous page so that we can display it.
        /// </summary>
        /// <param name="page">Page that we came from</param>
        protected override void SendWidget(object page)
        {
            if (page is CardNew newCardPage)
            {
                TransactionsView tempWidget = this;

                tempWidget.addTransaction.IsEnabled = false;
                tempWidget.addWidget.IsVisible      = false;
                tempWidget.removeWidget.IsVisible   = true;

                newCardPage.AddWidget(tempWidget);
            }
            else if (page is CardDetails cardDetailsPage)
            {
                TransactionsView tempWidget = this;

                tempWidget.addTransaction.IsEnabled     = true;
                tempWidget.addWidget.IsVisible          = false;
                tempWidget.removeWidget.IsVisible       = true;

                cardDetailsPage.AddWidget(tempWidget);
            }
        }

        /// <summary>
        /// Checking the type of page object and if it's a match access the page
        /// and remove the widget of this type.
        /// </summary>
        /// <param name="page">Page that we came from</param>
        protected override void TakeWidget(object page)
        {
            if (page is Page newCardPage)
                newCardPage.RemoveWidget(this);
        }

        /// <summary>
        /// The method sets up the widget based on what object we passed in.
        /// If we pass in a CardManager page then we can edit the widget
        /// </summary>
        /// <param name="obj">Pass either a Page or a Card</param>
        public override void SetupWidget(object page)
        {
            if (page is Page)
            {
                Page = page;

                if (page is ViewCards)
                {
                    Log.Warning("Page", "MainPage - " + page.GetType().ToString());
                    //transactionsList.Children.RemoveAt(0);
                    addTransaction.IsEnabled                = false;
                    addTransaction.IsVisible                = false;
                    addTransaction.ParentView.HeightRequest = 0;
                    transactionDisplayDetails.Margin        = 0;

                    addWidget.IsVisible         = false;
                    removeWidget.IsVisible      = false;
                }
                else if (page is CardManager && Card == null)
                {
                    Log.Warning("Page", "CardManger - Null Card - " + page.GetType().ToString());
                    addTransaction.IsEnabled    = false;
                    addTransaction.IsVisible    = true;
                    addWidget.IsVisible         = true;
                    removeWidget.IsVisible      = false;
                }
                else if (page is CardManager && Card != null)
                {
                    Log.Warning("Page", "CardManger - Not Null Card - " + page.GetType().ToString());
                    addTransaction.IsEnabled                = true;
                    addTransaction.IsVisible                = true;
                    addTransaction.ParentView.HeightRequest = 38;
                    transactionDisplayDetails.Margin        = new Thickness(0, 0, 0, 10);

                    addWidget.IsVisible         = false;
                    removeWidget.IsVisible      = true;
                }

                if (page is CardDetails cardDetails)
                    Card = cardDetails.StorredCard;
            }
        }
    }
}