using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Core;

namespace ListMvvmAndroid
{
    public class TaskItemAdapter : ArrayAdapter<TaskItem>
    {
        private readonly Context _context;

        public TaskItemAdapter(Context context, IList<TaskItem> objects) : base(context, 0, objects)
        {
            _context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = LayoutInflater.From(Context).Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
            }

            TaskItem taskItem = GetItem(position);
            TextView textView = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.Text = taskItem.Title;

            if (taskItem.IsComplete)
            {
                convertView.SetBackgroundResource(Resource.Drawable.completed_task_background);
            }
            else
            {
                convertView.SetBackgroundResource(Resource.Drawable.incomplete_task_background);
            }

            return convertView;
        }
    }
}
