using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Core;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Core.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _text;
        private string _description;
        private string _category;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public ObservableCollection<TaskItem> Items { get; }

        public ICommand SubmitCommand { get; }
        public ICommand ClearListCommand { get; }
        public ICommand GetLocationCommand { get; }

        public MainViewModel()
        {
            Items = new ObservableCollection<TaskItem>();
            SubmitCommand = new RelayCommand(Submit, CanSubmit);
            ClearListCommand = new RelayCommand(ClearList);
            GetLocationCommand = new AsyncRelayCommand(GetLocationAsync);
        }

        private void Submit()
        {
            await GetLocationAsync();
            var task = new TaskItem
            {
                Title = Text,
                Description = Description,
                Category = Category,
                Latitude = Latitude,
                Longitude = Longitude
            };

            Items.Add(task);
            Text = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            Latitude = 0;
            Longitude = 0;
        }

        private bool CanSubmit()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }

        private void ClearList()
        {
            Items.Clear();
        }

        public void UpdateTaskItem(TaskItem updatedTaskItem)
        {
            int index = Items.IndexOf(updatedTaskItem);
            if (index >= 0)
            {
                Items[index] = updatedTaskItem;
                OnPropertyChanged(nameof(Items));
            }
        }

        private async Task GetLocationAsync()
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
        }

        public void SetLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
