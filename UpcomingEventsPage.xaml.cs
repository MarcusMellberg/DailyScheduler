using DailyScheduler.Models;
using DailyScheduler.ViewModels;

namespace DailyScheduler;

public partial class UpcomingEventsPage : ContentPage
{
    public UpcomingEventsPage(UpcomingEventsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is UpcomingEventsViewModel viewModel)
        {
            viewModel.LoadEventsAsync().ConfigureAwait(false);
        }
    }
}
