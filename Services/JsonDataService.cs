using CommunityToolkit.Mvvm.Input;
using DailyScheduler.Helpers;
using DailyScheduler.Models;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Collections.ObjectModel;
using MauiFileSystem = Microsoft.Maui.Storage.FileSystem;

namespace DailyScheduler.Services
{
    public class JsonDataService
    {
        private readonly string _dataPath;

        public JsonDataService()
        {
            _dataPath = MauiFileSystem.AppDataDirectory;
        }

        // Metoder för Event
        public async Task<ObservableCollection<Event>?> LoadEventsAsync()
        {
            try
            {
                var filePath = Path.Combine(_dataPath, "events.json");
                if (File.Exists(filePath))
                {
                    var jsonString = await File.ReadAllTextAsync(filePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var events = JsonSerializer.Deserialize<List<Event>>(jsonString, options);
                    return events != null ? new ObservableCollection<Event>(events) : null;
                }
                else
                {
                    Debug.WriteLine($"File {filePath} not found.");
                    return null;
                }
            }
            catch (JsonException jsonEx)
            {
                Debug.WriteLine($"JSON error: {jsonEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading events: {ex.Message}");
                return null;
            }
        }

        public async Task SaveEventAsync(Event newEvent)
        {
            try
            {
                var events = await LoadEventsAsync() ?? new ObservableCollection<Event>();
                events.Add(newEvent);
                var filePath = Path.Combine(_dataPath, "events.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(events, options);
                await File.WriteAllTextAsync(filePath, jsonString);
                Debug.WriteLine($"Event saved successfully to {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving event: {ex.Message}");
            }
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            try
            {
                var events = await LoadEventsAsync() ?? new ObservableCollection<Event>();
                var index = events.ToList().FindIndex(e => e.Id == updatedEvent.Id);
                if (index != -1)
                {
                    events[index] = updatedEvent;
                }
                var filePath = Path.Combine(_dataPath, "events.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(events, options);
                await File.WriteAllTextAsync(filePath, jsonString);
                Debug.WriteLine($"Event updated successfully to {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating event: {ex.Message}");
            }
        }

        public async Task DeleteEventAsync(Event eventToDelete)
        {
            try
            {
                var events = await LoadEventsAsync() ?? new ObservableCollection<Event>();
                events.Remove(events.FirstOrDefault(e => e.Id == eventToDelete.Id));
                var filePath = Path.Combine(_dataPath, "events.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(events, options);
                await File.WriteAllTextAsync(filePath, jsonString);
                Debug.WriteLine($"Event deleted successfully from {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting event: {ex.Message}");
            }
        }

        // Metoder för User
        public async Task<ObservableCollection<User>?> LoadUsersAsync()
        {
            try
            {
                var filePath = Path.Combine(_dataPath, "users.json");
                if (File.Exists(filePath))
                {
                    var jsonString = await File.ReadAllTextAsync(filePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var users = JsonSerializer.Deserialize<List<User>>(jsonString, options);
                    return users != null ? new ObservableCollection<User>(users) : null;
                }
                else
                {
                    Debug.WriteLine($"File {filePath} not found.");
                    return null;
                }
            }
            catch (JsonException jsonEx)
            {
                Debug.WriteLine($"JSON error: {jsonEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading users: {ex.Message}");
                return null;
            }
        }

        public async Task SaveUserAsync(User newUser)
        {
            try
            {
                var users = await LoadUsersAsync() ?? new ObservableCollection<User>();
                users.Add(newUser);
                var filePath = Path.Combine(_dataPath, "users.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(users, options);
                await File.WriteAllTextAsync(filePath, jsonString);
                Debug.WriteLine($"User saved successfully to {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving user: {ex.Message}");
            }
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            try
            {
                var users = await LoadUsersAsync() ?? new ObservableCollection<User>();
                var index = users.ToList().FindIndex(u => u.Id == updatedUser.Id);
                if (index != -1)
                {
                    users[index] = updatedUser;
                }
                var filePath = Path.Combine(_dataPath, "users.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(users, options);
                await File.WriteAllTextAsync(filePath, jsonString);
                Debug.WriteLine($"User updated successfully to {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating user: {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(User userToDelete)
        {
            try
            {
                var users = await LoadUsersAsync() ?? new ObservableCollection<User>();
                users.Remove(users.FirstOrDefault(u => u.Id == userToDelete.Id));
                var filePath = Path.Combine(_dataPath, "users.json");
                var options = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(users, options);
                await File.WriteAllTextAsync(filePath, jsonString);
                Debug.WriteLine($"User deleted successfully from {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting user: {ex.Message}");
            }
        }
    }
}
