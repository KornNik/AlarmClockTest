using System;
using UnityEngine;

namespace Helpers.Extensions
{
    static class DateTimeExtension
    {
        public const int SECONDS_IN_DAY = 86400;
        public const int DEGREES_IN_HOUR = 30;  // 360/12 in hour.
        public const int DEGREES_IN_MINUTE = 6;    // 360/60 in minute.
        public const int DEGREES_IN_SECOND = 6;   // 360/60 in second.

        public static bool IsDayPastBetweenTwoDates(DateTime lastRequested, DateTime current)
        {
            if (lastRequested.Year < current.Year || lastRequested.Month < current.Month)
            {
                return true;
            }
            else if (lastRequested.Day + 1 < current.Day)
            {
                return true;
            }
            else if (lastRequested.Day < current.Day)
            {
                if (lastRequested.Hour <= current.Hour)
                {
                    return true;
                }
            }
            return false;
        }
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
        public static int PastTimeInSeconds(DateTime lastRequested, DateTime current)
        {
            var lastRequestedTime = new TimeSpan(lastRequested.Hour, lastRequested.Minute, lastRequested.Second);
            var currentTime = new TimeSpan(current.Hour, current.Minute, current.Second);

            int difference;
            int remainingTime;

            if (lastRequested.Day < current.Day)
            {
                var lastDifferenceSeconds = SECONDS_IN_DAY - lastRequestedTime.TotalSeconds;
                var currentTimeSeconds = currentTime.TotalSeconds;
                difference = (int)(lastDifferenceSeconds + currentTimeSeconds);
                remainingTime = SECONDS_IN_DAY - difference;
            }
            else
            {
                difference = (int)(currentTime.TotalSeconds - lastRequestedTime.TotalSeconds);
                remainingTime = SECONDS_IN_DAY - difference;
            }

            return remainingTime;
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
