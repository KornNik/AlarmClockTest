using System;
using TMPro;
using Helpers.Extensions;
using Helpers.Observer;

namespace GameUI
{
    sealed class TimeUI : ClockBase<TMP_Text>, IEventListener<TimeEvent>
    {
        private void OnEnable()
        {
            this.EventStartListening<TimeEvent>();

        }
        private void OnDisable()
        {
            this.EventStopListening<TimeEvent>();

        }
        protected override void FillText(TimeSpan timerValue)
        {
            var valueConverted = DateTimeExtension.FromDoubleToString(timerValue.TotalSeconds);
            _timerText.text = valueConverted;
        }
        public void OnEventTrigger(TimeEvent eventType)
        {
            FillText(eventType.TimeValue);
            SetClockHands(eventType.TimeValue);
        }
    }
}
