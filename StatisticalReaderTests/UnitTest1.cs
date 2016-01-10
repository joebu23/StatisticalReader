using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatisticalReader.Models;
using StatisticalReader.Extensions;
using System.Collections.Generic;

namespace StatisticalReaderTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMath()
        {
            var stats = new GameStats();
            stats.RushingStats.ListRushAtt = new List<int>{5, 10, 15, 20, 25};

            Assert.AreEqual(15.0, stats.RushingStats.ListRushAtt.Mean());
            Assert.AreEqual(50.0, stats.RushingStats.ListRushAtt.Variance());
            Assert.AreEqual(7.0710678118654755, stats.RushingStats.ListRushAtt.StandardDeviation());
        }
    }
}
