using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SystemOfTestingMobile.Models
{
    public class Answer : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}