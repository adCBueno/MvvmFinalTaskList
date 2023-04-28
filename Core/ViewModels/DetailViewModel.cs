using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Core;
using System;

namespace Core.ViewModels
{
    public class DetailViewModel : ObservableObject
    {
        private readonly TaskItem _taskItem;
        private readonly MainViewModel _mainViewModel;
        public event EventHandler IsCompleteChanged;
        private DetailViewModel _viewModel;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICommand ToggleCompleteCommand { get; }

        public DetailViewModel(TaskItem taskItem, MainViewModel mainViewModel)
        {
            _taskItem = taskItem;
            _mainViewModel = mainViewModel;
            ToggleCompleteCommand = new RelayCommand(TaskComplete);
            OnPropertyChanged(nameof(IsComplete));

            Latitude = taskItem.Latitude;
            Longitude = taskItem.Longitude;
        }

        public bool IsComplete
        {
            get => _taskItem.IsComplete;
            set
            {
                if (_taskItem.IsComplete != value)
                {
                    _taskItem.IsComplete = value;
                    OnPropertyChanged();
                    IsCompleteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void TaskComplete()
        {
            IsComplete = !IsComplete;
            _mainViewModel.UpdateTaskItem(_taskItem);
        }
    }
}
