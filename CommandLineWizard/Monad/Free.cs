using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineWizard
{
    public class Free<T> : ICommandLineProgram<T>
    {
        public ICommandLineInstruction<ICommandLineProgram<T>> Functor { get; }
        public Free(ICommandLineInstruction<ICommandLineProgram<T>> functorVal)
        {
            Functor = functorVal;
        }
    }
}
