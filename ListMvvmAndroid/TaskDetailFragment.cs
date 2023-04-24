using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using AndroidX.Fragment.App;
using Core;
using Core.ViewModels;

namespace ListMvvmAndroid
{
    public class TaskDetailFragment : Fragment
    {
        private TaskItem _taskItem;
        private DetailViewModel _viewModel;

        public static TaskDetailFragment NewInstance(TaskItem taskItem, MainViewModel mainViewModel)
        {
            TaskDetailFragment fragment = new TaskDetailFragment
            {
                _taskItem = taskItem,
                _viewModel = new DetailViewModel(taskItem, mainViewModel)
            };
            return fragment;
        }

        public static TaskDetailFragment NewInstance(TaskItem taskItem)
        {
            TaskDetailFragment fragment = new TaskDetailFragment
            {
                _taskItem = taskItem
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
            Button doneButton = view.FindViewById<Button>(Resource.Id.doneButton);

            titleTextView.Text = _taskItem.Title;
            descriptionTextView.Text = _taskItem.Description;
            categoryTextView.Text = _taskItem.Category;

            if (_taskItem.IsComplete)
            {
                view.Background = ContextCompat.GetDrawable(this.Context, Resource.Drawable.completed_task_background);
                doneButton.Text = "Deshacer";
            }
            else
            {
                view.Background = ContextCompat.GetDrawable(this.Context, Resource.Drawable.incomplete_task_background);
                doneButton.Text = "Hecho";
            }

            doneButton.Click += (s, e) =>
            {
               
            };

            return view;
        }

        public void UpdateData(TaskItem taskItem)
        {

            _taskItem = taskItem;
            if (View != null)
            {
                TextView titleTextView = View.FindViewById<TextView>(Resource.Id.titleTextView);
                TextView descriptionTextView = View.FindViewById<TextView>(Resource.Id.descriptionTextView);
                TextView categoryTextView = View.FindViewById<TextView>(Resource.Id.categoryTextView);
                Button doneButton = View.FindViewById<Button>(Resource.Id.doneButton);

                titleTextView.Text = _taskItem.Title;
                descriptionTextView.Text = _taskItem.Description;
                categoryTextView.Text = _taskItem.Category;
            }
        }
    }
}