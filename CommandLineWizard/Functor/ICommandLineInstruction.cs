using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineWizard
{
    public interface ICommandLineInstruction<T>
    {
    }

    public static class ICommandLineInstructionExt
    {
        public static void Deconstruct<T>(this ReadLine<T> readLine, out string text, out Func<string, T> fn)
        {
            text = readLine.Text;
            fn = readLine.Fn;
        }

        public static void Deconstruct<T>(this WriteLine<T> writeLine, out string text, out Func<string,T> fn)
        {
            text = writeLine.Text;
            fn = writeLine.Fn;
        }

        public static ICommandLineInstruction<R> MapI<T, R>(this ICommandLineInstruction<T> instruction, Func<T, R> f)
        {
            Func<ReadLine<T>, Func<T, R>, ReadLine<R>> mapM1 = (m1, g) =>
            {
                (var data, var f) = m1;
                return new ReadLine<R>(data, x => g(f(x)));
            };

            Func<WriteLine<T>, Func<T, R>, WriteLine<R>> mapM2 = (m2, g) =>
            {
                (var data, var f) = m2;
                return new WriteLine<R>(data, x => g(f(x)));
            };

            return instruction switch
            {
                ReadLine<T> m1 => mapM1(m1, f),
                WriteLine<T> m2 => mapM2(m2, f),
                _ => default
            };
        }
    }
}
