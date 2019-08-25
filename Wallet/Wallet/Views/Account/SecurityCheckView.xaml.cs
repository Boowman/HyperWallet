using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SecurityCheckView : Page
	{
        public static string LoginPin { get { return "7531"; } }
        public string enteredPin = "";

		public SecurityCheckView ()
		{
			InitializeComponent();

            pinEnterCheck.BackgroundColor = Color.FromRgba(255, 255, 255, 100);
        }

        private void PinNumberEntered(object sender, EventArgs e)
        {
            if (sender is ExtendedButton button)
            {
                if (enteredPin.Length < 4)
                {
                    enteredPin += button.Text;

                    switch (enteredPin.Length)
                    {
                        case 1:
                            firstPin.Opacity = 1;
                            firstPinText.Text = button.Text;
                            break;
                        case 2:
                            secondPin.Opacity = 1;

                            firstPinText.Text = "*";
                            secondPinText.Text = button.Text;

                            break;
                        case 3:
                            thirdPin.Opacity = 1;

                            firstPinText.Text = "*";
                            secondPinText.Text = "*";

                            thirdPinText.Text = button.Text;
                            break;
                        case 4:
                            fourthPin.Opacity = 1;

                            firstPinText.Text   = "*";
                            secondPinText.Text  = "*";
                            thirdPinText.Text   = "*";
                            fourthPinText.Text = button.Text;
                            break;
                    }

                    if (enteredPin.Length == 4)
                    {
                        if (CheckPin(enteredPin))
                        {
                            GoToMainMenu();
                            enteredPin = "";
                        }
                        //else
                        //    ResetEnteredPins();
                    }
                }

                //App.Animations.ButtonPress(button);
            }
        }

        private bool CheckPin(string pin)
        {
            if (pin == LoginPin)
                return true;
            else
                return false;
        }

        private void PinNumberBackspace(object sender, EventArgs e)
        {
            //if (sender is ExtendedButton button)
            //    App.Animations.ButtonPress(button);

            if (enteredPin.Length > 0)
            {
                switch (enteredPin.Length)
                {
                    case 1:
                        firstPin.Opacity = 0.5;
                        firstPinText.Text = "*";
                        break;
                    case 2:
                        secondPin.Opacity = 0.5;
                        secondPinText.Text = "*";
                        break;
                    case 3:
                        thirdPin.Opacity = 0.5;
                        thirdPinText.Text = "*";
                        break;
                    case 4:
                        fourthPin.Opacity = 0.5;
                        fourthPinText.Text = "*";
                        break;
                }

                enteredPin = enteredPin.Remove(enteredPin.Length - 1);
                Log.Warning("LoginCheck", "Pin:" + enteredPin);
            }
            //else
            //    ResetEnteredPins();
        }

        private void SubmitPinCheck(object sender, EventArgs e)
        {
            //if (sender is ExtendedButton button)
            //    App.Animations.ButtonPress(button);

            if (enteredPin == LoginPin)
            {
                GoToMainMenu();
                enteredPin = "";
            }
            //else
            //    ResetEnteredPins();
        }

        private void ResetEnteredPins()
        {
            //App.Animations.ShakeUnFilledElement(firstPin);
            //App.Animations.ShakeUnFilledElement(secondPin);
            //App.Animations.ShakeUnFilledElement(thirdPin);
            //App.Animations.ShakeUnFilledElement(fourthPin);

            firstPin.Opacity = 0.5;
            secondPin.Opacity = 0.5;
            thirdPin.Opacity = 0.5;
            fourthPin.Opacity = 0.5;

            firstPinText.Text = "*";
            secondPinText.Text = "*";
            thirdPinText.Text = "*";
            fourthPinText.Text = "*";

            enteredPin = "";
        }

        private void GoToMainMenu()
        {
            App.LoginChecked                = true;
            Application.Current.MainPage    = new NavigationPage(new MainPage());
            Log.Warning("Activity:", Navigation.NavigationStack.Count.ToString());
        }
    }
}