using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management
{
    public class Task
    {

        public string TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskDeadline { get; set; }
        public string Priority { get; set; }
        public string AddtnInfo { get; set; }
        public string Status { get; set; }
        public string DependentTaskID { get; set; }

      
    }
}
