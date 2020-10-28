using ArchivePicture;
using FPKatas.RoseTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Test.ArchiveTest
{
    using static RoseTree;
    class CalculateMovesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[] {
                Leaf<string,FileInfo>(new FileInfo("1")),
                Leaf<string,Move>(new Move { Source=new FileInfo("1"), Destination=new FileInfo("1") }),
            };

            yield return new object[] {
                Node("a", Leaf<string,FileInfo>(new FileInfo("1"))),
                Node("a", Leaf<string,Move>(new Move { Source=new FileInfo("1"), Destination=new FileInfo(Path.Combine("a","1")) } )),
            };

            yield return new object[] {
                Node("a", Leaf<string,FileInfo>(new FileInfo("1")), Leaf<string,FileInfo>(new FileInfo("2"))),
                Node("a", 
                    Leaf<string,Move>(new Move { Source=new FileInfo("1"), Destination=new FileInfo(Path.Combine("a","1")) } ),
                    Leaf<string,Move>(new Move { Source=new FileInfo("2"), Destination=new FileInfo(Path.Combine("a","2")) } )
                ),
            };

            yield return new object[] {
                Node("a", 
                    Node("b", Leaf<string,FileInfo>(new FileInfo("1")), Leaf<string,FileInfo>(new FileInfo("2"))),
                    Node("c", Leaf<string,FileInfo>(new FileInfo("3")))
                ),
                Node("a",
                    Node(Path.Combine("a", "b"),
                        Leaf<string,Move>(new Move { Source=new FileInfo("1"), Destination=new FileInfo(Path.Combine("a","b","1")) } ),
                        Leaf<string,Move>(new Move { Source=new FileInfo("2"), Destination=new FileInfo(Path.Combine("a","b","2")) } )
                    ),
                    Node(Path.Combine("a","c"),
                        Leaf<string,Move>(new Move { Source=new FileInfo("3"), Destination=new FileInfo(Path.Combine("a","c","3")) } )
                    )
                ),
            };

        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
