using UnityEngine;
using Behaviours;

namespace Controllers
{
    sealed class TimeControl : MonoBehaviour
    {
        private ClockTime _clockTime;
        private AlarmClock _alarmClock;

        public ClockTime ClockTime => _clockTime;
        public AlarmClock AlarmClock => _alarmClock;

        private void Awake()
        {
            _clockTime = new ClockTime();
            _alarmClock = new AlarmClock();
        }
        private void Start()
        {
            _clockTime.RequestTime();
        }
        private void OnEnable()
        {
            _clockTime.Subscribe();
            _alarmClock.Subscribe();
        }
        private void OnDisable()
        {
            _clockTime.Unsubscribe();
            _alarmClock.Unsubscribe();
        }
    }
}
