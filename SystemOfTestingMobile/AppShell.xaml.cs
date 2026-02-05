using SystemOfTestingMobile.Pages;
using System.Diagnostics;

namespace SystemOfTestingMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Debug.WriteLine("=== AppShell: Конструктор начал работу ===");

            try
            {
                InitializeComponent();
                Debug.WriteLine("=== AppShell: InitializeComponent выполнен ===");

                // Регистрируем маршруты для всех страниц
                Routing.RegisterRoute(nameof(ResultsPage), typeof(ResultsPage));
                Routing.RegisterRoute(nameof(TestsListPage), typeof(TestsListPage));
                Routing.RegisterRoute(nameof(TestPage), typeof(TestPage));
                Debug.WriteLine($"=== AppShell: Маршруты зарегистрированы ===");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== ERROR in AppShell constructor: {ex.Message} ===");
            }
        }
    }
}