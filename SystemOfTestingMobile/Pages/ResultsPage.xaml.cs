using SystemOfTestingMobile.Models;
using System.Diagnostics;

namespace SystemOfTestingMobile.Pages
{
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            Debug.WriteLine("=== ResultsPage: Конструктор начал работу ===");

            try
            {
                InitializeComponent();
                Debug.WriteLine("=== ResultsPage: InitializeComponent выполнен ===");

                // Проверяем, что CollectionView существует
                if (ResultsCollectionView == null)
                {
                    Debug.WriteLine("=== ERROR: ResultsCollectionView is NULL! ===");
                }
                else
                {
                    Debug.WriteLine("=== ResultsPage: ResultsCollectionView найден ===");
                    LoadTestResults();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== ERROR in ResultsPage constructor: {ex.Message} ===");
                Debug.WriteLine($"=== StackTrace: {ex.StackTrace} ===");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("=== ResultsPage: OnAppearing вызван ===");
        }

        private void LoadTestResults()
        {
            Debug.WriteLine("=== LoadTestResults: Начало ===");

            try
            {
                // Тестовые данные
                var testResults = new List<TestResult>
                {
                    new TestResult
                    {
                        Id = 1,
                        UserName = "Иван",
                        TestTitle = "Основы C#",
                        CorrectAnswers = 8,
                        TotalQuestions = 10,
                        Date = DateTime.Now.AddDays(-2)
                    },
                    new TestResult
                    {
                        Id = 2,
                        UserName = "Иван",
                        TestTitle = "MAUI и XAML",
                        CorrectAnswers = 9,
                        TotalQuestions = 10,
                        Date = DateTime.Now.AddDays(-1)
                    },
                    new TestResult
                    {
                        Id = 3,
                        UserName = "Иван",
                        TestTitle = "Алгоритмы",
                        CorrectAnswers = 6,
                        TotalQuestions = 10,
                        Date = DateTime.Now
                    },
                    new TestResult
                    {
                        Id = 4,
                        UserName = "Влад",
                        TestTitle = "Кс",
                        CorrectAnswers = 6,
                        TotalQuestions = 10,
                        Date = DateTime.Now
                    },
                    new TestResult
                    {
                        Id = 5,
                        UserName = "Егор",
                        TestTitle = "Доточка",
                        CorrectAnswers = 1,
                        TotalQuestions = 10,
                        Date = DateTime.Now
                    }
                };

                Debug.WriteLine($"=== LoadTestResults: Создано {testResults.Count} записей ===");

                foreach (var result in testResults)
                {
                    Debug.WriteLine($"Запись: {result.TestTitle}, {result.CorrectAnswers}/{result.TotalQuestions}, {result.Percentage}%");
                }

                // Устанавливаем данные
                ResultsCollectionView.ItemsSource = null;
                Debug.WriteLine("=== LoadTestResults: ItemsSource очищен ===");

                ResultsCollectionView.ItemsSource = testResults;
                Debug.WriteLine("=== LoadTestResults: ItemsSource установлен ===");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== ERROR in LoadTestResults: {ex.Message} ===");
            }
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("=== OnHomeButtonClicked: Кнопка нажата ===");
            await Shell.Current.GoToAsync("..");
        }
    }
}