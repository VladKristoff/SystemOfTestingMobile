using SystemOfTestingMobile.Data;
using SystemOfTestingMobile.Models;
using System.Diagnostics;

namespace SystemOfTestingMobile.Pages
{
    [QueryProperty(nameof(UserName), "userName")]
    public partial class TestsListPage : ContentPage
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
                LoadTests();
            }
        }

        public TestsListPage()
        {
            InitializeComponent();
            BindingContext = this;

            Debug.WriteLine("=== TestsListPage: Конструктор без параметров ===");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine($"=== TestsListPage: OnAppearing, UserName={UserName} ===");
        }

        private void LoadTests()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                Debug.WriteLine("=== TestsListPage: UserName пустой, пропускаем загрузку ===");
                return;
            }

            try
            {
                Debug.WriteLine($"=== TestsListPage: Загрузка тестов для {UserName} ===");

                var tests = TestsData.GetAvailableTests();
                Debug.WriteLine($"=== TestsListPage: Загружено {tests.Count} тестов ===");

                TestsCollectionView.ItemsSource = tests;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== ERROR in LoadTests: {ex.Message} ===");
                DisplayAlert("Ошибка", "Не удалось загрузить список тестов", "OK");
            }
        }

        private void OnTestSelected(object sender, SelectionChangedEventArgs e)
        {
            // Снимаем выделение
            TestsCollectionView.SelectedItem = null;
        }

        private async void OnStartTestClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Test selectedTest)
            {
                Debug.WriteLine($"=== TestsListPage: Выбран тест '{selectedTest.Title}' для пользователя {UserName} ===");

                if (string.IsNullOrEmpty(UserName))
                {
                    await DisplayAlert("Ошибка", "Имя пользователя не указано", "OK");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                // Переходим на страницу прохождения теста
                await Shell.Current.GoToAsync($"{nameof(TestPage)}?testId={selectedTest.Id}&userName={System.Net.WebUtility.UrlEncode(UserName)}");
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}