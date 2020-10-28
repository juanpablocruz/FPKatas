using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineWizard
{
    public class ReadLine<T> : ICommandLineInstruction<T>
    {
        public string Text { get; }
        public Func<string, T> Fn { get; }

        public ReadLine(string text, Func<string, T> fn)
        {
            Text = text;
            Fn = fn;
        }
    }
}
