using Core;
using Foundation;
using UIKit;

namespace ListMvvmiOS
{
    public class TaskItemCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TaskItemCell");

        public TaskItemCell() : base(UITableViewCellStyle.Subtitle, Key)
        {
        }

        public void UpdateCell(TaskItem taskItem)
        {
            TextLabel.Text = taskItem.Title;
            DetailTextLabel.Text = taskItem.Description;

            if (taskItem.IsComplete)
            {
                BackgroundColor = UIColor.Green;
            }
            else
            {
                BackgroundColor = UIColor.Red;
            }
        }
    }
}
