using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Wallet
{
	public partial class ViewCards : Page
	{
        List<Expense> Expenses = new List<Expense>()
        {
                new Expense
                {
                    Color = "#F0D05F",
                    Label = "Home",
                    Value = "800",
                },
                new Expense
                {
                    Color = "#9719CD",
                    Label = "Debt",
                    Value = "300",
                },
                new Expense
                {
                    Color = "#EAAF59",
                    Label = "Travel",
                    Value = "250",
                },
                new Expense
                {
                    Color = "#31E596",
                    Label = "Clothes",
                    Value = "220",
                },
                new Expense
                {
                    Color = "#2BB9CF",
                    Label = "Auto & Transport",
                    Value = "180",
                },
                new Expense
                {
                    Color = "#D9526B",
                    Label = "Others",
                    Value = "130",
                },
                new Expense()
                {
                    Color   = "#1D74C1",
                    Label   = "Food & Drinks",
                    Value   = "120",
                },
                new Expense
                {
                    Color = "#E58AC8",
                    Label = "Health",
                    Value = "80",
                },
        };
        
        public Card SelectedCard { get; set; }

        public ViewCards()
		{
            InitializeComponent();

            BackgroundColor = Color.FromHex("262F49");
            title.TextColor = Color.FromHex("9EAACC");

            menuButon.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => App.ShowNavMenu()), });

            Log.Warning("Check", "Main Page 2");
        }

        /// <summary>
        /// Instantiate all the created cards into the layout
        /// Loop through all the storred cards and then check if the cards already exists if it does, don't instantiate it
        /// Then check if the add new card image exists and if it does remove it so we can place it at the end.
        /// </summary>
        private void InstantiateCards()
        {
            for (int i = 0; i < App.CardsStorred.Count; i++)
            {
                bool exists = false;

                for (int j = 0; j < availableCards.Children.Count; j++)
                {
                    if(availableCards.Children[j] is CardModel card)
                    {
                        if (card.StorredCard.CardEnd == App.CardsStorred[i].CardEnd)
                        {
                            exists = true;
                            break;
                        }
                    }
                }

                if(exists == false)
                {
                    var tempModel = new CardModel(i, App.CardsStorred[i], this, availableCards, cardsScroller);

                    if (i == 0)
                        SelectedCard = tempModel.StorredCard;

                    availableCards.Children.Add(new CardModel(i, App.CardsStorred[i], this, availableCards, cardsScroller));
                }
            }

            for (int j = 0; j < availableCards.Children.Count; j++)
            {
                if (availableCards.Children[j] is Image img)
                {
                    availableCards.Children.Remove(img);
                    break;
                }
            }

            availableCards.Children.Add(new Image()
            {
                Source = "add_card_bg.png",
                WidthRequest = 239,
                HeightRequest = 160,
            });

            availableCards.Children[availableCards.Children.Count - 1].GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => AddNewCard()), });
        }

        /// <summary>
        /// Spawning all the available widgets for this specific card
        /// We check what widgets the card already contains and if it does contain it then we don't display it
        /// </summary>
        public void InstantiateWidgets()
        {
            for (int i = 0; i < SelectedCard.WidgetsAvailable.Count; i++)
            {
                var tempWidget = SelectedCard.WidgetsAvailable[i];
                tempWidget.SetupWidget(this);

                widgetsHolder.Children.Add(tempWidget);
            }
        }

        public void ClearWidgets()
        {
            widgetsHolder.Children.Clear();
        }

        /// <summary>
        /// Instantiate the storred card when this page is showing up, we are using this rather than the constructor
        /// Because when we create a new card we are taken to the details page so using PopAsync would take us back to the add new card page
        /// It's better to simply remove all the created pages and go back to the main page then create a new main page
        /// </summary>
        protected override void OnAppearing()
        {
            InstantiateCards();
            InstantiateWidgets();
            Log.Warning("Check", "Main Page OnAppearing");
        }

        /// <summary>
        /// Go to the add new card page
        /// </summary>
        private async void AddNewCard()
        {
            await Navigation.PushModalAsync(new CardNew());
        }
    }
}