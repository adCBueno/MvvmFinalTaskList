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
        }

        private void AddSubviewsAndConstraints()
        {
        }
                
    }
}