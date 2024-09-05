using Helpers;
using Controllers;
using Data;
using UnityEngine;
using Helpers.Extensions;
using GameUI;

namespace Assets.Scripts
{
    sealed class GameLoader : PersistanceSingleton<GameLoader>
    {
        [SerializeField] private DataBundle _dataBundle;

        private SettingsController _settingsController;

        private void Awake()
        {
            Services.Instance.Data.SetObject(_dataBundle);

            _settingsController = new SettingsController();

            var timeResources = CustomResources.Load<TimeControl>(Services.Instance.Data.ServicesObject.
                GetData<GameObjectsResourcesBundle>().GetTimeControlPath());

            Services.Instance.TimeController.SetObject(Instantiate(timeResources));
            Services.Instance.SettingsController.SetObject(_settingsController);
            ScreenInterface.GetInstance().Execute(ScreenTypes.ClockMenu);
        }
    }
}
