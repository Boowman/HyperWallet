using Xamarin.Forms;

namespace Wallet
{ 
    public class ExtendedStackLayout : StackLayout
    {
        public bool EnableGradient { get; set; }

        public string StartColor { get; set; }  // Top of the screen
        public string EndColor { get; set; }    // Bottom of the screen
    }
}
