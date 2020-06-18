using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management
{
    public class Project:INotifyPropertyChanged
    {
        public string ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public string ToDoLimit { get; set; }
        public string InProgressLimit { get; set; }
        public string TestingLimit { get; set; }
        public string DoneLimit { get; set; }
        public string ProjectStatus { get; set; }


        private ObservableCollection<Task> tasks_;
        public ObservableCollection<Task> tasks 
        { 
            get { return tasks_; }
            set
            {
                tasks_ = value;
                OnPropertyChanged("tasks");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
                
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Project()
        {
            tasks = new ObservableCollection<Task>();
        }

    }
}
