using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticalReader.Extensions;

namespace StatisticalReader.Models
{
    public class GameStats
    {
        public RushStats RushingStats { get; set; }
        public PassStats PassingStats { get; set; }
        public Special SpecTeams { get; set; }

        public class RushStats
        {
            public List<int> ListRushAtt { get; set; }
            public int RushTd { get; set; }
            public int Fumbles { get; set; }
        }

        public class PassStats
        {
            public int PassAttempts { get; set; }
            public List<int> CompletedPasses { get; set; }
            public int PassTd { get; set; }
            public int PassInt { get; set; }
            public List<int> Sacks { get; set; }
        }

        public class Special
        {
            public List<int> KickReturnYards { get; set; }
            public List<int> PuntReturnYards { get; set; }
            public List<int> PuntYards { get; set; }
            public List<int> FgMade { get; set; }
            public List<int> FgMissed { get; set; }
            public List<int> KickoffLength { get; set; }
            public List<int> StartingPoint { get; set; }
        }

    }
}
