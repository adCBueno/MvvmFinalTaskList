using System;
using Core;
using Foundation;
using System.Collections.Generic;
using UIKit;

namespace ListMvvmIOS
{
    public class TaskItemTableViewSource : UITableViewSource
    {
        private IList<TaskItem> _taskItems;
        private Action<int> _onRowSelected;

        public TaskItemTableViewSource(IList<TaskItem> taskItems, Action<int> onRowSelected)
        {
            _taskItems = taskItems;
            _onRowSelected = onRowSelected;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("TaskItemCell", indexPath);
            cell.TextLabel.Text = _taskItems[indexPath.Row].Title;
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _taskItems.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            _onRowSelected?.Invoke(indexPath.Row);
            tableView.DeselectRow(indexPath, true);
        }
    }

}

