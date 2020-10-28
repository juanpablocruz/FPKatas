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
    public class MoveToDestinationTestData : IEnumerable<object[]>
    {
        IRoseTree<string, PhotoFile> PhotoLeaf(string name, int y, int mnth, int d, int h, int m, int s)
            => new RoseLeaf<string, PhotoFile>(new PhotoFile { File = new FileInfo(name), TakenOn = new DateTime(y, mnth, d, h, m, s) });

        public IEnumerator<object[]> GetEnumerator()
        {

            yield return new object[] {
                PhotoLeaf("1", 2018,11,9,11,47,17),
                "D",
                Node("D", new [] {RoseTree.Node("2018-11", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("1"))})})
            };

            yield return new object[] {
                RoseTree.Node("S", new [] { PhotoLeaf("4", 1972,6,6,16,15,0) }),
                "D",
                RoseTree.Node("D", new [] {RoseTree.Node("1972-06", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("4"))})})
            };

            yield return new object[] {
                RoseTree.Node("S", new [] {
                    PhotoLeaf("L", 2002,10,12,17,16,15),
                    PhotoLeaf("J", 2007,4,21,17,18,19),
                }),
                "D",
                RoseTree.Node("D", new [] {
                    RoseTree.Node("2002-10", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("L"))}),
                    RoseTree.Node("2007-04", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("J"))}),
                })
            };

            yield return new object[] {
                RoseTree.Node("1", new [] {
                    PhotoLeaf("a", 2010,1,12,17,16,15),
                    PhotoLeaf("b", 2010,3,12,17,16,15),
                    PhotoLeaf("c", 2010,1,21,17,18,19),
                }),
                "2",
                RoseTree.Node("2", new [] {
                    RoseTree.Node("2010-01", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("a")), new RoseLeaf<string, FileInfo>(new FileInfo("c"))}),
                    RoseTree.Node("2010-03", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("b"))}),
                })
            };

            yield return new object[] {
                RoseTree.Node("foo", 
                    RoseTree.Node("bar",new [] {
                        PhotoLeaf("a", 2010,1,12,17,16,15),
                        PhotoLeaf("b", 2010,3,12,17,16,15),
                        PhotoLeaf("c", 2010,1,21,17,18,19),
                    }),
                    RoseTree.Node("baz",
                        PhotoLeaf("d", 2010,3,1,2,3,4),
                        PhotoLeaf("e", 2011,3,4,3,2,1)
                    )
                ),
                "qux",
                RoseTree.Node("qux", new [] {
                    RoseTree.Node("2010-01", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("a")), new RoseLeaf<string, FileInfo>(new FileInfo("c"))}),
                    RoseTree.Node("2010-03", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("b")), new RoseLeaf<string, FileInfo>(new FileInfo("d"))}),
                    RoseTree.Node("2011-03", new [] { new RoseLeaf<string, FileInfo>(new FileInfo("e"))}),
                })
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
