using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserAccountChoice : Page
	{
        public bool IsLogingIn = true;

		public UserAccountChoice()
		{
			InitializeComponent ();

            loginUsernameBG.BackgroundColor = Color.FromRgba(255, 255, 255, 100);
            loginPasswordBG.BackgroundColor = Color.FromRgba(255, 255, 255, 100);

            loginUsername.PlaceholderColor = Color.FromRgba(255, 255, 255, 100);
            loginPassword.PlaceholderColor = Color.FromRgba(255, 255, 255, 100);

            signUpUsernameBG.BackgroundColor = Color.FromRgba(255, 255, 255, 100);
            signUpEmailBG.BackgroundColor = Color.FromRgba(255, 255, 255, 100);
            signUpPasswordBG.BackgroundColor = Color.FromRgba(255, 255, 255, 100);
            signUpConfirmPasswordBG.BackgroundColor = Color.FromRgba(255, 255, 255, 100);

            signUpUsername.PlaceholderColor = Color.FromRgba(255, 255, 255, 100);
            signUpEmail.PlaceholderColor = Color.FromRgba(255, 255, 255, 100);
            signUpPassword.PlaceholderColor = Color.FromRgba(255, 255, 255, 100);
            signUpConfirmPassword.PlaceholderColor = Color.FromRgba(255, 255, 255, 100);

            if (IsLogingIn == true)
            {
                SignUpMenu.IsVisible = false;
                LoginMenu.IsVisible = true;
            }
            else
            {
                LoginMenu.IsVisible = false;
                SignUpMenu.IsVisible = true;
            }

            loginViewPassword.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => ShowPassword()), });

            logIntoAccount.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => LogInToAccount()), });         
        }

        private void SwitchUserAccountChoice(object sender, EventArgs e)
        {
            if (IsLogingIn)
                SigningIn();
            else
                LoggingIn();
        }

        private void UserInputEntry(object sender, TextChangedEventArgs e)
        {
            if (sender is ExtendedEntry entry)
            {
                if (e.NewTextValue.Length > 0)
                    entry.Opacity = 1;
                else
                    entry.Opacity = 0.5;
            }
        }

        private async void GoToSecurityCheckPage()
        {
            await Navigation.PushModalAsync(new SecurityCheckView());
        }

        private void ShowPassword()
        {
            if (loginPassword.IsPassword)
                loginPassword.IsPassword = false;
            else
                loginPassword.IsPassword = true;

            App.Animations.ButtonClickAnimation(loginViewPassword);
        }

        private void LogInToAccount()
        {
            if (!string.IsNullOrWhiteSpace(loginUsername.Text) && !string.IsNullOrWhiteSpace(loginPassword.Text))
            {
                if (loginUsername.Text.Equals("Default") && loginPassword.Text.Equals("lasvegas1"))
                {
                    // Once the account has been created go to the login page
                    //if(Account.SecurityCheck.IsEnabled)
                        GoToSecurityCheckPage();
                    //else
                    //    GoToMainPage();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(loginUsername.Text) || !loginUsername.Text.Equals("Default"))
                    App.Animations.ShakeUnFilledElement(loginUsername);

                if (string.IsNullOrWhiteSpace(loginPassword.Text) || !loginPassword.Text.Equals("lasvegas1"))
                    App.Animations.ShakeUnFilledElement(loginPassword);
            }
        }

        private void LoggingIn()
        {
            IsLogingIn = true;

            LoginMenu.IsVisible = true;
            SignUpMenu.IsVisible = false;

            switchActivity.Text = "OR SIGN UP";
        }

        private void SigningIn()
        {
            IsLogingIn = false;

            SignUpMenu.IsVisible = true;
            LoginMenu.IsVisible = false;

            switchActivity.Text = "OR LOG IN";
        }
    }
}