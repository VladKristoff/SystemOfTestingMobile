using SystemOfTestingMobile.Pages;
using System.Diagnostics;

namespace SystemOfTestingMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            ResultsBtn.Clicked += OnResultsClicked;
            ListTestBtn.Clicked += OnListTestClicked;

            Debug.WriteLine("=== MainPage: Инициализирована ===");
        }

        private async void OnResultsClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("=== MainPage: Кнопка Results нажата ===");

            try
            {
                await Shell.Current.GoToAsync(nameof(ResultsPage));
                Debug.WriteLine("=== MainPage: Навигация выполнена ===");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== ERROR in OnResultsClicked: {ex.Message} ===");
                await DisplayAlert("Ошибка", $"Не удалось перейти на страницу результатов: {ex.Message}", "OK");
            }
        }

        private async void OnListTestClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("=== MainPage: Кнопка ListTest нажата ===");

            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Внимание", "Пожалуйста, введите ваше имя", "OK");
                return;
            }

            try
            {
                // Кодируем имя пользователя для URL
                var encodedUserName = System.Net.WebUtility.UrlEncode(NameEntry.Text);

                // Переходим на страницу тестов с параметром
                await Shell.Current.GoToAsync($"{nameof(TestsListPage)}?userName={encodedUserName}");
                Debug.WriteLine($"=== MainPage: Переход к тестам для пользователя {NameEntry.Text} ===");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== ERROR in OnListTestClicked: {ex.Message} ===");
                await DisplayAlert("Ошибка", $"Не удалось перейти к списку тестов: {ex.Message}", "OK");
            }
        }
    }
}