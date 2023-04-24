using System;
using UIKit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.ViewModels;
using System.Collections.ObjectModel;
using Foundation;
using Core;
using ListMvvmiOS;

namespace ListMvvmIOS
{
    public partial class ViewController : UIViewController
    {
        private MainViewModel _viewModel;
        private UITextField _titleTextField;
        private UITextField _descriptionTextField;
        private UITextField _categoryTextField;
        private UIButton _submitButton;
        private UIButton _clearListButton;
        private UITableView _tableView;

        public ViewController()
        {
            _viewModel = new MainViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            _titleTextField = new UITextField
            {
                Placeholder = "Task title",
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            _descriptionTextField = new UITextField
            {
                Placeholder = "Task description",
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            _categoryTextField = new UITextField
            {
                Placeholder = "Task category",
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            _submitButton = UIButton.FromType(UIButtonType.System);
            _submitButton.SetTitle("Add task", UIControlState.Normal);

            _clearListButton = UIButton.FromType(UIButtonType.System);
            _clearListButton.SetTitle("Clear list", UIControlState.Normal);

            _tableView = new UITableView();

            AddSubviewsAndConstraints();

            WireUpEvents();
            UpdateTableView();
        }

        private void AddSubviewsAndConstraints()
        {
            View.AddSubview(_titleTextField);
            View.AddSubview(_descriptionTextField);
            View.AddSubview(_categoryTextField);
            View.AddSubview(_submitButton);
            View.AddSubview(_clearListButton);
            View.AddSubview(_tableView);

            _titleTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            _descriptionTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            _categoryTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            _submitButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _clearListButton.TranslatesAutoresizingMaskIntoConstraints = false;
            _tableView.TranslatesAutoresizingMaskIntoConstraints = false;

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                _titleTextField.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 16),
                _titleTextField.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 16),
                _titleTextField.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -16),

                _descriptionTextField.TopAnchor.ConstraintEqualTo(_titleTextField.BottomAnchor, 16),
                _descriptionTextField.LeadingAnchor.ConstraintEqualTo(_titleTextField.LeadingAnchor),
                _descriptionTextField.TrailingAnchor.ConstraintEqualTo(_titleTextField.TrailingAnchor),

                _categoryTextField.TopAnchor.ConstraintEqualTo(_descriptionTextField.BottomAnchor, 16),
                _categoryTextField.LeadingAnchor.ConstraintEqualTo(_descriptionTextField.LeadingAnchor),
                _categoryTextField.TrailingAnchor.ConstraintEqualTo(_descriptionTextField.TrailingAnchor),

                _submitButton.TopAnchor.ConstraintEqualTo(_categoryTextField.BottomAnchor, 16),
                _submitButton.LeadingAnchor.ConstraintEqualTo(_categoryTextField.LeadingAnchor),

                _clearListButton.LeadingAnchor.ConstraintEqualTo(_submitButton.TrailingAnchor, 16),
                _clearListButton.CenterYAnchor.ConstraintEqualTo(_submitButton.CenterYAnchor),

                _tableView.TopAnchor.ConstraintEqualTo(_submitButton.BottomAnchor, 16),
                _tableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor),
                _tableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor),
                _tableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor)
            });
        }

        private void WireUpEvents()
        {
            _submitButton.TouchUpInside += (s, e) =>
            {
                if (_viewModel.SubmitCommand.CanExecute(null))
                {
                    _viewModel.SubmitCommand.Execute(null);
                    UpdateTableView();
                }
                _titleTextField.Text = "";
                _descriptionTextField.Text = "";
                _categoryTextField.Text = "";
            };

            _clearListButton.TouchUpInside += (s, e) =>
            {
                if (_viewModel.ClearListCommand.CanExecute(null))
                {
                    _viewModel.ClearListCommand.Execute(null);
                    UpdateTableView();
                }
            };

            _tableView.Source = new TaskItemTableViewSource(_viewModel.Items, rowSelected =>
            {
                TaskItem selectedTask = _viewModel.Items[rowSelected];

                TaskDetailViewController taskDetailViewController = new TaskDetailViewController(selectedTask, _viewModel);
                NavigationController.PushViewController(taskDetailViewController, true);
            });

            _viewModel.Items.CollectionChanged += (s, e) =>
            {
                UpdateTableView();
            };

            UpdateTableView();
        }

        private void UpdateTableView()
        {
            _tableView.Source = new TaskItemTableViewSource(_viewModel.Items, rowSelected =>
            {
                TaskItem selectedTask = _viewModel.Items[rowSelected];

                TaskDetailViewController taskDetailViewController = new TaskDetailViewController(selectedTask, _viewModel);
                NavigationController.PushViewController(taskDetailViewController, true);
            });
            _tableView.ReloadData();
        }

    }
}