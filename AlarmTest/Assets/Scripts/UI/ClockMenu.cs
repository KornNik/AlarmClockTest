using Helpers;
using Helpers.Observer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameUI
{
    class ClockMenu : BaseUI, IEventListener<AlarmEvent>
    {
        [SerializeField] private Button _alarmButton;
        [SerializeField] private TimeUI _timeUI;
        [SerializeField] private TMP_Text _alarmText;

        private void OnEnable()
        {
            _alarmButton.onClick.AddListener(OnAlarmButtonDown);
            this.EventStartListening<AlarmEvent>();
        }

        private void OnDisable()
        {
            _alarmButton.onClick.RemoveListener(OnAlarmButtonDown);
            this.EventStopListening<AlarmEvent>();
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

        private void OnAlarmButtonDown()
        {
            ScreenInterface.GetInstance().Execute(ScreenTypes.AlarmMenu);
            AlarmTimeEvent.Trigger(Services.Instance.TimeController.ServicesObject.ClockTime.GetCurrentTime());
        }

        public void OnEventTrigger(AlarmEvent eventType)
        {
            if (eventType.EventType == AlarmEventType.ActivateAlarm)
            {
                _alarmText.enabled = true;
            }
        }
    }
}