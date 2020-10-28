using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineWizard
{
    public class Pure<T> : ICommandLineProgram<T>
    {
        public T Value { get; }

        public Pure(T value)
        {
            Value = value;
        }
    }
}
