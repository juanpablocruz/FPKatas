using BookingAPI;
using Newtonsoft.Json;
using SGS.Framework.FunK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.BookingApi;
using Xunit;

namespace Test.Examples
{
    using static F;
    
   public class LogData
    {
        public Interaction entry { get; set; }
        public IEnumerable<Interaction> interactions { get; set; }
        public Interaction exit { get; set; }
    }
    
    public class ValidationException : Exception
    {
        private IEnumerable<string> validations { get; } 
        public ValidationException(IEnumerable<string> validations)
        {
            this.validations = validations;
        }
    }

    public class ResultComposition
    {
        public Result<LogData> ReadFromFile()
        {
            var json = Log.LoadEmbeddedFile("CapacityEdgeCase.txt");
            var logEntry = JsonConvert.DeserializeObject<LogData>(json);

            if (logEntry.entry is null)
            {
                return Result<LogData>(new ArgumentNullException());
            }

            return logEntry;
        }


        public Result<int> LogToConsole(LogData data)
        {
            try
            {
                Console.WriteLine(JsonConvert.SerializeObject(data.interactions));
            } catch(Exception e)
            {
                return Result<int>(e);
            }
            return data.interactions.Count();
        }

        public int CountInteractions(IEnumerable<Interaction> interactions)
            => interactions.Count();

        public int CountLogInteractions(LogData log)
            => CountInteractions(log.interactions);

        public (LogData, int) GetOriginalAndSize(LogData log)
            => (log, CountLogInteractions(log));

        public Maybe<LogData> ValidIfMoreThan1Interaction(LogData log)
            => CountLogInteractions(log) > 1 ? Just(log) : Nothing;

        public Result<LogData> ValidateLogData(LogData log)
            => ValidIfMoreThan1Interaction(log).Match(
                Nothing: () => Result<LogData>(new ArgumentException()),
                Just: t => t);


        public Maybe<string> ValidateNotEmptyTime(Interaction interaction)
            => string.IsNullOrEmpty(interaction.Time) ? Nothing : Just(interaction.Time);

        public Maybe<string> ValidateNotEmptyOperation(Interaction interaction)
            => string.IsNullOrEmpty(interaction.Operation) ? Nothing : Just(interaction.Operation);

        public Maybe<object> ValidateNotEmptyInput(Interaction interaction)
            => interaction.Input is null ? Nothing : Just(interaction.Input);

        public Maybe<object> ValidateNotEmptyOutput(Interaction interaction)
            => interaction.Output is null ? Nothing : Just(interaction.Output);


        public Either<List<string>, Interaction> ValidateInteractionEither(Interaction interaction)
        {
            var validTime = ValidateNotEmptyTime(interaction);
            var validOperation = ValidateNotEmptyOperation(interaction);
            var validInput = ValidateNotEmptyInput(interaction);
            var validOutput = ValidateNotEmptyOutput(interaction);

            var acc = new List<string>();

            validTime.Match(
                Nothing: () => acc.Add("Invalid time"),
                Just: t => { }
            );
            validOperation.Match(
                Nothing: () => acc.Add("Invalid operation"),
                Just: t => { }
            );
            validInput.Match(
               Nothing: () => acc.Add("Invalid input"),
               Just: t => { }
           );
            validOutput.Match(
                Nothing: () => acc.Add("Invalid output"),
                Just: t => { }
            );

            if (acc.Count > 0)
                return Left(acc);

            return Right(interaction);
        }

        public Result<Interaction> ValidateInteractionResult(Interaction interaction)
        {
            var validTime = ValidateNotEmptyTime(interaction);
            var validOperation = ValidateNotEmptyOperation(interaction);
            var validInput = ValidateNotEmptyInput(interaction);
            var validOutput = ValidateNotEmptyOutput(interaction);

            var acc = new List<string>();

            validTime.Match(
                Nothing: () => acc.Add("Invalid time"),
                Just: t => { }
            );
            validOperation.Match(
                Nothing: () => acc.Add("Invalid operation"),
                Just: t => { }
            );
            validInput.Match(
               Nothing: () => acc.Add("Invalid input"),
               Just: t => { }
           );
            validOutput.Match(
                Nothing: () => acc.Add("Invalid output"),
                Just: t => { }
            );

            return acc.Count > 0 ? Result<Interaction>(new ValidationException(acc)) : interaction;
        }

        public Validation<Interaction> ValidateInteraction(Interaction interaction)
        {
            var validTime = ValidateNotEmptyTime(interaction).Match(
               Nothing: () => Error("Invalid time"),
               Just: t => Valid(interaction)
           );
            var validOperation = ValidateNotEmptyOperation(interaction).Match(
                Nothing: () => Error("Invalid operation"),
                Just: t => Valid(interaction)
            );
            var validInput = ValidateNotEmptyInput(interaction).Match(
               Nothing: () => Error("Invalid input"),
               Just: t => Valid(interaction)
           );
            var validOutput = ValidateNotEmptyOutput(interaction).Match(
                Nothing: () => Error("Invalid output"),
                Just: t => Valid(interaction)
            );

            return validTime.Aggregate(validOperation).Aggregate(validInput).Aggregate(validOutput);
        }

        public Result<LogData> ValidateInteractions(LogData logData)
        {
            var validated = logData.interactions.Map(ValidateInteraction);

            var failed = validated.Aggregate(new List<string>(), (acc, item) => item.Errors.Count() > 0 ? acc.Concat(item.Errors.Map(e => e.Message)).ToList() : acc);

            if (failed.Count > 0 )
            {
                return Result<LogData>(new ValidationException(failed));
            }
            return logData;
        }


        [Fact]
        public void Test()
        {
            var res = ReadFromFile();

            var newRes = res.Bind(ValidateInteractions);

            Assert.True(res.IsSuccess);
        }


        public static string ConcatTwo(string a, string b) 
            => a + b;

        public static int AddTwo(int a, int b)
            => a + b;


        [Fact]
        public void TestStatic()
        {
            var res1 = ConcatTwo("a", "b");

            AddTwo(2, 3);

            var res2 = ConcatTwo("a", "b");

            AddTwo(2, 3);

            Assert.Equal(res1, res2);
        }
    }
}
