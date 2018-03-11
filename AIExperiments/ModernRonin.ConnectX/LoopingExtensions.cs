using System;

namespace ModernRonin.ConnectX
{
    public static class LoopingExtensions
    {
        public static LoopContinuation By(this int width, int height) => new LoopContinuation(width, height);
        public static void Do(this int x, Action<int> action)
        {
            for (var i = 0; i < x; ++i) action(i);
        }
    }

    public class LoopContinuation
    {
        readonly int mHeight;
        readonly int mWidth;
        public LoopContinuation(int width, int height)
        {
            mWidth = width;
            mHeight = height;
        }
        public void Do(Action<int, int> action)
        {
            for (var x = 0; x < mWidth; ++x)
            for (var y = 0; y < mHeight; ++y)
                action(x, y);
        }
    }
}