using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatisticalReader.Models;
using System.Collections.Generic;

namespace StatisticalReaderTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRushAverage()
        {
            var stats = new Football();
            stats.ListRushAtt = new List<int>{5, 10, 15, 20, 25};

            Assert.AreEqual(15.0, stats.RushAvg);
            Assert.AreEqual(50.0, stats.RushVariance);
            Assert.AreEqual(7.0710678118654755, stats.RushStandardDeviation);
            

        }
    }
}
