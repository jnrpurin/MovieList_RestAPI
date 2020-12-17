using Microsoft.Extensions.DependencyInjection;
using ReadSpreadsheet.InjectDep;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ReadSpreadsheetUI
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (HasProcessOpened())
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigServices();
            Application.Run((Form)ServiceProvider.GetService(typeof(LoadSpreadsheetContent)));
        }

        private static void ConfigServices()
        {
            var services = new ServiceCollection();

            services.RegisterGeneralServices();
            RegisterForms(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private static void RegisterForms(ServiceCollection services)
        {
            services.AddScoped<LoadSpreadsheetContent>();
        }

        /// <summary>
        /// Verify if has the same process already opened
        /// </summary>
        /// <returns>If has the same process already opened</returns>
        private static bool HasProcessOpened()
        {
            return Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1;
        }
    }
}
