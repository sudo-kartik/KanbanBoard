using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Management
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Project> _projects;
       


        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            _projects = Storage.ReadXml<ObservableCollection<Project>>("project.xml");
            if (_projects == null)
            {
                _projects = new ObservableCollection<Project>();
            }
           
        }




        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Storage.WriteXml<ObservableCollection<Project>>(_projects, "project.xml");

        }

    }
}
