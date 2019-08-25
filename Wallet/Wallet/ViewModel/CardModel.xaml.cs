using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardModel : ContentView
    {
        public ViewCards ViewCards       { get; set; }
        public Card StorredCard         { get; set; }
        public StackLayout CardsParent  { get; set; }
        public ScrollView CardsScroller { get; set; }

        public CardModel(int index, Card card, ViewCards page, StackLayout cardsParent, ScrollView cardsScroller)
        {
            InitializeComponent();

            ViewCards = page;
            StorredCard = card;
            CardsParent = cardsParent;
            CardsScroller = cardsScroller;

            InitialCardModel();

            if (index == 0) SelectedCard();

            cardLabel.Text = card.Label;
            balanceSymbol.Text = "£";

            string balanceToDisplay = card.GetBalanceRounded().ToString();

            if (card.GetBalanceRounded() > 999999999)
            {
                var tempBalance = card.GetBalanceRounded().ToString().Substring(0, 3);
                balanceToDisplay = tempBalance + "bil";
                balancePence.IsVisible = false;
            }
            else if (card.GetBalanceRounded() > 999999)
            {
                var tempBalance = card.GetBalanceRounded().ToString().Substring(0, 3);
                balanceToDisplay = tempBalance + "mil";
                balancePence.IsVisible = false;
            }

            balanceLabel.Text = balanceToDisplay;
            balancePence.Text = "." + card.GetPence();

            // Assigning the right card icon
            if (card.Type == ECardType.DebitCard)
                cardType.Source = "debit_card_icon.png";
            else if(card.Type == ECardType.CreditCard)
                cardType.Source = "credit_card_icon.png";
            else if (card.Type == ECardType.Savings)
                cardType.Source = "savings_account_icon.png";
            //else if (card.Type == ECardType.Crypto)
            //    cardType.Source = "crypto_wallet.png";

            // Displaying the latest transaction
            latestTrasactionLabel.Text = card.DisplayRecentTransactionToBalancePercentage();

            if (card.RecentTransaction.Value < 0)
                latestTransactionImage.Source = "balance_loss.png";
            else if(card.RecentTransaction.Value > 0)
                latestTransactionImage.Source = "balance_gain.png";

            cardDetails.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => ViewCardDetails()), });
        }

        private void InitialCardModel()
        {
            Margin = new Thickness(0, 0, 0, 0);

            WidthRequest = 239;
            HeightRequest = 160;

            cardLabel.FontSize = 16;

            balanceSymbol.FontSize = 20;
            balancePence.FontSize = 20;

            balanceLabel.FontSize = 35;

            latestTrasactionLabel.FontSize = 14;
        }

        public void SelectedCard()
        {
            StorredCard.IsSelected = true;
            ScaleCardButton(1.25, 0.5f);

            cardDetails.IsVisible = true;
            Margin = new Thickness(50, 0, 50, 0);

            Log.Warning("Selected Card", StorredCard.Label);
        }

        public void DisplayedCard()
        {
            StorredCard.IsSelected = false;
            ScaleCardButton(1, 0.5f);

            cardDetails.IsVisible = false;
            Margin = new Thickness(0, 0, 0, 0);

            Log.Warning("Display Card", StorredCard.Label);
        }

        private void SelectCard(object sender, EventArgs e)
        {
            if(StorredCard.IsSelected == false)
            {
                for (int i = 0; i < App.CardsStorred.Count; i++)
                {
                    if(App.CardsStorred[i].ID != StorredCard.ID)
                        ((CardModel)CardsParent.Children[i]).DisplayedCard();
                }

                if (StorredCard.IsSelected)
                    DisplayedCard();
                else
                    SelectedCard();

                ViewCards.SelectedCard = StorredCard;
                ViewCards.ClearWidgets();
                ViewCards.InstantiateWidgets();

                MoveScroller();
            }
        }

        private async void MoveScroller()
        {
            if (CardsScroller != null)
            {
                await Task.Yield();
                await CardsScroller.ScrollToAsync(this, ScrollToPosition.Center, true);
            }
            else
                throw new ArgumentException("ScrollView is null -> CardModel");
        }

        private async void ScaleCardButton(double scale, float sec)
        {
            await this.ScaleTo(scale, (uint)(sec * 1000), Easing.Linear);
        }

        private async void ViewCardDetails()
        {
            await Navigation.PushAsync(new CardDetails(StorredCard));
        }
    }
}