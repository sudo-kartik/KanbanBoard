using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Project_Management
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public string projectId;
        public AddTask(string projId)
        {
            projectId = projId;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Lbx_Task.ItemsSource = App._projects;
            
            Project selectedProject = (from project in App._projects where project.ProjectId == projectId select project).FirstOrDefault() as Project;
            TaskProjectTitle.Text = selectedProject.ProjectTitle;
            ObservableCollection<Task> tasks = selectedProject.tasks;
            List<string> taskNames = new List<string>();
            foreach(Task task in tasks)
            {
                taskNames.Add(task.Title);
            }
            CoBoxDepend_Tsk.ItemsSource = taskNames;

        }

        private void Btn_CrtTsk_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = (from project in App._projects where project.ProjectId == projectId select project).FirstOrDefault() as Project;
            var toDoTasks = from t in selectedProject.tasks where t.Status == "To Do" select t;
            var inProgressTasks = from t in selectedProject.tasks where t.Status == "In Progress" select t;
            Task newTask = new Task
            {
                TaskId = Math.Abs(Guid.NewGuid().GetHashCode()).ToString(),
                Title = "Edit...",
                Description = "Edit...",
                TaskStartDate = DateTime.Now,
                TaskDeadline = DateTime.Now,
                AddtnInfo = "Edit...",
                Status = "To DO"

                /* Title = Tbx_title.Text,
                 Description = Tbx_Description.Text,
                 Priority = CoBox_TaskPrioprity.SelectionBoxItem as String,
                 Status = CoBox_Status.SelectionBoxItem as String,
                 AddtnInfo = Tbx_Add_Info.Text,
                 DependentTaskID = CoBoxDepend_Tsk.SelectionBoxItem as String*/

            };
            selectedProject.tasks.Add(newTask);
            App._projects.Add(selectedProject); 
            Lbx_Task.SelectedItem = newTask;
            Lbx_Task.ScrollIntoView(newTask);

            if (toDoTasks != null && toDoTasks.Count() >= Int32.Parse(selectedProject.ToDoLimit))
            {
                MessageBox.Show("Number of allowed tasks in To do is exhausted.", "Project Management", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (inProgressTasks != null && inProgressTasks.Count() >= Int32.Parse(selectedProject.InProgressLimit))
            {
                MessageBox.Show("Number of allowed tasks in In Progress is exhausted.", "Project Management", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            selectedProject.tasks.Add(newTask);
            
            var res = MessageBox.Show("Task created successfully! \n Do you want to create more tasks ?",
                                "Project Management", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(res == MessageBoxResult.No)
            {
                Kanban kanban = new Kanban(selectedProject.ProjectId);
                kanban.Show();
                this.Close();
            } 
            else 
            {
                AddTask addTask = new AddTask(selectedProject.ProjectId);
                addTask.Show();
                this.Close();
            }



        }

        private void Btn_Cnl_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = (from project in App._projects where project.ProjectId == projectId select project).FirstOrDefault() as Project;
            var Kanban = new Kanban(selectedProject.ProjectId);
            Kanban.Show();
            this.Close();
        }

        private void DpTaskDeadLine_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Task selectedTask = Lbx_Task.SelectedItem as Task;
            if(selectedTask != null)
            {
                if(DpTaskDeadLine.SelectedDate != null)
                {
                    selectedTask.TaskDeadline = (DateTime)DpTaskDeadLine.SelectedDate;
                }
            }
        }

        private void DpTaskStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Task selectedTask = Lbx_Task.SelectedItem as Task;
            if (selectedTask != null)
            {
                if (DpTaskStartDate.SelectedDate != null)
                {
                    selectedTask.TaskStartDate = (DateTime)DpTaskStartDate.SelectedDate;
                }
            }
        }

        

        private void CoBox_TaskPrioprity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task selectedTask = Lbx_Task.SelectedItem as Task;
            if (selectedTask != null)
                selectedTask.Priority = CoBox_TaskPrioprity.SelectedItem as String;
        }

        private void Lbx_Task_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CoBox_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task selectedTasks = Lbx_Task.SelectedItem as Task;
            if (selectedTasks != null)
                selectedTasks.Status = CoBox_Status.SelectedItem as string;

        }
    }
}
