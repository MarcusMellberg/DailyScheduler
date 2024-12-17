using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace DailyScheduler.Components
{
    public partial class TitleBar : ContentView
    {
        public TitleBar()
        {
            InitializeComponent();
        }

        private void OnMenuButtonClicked(object sender, EventArgs e)
        {
            var menuPopup = new MenuPopup();

            menuPopup.Anchor = sender as View;
            Application.Current.MainPage?.ShowPopup(menuPopup);
        }
    }
}
