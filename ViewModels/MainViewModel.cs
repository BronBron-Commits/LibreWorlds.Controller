using System.Collections.ObjectModel;
using LibreWorlds.Controller.Models;
using LibreWorlds.WorldAdapter;

namespace LibreWorlds.Controller.ViewModels
{
    public sealed class MainViewModel
    {
        // ---- Subsystem models (UI lights) ----
        public SubsystemModel WorldAdapterSubsystem { get; }

        public ObservableCollection<SubsystemModel> Subsystems { get; }

        // ---- Real adapter (authoritative) ----
        private readonly LibreWorlds.WorldAdapter.WorldAdapter _worldAdapter;

        public MainViewModel()
        {
            WorldAdapterSubsystem = new SubsystemModel("World Adapter");

            Subsystems = new ObservableCollection<SubsystemModel>
            {
                WorldAdapterSubsystem
            };

            var engine = new RealmlibWorldEngine();
            _worldAdapter = new LibreWorlds.WorldAdapter.WorldAdapter(engine);

            _worldAdapter.StateChanged += OnWorldAdapterStateChanged;
        }

        // ---- Adapter → UI binding ----
        private void OnWorldAdapterStateChanged(WorldAdapterState state)
        {
            WorldAdapterSubsystem.State = MapAdapterState(state);
        }

        private static SubsystemState MapAdapterState(WorldAdapterState state)
        {
            return state switch
            {
                WorldAdapterState.Offline => SubsystemState.Offline,
                WorldAdapterState.Starting => SubsystemState.Starting,
                WorldAdapterState.Connected => SubsystemState.Active,
                WorldAdapterState.Authenticating => SubsystemState.Starting,
                WorldAdapterState.Authenticated => SubsystemState.Active,
                WorldAdapterState.EnteringWorld => SubsystemState.Starting,
                WorldAdapterState.InWorld => SubsystemState.Active,
                _ => SubsystemState.Offline
            };
        }

        // ---- UI commands ----
        public void StartSession()
        {
            _worldAdapter.Start();
        }

        public void StopSession()
        {
            _worldAdapter.Stop();
        }
    }
}
