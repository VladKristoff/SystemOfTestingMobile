using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOfTestingMobile.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string TestTitle { get; set; } = string.Empty;
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public double Percentage => Math.Round((double)CorrectAnswers / TotalQuestions * 100, 1);
        public string Grade
        {
            get
            {
                return Percentage switch
                {
                    >= 90 => "Отлично",
                    >= 75 => "Хорошо",
                    >= 60 => "Удовлетворительно",
                    _ => "Неудовлетворительно"
                };
            }
        }
    }
}
