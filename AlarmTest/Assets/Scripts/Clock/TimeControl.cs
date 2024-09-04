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
            _clockTime = new ClockTime(this);
            _alarmClock = new AlarmClock();
        }
    }
}
