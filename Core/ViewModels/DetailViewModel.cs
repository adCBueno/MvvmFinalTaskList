using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Core;

namespace Core.ViewModels
{
    public class DetailViewModel : ObservableObject
    {
        private readonly TaskItem _taskItem;
        private readonly MainViewModel _mainViewModel;

        public ICommand ToggleCompleteCommand { get; }

        public DetailViewModel(TaskItem taskItem, MainViewModel mainViewModel)
        {
            _taskItem = taskItem;
            _mainViewModel = mainViewModel;
            ToggleCompleteCommand = new RelayCommand(ToggleComplete);
        }

        private void ToggleComplete()
        {
            _taskItem.IsComplete = !_taskItem.IsComplete;
            _mainViewModel.UpdateTaskItem(_taskItem);
        }
    }
}
