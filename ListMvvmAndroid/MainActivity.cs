using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Lifecycle;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Core;
using Core.ViewModels;
using ListMvvmAndroid.Services;
using static Android.Content.ClipData;

namespace ListMvvmAndroid
{
    public class MainActivity : AppCompatActivity
    {
        private MainViewModel _viewModel;
        private TaskItemAdapter _adapter;
        private INavigationService _navigationService;
        private TaskItem _taskItem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _viewModel = new MainViewModel();

            EditText titleEditText = FindViewById<EditText>(Resource.Id.editTextTitle);
            EditText descriptionEditText = FindViewById<EditText>(Resource.Id.editTextDescription);
            EditText categoryEditText = FindViewById<EditText>(Resource.Id.editTextCategory);
            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);
            Button clearListButton = FindViewById<Button>(Resource.Id.clearListButton);
            ListView listView = FindViewById<ListView>(Resource.Id.listView);

            titleEditText.TextChanged += (s, e) =>
            {
                _viewModel.Text = titleEditText.Text;
                submitButton.Enabled = _viewModel.SubmitCommand.CanExecute(null);
                clearListButton.Enabled = _viewModel.ClearListCommand.CanExecute(null);
            };

            descriptionEditText.TextChanged += (s, e) =>
            {
                _viewModel.Description = descriptionEditText.Text;
            };

            categoryEditText.TextChanged += (s, e) =>
            {
                _viewModel.Category = categoryEditText.Text;
            };

            submitButton.Click += (s, e) =>
            {
                if (_viewModel.SubmitCommand.CanExecute(null))
                {
                    _viewModel.SubmitCommand.Execute(null);
                    UpdateListView(listView);
                }
                titleEditText.Text = "";
                descriptionEditText.Text = "";
                categoryEditText.Text = "";
            };

            clearListButton.Click += (s, e) =>
            {
                if (_viewModel.ClearListCommand.CanExecute(null))
                {
                    _viewModel.ClearListCommand.Execute(null);
                    UpdateListView(listView);
                }
            };

            listView.ItemClick += (s, e) =>
            {
                TaskItem selectedTask = _viewModel.Items[e.Position];

                TaskDetailFragment taskDetailFragment = (TaskDetailFragment)SupportFragmentManager.FindFragmentByTag(Constants.Constants.TaskDetailFragment);
                if (taskDetailFragment == null)
                {
                    TaskItem item = _viewModel.Items[e.Position];
                    _navigationService.GoToTaskDetail(item, _viewModel);
                    taskDetailFragment = new TaskDetailFragment(selectedTask, _viewModel, _navigationService);
                    SupportFragmentManager.BeginTransaction()
                        .AddToBackStack(null)
                        .Add(Android.Resource.Id.Content, taskDetailFragment, Constants.Constants.TaskDetailFragment)
                        .Commit();
                }
                else
                {
                    taskDetailFragment.UpdateData(selectedTask);
                }
            };

            _navigationService = new NavigationService(SupportFragmentManager);
            TaskDetailFragment taskListFragment = new TaskDetailFragment( _taskItem, _viewModel, _navigationService);
            SupportFragmentManager.BeginTransaction()
                .Add(Android.Resource.Id.Content, taskListFragment, Constants.Constants.TaskDetailFragment)
                .Commit();

            _viewModel.Items.CollectionChanged += (s, e) =>
            {
                UpdateListView(listView);
            };
            UpdateListView(listView);
        }

        private void UpdateListView(ListView listView)
        {
            TaskItemAdapter adapter = new TaskItemAdapter(this, _viewModel.Items);
            listView.Adapter = adapter;
        }
    }
}
