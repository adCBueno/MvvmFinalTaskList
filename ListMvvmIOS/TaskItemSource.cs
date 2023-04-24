using System;
using Core;
using System.Collections.Generic;
using UIKit;

namespace ListMvvmIOS
{
	public class TaskItemSource: UITableViewSource
	{

        private readonly IList<TaskItem> _taskItems;
        private const string CellIdentifier = "TaskItemCell";

        public TaskItemSource(IList<TaskItem> taskItems)
        {
            _taskItems = taskItems;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            TaskItem taskItem = _taskItems[indexPath.Row];
            cell.TextLabel.Text = taskItem.Title;

            if (taskItem.IsComplete)
            {
                cell.BackgroundColor = UIColor.LightGray;
            }
            else
            {
                cell.BackgroundColor = UIColor.White;
            }

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _taskItems.Count;
        }

        public TaskItem GetItem(int position)
        {
            return _taskItems[position];
        }
    }
}

