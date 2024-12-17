using DailyScheduler.Models;
using DailyScheduler.ViewModels;

namespace DailyScheduler;

public partial class PastEventsPage : ContentPage
{
    public PastEventsPage(PastEventsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PastEventsViewModel viewModel)
        {
            viewModel.LoadEventsAsync().ConfigureAwait(false);
        }
    }
}
