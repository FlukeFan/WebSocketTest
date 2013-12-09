using System;
using System.Threading;

namespace WebSocketTest
{
    public static class Wait
    {
        private static TimeSpan Timeout = TimeSpan.FromSeconds(15);

        public static void For(Action action)
        {
            For(Timeout, action);
        }

        public static void For(TimeSpan timeout, Action action)
        {
            var waitUntil = DateTime.Now + timeout;

            while (true)
            {
                try
                {
                    Thread.Sleep(0);
                    action();
                    return;
                }
                catch
                {
                    if (DateTime.Now > waitUntil)
                        throw;

                    Thread.Sleep(50);
                }
            }
        }
    }
}
