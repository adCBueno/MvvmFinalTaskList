using System;
namespace Core
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
