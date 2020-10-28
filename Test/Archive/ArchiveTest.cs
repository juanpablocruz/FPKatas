using ArchivePicture;
using FPKatas.RoseTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Test.ArchiveTest
{
    public class ArchiveTest
    {
        [Theory]
        [ClassData(typeof(MoveToDestinationTestData))]
        public void MoveToDestination(IRoseTree<string, PhotoFile> source, string destination, IRoseTree<string, FileInfo> expected)
        {
            var actual = Archive.moveTo(destination, source);
            var expectedVal = expected.Select(i => i.ToString());
            var actualVal = actual.Select(i => i.ToString());
            Assert.Equal(expectedVal, actualVal);
        }

        [Theory]
        [ClassData(typeof(CalculateMovesTestData))]
        public void CalculateMoves(IRoseTree<string, FileInfo> source, IRoseTree<string, Move> expected)
        {
            var actual = Archive.calculateMoves(source);
            var expectedVal = expected.Select(i => (i.Source.ToString(), i.Destination.ToString()));
            var actualVal = actual.Select(i => (i.Source.ToString(), i.Destination.ToString()));
            Assert.Equal(expectedVal, actualVal);
        }
    }
}
