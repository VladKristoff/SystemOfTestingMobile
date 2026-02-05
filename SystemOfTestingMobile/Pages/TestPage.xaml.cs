using SystemOfTestingMobile.ViewModels;
using SystemOfTestingMobile.Models;
using System.Diagnostics;

namespace SystemOfTestingMobile.Pages
{
    [QueryProperty(nameof(TestId), "testId")]
    [QueryProperty(nameof(UserName), "userName")]
    public partial class TestPage : ContentPage
    {
        private TestPageViewModel ViewModel => BindingContext as TestPageViewModel;

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                if (ViewModel != null)
                {
                    ViewModel.UserName = value;
                }
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set
            {
                _testId = value;
                LoadTest(value);
            }
        }

        public TestPage()
        {
            InitializeComponent();
            Debug.WriteLine("=== TestPage: Конструктор ===");
        }

        private void LoadTest(int testId)
        {
            try
            {
                Debug.WriteLine($"=== TestPage: Загрузка теста ID={testId} для пользователя {UserName} ===");

                // Получаем тест из данных (позже из БД)
                var test = SystemOfTestingMobile.Data.TestsData.GetAvailableTests()
                    .FirstOrDefault(t => t.Id == testId);

                if (test == null)
                {
                    Debug.WriteLine($"=== ERROR: Тест с ID={testId} не найден ===");
                    DisplayAlert("Ошибка", "Тест не найден", "OK");
                    Shell.Current.GoToAsync("..");
                    return;
                }

                if (ViewModel != null)
                {
                    ViewModel.Test = test;
                    ViewModel.UserName = UserName;
                    Debug.WriteLine($"=== TestPage: Тест '{test.Title}' загружен, вопросов: {test.Questions.Count} ===");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== ERROR in LoadTest: {ex.Message} ===");
                DisplayAlert("Ошибка", "Не удалось загрузить тест", "OK");
            }
        }

        private void OnAnswerCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.BindingContext is Answer answer)
            {
                if (e.Value) // Если выбрано
                {
                    ViewModel?.SelectAnswer(answer.Id);
                }
            }
        }

        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            ViewModel?.MoveToPreviousQuestion();
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel == null) return;

            if (!ViewModel.IsAnswerSelected)
            {
                await DisplayAlert("Внимание", "Пожалуйста, выберите ответ", "OK");
                return;
            }

            if (!ViewModel.MoveToNextQuestion())
            {
                // Это последний вопрос - завершаем тест
                await CompleteTest();
            }
        }

        private async Task CompleteTest()
        {
            if (ViewModel == null) return;

            var result = ViewModel.CalculateResult();

            Debug.WriteLine($"=== TestPage: Тест завершен. Результат: {result.CorrectAnswers}/{result.TotalQuestions} ===");

            // TODO: Сохранить результат в БД
            // TODO: Перейти на страницу результатов теста

            await DisplayAlert("Тест завершен",
                $"Тест: {result.TestTitle}\n" +
                $"Правильных ответов: {result.CorrectAnswers} из {result.TotalQuestions}\n" +
                $"Оценка: {result.Grade}\n" +
                $"Процент: {result.Percentage}%\n\n" +
                "Страница результатов теста в разработке.",
                "OK");

            // Возвращаемся к списку тестов
            await Shell.Current.GoToAsync($"..");
        }

        protected override bool OnBackButtonPressed()
        {
            // Запрещаем возврат назад
            DisplayAlert("Внимание", "Вы не можете вернуться назад во время прохождения теста", "OK");
            return true;
        }
    }
}