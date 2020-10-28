using FPKatas.RoseTree;
using SGS.Framework.FunK;
using System;
using System.IO;
using System.Linq;
using Unit = System.ValueTuple;
namespace ArchivePicture
{
    using static F;

    public struct PhotoFile
    {
        public FileInfo File { get; set; }
        public DateTime TakenOn { get; set; }
    }
    public class Program
    {
        // Impure
        public static IRoseTree<string, string> readTree(string path)
        {
            if (File.Exists(path)) {
                return new RoseLeaf<string, string>(path);
            } else
            {
                var dirsAndFiles = Directory.EnumerateFileSystemEntries(path);
                var branches = dirsAndFiles.Select(readTree).ToArray();
                return RoseTree.Node(path, branches);
            }
        }

        public static Maybe<PhotoFile> readPhoto(FileInfo file)
        {
            var dt = Photo.extractDateTaken(file);
            var photo = dt.Map(dateTaken => new PhotoFile { File = file, TakenOn = dateTaken });
            return photo;
        }

        public static Unit writeTree(IRoseTree<string,Move> tree)
        {
            void copy(Move m)
            {
                Directory.CreateDirectory(m.Destination.DirectoryName);
                m.Source.CopyTo(m.Destination.FullName);
                Console.WriteLine($"Copied to {m.Destination.FullName}");
            }

            bool compareFiles(Move m)
            {
                var sourceStream = File.ReadAllBytes(m.Source.FullName);
                var destinationStream = File.ReadAllBytes(m.Destination.FullName);
                return sourceStream == destinationStream;
            }

            Unit move(Move m)
            {
                copy(m);
                if (compareFiles(m))
                {
                    m.Source.Delete();
                }
                return new Unit();
            }

            tree.Iter(move);
            return Unit();
        }

        public static Unit movePhotos(string source, string destination)
        {
            var sourceTree = readTree(source).MapLeaf(l => new FileInfo(l));
            var photoTree = sourceTree.Choose(readPhoto);

            var destinationTree = photoTree.Map(t => {
                var moves = Archive.moveTo(destination, t);
                return Archive.calculateMoves(moves);
                });

            return destinationTree.Match(
                Nothing: () => new Unit(),
                Just: t => writeTree(t)
            );
        }

        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                var source = args[0];
                var destination = args[1];

                movePhotos(source, destination);
            } else
            {
                Console.WriteLine("Please provide source and destination directories as arguments.");
            }
        }
    }
}
