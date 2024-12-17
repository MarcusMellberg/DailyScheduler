using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DailyScheduler.Models;
using DailyScheduler.Services;

namespace DailyScheduler.Helpers
{
    public static class UserHelper
    {
        public static async Task<List<User>> LoadUsersAsync(JsonDataService jsonDataService)
        {
            try
            {
                var users = await jsonDataService.LoadUsersAsync() ?? new ObservableCollection<User>();
                return users.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading users: {ex.Message}");
                return new List<User>();
            }
        }
    }
}


