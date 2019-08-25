using Xamarin.Forms;

namespace Wallet
{
    public class ExtendedPicker : Picker
    {
        /// Button padding for the text
        /// </summary>
        public Thickness Padding { get; set; }

        /// <summary>
        /// Assigning the color of the text when simply displayed
        /// </summary>
        public string DisplayTextColor { get { return "#FFFFFF"; } set { } }

        /// <summary>
        /// We need to keep a reference of the title because when we use the picker
        /// the title will be hidden
        /// </summary>
        public string OldTitle { get; set; }
    }
}
