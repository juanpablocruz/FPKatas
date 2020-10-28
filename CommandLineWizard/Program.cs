using System;
using System.Linq;

namespace CommandLineWizard
{
    using static CommandLineProgramExt;

    public class CommandLineBuilder
    {
        public ICommandLineProgram<string> From(string initial, params Func<string, ICommandLineProgram<string>>[] programs)
        {
            Func<string, ICommandLineProgram<string>> seed = x => Return(x);
            return programs.Aggregate(seed, (acc, item) => x => Bind(item)(acc(x)))(initial);
        }

        public Func<ICommandLineProgram<T>, ICommandLineProgram<R>> Bind<T, R>(Func<T, ICommandLineProgram<R>> func)
            => program
            => CommandLineProgramExt.Bind(func)(program);

        public ICommandLineProgram<T> Return<T>(T x)
            => new Pure<T>(x);

        public ICommandLineProgram<T> Zero<T>()
            => new Pure<T>(default);



        public static T interpret<T>(ICommandLineProgram<T> program)
        {
            Func<string, int> imp1 = x => {
                var b = int.TryParse(x, out var result);
                if (b)
                    return result;
                return default;
            };


            Func<ReadLine<ICommandLineProgram<T>>, T> interpretM1 = f =>
            {
                return interpret(f.Fn(Console.ReadLine()));
            };

            Func<WriteLine<ICommandLineProgram<T>>, T> interpretM2 = f =>
            {
                Func<string, string> fn = x =>
                {
                    Console.WriteLine(x);
                    return x;
                };
                return interpret(f.Fn(fn(f.Text)));
            };

            return program switch
            {
                Pure<T> p => p.Value,
                Free<T> f => f.Functor switch
                {
                    ReadLine<ICommandLineProgram<T>> m1 => interpretM1(m1),
                    WriteLine<ICommandLineProgram<T>> m2 => interpretM2(m2)
                },
                _ => default
            };
        }
    }



    class Program
    {
        static void Main(string[] args)
        {

            var commandLine = new CommandLineBuilder();

            var program = commandLine.From("",
                x => writeLine<string>("Please enter your name."),
                x => readLine<string>(""),
                x => writeLine<string>($"Hello, {x}")
            );

            var res = CommandLineBuilder.interpret(program);

            Console.WriteLine("End");
        }
    }
}
