using SystemOfTestingMobile.Models;

namespace SystemOfTestingMobile.Data
{
    public static class TestsData
    {
        public static List<Test> GetAvailableTests()
        {
            return new List<Test>
            {
                new Test
                {
                    Id = 1,
                    Title = "Основы C#",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Id = 1,
                            Text = "Что такое CLR?",
                            CorrectAnswerId = 2,
                            Answers = new List<Answer>
                            {
                                new Answer { Id = 1, Text = "Common Language Runtime" },
                                new Answer { Id = 2, Text = "Common Language Runtime - среда выполнения кода" },
                                new Answer { Id = 3, Text = "Compiler Language Resource" },
                                new Answer { Id = 4, Text = "Code Language Runtime" }
                            }
                        },
                        new Question
                        {
                            Id = 2,
                            Text = "Что такое nullable тип?",
                            CorrectAnswerId = 3,
                            Answers = new List<Answer>
                            {
                                new Answer { Id = 1, Text = "Тип, который не может быть null" },
                                new Answer { Id = 2, Text = "Тип, который всегда null" },
                                new Answer { Id = 3, Text = "Тип, который может принимать значение null" },
                                new Answer { Id = 4, Text = "Тип, который удален из кода" }
                            }
                        }
                    }
                },
                new Test
                {
                    Id = 2,
                    Title = "MAUI и XAML",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Id = 1,
                            Text = "Что означает MAUI?",
                            CorrectAnswerId = 1,
                            Answers = new List<Answer>
                            {
                                new Answer { Id = 1, Text = ".NET Multi-platform App UI" },
                                new Answer { Id = 2, Text = "Microsoft App User Interface" },
                                new Answer { Id = 3, Text = "Mobile App UI Integration" },
                                new Answer { Id = 4, Text = "Multi-platform Application User Interface" }
                            }
                        },
                        new Question
                        {
                            Id = 2,
                            Text = "Какой файл определяет интерфейс в MAUI?",
                            CorrectAnswerId = 2,
                            Answers = new List<Answer>
                            {
                                new Answer { Id = 1, Text = ".cs файл" },
                                new Answer { Id = 2, Text = ".xaml файл" },
                                new Answer { Id = 3, Text = ".xml файл" },
                                new Answer { Id = 4, Text = ".json файл" }
                            }
                        }
                    }
                },
                new Test
                {
                    Id = 3,
                    Title = "Алгоритмы и структуры данных",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Id = 1,
                            Text = "Что такое Big O notation?",
                            CorrectAnswerId = 1,
                            Answers = new List<Answer>
                            {
                                new Answer { Id = 1, Text = "Оценка сложности алгоритма" },
                                new Answer { Id = 2, Text = "Тип структуры данных" },
                                new Answer { Id = 3, Text = "Метод сортировки" },
                                new Answer { Id = 4, Text = "Язык программирования" }
                            }
                        }
                    }
                }
            };
        }
    }
}