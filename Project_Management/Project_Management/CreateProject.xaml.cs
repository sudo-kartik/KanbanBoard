using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CreateProject : Window
    {
        public CreateProject()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Lbx_Project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {
            var itm = Lbx_Project.SelectedItem;
            if (itm == null)
            {
                MessageBox.Show("Pleae Selected an Item to be deleted first!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var toDelete = itm as Project;
            var res = MessageBox.Show($"Are you sure you want to delete {toDelete.ProjectTitle}?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (res == MessageBoxResult.OK)
                App._projects.Remove(toDelete);



        }

        private void Btn_Crt_Click(object sender, RoutedEventArgs e)
        {
            Project proj = new Project
            {
                ProjectTitle = "Edit...",
                ProjectDescription = "Edit...",
                StartDate = DateTime.Now,
                Deadline = DateTime.Now,
                ToDoLimit = "0",
                InProgressLimit = "0",
                TestingLimit = "0",
                ProjectId = Math.Abs(Guid.NewGuid().GetHashCode()).ToString()
            };

            App._projects.Add(proj);
            Lbx_Project.SelectedItem = proj;
            Lbx_Project.ScrollIntoView(proj);
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text.ToLower();
            var tile = from s in App._projects where s.ProjectTitle.ToLower().Contains(filter) select s;
            Lbx_Project.ItemsSource = tile;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Lbx_Project.ItemsSource = App._projects;
            List<string> status = new List<string> { "Planned", "In Progress", "Done" };
            CoBox_ProjectStatus.ItemsSource = status;
            CoBox_ProjectStatusFilter.ItemsSource = new List<string> {"All", "Planned", "In Progress", "Done" };
        }

        private void Btn_filter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Lbx_Project_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Project project = Lbx_Project.SelectedItem as Project;
            var goToKanBan = new Kanban(project.ProjectId);
            goToKanBan.Show();
            this.Close();
        }

      
        private void CoBox_ProjectStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Project selectedProject = Lbx_Project.SelectedItem as Project;
            if (selectedProject != null)
                selectedProject.ProjectStatus = CoBox_ProjectStatus.SelectedItem as String;
        }

        private void CoBox_ProjectStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CoBox_ProjectStatusFilter.SelectedItem != null)
            {
                if (CoBox_ProjectStatusFilter.SelectedItem.ToString() == "All")
                {
                    var projects = from p in App._projects select p;
                    Lbx_Project.ItemsSource = projects;
                }
                else
                {
                    var filteredProjects = from p in App._projects where p.ProjectStatus == CoBox_ProjectStatusFilter.SelectedItem.ToString() select p;
                    Lbx_Project.ItemsSource = filteredProjects;
                }
            }
        }

        private void DpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Project selectedProject = Lbx_Project.SelectedItem as Project;
            if(selectedProject != null)
            {
                if(DpStartDate.SelectedDate != null)
                {
                    selectedProject.StartDate = (DateTime)DpStartDate.SelectedDate;
                }
            }
        }

        private void DpDeadLine_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Project selectedProject = Lbx_Project.SelectedItem as Project;
            if (selectedProject != null)
            {
                if (DpDeadLine.SelectedDate != null)
                {
                    selectedProject.Deadline = (DateTime)DpDeadLine.SelectedDate;
                }
            }
        }

        private void Btn_BtnGoToKanban_Click(object sender, RoutedEventArgs e)
        {
            
            Project project = Lbx_Project.SelectedItem as Project;
            if (project!= null)
            {
                var goToKanBan = new Kanban(project.ProjectId);
                goToKanBan.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Please select a Project..", "Project Management", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
                
            

        }
    }
}
