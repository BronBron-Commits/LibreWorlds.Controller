#nullable enable
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

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

        private SubsystemState _state = SubsystemState.Offline;
        public SubsystemState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StateText));
                OnPropertyChanged(nameof(StateBrush));
            }
        }

        public string StateText => State.ToString();

        public Brush StateBrush => State switch
        {
            SubsystemState.Offline => Brushes.DarkGray,
            SubsystemState.Starting => Brushes.Goldenrod,
            SubsystemState.Active => Brushes.LimeGreen,
            SubsystemState.Error => Brushes.Red,
            _ => Brushes.Black
        };

        public SubsystemModel(string name)
        {
            Name = name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
