using SystemOfTestingMobile.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace SystemOfTestingMobile.ViewModels
{
    public class TestPageViewModel : INotifyPropertyChanged
    {
        private Test _test;
        private int _currentQuestionIndex = 0;
        private int? _selectedAnswerId = null;
        private Dictionary<int, int?> _userAnswers = new(); // questionId -> answerId
        private string _userName;

        public event PropertyChangedEventHandler PropertyChanged;

        public Test Test
        {
            get => _test;
            set
            {
                _test = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(QuestionNumberText));
                OnPropertyChanged(nameof(TotalQuestions));
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public Question CurrentQuestion =>
            Test?.Questions != null && Test.Questions.Count > 0 && _currentQuestionIndex < Test.Questions.Count
            ? Test.Questions[_currentQuestionIndex]
            : null;

        public string QuestionNumberText =>
            $"Вопрос {_currentQuestionIndex + 1} из {TotalQuestions}";

        public int TotalQuestions => Test?.Questions?.Count ?? 0;

        public int? SelectedAnswerId
        {
            get => _selectedAnswerId;
            set
            {
                _selectedAnswerId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsAnswerSelected));
            }
        }

        public bool IsAnswerSelected => SelectedAnswerId.HasValue;

        public Dictionary<int, int?> UserAnswers => _userAnswers;

        public void SelectAnswer(int answerId)
        {
            SelectedAnswerId = answerId;

            if (CurrentQuestion != null)
            {
                _userAnswers[CurrentQuestion.Id] = answerId;
                Debug.WriteLine($"=== Выбран ответ {answerId} для вопроса {CurrentQuestion.Id} ===");
            }
        }

        public bool MoveToNextQuestion()
        {
            if (_currentQuestionIndex < TotalQuestions - 1)
            {
                // Сохраняем ответ для текущего вопроса
                if (CurrentQuestion != null && SelectedAnswerId.HasValue)
                {
                    _userAnswers[CurrentQuestion.Id] = SelectedAnswerId.Value;
                }

                _currentQuestionIndex++;
                SelectedAnswerId = null;

                // Восстанавливаем ответ для следующего вопроса, если он был
                if (CurrentQuestion != null && _userAnswers.TryGetValue(CurrentQuestion.Id, out var savedAnswer))
                {
                    SelectedAnswerId = savedAnswer;
                }

                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(QuestionNumberText));
                OnPropertyChanged(nameof(IsAnswerSelected));

                Debug.WriteLine($"=== Переход к вопросу {_currentQuestionIndex + 1} ===");
                return true;
            }
            return false;
        }

        public bool MoveToPreviousQuestion()
        {
            if (_currentQuestionIndex > 0)
            {
                // Сохраняем ответ для текущего вопроса
                if (CurrentQuestion != null && SelectedAnswerId.HasValue)
                {
                    _userAnswers[CurrentQuestion.Id] = SelectedAnswerId.Value;
                }

                _currentQuestionIndex--;
                SelectedAnswerId = null;

                // Восстанавливаем ответ для предыдущего вопроса
                if (CurrentQuestion != null && _userAnswers.TryGetValue(CurrentQuestion.Id, out var savedAnswer))
                {
                    SelectedAnswerId = savedAnswer;
                }

                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(QuestionNumberText));
                OnPropertyChanged(nameof(IsAnswerSelected));

                Debug.WriteLine($"=== Возврат к вопросу {_currentQuestionIndex + 1} ===");
                return true;
            }
            return false;
        }

        public TestResult CalculateResult()
        {
            int correctAnswers = 0;

            foreach (var question in Test.Questions)
            {
                if (_userAnswers.TryGetValue(question.Id, out var userAnswer) &&
                    userAnswer.HasValue &&
                    userAnswer.Value == question.CorrectAnswerId)
                {
                    correctAnswers++;
                }
            }

            return new TestResult
            {
                UserName = UserName,
                TestTitle = Test.Title,
                CorrectAnswers = correctAnswers,
                TotalQuestions = TotalQuestions,
                Date = DateTime.Now
            };
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double Progress => TotalQuestions > 0 ? (double)(_currentQuestionIndex + 1) / TotalQuestions : 0;

        public string ProgressText => $"{_currentQuestionIndex + 1}/{TotalQuestions}";

        public bool CanGoBack => _currentQuestionIndex > 0;
    }
}