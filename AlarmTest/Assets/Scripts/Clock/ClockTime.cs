using UnityEngine;
using Cysharp.Threading.Tasks;
using Helpers.Observer;
using System.Threading;
using System;


namespace Behaviours
{
    class ClockTime : IClock, IEventSubscription
    {
        public const int TIMER_UPDATE_FREQUENCY_HOURS = 1;

        private DateTime _dateTime;
        private TimeSpan _mainTime;
        private TimeSpan _timerUpdateFrequency;
        private TimeSpan _currentTime;
        private RealTimeRequester _timeRequester;

        protected WaitForEndOfFrame _waitForEndOfFrame;
        protected UniTask _timeValidationWaitTask;
        protected UniTask _timeTickTask;

        private CancellationTokenSource _timerCancellationTokenSource;
        private CancellationToken _timerCancellationToken;

        private float _updateTimesLeft;
        private float _timeValue;
        private bool _isTimeCanTick;

        public ClockTime()
        {
            _timeRequester = new RealTimeRequester();
            _timerCancellationTokenSource = new CancellationTokenSource();

            _dateTime = default;
            _mainTime = default;
            _timerUpdateFrequency = new TimeSpan(TIMER_UPDATE_FREQUENCY_HOURS, 0,0);
        }

        private void CheckUpdateTime(DateTime currentDateTime)
        {
            FreshTimerStart(currentDateTime);
        }

        private void FreshTimerStart(DateTime currentDateTime)
        {
            StopTime();
            _dateTime = currentDateTime;
            _mainTime = new TimeSpan(_dateTime.Hour, _dateTime.Minute, _dateTime.Second);
            StartTime();
        }

        public void StartTime()
        {
            _timerCancellationTokenSource = new CancellationTokenSource();
            _timerCancellationToken = _timerCancellationTokenSource.Token;
            _isTimeCanTick = true;

            _timeValidationWaitTask = TimeValidationAwate();
            _timeTickTask = TimeTick();
        }
        public void StopTime()
        {
            _timerCancellationTokenSource.Cancel();
            EndAsync();
        }
        public virtual void RequestTime()
        {
            _timeRequester.GetCurrentTime(RealTimeRequester.TIME_IO_API, isInvokeOnEnd:  true);
        }
        public TimeSpan GetCurrentTime()
        {
            return _currentTime;
        }

        protected void EndAsync()
        {
            _timeValidationWaitTask.Forget();
            _timeTickTask.Forget();
        }
        protected async UniTask TimeTick()
        {
            _timeValue = (float)_mainTime.TotalSeconds;
            while (_isTimeCanTick)
            {
                _timeValue += Time.deltaTime;
                _currentTime = TimeSpan.FromSeconds(_timeValue);
                TimeEvent.Trigger(_currentTime);
                await UniTask.Yield(PlayerLoopTiming.Update, _timerCancellationToken, true);
            }
            await UniTask.Yield();
        }
        protected async UniTask TimeValidationAwate()
        {
            _updateTimesLeft = (float)_timerUpdateFrequency.TotalSeconds;
            while (_updateTimesLeft > 0)
            {
                _updateTimesLeft -= Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            RequestTime();
            await UniTask.Yield();
        }

        public void Subscribe()
        {
            _timeRequester.TimeReceived += CheckUpdateTime;
        }

        public void Unsubscribe()
        {
            _timeRequester.TimeReceived -= CheckUpdateTime;
        }
    }
}
