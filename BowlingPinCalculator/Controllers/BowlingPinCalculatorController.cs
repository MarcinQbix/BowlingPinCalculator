using BowlingPinCalculator.BowlingPinData;
using BowlingPinCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BowlingPinCalculator.Controllers
{
    public class BowlingPinCalculatorController : Controller
    {
        FileInputToText _fileInputToText;
        
        public BowlingPinCalculatorController(FileInputToText fileInput)
        {
            _fileInputToText = fileInput;
           
        }
        // GET: BowlingPinCalculator
        public ActionResult Index() {
            BowlingPinViewModel bowlingPinViewModel= new BowlingPinViewModel();
            return View(bowlingPinViewModel);
        }

       
        [HttpPost]
        public ActionResult Index(BowlingPinViewModel bowlingPinViewModel)
        {
            string recivedFile = _fileInputToText.reciveATextFile(bowlingPinViewModel.scoreFile);
            bowlingPinViewModel.players = readFileReciveScoreAndNames(recivedFile);
            return View(bowlingPinViewModel);
        }
        // getting a score and name from scorefile posted through website, parsing score and putting into method to count points
        private IEnumerable<Player> readFileReciveScoreAndNames(string scoreFile)
        {

            List<Player> players = new List<Player>();
            string[] record = scoreFile.Split(
            new[] { Environment.NewLine },
    StringSplitOptions.RemoveEmptyEntries);
           // int hit;
            for (int i = 0; i < record.Length; i += 2)
            {
                int[]points = scoreStringArraytoInt(record[i+1]);
                Player player = new Player();
                player.name = record[i];
                player.points = points;
                player.score = countPoints(points);
                players.Add(player);
            }
            //  StreamReader sr = new StreamReader(redivedFile);
            ////*  while ((sr.ReadLine()) != null)
            //  {
            //      Player player = new Player();
            //      line = sr.ReadLine();
            //      var stringScore = line.Split(new[] { ',' }
            //          , StringSplitOptions.RemoveEmptyEntries);
            //      //parasing a string score to int score 
            //      player.points = scoreStringArraytoInt(stringScore);

            //      //counting a score 
            //     player.score= countPoints(player.points);
            //      player.name = sr.ReadLine();
            //      players.Add(player);

            //  }
            ////  return players;

            return players;
        }

        private int [] scoreStringArraytoInt(string scoreStringArray)
        {
            int hit;
            var arrayOfStrings = scoreStringArray.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] arrayIntScore = new int[arrayOfStrings.Length];
            for (int i = 0; i <arrayIntScore.Length; i++)
            {

                int.TryParse(arrayOfStrings[i], out hit);
                arrayIntScore[i] = hit ;
            }
            return arrayIntScore;
        }
        private int countPoints(int[] scoreTable)
        {
            int round = 0;
            int score = 0;
            int maxThrowCount = 20;
            int lastRoundThrow=0;
            int attempNumber = 1;
           
            for (int i = 0; i < maxThrowCount; i ++)
            {
               
                int yourThrow = scoreTable[i];
                if (yourThrow == 10)
                {
                    round++;
                    score += yourThrow + scoreTable[i + 1] + scoreTable[i + 2];
                   
                    if (round==10)
                    {
                        //checking for last round if strike
                        return score;
                    }
                }
                else
                {
                    score += yourThrow;
                    
                    //checking if strike

                    if (round == 10)
                    {
                        //checking for last round 
                        return score;
                    }
                    if (attempNumber == 1)
                    {
                        lastRoundThrow = yourThrow;
                    }
                    

                }
                if (attempNumber == 2)
                {
                    round++;
                    if (yourThrow != 10 && yourThrow + lastRoundThrow == 10)
                    {
                        score += scoreTable[i + 1];
                        
                    }
                    if (round == 10)
                    {
                        return score;
                    }
                    attempNumber = 1;
                    

                }
                else if(yourThrow!=10&& attempNumber==1)
                {
                    attempNumber++;
                }


            }
            return score;
        }
    }
}