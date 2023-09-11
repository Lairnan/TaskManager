using System.Windows;
using TaskManager.Models;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            /*using var db = new TaskManageContext();
            db.Database.EnsureCreated();*/
        }
    }
}