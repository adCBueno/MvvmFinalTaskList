using System;
using Core;
using Foundation;
using UIKit;

namespace ListMvvmiOS
{
    public class TaskItemCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TaskItemCell");

        public TaskItemCell(IntPtr handle) : base(handle)
        {
        }

        public void UpdateCell(TaskItem taskItem)
        {
            TextLabel.Text = taskItem.Title;

            Layer.BorderWidth = 2;
            if (taskItem.IsComplete)
            {
                Layer.BorderColor = UIColor.Green.CGColor;
            }
            else
            {
                Layer.BorderColor = UIColor.LightGray.CGColor;
            }
        }
    }
}
