using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Behaviours
{
    struct DateTimeTimeApi
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int seconds;
        public int milliSeconds;
        public DateTime dateTime;
        public string date;
        public string time;
        public string timeZone;
        public string dayOfWeek;
        public bool dstActive;
    }
    struct DateTimeYandexApi
    {
        public string time;
    }
    sealed class RealTimeRequester
    {
        public event Action<DateTime> TimeReceived;

        public const string YANDEX_TIME_API = "https://yandex.com/time/sync.json?geo=213";
        public const string TIME_IO_API = "https://www.timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow";

        private DateTime _localDateTime;
        private UniTask _webRequestTask;

        public DateTime LocalDateTime => _localDateTime;

        public void GetCurrentTime(string url, bool isInvokeOnEnd)
        {
            _webRequestTask = URLRequestTime(url, isInvokeOnEnd);
        }
        private async UniTask URLRequestTime(string url, bool isInvokeOnEnd)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                await webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        break;
                    case UnityWebRequest.Result.Success:
                        if (url == YANDEX_TIME_API)
                        {
                            DateTimeYandexApi dateTime = JsonUtility.FromJson<DateTimeYandexApi>(webRequest.downloadHandler.text);
                            Debug.Log(dateTime.time);
                            _localDateTime = ParseToDateTime(dateTime.time);
                        }
                        else
                        {
                            DateTimeTimeApi dateTime = JsonUtility.FromJson<DateTimeTimeApi>(webRequest.downloadHandler.text);
                            _localDateTime = ParseToDateTime(dateTime);
                        }
                        if (isInvokeOnEnd) { TimeReceived?.Invoke(_localDateTime); }
                        break;
                }
            }
        }
        private DateTime ParseToDateTime(string value)
        {
            long time = long.Parse(value);
            return DateTime.FromFileTime(time);
        }
        private DateTime ParseToDateTime(DateTimeTimeApi dateTime)
        {
            DateTime time = new DateTime(dateTime.year, dateTime.month, dateTime.day, dateTime.hour, dateTime.minute, dateTime.seconds);
            return time;
        }
    }
}
