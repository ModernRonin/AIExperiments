using System;
using System.Diagnostics;

namespace ModernRonin.ConnectX
{
    public class StopWatchEx : IDisposable
    {
        readonly Action<TimeSpan> mUseMeasuredTime;
        readonly Stopwatch mWatch = new Stopwatch();
        public StopWatchEx(Action<TimeSpan> useMeasuredTime)
        {
            mUseMeasuredTime = useMeasuredTime;
            mWatch.Start();
        }
        public void Dispose()
        {
            mWatch.Stop();
            mUseMeasuredTime(mWatch.Elapsed);
        }
    }
}