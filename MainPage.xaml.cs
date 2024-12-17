using DailyScheduler.Services;
using DailyScheduler.ViewModels;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Xamarin.Essentials;  // För att använda Xamarin Essentials FileSystem
using Microsoft.Maui.Controls;  // För att använda MAUI Controls
using DailyScheduler.Enums;
using DailyScheduler.Models;

namespace DailyScheduler
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

            LogOriginalFiles();
        }

        private MainViewModel ViewModel
        {
            get
            {
                if (BindingContext is MainViewModel viewModel)
                {
                    return viewModel;
                }
                else
                {
                    throw new InvalidOperationException("BindingContext is not of type MainViewModel.");
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadEventsCommand.Execute(null);
            ViewModel.LoadUsersCommand.Execute(null);
        }

        private void LogOriginalFiles()
        {
            LogFileContent("events.json");
            LogFileContent("users.json");
        }

        private void LogFileContent(string filename)
        {
            try
            {
                string filePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, filename);
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    Debug.WriteLine($"Loaded {filename}: {jsonContent}");
                }
                else
                {
                    Debug.WriteLine($"File {filePath} not found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading {filename}: {ex.Message}");
            }
        }
    }
}
