using Xamarin.Forms;

namespace Wallet
{
    public class ExtendedButton : Button
    {
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create("Padding", typeof(Thickness), typeof(ExtendedButton), default(Thickness));

        public Thickness Padding {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        /// <summary>
        /// Applying a border to the button
        /// Left - Top - Right - Bottom
        /// </summary>
        public int[] Border { get; set; }

        /// <summary>
        /// Assigning the color of the text when simply hovered
        /// </summary>
        public string PressedTextColor { get; set; }
    }
}
