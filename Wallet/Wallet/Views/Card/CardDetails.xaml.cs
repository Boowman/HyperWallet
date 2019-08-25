using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardDetails : CardManager
    {
        public Card StorredCard { get; private set; } = null;

        public CardDetails (Card card)
		{
			InitializeComponent();
            SetupAccount(card);

            StorredCard = card;


            BackgroundColor = Color.FromHex("262F49");

            cardLabel.TextColor = Color.FromHex("FFFFFF");
            cardLabel.FontSize = 20;

            cardEndLabel.TextColor = Color.FromHex("FFFFFF");
            cardEndLabel.FontSize = 30;
            cardEndLabel.Text = card.CardEnd.ToString();

            balanceLabel.TextColor = Color.FromHex("FFFFFF");
            balanceLabel.FontSize = 30;

            currency.TextColor = Color.FromHex("FFFFFF");
            currency.FontSize = 20;

            penceLabel.TextColor = Color.FromHex("FFFFFF");
            penceLabel.FontSize = 20;

            cardType.TextColor = Color.FromHex("FFFFFF");
            cardType.FontSize = 20;
            cardType.Text = card.Type.ToString();

            //menuButon.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => App.ShowNavMenu()), });

            InstantiateWidgets();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        public void SetupAccount(Card card)
        {
            if (card != null)
            {
                if (StorredCard == null)
                    StorredCard = card;

                cardLabel.Text = card.Label;

                currency.Text = "£";
                balanceLabel.Text = card.GetBalanceRounded().ToString();
                penceLabel.Text = "." + card.GetPence().ToString();
            }
            else
                throw (new ArgumentNullException("card", "Check CardDetails for more informations"));
        }

        /// <summary>
        /// Spawning all the available widgets for this specific card
        /// We check what widgets the card already contains and if it does contain it then we don't display it
        /// </summary>
        private void InstantiateWidgets()
        {
            Log.Warning("Widgets:", StorredCard.WidgetsAvailable.Count.ToString());
            for (int i = 0; i < StorredCard.WidgetsAvailable.Count; i++)
            {
                var tempWidget = StorredCard.WidgetsAvailable[i];
                tempWidget.SetupWidget(this);

                widgetsHolder.Children.Add(tempWidget);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override async void GoToWidgetPage()
        {
            await Navigation.PushAsync(new WidgetsView(this));
        }

        public override void AddWidget(object widgetSent)
        {
            var widget = widgetSent as Widget;
            widgetsHolder.Children.Add(widget);

            StorredCard.WidgetsAvailable.Add(widget);
        }

        public override void RemoveWidget(object widgetSent)
        {
            var widget = widgetSent as Widget;

            if (widgetsHolder.Children.Contains(widget))
            {
                if (StorredCard.WidgetsAvailable.Contains(widget))
                    StorredCard.WidgetsAvailable.Remove(widget);

                widgetsHolder.Children.Remove(widget);
            }
        }
    }
}