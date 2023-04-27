// This file has been autogenerated from a class added in the UI designer.

using System;
using Core;
using Core.ViewModels;
using Foundation;
using ListMvvmIOS.Util;
using UIKit;

namespace ListMvvmIOS
{
	public partial class TaskDetailViewController : UIViewController
	{
		public TaskItem Task;
        public DetailViewModel viewModel;
        public MainViewModel mainViewModel;

        public TaskDetailViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			titleLabel.Text = Task.Title;
			descriptionLabel.Text = Task.Description;
			categoryLabel.Text = Task.Category;
            deleteButton.SetTitle(Constants.GetLocalizable(Constants.DeleteTaskLocalizable), UIControlState.Normal);
            UpdateDoneButton();
			doneButton.TouchUpInside += (s, e) =>
			{
                viewModel.ToggleCompleteCommand.Execute(null);
				NavigationController.PopViewController(true);
            };
            deleteButton.TouchUpInside += ConfirmDelete;
        }

        private void ConfirmDelete(object sender, EventArgs e)
        {
            var alert = UIAlertController.Create(Constants.GetLocalizable(Constants.DeleteTaskLocalizable), Constants.GetLocalizable(Constants.DeleteTaskMsgLocalizable), UIAlertControllerStyle.Alert);
            alert.AddAction(
                UIAlertAction.Create(Constants.GetLocalizable(Constants.NoLocalizable), UIAlertActionStyle.Cancel, null)
            );
            alert.AddAction(
                UIAlertAction.Create(Constants.GetLocalizable(Constants.YesLocalizable), UIAlertActionStyle.Destructive, (action) =>
                {
                    mainViewModel.Items.Remove(Task);
                    NavigationController.PopViewController(true);
                })
            );
            this.PresentViewController(alert, true, null);
        }

        private void UpdateDoneButton()
        {
            if (viewModel.IsComplete)
            {
                doneButton.SetTitle(Constants.GetLocalizable(Constants.PendingLocalizable), UIControlState.Normal);
            }
            else
            {
                doneButton.SetTitle(Constants.GetLocalizable(Constants.DoneLocalizable), UIControlState.Normal);
            }
        }
    }
}
