using CommunityToolkit.Mvvm;
using Microsoft.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DailyScheduler.Services;
using DailyScheduler.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace DailyScheduler
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

            builder.Services.AddSingleton<JsonDataService>();


            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddEventPage>();
            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<EditEventPage>();
            builder.Services.AddTransient<UpcomingEventsPage>();
            builder.Services.AddTransient<PastEventsPage>();

            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<AddEventViewModel>();
            builder.Services.AddTransient<DetailsViewModel>();
            builder.Services.AddTransient<EditEventViewModel>();
            builder.Services.AddTransient<UpcomingEventsViewModel>();
            builder.Services.AddTransient<PastEventsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
