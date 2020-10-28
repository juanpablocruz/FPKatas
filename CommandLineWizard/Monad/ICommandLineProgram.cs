using System;

namespace CommandLineWizard
{
    public interface ICommandLineProgram<T>
    {
    }


   
    public static class CommandLineProgramExt
    {
        public static Func<ICommandLineProgram<T>, ICommandLineProgram<R>> Bind<T, R>(Func<T, ICommandLineProgram<R>> f)
            => program
            =>
            {
                return program switch
                {
                    Free<T> free => new Free<R>(free.Functor.MapI(Bind(f))),
                    Pure<T> x => f(x.Value),
                    _ => default
                };
            };

        public static ICommandLineProgram<string> readLine<T>(string inpt)
            => new Free<string>(new ReadLine<ICommandLineProgram<string>>(inpt, x => new Pure<string>(x)));

        public static ICommandLineProgram<T> writeLine<T>(string inpt)
            => new Free<T>(new WriteLine<ICommandLineProgram<T>>(inpt, x => new Pure<T>(default)));

        
    }
}
