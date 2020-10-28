using System;

namespace Test.RoseTreeTest
{
    public class FindCommand : Command
    {
        public FindCommand(string name) : base(name)
        {
        }

        public override void Execute()
        {
            Console.WriteLine("Find");
        }
    }
}
