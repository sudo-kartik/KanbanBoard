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

namespace Project_Management
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Kanban : Window
    {
      
        public string projId;

        public Kanban(string id)
        {
            InitializeComponent();
            projId = id;        
        }


   
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Project projectdetails = (from s in App._projects where s.ProjectId == projId select s ).FirstOrDefault() as Project;
            this.DataContext = projectdetails;
            var toDoTasks = from t in projectdetails.tasks where t.Status == "To Do" select t;
            Lbx_ToDoTasks.ItemsSource = toDoTasks;

            var inProgress = from t in projectdetails.tasks where t.Status == "In Progress" select t;
            Lbx_InProgressTasks.ItemsSource = inProgress;

            var testingTasks = from t in projectdetails.tasks where t.Status == "Testing" select t;
            Lbx_TestingTasks.ItemsSource = testingTasks;

            var doneTasks = from t in projectdetails.tasks where t.Status == "Done" select t;
            Lbx_DoneTasks.ItemsSource = doneTasks;


        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            Project projectdetails = (from s in App._projects where s.ProjectId == projId select s).FirstOrDefault() as Project;
            var toDoTasks = from t in projectdetails.tasks where t.Status == "To Do" select t;
            if (toDoTasks != null) {
                if (toDoTasks.Count() >= Int32.Parse(projectdetails.ToDoLimit))
                {
                    MessageBox.Show("Number of allowed tasks in To do is exhausted.", "Project Management", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            var goToAddTask = new AddTask(projId);
            goToAddTask.Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var projectboard = new CreateProject();
            projectboard.Show();
            this.Close();
        }


        private void Move_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = (from p in App._projects where p.ProjectId == projId select p).FirstOrDefault() as Project;
            Task toMove = new Task();
            string nextStatus = "In Progress"; 

            MenuItem menu = sender as MenuItem;
            if (menu.Name == "ToDoMoveMenu")
            {
                toMove  = Lbx_ToDoTasks.SelectedItem as Task;
                nextStatus = "In Progress";

            }
            else if (menu.Name == "InProgMoveMenu")
            {
                toMove= Lbx_InProgressTasks.SelectedItem as Task;
                
                nextStatus = "Testing";

            }
            else if (menu.Name == "TestMoveMenu")
            {
                toMove = Lbx_TestingTasks.SelectedItem as Task;
                
                nextStatus = "Done";

            }
            

            var mov = MessageBox.Show($"Are you sure you want to Move {toMove.Title} to {nextStatus} ?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (mov == MessageBoxResult.OK)
            {
                Task task = (from t in selectedProject.tasks where t.TaskId == toMove.TaskId select t).FirstOrDefault();
                task.Status = nextStatus;

            }
            Kanban kanban = new Kanban(selectedProject.ProjectId);
            kanban.Show();
            this.Close();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = (from p in App._projects where p.ProjectId == projId select p).FirstOrDefault() as Project;
            Task toDelete = new Task();
          

            MenuItem menu = sender as MenuItem;
            if (menu.Name == "ToDoDelMenu")
            {
                toDelete = Lbx_ToDoTasks.SelectedItem as Task;
                
            }
            else if (menu.Name == "InProgDelMenu")
            {
                toDelete = Lbx_InProgressTasks.SelectedItem as Task;
                
            }
            else if (menu.Name == "TestDelMenu") {
                toDelete = Lbx_TestingTasks.SelectedItem as Task;
                
            }
            else if (menu.Name == "DoneDelMenu") {
                toDelete = Lbx_DoneTasks.SelectedItem as Task;
                
            }
            

            /*if (clickedItem == null)
            {MessageBox.Show("Pleae Selected an Item to be deleted first!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;}*/
            
            var res = MessageBox.Show($"Are you sure you want to delete {toDelete.Title}?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (res == MessageBoxResult.OK)
                selectedProject.tasks.Remove(toDelete);

            Kanban kanban = new Kanban(selectedProject.ProjectId);
            kanban.Show();
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = (from p in App._projects where p.ProjectId == projId select p).FirstOrDefault() as Project;
            Task toEdit = new Task();

            MenuItem menu = sender as MenuItem;
            if (menu.Name == "ToDoDelMenu")
            {
                toEdit = Lbx_ToDoTasks.SelectedItem as Task;
                var goToAddTask = new AddTask(projId);
                goToAddTask.Show();
                this.Close();

            }
            else if (menu.Name == "InProgDelMenu")
            {
                toEdit = Lbx_InProgressTasks.SelectedItem as Task;
                var goToAddTask = new AddTask(projId);
                goToAddTask.Show();
                this.Close();

            }
            else if (menu.Name == "TestDelMenu")
            {
                toEdit = Lbx_TestingTasks.SelectedItem as Task;
                var goToAddTask = new AddTask(projId);
                goToAddTask.Show();
                this.Close();

            }
            else if (menu.Name == "DoneDelMenu")
            {
                toEdit = Lbx_DoneTasks.SelectedItem as Task;
                var goToAddTask = new AddTask(projId);
                goToAddTask.Show();
                this.Close();

            }

        }

        private void Lbx_DoneTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
