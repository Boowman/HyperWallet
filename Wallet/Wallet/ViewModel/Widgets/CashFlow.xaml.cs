using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CashFlow : Widget
	{
        public CashFlow()
        {
            InitializeComponent();

            cashFlowRemaining.Text  = "£" + 0.00;
            incomeTotal.Text        = "£" + 0.00;
            expensesTotal.Text      = "- £" +0.00;

            addWidget.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => SendWidget(Page)), });
            removeWidget.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => TakeWidget(Page)), });
        }

        public CashFlow(Card storredCard)
        {
            InitializeComponent();

            cashFlowRemaining.Text  = "£" + 950.00;
            incomeTotal.Text        = "£" + 1600.00;
            expensesTotal.Text      = "- £" + 650.00;
        }

        private void SetupUI()
        {
            cashFlowRemaining.Text  = "£" + Card.Balance;
            incomeTotal.Text        = "£" + 1600.00;
            expensesTotal.Text      = "- £" + 650.00;
        }

        /// <summary>
        /// Check the page that we were one and if it's a match customize the current widget
        /// and pass it to the previous page so that we can display it.
        /// </summary>
        /// <param name="page">Page that we came from</param>
        protected override void SendWidget(object page)
        {
            if (page is CardManager prevPage)
            {
                CashFlow tempWidget = this;

                tempWidget.addWidget.IsVisible = false;
                tempWidget.removeWidget.IsVisible = true;

                prevPage.AddWidget(tempWidget);
            }

            // Only call if we came from a page where a card already exists
            if(page is CardDetails)
                SetupUI();
        }

        /// <summary>
        /// Checking the type of page object and if it's a match access the page
        /// and remove the widget of this type.
        /// </summary>
        /// <param name="page">Page that we came from</param>
        protected override void TakeWidget(object page)
        {
            if (page is Page prevPage)
                prevPage.RemoveWidget(this);
        }

        /// <summary>
        /// The method sets up the widget based on what object we passed in.
        /// If we pass in a CardManager page then we can edit the widget
        /// </summary>
        /// <param name="obj">Page that we came from</param>
        public override void SetupWidget(object page)
        {
            if (page is Page)
            {
                Page = page;

                if (page is ViewCards mainPage)
                {
                    addWidget.IsVisible     = false;
                    removeWidget.IsVisible  = false;

                    Card = mainPage.SelectedCard;
                    SetupUI();
                }
                else if (page is CardManager && Card == null)
                {
                    addWidget.IsVisible = true;
                    removeWidget.IsVisible = false;

                    if (page is CardDetails cardDetails)
                        Card = cardDetails.StorredCard;
                }
                else if (page is CardManager && Card != null)
                {
                    addWidget.IsVisible = false;
                    removeWidget.IsVisible = true;

                    SetupUI();
                }
            }
        }
    }
}