using UnityEngine;
using Helpers;
using Helpers.Extensions;
using Data;

namespace GameUI
{
    sealed class ScreenFactory
    {
        private Canvas _canvas;
        private AlarmMenu _alarmMenu;
        private ClockMenu _clockMenu;


        public ScreenFactory()
        {
            var resources = CustomResources.Load<Canvas>(Services.Instance.Data.ServicesObject.
                GetData<GameObjectsResourcesBundle>().GetScreenPath(ScreenTypes.Canvas));
            _canvas = Object.Instantiate(resources, Vector3.zero, Quaternion.identity);
        }

        public ClockMenu GetClockMenu()
        {
            if (_clockMenu == null)
            {
                var resources = CustomResources.Load<ClockMenu>(Services.Instance.Data.ServicesObject.
                    GetData<GameObjectsResourcesBundle>().GetScreenPath(ScreenTypes.ClockMenu));
                _clockMenu = Object.Instantiate(resources, _canvas.transform.position, Quaternion.identity, _canvas.transform);
            }
            return _clockMenu;
        }

        public AlarmMenu GetAlarmMenu()
        {
            if (_alarmMenu == null)
            {
                var resources = CustomResources.Load<AlarmMenu>(Services.Instance.Data.ServicesObject.
                    GetData<GameObjectsResourcesBundle>().GetScreenPath(ScreenTypes.AlarmMenu));
                _alarmMenu = Object.Instantiate(resources, _canvas.transform.position, Quaternion.identity, _canvas.transform);
            }
            return _alarmMenu;
        }
    }
}