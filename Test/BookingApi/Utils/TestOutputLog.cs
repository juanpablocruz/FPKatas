using BookingAPI;
using System;
using Xunit.Abstractions;

namespace Test.BookingApi
{
    public class TestOutputLog : ILog
    {
        public ITestOutputHelper Output { get; }
        public TestOutputLog(ITestOutputHelper output)
        {
            Output = output;
        }

        public void Debug(string message)
        {
            Write(message);
        }

        public void Error(string message)
        {
            Write(message);
        }

        public void Info(string message)
        {
            Write(message);
        }

        public void Warning(string message)
        {
            Write(message);
        }
        public void Write(string message)
        {
            Output.WriteLine("{0}: {1}", DateTimeOffset.Now, message);
        }
    }
}
