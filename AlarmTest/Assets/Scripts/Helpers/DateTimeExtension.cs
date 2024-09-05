using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Helpers.Extensions
{
    static class DateTimeExtension
    {
        public const int SECONDS_IN_DAY = 86400; //24*60*60
        public const int DEGREES_IN_HOUR = 30;  // 360/12 in hour.
        public const int DEGREES_IN_MINUTE = 6;    // 360/60 in minute.
        public const int DEGREES_IN_SECOND = 6;   // 360/60 in second.

        public static string FromDoubleToString(double timeInSeconds)
        {
            var secondsToTime = TimeSpan.FromSeconds(timeInSeconds);
            string time;

            if (secondsToTime.Hours > 0)
            {
                time = StringBuilderExtender.CreateString(secondsToTime.Hours.ToString("F0"), ":", secondsToTime.Minutes.ToString("F0"), ":", secondsToTime.Seconds.ToString("00"));
            }
            else
            {
                time = StringBuilderExtender.CreateString(secondsToTime.Minutes.ToString("F0"), ":", secondsToTime.Seconds.ToString("00"));
            }
            return time;
        }
        public static bool IsTimeEqualHands(TimeSpan firstTime, TimeSpan secondTime)
        {
            var substruct = firstTime.Subtract(secondTime);

            if (substruct.Hours == -12 ||substruct.Hours == 12 && substruct.Minutes == 0)
            {
                return true;
            }
            return false;
        }
        public static bool IsTimeEqualDigit(TimeSpan firstTime, TimeSpan secondTime)
        {
            var compair = TimeSpan.Compare(firstTime, secondTime);

            if (compair < 0)
            {
                return true;
            }
            return false;
        }
        public static TimeSpan HandsDegreesInTime(Vector3 hourEulerAngle, Vector3 minuteEulerAngle, Vector3 secondsEulerAngle)
        {
            TimeSpan alarmTime;
            int alarmHour = (int)Mathf.Abs((hourEulerAngle.z - 360) / DEGREES_IN_HOUR);
            int alarmMinute = (int)Mathf.Abs((minuteEulerAngle.z - 360) / DEGREES_IN_MINUTE);
            int alarmSeconds = (int)Mathf.Abs((secondsEulerAngle.z - 360) / DEGREES_IN_SECOND);
            alarmTime = new TimeSpan(alarmHour, alarmMinute, alarmSeconds);
            return alarmTime;
        }
        public static TimeSpan EnterStringInTime(string enteredString)
        {
            TimeSpan alarmTime;
            alarmTime = TimeSpan.Parse(enteredString);
            return alarmTime;
        }

        public static int HourToSeconds(int hour)
        {
            var inSeconds = hour * 60 * 60;
            return inSeconds;
        }
        public static int MinuteToSeconds(int minute)
        {
            var inSeconds = minute * 60;
            return inSeconds;
        }
    }
}
