using System;
using Helpers;

namespace GameUI
{
    sealed class ScreenInterface : IDisposable
    {
        private BaseUI _currentWindow;
        private readonly ScreenFactory _screenFactory;
        private static ScreenInterface _instance;

        private ScreenInterface()
        {
            _screenFactory = new ScreenFactory();
        }

        public BaseUI CurrentWindow => _currentWindow;

        public static ScreenInterface GetInstance()
        {
            return _instance ?? (_instance = new ScreenInterface());
        }

        public void Execute(ScreenTypes screenType)
        {
            if (CurrentWindow != null)
            {
                CurrentWindow.Hide();
            }

            switch (screenType)
            {
                case ScreenTypes.ClockMenu:
                    _currentWindow = _screenFactory.GetClockMenu();
                    break;
                case ScreenTypes.AlarmMenu:
                    _currentWindow = _screenFactory.GetAlarmMenu();
                    break;
                default:
                    break;
            }

            CurrentWindow.Show();
        }

        public void AddObserver(ScreenTypes screenType, IListenerScreen listenerScreen)
        {
            switch (screenType)
            {
                case ScreenTypes.ClockMenu:
                    _screenFactory.GetClockMenu().ShowUI += listenerScreen.ShowScreen;
                    _screenFactory.GetClockMenu().HideUI += listenerScreen.HideScreen;
                    _screenFactory.GetClockMenu().Hide();
                    break;
                case ScreenTypes.AlarmMenu:
                    _screenFactory.GetAlarmMenu().ShowUI += listenerScreen.ShowScreen;
                    _screenFactory.GetAlarmMenu().HideUI += listenerScreen.HideScreen;
                    _screenFactory.GetAlarmMenu().Hide();
                    break;
                default:
                    break;
            }
        }

        public void RemoveObserver(ScreenTypes screenType, IListenerScreen listenerScreen)
        {
            switch (screenType)
            {
                case ScreenTypes.ClockMenu:
                    _screenFactory.GetClockMenu().ShowUI -= listenerScreen.ShowScreen;
                    _screenFactory.GetClockMenu().HideUI -= listenerScreen.HideScreen;
                    _screenFactory.GetClockMenu().Hide();
                    break;
                case ScreenTypes.AlarmMenu:
                    _screenFactory.GetAlarmMenu().ShowUI -= listenerScreen.ShowScreen;
                    _screenFactory.GetAlarmMenu().HideUI -= listenerScreen.HideScreen;
                    _screenFactory.GetAlarmMenu().Hide();
                    break;
                default:
                    break;
            }
        }

        public void Dispose()
        {
            _instance = null;
        }
    }
}
