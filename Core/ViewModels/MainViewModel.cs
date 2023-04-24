using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Core;

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

        public ObservableCollection<TaskItem> Items { get; }

        public ICommand SubmitCommand { get; }
        public ICommand ClearListCommand { get; }

        public MainViewModel()
        {
            Items = new ObservableCollection<TaskItem>();
            SubmitCommand = new RelayCommand(Submit, CanSubmit);
            ClearListCommand = new RelayCommand(ClearList);
        }

        private void Submit()
        {
            var newItem = new TaskItem { Title = Text, Description = Description, Category = Category };
            Items.Add(newItem);
            Text = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
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
    }
}
