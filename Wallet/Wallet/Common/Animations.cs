using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Wallet
{
    public class Animations : Page
    {
        public async void ShakeUnFilledElement(View sender)
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

        public async void BackgroundAnimation(object sender)
        {
            if (sender is View obj)
            {
                Log.Warning("Check", "Background Animation");

                uint timer = 25;
                obj.BackgroundColor = Color.Accent;
                await obj.ScaleTo(1.05, timer);
                await obj.ScaleTo(1.10, timer);
                await obj.ScaleTo(1.15, timer);
                await obj.ScaleTo(1.10, timer);
                await obj.ScaleTo(1.05, timer);
                await obj.ScaleTo(1, timer);
                obj.BackgroundColor = Color.Transparent;
                obj.Scale = 1;
            }
        }

        public async void ButtonPress(View obj)
        {
            uint timer = 25;
            obj.BackgroundColor = Color.Accent;
            await obj.ScaleTo(0.95, timer);
            await obj.ScaleTo(0.90, timer);
            await obj.ScaleTo(0.85, timer);
            await obj.ScaleTo(0.90, timer);
            await obj.ScaleTo(0.95, timer);
            await obj.ScaleTo(1, timer);
            obj.BackgroundColor = Color.Transparent;
            obj.Scale = 1;
        }

        public async void ButtonClickAnimation(View obj)
        {
            uint timeout = 25;
            await obj.ScaleTo(1.1, timeout);
            await obj.ScaleTo(1.2, timeout);
            await obj.ScaleTo(1.3, timeout);
            await obj.ScaleTo(1.2, timeout);
            await obj.ScaleTo(1.1, timeout);
            await obj.ScaleTo(1, timeout);
            obj.Scale = 1;
        }
    }
}
