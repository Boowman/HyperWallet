using Xamarin.Forms;

namespace Wallet
{
    public class ExtendedEntry : Entry
    {
        /// <summary>
        /// Vertical Alignment for the text within the entry
        /// </summary>
        public TextAlignment VerticalTextAlignment { get; set; }

        /// <summary>
        /// Adding padding to the content within the entry
        /// </summary>
        public Thickness Padding { get; set; }

        /// <summary>
        /// Default value for how many characters are allowed to be typed in
        /// </summary>
        public int MaxCharacters { get; set; }

        /// <summary>
        /// Making sure to only update the width of an entry that should be resized
        /// otherwise don't do it.
        /// </summary>
        public bool ResizeField { get; set; }

        /// <summary>
        /// We have the option to display a white underline inside the text entry
        /// </summary>
        public bool Underline { get; set; }
    }
}