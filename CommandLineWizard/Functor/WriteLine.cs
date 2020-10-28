using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineWizard
{
    public class WriteLine<T> : ICommandLineInstruction<T>
    {
        public string Text { get; }
        public Func<string, T> Fn { get; }
        public WriteLine(string text, Func<string,T> fn)
        {
            Text = text;
            Fn = fn;
        }
    }
}
