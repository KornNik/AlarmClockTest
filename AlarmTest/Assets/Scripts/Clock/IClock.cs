using System;

namespace Behaviours
{
    interface IClock
    {
        void StartTime();
        void StopTime();
        TimeSpan GetCurrentTime();
    }
}
