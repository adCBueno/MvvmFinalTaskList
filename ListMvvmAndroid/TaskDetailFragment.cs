using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.Fragment.App;
using Core;
using Core.ViewModels;
using Android.App;
using Android.Graphics;

namespace ListMvvmAndroid
{
    public class TaskDetailFragment : AndroidX.Fragment.App.Fragment
    {
        private TaskItem _taskItem;
        private MainViewModel _mainViewModel;
        private DetailViewModel _viewModel;
        private Button doneButton;

        public static TaskDetailFragment NewInstance(TaskItem taskItem, MainViewModel mainViewModel)
        {
            TaskDetailFragment fragment = new TaskDetailFragment
            {
                _taskItem = taskItem,
                _mainViewModel = mainViewModel,
                _viewModel = new DetailViewModel(taskItem, mainViewModel)
            };
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_task_detail, container, false);
            view.SetBackgroundColor(Android.Graphics.Color.White);

            TextView titleTextView = view.FindViewById<TextView>(Resource.Id.titleTextView);
            TextView descriptionTextView = view.FindViewById<TextView>(Resource.Id.descriptionTextView);
            TextView categoryTextView = view.FindViewById<TextView>(Resource.Id.categoryTextView);
            Button deleteTaskButton = view.FindViewById<Button>(Resource.Id.deleteTaskButton);
            doneButton = view.FindViewById<Button>(Resource.Id.doneButton);

            titleTextView.Text = _taskItem.Title;
            descriptionTextView.Text = _taskItem.Description;
            categoryTextView.Text = _taskItem.Category;

            UpdateDoneButton();

            doneButton.Click += (s, e) =>
            {
                _viewModel.ToggleCompleteCommand.Execute(null);
                UpdateDoneButton();
                ParentFragmentManager.PopBackStack();
            };

            deleteTaskButton.Click += (s, e) =>
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this.Activity);
                builder.SetTitle(Constants.Constants.DeleteTask);
                builder.SetMessage(Constants.Constants.MessageDeleteTask);
                builder.SetPositiveButton(Constants.Constants.Yes, (sender, args) =>
                {
                    _mainViewModel.Items.Remove(_taskItem);
                    Toast.MakeText(Activity, Constants.Constants.MessageDeleteTaskSuccess, ToastLength.Short).Show();
                    ParentFragmentManager.PopBackStack();                    
                });                
                builder.SetNegativeButton(Constants.Constants.No, (sender, args) => {});
                AlertDialog dialog = builder.Create();
                dialog.Show();
            };

            return view;
        }

        public void UpdateData(TaskItem taskItem)
        {
            _taskItem = taskItem;
            _viewModel = new DetailViewModel(taskItem, _mainViewModel);

            TextView titleTextView = View.FindViewById<TextView>(Resource.Id.titleTextView);
            TextView descriptionTextView = View.FindViewById<TextView>(Resource.Id.descriptionTextView);
            TextView categoryTextView = View.FindViewById<TextView>(Resource.Id.categoryTextView);

            titleTextView.Text = _taskItem.Title;
            descriptionTextView.Text = _taskItem.Description;
            categoryTextView.Text = _taskItem.Category;
        }

        private void UpdateDoneButton()
        {
            if (_taskItem.IsComplete)
            {
                doneButton.Text = Constants.Constants.Pending;
            }
            else
            {
                doneButton.Text = Constants.Constants.Done;
            }
        }
    }
}
