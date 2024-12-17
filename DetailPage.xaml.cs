using DailyScheduler.ViewModels;

namespace DailyScheduler;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailsViewModel detailVM)
	{
		InitializeComponent();
		BindingContext = detailVM;
    }
}