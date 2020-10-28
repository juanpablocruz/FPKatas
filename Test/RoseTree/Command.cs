namespace Test.RoseTreeTest
{
    public class Command
    {
        public Command(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public virtual void Execute()
        {
        }
    }
}
