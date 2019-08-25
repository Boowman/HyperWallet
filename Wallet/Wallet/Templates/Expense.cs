using Xamarin.Forms;

namespace Wallet
{
    public class Expense
    {
        public string Label    { get; set; }
        public string Value    { get; set; }
        public string Color    { get; set; }

        public string DisplayQuickExpense
        {
            get
            {
                return string.Format("£{0} {1}", Value, Label);
            }
        }
    }
}
