using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    public class Task
    {
        public string ProjectName { get; private set; }
        public string TaskName { get; private set; }
        public string WorkLogs { get; private set; }
        public DateOnly DueDate { get; private set; }

        public static List<Task> LoadTaskData()
        {
            List<Task> tasks = [];
            tasks.Add(new Task("Test1", "Do something", "new TimeSpan(10)", new DateOnly(1975, 02, 23)));
            tasks.Add(new Task("Test2", "Do something else", "new TimeSpan(20)", new DateOnly(2005, 3, 20)));

            return tasks;
        }

        public Task(string projectName, string taskName, string workLogs, DateOnly dueDate)
        {
            ProjectName = projectName;
            TaskName = taskName;
            WorkLogs = workLogs;
            DueDate = dueDate;
        }
    }
}