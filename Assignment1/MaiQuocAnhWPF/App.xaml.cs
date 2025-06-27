using MaiQuocAnhWPF.Data.models;
using MaiQuocAnhWPF.Data.repositories;
using System;
using System.Windows;

namespace MaiQuocAnhWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            try
            {
                // Initialize sample data
                MaiQuocAnhWPF.Data.SampleDataInitializer.Initialize();
                
                // Start with LoginWindow
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                
                // Ensure the main window is set
                this.MainWindow = loginWindow;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting application: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown();
            }
        }
    }
}
