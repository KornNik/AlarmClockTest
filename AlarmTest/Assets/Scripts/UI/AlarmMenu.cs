using UnityEngine;
using UnityEngine.UI;
using Helpers.Observer;

namespace GameUI
{
    class AlarmMenu : BaseUI
    {
        [SerializeField] private Button _clockButton;
        [SerializeField] private Button _setAlarmButton;
        [SerializeField] private AlarmClockUI _alarmClock;

        private void OnEnable()
        {
            _clockButton.onClick.AddListener(OnClockButtonDown);
            _setAlarmButton.onClick.AddListener(OnSetAlarmButtonDown);
        }

        private void OnDisable()
        {
            _clockButton.onClick.RemoveListener(OnClockButtonDown);
            _setAlarmButton.onClick.RemoveListener(OnSetAlarmButtonDown);
        }
        public override void Show()
        {
            gameObject.SetActive(true);
            ShowUI.Invoke();
        }
        public override void Hide()
        {
            gameObject.SetActive(false);
            HideUI.Invoke();
        }

        private void OnClockButtonDown()
        {
            ScreenInterface.GetInstance().Execute(Helpers.ScreenTypes.ClockMenu);
        }
        private void OnSetAlarmButtonDown()
        {
            AlarmEvent.Trigger(AlarmEventType.ButtonDown);
        }
    }
}