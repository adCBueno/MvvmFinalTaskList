using System;
using Core;
using Core.ViewModels;
using UIKit;

namespace ListMvvmiOS
{
    public class TaskDetailViewController : UIViewController
    {
        private TaskItem _taskItem;
        private MainViewModel _mainViewModel;
        private DetailViewModel _viewModel;

        private UILabel _titleTextView;
        private UILabel _descriptionTextView;
        private UILabel _categoryTextView;
        private UIButton _doneButton;
        private UIButton _deleteTaskButton;

        public TaskDetailViewController(TaskItem taskItem, MainViewModel mainViewModel)
        {
            _taskItem = taskItem;
            _mainViewModel = mainViewModel;
            _viewModel = new DetailViewModel(taskItem, mainViewModel);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;

            _titleTextView = new UILabel();
            _descriptionTextView = new UILabel();
            _categoryTextView = new UILabel();
            _doneButton = new UIButton(UIButtonType.System);
            _deleteTaskButton = new UIButton(UIButtonType.System);

            UpdateData(_taskItem);

            _titleTextView.TranslatesAutoresizingMaskIntoConstraints = false;
            _descriptionTextView.TranslatesAutoresizingMaskIntoConstraints = false;
            _categoryTextView.TranslatesAutoresizingMaskIntoConstraints = false;
            _doneButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _deleteTaskButton.TranslatesAutoresizingMaskIntoConstraints = false;

            _doneButton.TouchUpInside += (s, e) =>
            {
                _viewModel.ToggleCompleteCommand.Execute(null);
                UpdateDoneButton();
                NavigationController.PopViewController(true);
            };

            _deleteTaskButton.SetTitle("Delete Task", UIControlState.Normal);
            _deleteTaskButton.TouchUpInside += (s, e) =>
            {
                UIAlertController alert = UIAlertController.Create("Delete Task", "Are you sure you want to delete this task?", UIAlertControllerStyle.Alert);
                UIAlertAction yesAction = UIAlertAction.Create("Yes", UIAlertActionStyle.Default, action =>
                {
                    _mainViewModel.Items.Remove(_taskItem);
                    NavigationController.PopViewController(true);
                });
                UIAlertAction noAction = UIAlertAction.Create("No", UIAlertActionStyle.Cancel, null);

                alert.AddAction(yesAction);
                alert.AddAction(noAction);

                PresentViewController(alert, true, null);
            };

            View.AddSubview(_titleTextView);
            View.AddSubview(_descriptionTextView);
            View.AddSubview(_categoryTextView);
            View.AddSubview(_doneButton);
            View.AddSubview(_deleteTaskButton);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                _titleTextView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 16),
                _titleTextView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 16),
                _titleTextView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -16),

                _descriptionTextView.TopAnchor.ConstraintEqualTo(_titleTextView.BottomAnchor, 16),
                _descriptionTextView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 16),
                _descriptionTextView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -16),

                _categoryTextView.TopAnchor.ConstraintEqualTo(_descriptionTextView.BottomAnchor, 16),
                _categoryTextView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 16),
                _categoryTextView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -16),

                _doneButton.TopAnchor.ConstraintEqualTo(_categoryTextView.BottomAnchor, 16),
                _doneButton.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 16),
                _doneButton.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -16),

                _deleteTaskButton.TopAnchor.ConstraintEqualTo(_doneButton.BottomAnchor, 16),
                                _deleteTaskButton.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 16),
                _deleteTaskButton.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -16),
            });
        }

        public void UpdateData(TaskItem taskItem)
        {
            _taskItem = taskItem;
            _viewModel = new DetailViewModel(taskItem, _mainViewModel);

            _titleTextView.Text = _taskItem.Title;
            _descriptionTextView.Text = _taskItem.Description;
            _categoryTextView.Text = _taskItem.Category;

            UpdateDoneButton();
        }

        private void UpdateDoneButton()
        {
            if (_taskItem.IsComplete)
            {
                _doneButton.SetTitle("Pending", UIControlState.Normal);
            }
            else
            {
                _doneButton.SetTitle("Done", UIControlState.Normal);
            }
        }
    }
}


