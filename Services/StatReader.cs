using StatisticalReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StatisticalReader.Services
{
    public class StatReader
    {
        private StreamReader _fileReader;
        private string _homeTeam;
        private string _awayTeam;

        public StatReader(string fileLocation, string homeTeam, string awayTeam)
        {
            _fileReader = new StreamReader(File.OpenRead(fileLocation));
            _homeTeam = homeTeam;
            _awayTeam = awayTeam;
        }

        public string ReadInGame()
        {
            var homeOffense = InitializeGameModel();
            var homeDefense = InitializeGameModel();
            var awayOffense = InitializeGameModel();
            var awayDefense = InitializeGameModel();

            var awayPlayers = _fileReader.ReadLine().Split(',');
            var homePlayers = _fileReader.ReadLine().Split(',');

            while (!_fileReader.EndOfStream)
            {
                var line = _fileReader.ReadLine();
                var values = line.Split(',');
                var statLine = values[5].ToLower();
                
                // determine which team to account play for
                bool awayPlay = false;
                bool homePlay = false;

                foreach (var player in awayPlayers)
                {
                    if (statLine.Contains(player.ToLower().Trim()))
                    {
                        awayPlay = true;
                    }
                }

                foreach (var player in homePlayers)
                {
                    if (statLine.Contains(player.ToLower().Trim()))
                    {
                        homePlay = true;
                    }
                }

                // determine play and value
                int playYardage = GetStatValue(statLine);
                bool touchdown = statLine.Contains("touchdown");
                bool interception = statLine.Contains(" is intercepted by ");
                bool fumble = statLine.Contains("fumbles");
                var playType = DeterminePlay(statLine);

                if (playType == "rush")
                {
                    if (awayPlay)
                    {
                        awayOffense.RushingStats.ListRushAtt.Add(playYardage);
                        if (touchdown)
                        {
                            awayOffense.RushingStats.RushTd++;
                        }
                        if (fumble)
                        {
                            awayOffense.RushingStats.Fumbles++;
                        }
                    }
                    if (homePlay)
                    {
                        homeOffense.RushingStats.ListRushAtt.Add(playYardage);
                        if (touchdown)
                        {
                            homeOffense.RushingStats.RushTd++;
                        }
                        if (fumble)
                        {
                            homeOffense.RushingStats.Fumbles++;
                        }
                    }
                }

                if(playType == "pass")
                {
                    if(awayPlay)
                    {
                        if (statLine.Contains(" complete"))
                        {
                            awayOffense.PassingStats.CompletedPasses.Add(playYardage);
                            awayOffense.PassingStats.PassAttempts++;
                            if (statLine.Contains("touchdown"))
                            {
                                awayOffense.PassingStats.PassTd++;
                            }
                        }
                        else
                        {
                            awayOffense.PassingStats.PassAttempts++;
                            if (statLine.Contains(" is intercepted by "))
                            {
                                awayOffense.PassingStats.PassInt++;
                            }
                        }
                        if(fumble)
                        {
                            awayOffense.RushingStats.Fumbles++;
                        }
                    }
                    if(homePlay)
                    {
                        if (statLine.Contains(" complete"))
                        {
                            homeOffense.PassingStats.CompletedPasses.Add(playYardage);
                            homeOffense.PassingStats.PassAttempts++;
                            if (statLine.Contains("touchdown"))
                            {
                                homeOffense.PassingStats.PassTd++;
                            }
                        }
                        else
                        {
                            homeOffense.PassingStats.PassAttempts++;
                            if (statLine.Contains(" is intercepted by "))
                            {
                                homeOffense.PassingStats.PassInt++;
                            }
                        }
                        if (fumble)
                        {
                            homeOffense.RushingStats.Fumbles++;
                        }
                    }
                }

                if(playType == "other")
                {
                    var x = 1;
                }
            }

            var returnString = "";
            returnString = "Away Passing: " + awayOffense.PassingStats.CompletedPasses.Count + "-" + awayOffense.PassingStats.PassAttempts + " for " + awayOffense.PassingStats.CompletedPasses.Sum() + " yards, " + awayOffense.PassingStats.PassTd + " TD, " + awayOffense.PassingStats.PassInt + " Int.\n";
            returnString += "Home Passing: " + homeOffense.PassingStats.CompletedPasses.Count + "-" + homeOffense.PassingStats.PassAttempts + " for " + homeOffense.PassingStats.CompletedPasses.Sum() + " yards, " + homeOffense.PassingStats.PassTd + " TD, " + homeOffense.PassingStats.PassInt + " Int.\n";
            returnString += "Away Rushing: " + awayOffense.RushingStats.ListRushAtt.Count + "-" + awayOffense.RushingStats.ListRushAtt.Sum() + " " + awayOffense.RushingStats.RushTd + " TD \n";
            returnString += "Home Rushing: " + homeOffense.RushingStats.ListRushAtt.Count + "-" + homeOffense.RushingStats.ListRushAtt.Sum() + " " + homeOffense.RushingStats.RushTd + " TD \n";
            returnString += "Away Fumbles: " + awayOffense.RushingStats.Fumbles + " Int: " + awayOffense.PassingStats.PassInt + "\n";
            returnString += "Home Fumbles: " + homeOffense.RushingStats.Fumbles + " Int: " + homeOffense.PassingStats.PassInt + "\n";


            return returnString;
        }

        private string DeterminePlay(string statLine)
        {
            if (statLine.Contains(" pass "))
            {
                return "pass";
            }

            if (statLine.Contains(" left tackle for ") || statLine.Contains(" middle for ") || statLine.Contains(" right tackle for ") ||
                statLine.Contains(" left end for ") || statLine.Contains(" right end for ") || statLine.Contains(" for no gain") ||
                statLine.Contains(" left guard for ") || statLine.Contains(" right guard for "))
            {
                return "rush";
            }

            return "other";
        }

        private int GetStatValue(string statLine)
        {
            var yardage = 0;
            try
            {
                yardage = Convert.ToInt32(Regex.Match(statLine, @"\d+").Value);
            }
            catch(FormatException er)
            {
                yardage = 0;
            }
            return yardage;
        }

        private GameStats InitializeGameModel()
        {
            var returnModel = new GameStats
            {
                PassingStats = new GameStats.PassStats
                {
                    CompletedPasses = new List<int>(),
                    PassAttempts = 0,
                    PassInt = 0,
                    PassTd = 0,
                    Sacks = new List<int>()
                },
                RushingStats = new GameStats.RushStats
                {
                    ListRushAtt = new List<int>(),
                    RushTd = 0,
                    Fumbles = 0
                },
                SpecTeams = new GameStats.Special { 
                    FgMade = new List<int>(),
                    FgMissed = new List<int>(),
                    KickoffLength = new List<int>(),
                    KickReturnYards = new List<int>(),
                    PuntReturnYards = new List<int>(),
                    PuntYards = new List<int>(),
                    StartingPoint = new List<int>(),
                }
            };
            return returnModel;
        }
    }
}
