using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalReader.Models
{
    public class Football
    {
        public class Offense
        {
            // offense
            // rushing
            int RushAttempts { get; set; }
            int RushYards { get; set; }
            List<int> RushAtt { get; set; }

            int RushTd { get; set; }
            int Fumbles { get; set; }

            double RushAvg
            {
                get
                {
                    return Mean(RushAtt);
                }
            }

            double RushVariance
            {
                get
                {
                    return Variance(RushAtt, SumOfSquares(RushAtt));
                }
            }

            double RushStandardDeviation
            { // this finds population standard deviation, as it should
                get
                {
                    return StandardDeviation(SumOfSquares(RushAtt), RushAtt.Count);
                }
            }

            double RushMinStandardDeviation
            {
                get
                {
                    return this.RushAvg - this.RushStandardDeviation;
                }
            }

            double RushMaxStandardDeviation
            {
                get
                {
                    return this.RushAvg + this.RushStandardDeviation;
                }
            }


            // passing
            int PassCompletions { get; set; }
            int PassAttempts { get; set; }
            int PassYards { get; set; }
            int PassTd { get; set; }
            int PassInt { get; set; }

            int OffenseTurnovers
            {
                get
                {
                    return this.Fumbles + this.PassInt;
                }
            }
        }

        public class Defense
        {
            // not implemented yet
        }

        #region ## Calculators
        private static double Mean(this List<int> values)
        {
            return values.Sum() / values.Count;
        }

        private static int SumOfSquares(this List<int> values)
        {
            return values.Sum();
        }

        private static double Variance(this List<int> values, this int sumOfSquares)
        {
            return sumOfSquares / (values.Count - 1);
        }

        private static double StandardDeviation(this int sumOfSquares, this int count)
        {
            return Math.Sqrt(sumOfSquares / count);
        }
        #endregion
    }
}
