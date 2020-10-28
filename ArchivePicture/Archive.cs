using FPKatas.RoseTree;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ArchivePicture
{
    public struct Move
    {
        public FileInfo Source { get; set; }
        public FileInfo Destination { get; set; }
    }
    public class Archive
    {
        [Pure]
        public static IRoseTree<string, FileInfo> moveTo(string destination, IRoseTree<string, PhotoFile> t)
        {
            string dirNameOf(DateTime dt) => dt.ToString("yyyy-MM");


            Dictionary<string, IEnumerable<FileInfo>> groupByDir(Dictionary<string, IEnumerable<FileInfo>> m, PhotoFile pf)
            {
                var key = dirNameOf(pf.TakenOn);
                var exists = m.TryGetValue(key, out var item);
                IEnumerable<FileInfo> dir;
                if (exists)
                {
                    dir = item;
                    m[key] = dir.Concat(new List<FileInfo> { pf.File });
                } else
                {
                    m.Add(key, new List<FileInfo> { pf.File });
                }
                   
                return m;
            }

            List<IRoseTree<string, FileInfo>> addDir(IEnumerable<IRoseTree<string, FileInfo>> dirs, KeyValuePair<string,IEnumerable<FileInfo>> pair)
            {
                var name = pair.Key;
                var files = pair.Value;
                var branches = files.Select(f => RoseTree.Leaf<string,FileInfo>(f));

                return dirs.Concat(new[] { RoseTree.Node(name, branches.ToArray()) }).ToList();
            }

            var m = t.Fold((acc,x) => groupByDir(acc, x), new Dictionary<string, IEnumerable<FileInfo>>());

            var dirs = m.Aggregate(new List<IRoseTree<string, FileInfo>>(), addDir);
            return RoseTree.Node(destination, dirs.ToArray());
        }

        [Pure]
        public static IRoseTree<string, Move> calculateMoves(IRoseTree<string, FileInfo> tree)
        {
            FileInfo ReplaceDirectory(FileInfo f, string d)
                => new FileInfo(Path.Combine(d, f.Name));

            IRoseTree<string, Move> imp(string path, IRoseTree<string, FileInfo> tree)
                => tree.Match(
                    leaf: l => RoseTree.Leaf<string, Move>(new Move { Source = l, Destination = ReplaceDirectory(l, path) }),
                    node: (x, xs) =>
                    {
                        var newPath = Path.Combine(path, x);
                        return RoseTree.Node<string,Move>(newPath, xs.Select(t => imp(newPath, t)).ToArray());
                    });
            return imp("", tree);
        }


        
    }
}
