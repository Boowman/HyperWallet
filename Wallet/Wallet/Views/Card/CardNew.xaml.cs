using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardNew : CardManager
	{
        public List<string> CurrencyItems = new List<string>()
        {
            "£",
            "$",
            "€",
            "₽",
            "؋",
            "L",
            "د.ج",
            "Forint",
            "Lei",
        };

        public List<string> CardTypeItems = new List<string>()
        {
            "Debit Card",
            "Credit Card",
            "Savings Account",
            //"Crypto Wallet",
        };

        public CardNew()
        {
            InitializeComponent();

            BackgroundColor = Color.FromHex("262F49");

            SetEntryStyle(cardLabel, 14, 20, new Thickness(18, 0, 15, 30), 150, "Enter Card Name", true);
            SetEntryStyle(cardEndLabel, 4, 30, new Thickness(5, 0, 0, 18), 70,  "0000");
            SetEntryStyle(balanceLabel, 12, 30, new Thickness(5, 0, 0, 18), 90, "000.00", true);

            /*
             * Applying the style to the picker elements and setting the title that will be used
             * We are also adding all the items used by the picker
            */
            SetPickerStyle(currency, "£");
            SetPickerStyle(cardType, "Card Type");

            for (int i = 0; i < CurrencyItems.Count; i++)
                currency.Items.Add(CurrencyItems[i]);

            for (int i = 0; i < CardTypeItems.Count; i++)
                cardType.Items.Add(CardTypeItems[i]);

            //Assigning gesture regonition for the top images/buttons
            saveCard.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => SavingCard()), });
            //menuButon.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => App.ShowNavMenu()), });
        }

        /// <summary>
        /// Setting the style of the extended entry used to add a new card to the account.
        /// </summary>
        /// <param name="entry">The object that will be changed</param>
        /// <param name="maxLength">Max Characters Length</param>
        /// <param name="fontSize">Font Size</param>
        /// <param name="padding">Padding</param>
        /// <param name="width">Width of Element</param>
        /// <param name="placeHolder">Placeholder</param>
        /// <param name="resizeField">Whether we want to be resizable based on the content or not</param>
        /// <param name="verticalTextAlignment">Vertical Text Alignment</param>
        private void SetEntryStyle(ExtendedEntry entry, int maxLength, double fontSize, Thickness padding, int width = 90, string placeHolder = "", bool resizeField = false, TextAlignment verticalTextAlignment = TextAlignment.End)
        {
            entry.IsSpellCheckEnabled = false;

            entry.FontSize              = fontSize;
            entry.Padding               = padding;
            entry.VerticalTextAlignment = verticalTextAlignment;
            entry.MaxCharacters         = maxLength;
            entry.MaxLength             = maxLength;
            entry.ResizeField           = resizeField;
            entry.WidthRequest          = width;
            entry.Placeholder           = placeHolder;

            entry.TextColor         = Color.FromHex("FFFFFF");
            entry.PlaceholderColor  = Color.FromHex("FFFFFF");
            entry.Opacity           = 0.35;
        }

        /// <summary>
        /// Setting the style and updating the focus of the picker
        /// </summary>
        /// <param name="picker">The picker element</param>
        /// <param name="title">Title used</param>
        private void SetPickerStyle(ExtendedPicker picker, string title)
        {
            picker.TextColor    = Color.White;
            picker.FontSize     = 20;
            picker.Padding      = new Thickness(0, 0, 0, 3.8);
            picker.Title        = title;
            picker.OldTitle     = title;
            picker.Opacity      = 0.35;

            picker.Focused                += (sender, args) => { picker.Title = ""; };
            picker.Unfocused              += (sender, args) => { picker.Title = picker.OldTitle; };
            picker.SelectedIndexChanged   += (sender, args) => { picker.Opacity = 1; };
        }

        /// <summary>
        /// Editing the balance entry and making sure only valid characters are being used
        /// </summary>
        /// <param name="sender">The object being used</param>
        /// <param name="e">The text entered by the sender</param>
        bool penceNotEmpty = false;
        private void EnterBalance(object sender, TextChangedEventArgs e)
        {
            var entry = sender as ExtendedEntry;
            int penceDotOffSet = 0;

            // Spliting the entered text into char so that we can check what character we have just entered
            char[] words = e.NewTextValue.ToCharArray();

            //Only update field that allow to be resized
            if (entry.ResizeField)
            {
                char checkValue = ' ';

                if (e.NewTextValue.Length >= 1)
                    checkValue = e.NewTextValue[e.NewTextValue.Length - 1];

                if (e.NewTextValue.Length > 0 && CheckForAvailableEntry(checkValue))
                {
                    entry.Opacity = 1;

                    if (e.NewTextValue.StartsWith("00") || e.NewTextValue.StartsWith(".") || e.NewTextValue.StartsWith(".."))
                    {
                        entry.Text = e.OldTextValue ?? "";
                        return;
                    }
                    else if (e.NewTextValue.EndsWith("..") || e.NewTextValue.EndsWith("..."))
                        entry.Text = e.OldTextValue;
                    else
                    {
                        if (!CheckForExtraDot(words))
                        {
                            if (e.NewTextValue.Contains('.'))
                            {
                                if (penceNotEmpty == false)
                                    entry.MaxLength = e.NewTextValue.Length + 2;

                                penceNotEmpty = true;
                                penceDotOffSet = 10;
                            }
                            else
                            {
                                entry.MaxLength = entry.MaxCharacters;
                                penceNotEmpty = false;
                                penceDotOffSet = 0;
                            }

                            if (e.NewTextValue.Length > 0)
                                entry.WidthRequest = 10 + (e.NewTextValue.Length * (entry.FontSize / 2)) - penceDotOffSet;
                            else
                                entry.WidthRequest = 10 + (entry.Placeholder.Length * (entry.FontSize / 2));

                            //String.Format("{0:N0}", e.NewTextValue); // Format the balance so it has commas;
                        }
                        else
                            entry.Text = e.OldTextValue;
                    }
                }
                else
                {
                    if (e.NewTextValue.Length > 1)
                        entry.Text = e.OldTextValue;
                    else
                        entry.Text = "";

                    entry.Opacity = 0.35;
                    entry.WidthRequest = 10 + (entry.Placeholder.Length * (entry.FontSize / 2));
                }
            }
        }

        private string PlaceBalanceComma(string balance)
        {

            return "";
        }

        private bool CheckForExtraDot(char[] array)
        {
            int count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '.')
                    count++;

                if (count > 1)
                    return true;
            }

            return false;
        }

        private bool CheckForAvailableEntry(char lastChar)
        {
            char[] allowedCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };

            for (int i = 0; i < allowedCharacters.Length; i++)
            {
                if (allowedCharacters[i] == lastChar)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Resizing the card name entry
        /// </summary>
        /// <param name="sender">The object being used</param>
        /// <param name="e">The text entered by the sender</param>
        private void EntryCardName(object sender, TextChangedEventArgs e)
        {
            var card = sender as ExtendedEntry;

            if (e.NewTextValue.Length > 0)
                card.Opacity = 1;
            else
                card.Opacity = 0.35;

            var entry = sender as ExtendedEntry;
            int spaceOffSet = 0;

            if (entry.ResizeField)
            {
                if (e.NewTextValue.Contains(' '))
                    spaceOffSet = 8;
                else
                    spaceOffSet = 0;

                if (e.NewTextValue.Length > 0)
                    entry.WidthRequest = 10 + (e.NewTextValue.Length * (entry.FontSize / 2)) - spaceOffSet;
                else
                    entry.WidthRequest = 10 + (entry.Placeholder.Length * (entry.FontSize / 2)) - spaceOffSet;
            }
        }

        /// <summary>
        /// Enter the last 4 characters of the card being used
        /// </summary>
        /// <param name="sender">The object being used</param>
        /// <param name="e">The text entered by the sender</param>
        private void EntryCardNumber(object sender, TextChangedEventArgs e)
        {
            var entry = sender as ExtendedEntry;
            char checkValue = ' ';

            if (e.NewTextValue.Length >= 1)
            {
                entry.Opacity = 1;
                checkValue = e.NewTextValue[e.NewTextValue.Length - 1];
            }

            if (!CardNameAvailability(checkValue))
            {
                if (e.NewTextValue.Length > 1)
                    entry.Text = e.OldTextValue;
                else
                {
                    entry.Text = "";
                    entry.Opacity = 0.35;
                }
            }
        }

        private bool CardNameAvailability(char lastChar)
        {
            char[] allowedCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            for (int i = 0; i < allowedCharacters.Length; i++)
            {
                if (allowedCharacters[i] == lastChar)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Applying a shaking animation to the objects passed to the method
        /// </summary>
        /// <param name="sender">Object to be shaken</param>
        private async void ShakeUnFilledElement(View sender)
        {
            uint timeout = 50;
            await sender.TranslateTo(-15, 0, timeout);
            await sender.TranslateTo(15, 0, timeout);
            await sender.TranslateTo(-10, 0, timeout);
            await sender.TranslateTo(10, 0, timeout);
            await sender.TranslateTo(-5, 0, timeout);
            await sender.TranslateTo(5, 0, timeout);
            sender.TranslationX = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override async void GoToWidgetPage()
        {
            await Navigation.PushAsync(new WidgetsView(this));
        }

        public override void AddWidget(object widgetSent)
        {
            var widget = widgetSent as Widget;
            widgetsHolder.Children.Add(widget);

            WidgetsAdded.Add(widget);
        }

        public override void RemoveWidget(object widgetSent)
        {
            var widget = widgetSent as Widget;

            if (widgetsHolder.Children.Contains(widget))
            {
                if (WidgetsAdded.Contains(widget))
                    WidgetsAdded.Remove(widget);

                widgetsHolder.Children.Remove(widget);
            }
        }

        /// <summary>
        /// Using all the data inputed by the user send it over to the app and store it in a database.
        /// </summary>
        private void SavingCard()
        {
            if (!string.IsNullOrWhiteSpace(cardLabel.Text) && !string.IsNullOrWhiteSpace(cardEndLabel.Text) && !string.IsNullOrWhiteSpace(balanceLabel.Text) && cardType.SelectedItem != null && currency.SelectedItem != null)
            {
                if (cardLabel.Text.Length > 0 && cardEndLabel.Text.Length == 4 && balanceLabel.Text.Length > 0)
                {
                    int tempID = App.CardsStorred.Count;
                    ECardType tempCardType = ECardType.Other;

                    if (cardType.SelectedItem.ToString() == "Debit Card")
                        tempCardType = ECardType.DebitCard;
                    else if (cardType.SelectedItem.ToString() == "Credit Card")
                        tempCardType = ECardType.CreditCard;
                    else if (cardType.SelectedItem.ToString() == "Savings Account")
                        tempCardType = ECardType.Savings;
                    //else if (cardType.SelectedItem.ToString() == "Crypto Wallet")
                    //    tempCardType = ECardType.Crypto;

                    Card tempCard = new Card()
                    {
                        ID = tempID++,
                        Type = tempCardType,

                        CardEnd             = int.Parse(cardEndLabel.Text),
                        Label               = cardLabel.Text,
                        CurrencySymbol      = currency.SelectedItem.ToString(),
                        Balance             = double.Parse(balanceLabel.Text),

                        WidgetsAvailable    = WidgetsAdded,
                    };

                    App.CardsStorred.Add(tempCard);
                    GoToAccount(tempCard);
                }
                else
                {
                    if (cardLabel.Text.Length <= 0)
                        ShakeUnFilledElement(cardLabel);

                    if (cardEndLabel.Text.Length != 4)
                        ShakeUnFilledElement(cardEndLabel);

                    if (balanceLabel.Text.Length <= 0)
                        ShakeUnFilledElement(balanceLabel);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(cardLabel.Text))
                    ShakeUnFilledElement(cardLabel);

                if (string.IsNullOrWhiteSpace(cardEndLabel.Text))
                    ShakeUnFilledElement(cardEndLabel);

                if (string.IsNullOrWhiteSpace(balanceLabel.Text))
                    ShakeUnFilledElement(balanceLabel);

                if (currency.SelectedItem == null)
                    ShakeUnFilledElement(currency);

                if (cardType.SelectedItem == null)
                    ShakeUnFilledElement(cardType);
            }
        }

        /// <summary>
        /// Going to the main menu page
        /// </summary>
        private async void GoToAccount(Card card)
        {
            await Navigation.PushAsync(new CardDetails(card));
        }
    }
}