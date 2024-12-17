using DailyScheduler.ViewModels;

namespace DailyScheduler;

public partial class AddEventPage : ContentPage
{
    public AddEventPage(AddEventViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AddEventViewModel viewModel)
        {
            viewModel.LoadUsersCommand.Execute(null);
        }
    }
}
