using System.ComponentModel;

namespace LibreWorlds.Controller.Models
{
    public enum SubsystemState
    {
        Offline,
        Starting,
        Active,
        Error
    }

    public sealed class SubsystemModel : INotifyPropertyChanged
    {
        public string Name { get; }

        private SubsystemState _state;
        public SubsystemState State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
                }
            }
        }

        public SubsystemModel(string name)
        {
            Name = name;
            State = SubsystemState.Offline;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
