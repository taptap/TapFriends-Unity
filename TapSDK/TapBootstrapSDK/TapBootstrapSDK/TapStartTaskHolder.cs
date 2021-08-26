using System.Collections.Generic;
using TapTap.Common;

namespace TapTap.Bootstrap
{
    public class TapStartTaskHolder : IStartTask
    {
        private readonly List<IStartTask> _tasks;

        public TapStartTaskHolder()
        {
            _tasks = new List<IStartTask>();
        }

        public void AddTask(IStartTask task)
        {
            _tasks.Add(task);
        }

        public void Invoke(TapConfig config)
        {
            foreach (var task in _tasks)
            {
                task.Invoke(config);
            }
        }
    }
}