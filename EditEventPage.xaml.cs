using DailyScheduler.ViewModels;

namespace DailyScheduler;

public partial class EditEventPage : ContentPage
{
    public EditEventPage(EditEventViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is EditEventViewModel viewModel)
        {
            viewModel.LoadUsersCommand.Execute(null);
        }
    }
}

