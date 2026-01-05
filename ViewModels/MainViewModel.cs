#nullable enable
using System.Collections.ObjectModel;
using LibreWorlds.Controller.Models;

// IMPORTANT:
// Do NOT rely on "using LibreWorlds.WorldAdapter" for the type
// We will fully-qualify it to avoid namespace collisions

namespace LibreWorlds.Controller.ViewModels
{
    public sealed class MainViewModel
    {
        // ---- Subsystems ----
        public SubsystemModel WorldAdapterSubsystem { get; }

        public ObservableCollection<SubsystemModel> Subsystems { get; }

        // ---- Adapter (FULLY QUALIFIED TYPE) ----
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

        private void OnWorldAdapterStateChanged(
            LibreWorlds.WorldAdapter.WorldAdapterState state)
        {
            WorldAdapterSubsystem.State = state switch
            {
                LibreWorlds.WorldAdapter.WorldAdapterState.Offline => SubsystemState.Offline,
                LibreWorlds.WorldAdapter.WorldAdapterState.Starting => SubsystemState.Starting,
                LibreWorlds.WorldAdapter.WorldAdapterState.Connected => SubsystemState.Active,
                LibreWorlds.WorldAdapter.WorldAdapterState.Authenticating => SubsystemState.Starting,
                LibreWorlds.WorldAdapter.WorldAdapterState.Authenticated => SubsystemState.Active,
                LibreWorlds.WorldAdapter.WorldAdapterState.EnteringWorld => SubsystemState.Starting,
                LibreWorlds.WorldAdapter.WorldAdapterState.InWorld => SubsystemState.Active,
                LibreWorlds.WorldAdapter.WorldAdapterState.Stopping => SubsystemState.Starting,
                _ => SubsystemState.Error
            };
        }

        // ---- UI Commands ----

        public void StartSession()
        {
            _worldAdapter.Start();
            _worldAdapter.Authenticate();
            _worldAdapter.EnterWorld();
        }

        public void StopSession()
        {
            _worldAdapter.Stop();
        }
    }
}
