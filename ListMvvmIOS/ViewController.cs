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
        private MainViewModel viewModel;

        public ViewController (IntPtr handle) : base (handle)
        {
            viewModel = new MainViewModel();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "goToDetails")
            {
                var indexPath = (NSIndexPath)sender;
                TaskDetailViewController controller = (TaskDetailViewController)segue.DestinationViewController;
                controller.Task = viewModel.Items[indexPath.Row];
                DetailViewModel detailViewModel = new DetailViewModel(viewModel.Items[indexPath.Row], viewModel);
                controller.viewModel = detailViewModel;
                controller.mainViewModel = viewModel;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            tasksTableView.RegisterClassForCellReuse(typeof(TaskItemCell), TaskItemCell.Key);
            tasksTableView.DataSource = new ViewControllerTableViewDataSource(this);
            tasksTableView.Delegate = new ViewControllerTableViewDelegate(this);

            viewModel.PropertyChanged += UpdateUi;
            viewModel.Items.CollectionChanged += (s, e) =>
            {
                tasksTableView.ReloadData();
            };


            titleTextField.EditingChanged += (sender, e) =>
            {
                viewModel.Text = titleTextField.Text;
                addButton.Enabled = viewModel.SubmitCommand.CanExecute(null);
                clearButton.Enabled = viewModel.ClearListCommand.CanExecute(null);
            };
            descriptionTextField.EditingChanged += (sender, e) =>
            {
                viewModel.Description = descriptionTextField.Text;
            };
            categoryTextField.EditingChanged += (sender, e) =>
            {
                viewModel.Category = categoryTextField.Text;
            };
            

            addButton.TouchUpInside += (s, e) =>
            {
                viewModel.SubmitCommand.Execute(null);
                titleTextField.Text = "";
                descriptionTextField.Text = "";
                categoryTextField.Text = "";
            };

            clearButton.TouchUpInside += (s, e) =>
            {
                viewModel.ClearListCommand.Execute(null);
            };
        }

        public void UpdateUi(object sender, EventArgs e)
        {

        }

        class ViewControllerTableViewDataSource : UITableViewDataSource
        {
            private ViewController viewController;

            public ViewControllerTableViewDataSource(ViewController viewController)
            {
                this.viewController = viewController;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell("TaskItemCell", indexPath) as TaskItemCell;
                cell.UpdateCell(viewController.viewModel.Items[indexPath.Row]);
                //cell.TextLabel.Text = viewController.viewModel.Items[indexPath.Row].Title;
                return cell;
            }

            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return viewController.viewModel.Items.Count;
            }
        }

        class ViewControllerTableViewDelegate: UITableViewDelegate
        {
            private ViewController viewController;

            public ViewControllerTableViewDelegate(ViewController viewController)
            {
                this.viewController = viewController;
            }
            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {

                viewController.PerformSegue("goToDetails", indexPath);
                tableView.DeselectRow(indexPath, true);
            }
        }
    }
}