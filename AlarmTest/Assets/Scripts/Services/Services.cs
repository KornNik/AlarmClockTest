using System;
using Controllers;
using Data;

namespace Helpers
{
    sealed class Services
    {
        private static readonly Lazy<Services> _instance = new Lazy<Services>();

        public static Services Instance => _instance.Value;
        public Service<TimeControl> TimeController { get; private set; }
        public Service<SettingsController> SettingsController { get; private set; }
        public Service<DataBundle> Data { get; private set; }

        public Services()
        {
            Initialize();
        }

        private void Initialize()
        {
            SettingsController = new Service<SettingsController>();
            TimeController = new Service<TimeControl>();
            Data = new Service<DataBundle>();
        }

    }
}
