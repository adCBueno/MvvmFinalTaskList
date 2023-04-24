using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;

namespace Core.ViewModels
{
    public class DetailViewModel : ObservableObject
    {
        private readonly TaskItem _taskItem;
        private readonly MainViewModel _mainViewModel;

        public string Title => _taskItem.Title;
        public string Description => _taskItem.Description;
        public string Category => _taskItem.Category;

        public bool IsComplete
        {
            get => _taskItem.IsComplete;
            set
            {
                if (_taskItem.IsComplete != value)
                {
                    _taskItem.IsComplete = value;
                    RaisePropertyChanged(nameof(IsComplete));
                    RaisePropertyChanged(nameof(ButtonText));
                    _mainViewModel.UpdateTaskItem(_taskItem);
                }
            }
        }

        public string ButtonText => IsComplete ? "Deshacer" : "Hecho";
        public RelayCommand ToggleCompleteCommand { get; }

        public DetailViewModel(TaskItem taskItem, MainViewModel mainViewModel)
        {
            _taskItem = taskItem;
            _mainViewModel = mainViewModel;

            ToggleCompleteCommand = new RelayCommand(ToggleComplete);
        }

        private void ToggleComplete()
        {
            IsComplete = !IsComplete;
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
