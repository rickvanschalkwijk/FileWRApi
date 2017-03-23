using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWRApi.Business
{
    public class SimpleTaskScheduler : TaskScheduler, IDisposable
    {
        private BlockingCollection<Task> _tasks = new BlockingCollection<Task>();
        private Thread _mainThread = null;

        public SimpleTaskScheduler()
        {
            _mainThread = new Thread(new ThreadStart(Main));
        }

        private void Main()
        {
            Console.WriteLine("Starting Thread " + Thread.CurrentThread.ManagedThreadId.ToString());

            foreach (var t in _tasks.GetConsumingEnumerable())
            {
                TryExecuteTask(t);
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _tasks.ToArray<Task>();
        }

        protected override void QueueTask(Task task)
        {
            _tasks.Add(task);

            if (_mainThread.IsAlive == false)
            {
                _mainThread.Start();
            }

        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        public void Dispose()
        {
            _tasks.CompleteAdding();
        }
    }
}
