﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalReader.Extensions
{
    public static class MathCalculations
    {
        public static double Mean(this List<int> values)
        {
            double sum = 0;
            foreach(var value in values)
            {
                sum += value;
            }
            return sum / values.Count;
        }

        public static double Variance(this List<int> values)
        {
            double variance = 0;
            foreach (var value in values)
            {
                variance += Math.Pow((value - values.Mean()), 2);
            }
            return variance / values.Count;
        }

        public static double StandardDeviation(this List<int> values)
        {
            return Math.Sqrt(values.Variance());
        }
    }
}
