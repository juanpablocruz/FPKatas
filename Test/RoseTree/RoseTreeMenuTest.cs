using FPKatas.RoseTree;
using SGS.Framework.FunK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Test.RoseTreeTest
{
    public class RoseTreeMenuTest
    {
        IRoseTree<string, string> editMenuTemplate { get; set; }
        Dictionary<string, Command> commandStore { get; set; }

        public RoseTreeMenuTest()
        {
            commandStore = new Dictionary<string, Command>()
            {
                { "Find", new FindCommand("Find") }
            };

            editMenuTemplate =
                RoseTree.Node("Edit",
                    RoseTree.Node("Find and Replace",
                        new RoseLeaf<string, string>("Find"),
                        new RoseLeaf<string, string>("Replace")),
                    RoseTree.Node("Case",
                        new RoseLeaf<string, string>("Upper"),
                        new RoseLeaf<string, string>("Lower")),
                    new RoseLeaf<string, string>("Cut"),
                    new RoseLeaf<string, string>("Copy"),
                    new RoseLeaf<string, string>("Paste"));
        }

        [Fact]
        public void test()
        {
            IRoseTree<string, Command> editMenu =
                from name in editMenuTemplate
                select commandStore.Lookup(name).GetOrElse(new Command(name));

            Maybe<Command> find;


            using StringWriter sw = new StringWriter();

            Console.SetOut(sw);
            find = editMenu.Find(e => e.Name == "Find");
            var findAll = editMenu.FindAll(e => e.Name == "Find" || e.Name == "Copy");
            
            var concat = editMenu.Fold((acc, c) => acc + c.Name, "");

            find.Match(Nothing: () => { }, Just: c => c.Execute());

            Assert.Equal($"Find\r\n", sw.ToString());
        }
    }
}
