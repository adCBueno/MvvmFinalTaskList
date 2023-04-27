using AndroidX.Fragment.App;
using Core;
using Core.ViewModels;
using ListMvvmAndroid;

namespace ListMvvmAndroid.Services
{
    public interface INavigationService
    {
        void GoToTaskDetail(TaskItem taskItem, MainViewModel mainViewModel);
    }

    public class NavigationService : INavigationService
    {
        private readonly FragmentManager _fragmentManager;

        public NavigationService(FragmentManager fragmentManager)
        {
            _fragmentManager = fragmentManager;
        }

        public void GoToTaskDetail(TaskItem taskItem, MainViewModel mainViewModel)
        {
            TaskDetailFragment taskDetailFragment = (TaskDetailFragment)_fragmentManager.FindFragmentByTag(Constants.Constants.TaskDetailFragment);
            if (taskDetailFragment == null)
            {
                taskDetailFragment = new TaskDetailFragment(taskItem, mainViewModel, this);
                _fragmentManager.BeginTransaction()
                    .AddToBackStack(null)
                    .Add(Android.Resource.Id.Content, taskDetailFragment, Constants.Constants.TaskDetailFragment)
                    .Commit();
            }
            else
            {
                taskDetailFragment.UpdateData(taskItem);
            }
        }
    }
}
