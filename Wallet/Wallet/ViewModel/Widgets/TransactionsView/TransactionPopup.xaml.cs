using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private TransactionsView transactionView;

        public TransactionPopup(TransactionsView transView)
        {
            InitializeComponent();

            this.transactionView = transView;
        }

        private void SubmitPayment(object sender, System.EventArgs e)
        {
            transactionView.AddTransaction(description.Text, amount.Text, date.Text);
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}