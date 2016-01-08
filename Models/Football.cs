using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticalReader.Extensions;

namespace StatisticalReader.Models
{
    public class Football
    {
        // offense
        // rushing
        public List<int> ListRushAtt { get; set; }

        public int RushTd { get; set; }
        public int Fumbles { get; set; }

        public double RushAvg
        {
            get
            {
                return ListRushAtt.Mean();
            }
        }

        public double RushVariance
        {
            get
            {
                return ListRushAtt.Variance();
            }
        }

        public double RushStandardDeviation
        { 
            get
            {
                return ListRushAtt.StandardDeviation();
            }
        }

        public double RushMinStandardDeviation
        {
            get
            {
                return this.RushAvg - this.RushStandardDeviation;
            }
        }

        public double RushMaxStandardDeviation
        {
            get
            {
                return this.RushAvg + this.RushStandardDeviation;
            }
        }

        // passing
        public int PassCompletions { get; set; }
        public int PassAttempts { get; set; }
        public int PassYards { get; set; }
        public int PassTd { get; set; }
        public int PassInt { get; set; }

        public int Turnovers
        {
            get
            {
                return this.Fumbles + this.PassInt;
            }
        }
    }
}
