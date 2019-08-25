using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Wallet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WidgetsView : ContentPage
	{
        /// <summary>
        /// List of available widgets
        /// </summary>
        private List<Widget> WidgetsAvailable = new List<Widget>()
        {
            new TransactionsView(),
            new CashFlow(),
        };

        public WidgetsView (object page)
		{
			InitializeComponent ();

            if(page is CardNew cardNew)
                InstantiateWidgets(cardNew);
            else if(page is CardDetails cardDetails)
                InstantiateWidgets(cardDetails);

            backButton.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => GoBackMenu()), });
        }

        /// <summary>
        /// Spawning all the available widgets for this specific card
        /// We check what widgets the card already contains and if it does contain it then we don't display it
        /// </summary>
        /// <param name="prevPage">Page we came from</param>
        private void InstantiateWidgets(object prevPage)
        {
            if (prevPage is CardNew newPage)
            {
                for (int i = 0; i < WidgetsAvailable.Count; i++)
                {
                    bool widgetAdded = false;

                    for (int j = 0; j < newPage.WidgetsAdded.Count; j++)
                    {
                        if(newPage.WidgetsAdded[j].GetType() == WidgetsAvailable[i].GetType())
                        {
                            widgetAdded = true;
                            break;
                        }
                    }

                    if(widgetAdded == false)
                    {
                        WidgetsAvailable[i].SetupWidget(newPage);
                        widgetsHolder.Children.Add(WidgetsAvailable[i]);
                    }
                }
            }
            else if (prevPage is CardDetails detailsPage)
            {
                var widgets = detailsPage.StorredCard.WidgetsAvailable;

                for (int i = 0; i < WidgetsAvailable.Count; i++)
                {
                    bool widgetAdded = false;

                    for (int j = 0; j < widgets.Count; j++)
                    {
                        if (widgets[j].GetType() == WidgetsAvailable[i].GetType())
                        {
                            widgetAdded = true;
                            break;
                        }
                    }

                    if (widgetAdded == false)
                    {
                        WidgetsAvailable[i].SetupWidget(detailsPage);
                        widgetsHolder.Children.Add(WidgetsAvailable[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Going to the previous page
        /// </summary>
        private async void GoBackMenu()
        {
            await Navigation.PopAsync();
        }
    }
}