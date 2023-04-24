using System;
using UIKit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.ViewModels;
using System.Collections.ObjectModel;
using Foundation;

namespace ListMvvmIOS
{
    public partial class ViewController : UIViewController
    {
        private MainViewModel ViewModel { get; } = new MainViewModel();

        private UITextField textField;
        private UIButton submitButton;
        private UIButton clearListButton;
        private UITableView tableView;

        public ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupUserInterface();
            SetupBindings();
        }

        private void SetupUserInterface()
        {
            textField = new UITextField
            {
                Frame = new CoreGraphics.CGRect(20, 80, View.Bounds.Width - 40, 40),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Escribe algo aquí"
            };
            View.AddSubview(textField);

            submitButton = new UIButton(UIButtonType.System)
            {
                Frame = new CoreGraphics.CGRect(20, 130, (View.Bounds.Width - 60) / 2, 40),
                AccessibilityLabel = "Agregar"
            };
            submitButton.SetTitle("Agregar", UIControlState.Normal);
            View.AddSubview(submitButton);

            clearListButton = new UIButton(UIButtonType.System)
            {
                Frame = new CoreGraphics.CGRect(40 + (View.Bounds.Width - 60) / 2, 130, (View.Bounds.Width - 60) / 2, 40),
                AccessibilityLabel = "Limpiar lista"
            };
            clearListButton.SetTitle("Limpiar lista", UIControlState.Normal);
            View.AddSubview(clearListButton);

            tableView = new UITableView
            {
                Frame = new CoreGraphics.CGRect(20, 180, View.Bounds.Width - 40, View.Bounds.Height - 200),
                AccessibilityLabel = "Lista"
            };
            View.AddSubview(tableView);
        }

        private void SetupBindings()
        {
            textField.EditingChanged += (s, e) =>
            {
                ViewModel.Text = textField.Text;
                submitButton.Enabled = ViewModel.SubmitCommand.CanExecute(null);
                clearListButton.Enabled = ViewModel.ClearListCommand.CanExecute(null);
            };

            submitButton.TouchUpInside += (s, e) =>
            {
                if (ViewModel.SubmitCommand.CanExecute(null))
                {
                    ViewModel.SubmitCommand.Execute(null);
                }
            };

            clearListButton.TouchUpInside += (s, e) =>
            {
                if (ViewModel.ClearListCommand.CanExecute(null))
                {
                    ViewModel.ClearListCommand.Execute(null);
                }
            };

            ViewModel.SubmitCommand.CanExecuteChanged += (s, e) => submitButton.Enabled = ViewModel.SubmitCommand.CanExecute(null);
            ViewModel.ClearListCommand.CanExecuteChanged += (s, e) => clearListButton.Enabled = ViewModel.ClearListCommand.CanExecute(null);

            ViewModel.Items.CollectionChanged += (s, e) => tableView.ReloadData();
            tableView.DataSource = new ListDataSource(ViewModel.Items);

            submitButton.Enabled = ViewModel.SubmitCommand.CanExecute(null);
            clearListButton.Enabled = ViewModel.ClearListCommand.CanExecute(null);
        }

        private class ListDataSource : UITableViewDataSource
        {
            private readonly ObservableCollection<string> _items;

            public ListDataSource(ObservableCollection<string> items)
            {
                _items = items;
            }

            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return _items.Count;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell("itemCell") ?? new UITableViewCell(UITableViewCellStyle.Default, "itemCell");

                cell.TextLabel.Text = _items[indexPath.Row];
                return cell;
            }
        }
    }
}
