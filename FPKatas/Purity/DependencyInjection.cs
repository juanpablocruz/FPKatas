using System;

namespace FPKatas.Purity
{
    public static class DependencyInjection
    {
        public static void impureLogSomething(string text)
        {
            Console.WriteLine($"{DateTime.UtcNow.ToString("o")}: {text}");
        }

        public static void pureLogSomething(DateTime dt, Action<string> output, string text)
        {
            output($"{dt.ToString("o")} {text}");
        }
    }
}
