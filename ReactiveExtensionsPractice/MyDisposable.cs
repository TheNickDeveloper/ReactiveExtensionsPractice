using System;
using System.Diagnostics;

namespace ReactiveExtensionsPractice
{
    public class MyDisposable : IDisposable
    {
        public void Dispose()
        {
            var callerName = new StackTrace().GetFrame(1)?.GetMethod()?.Name;
            Console.WriteLine($"Dispose by {callerName}");
        }
    }
}