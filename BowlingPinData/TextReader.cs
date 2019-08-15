using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace BowlingPinData
{
    class TextReader
    {
        
        public string readFileReciveScore(HttpPostedFileBase file)
        {
            var sr = new StreamReader(file.inputStream);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Player player = new Player();
                player.name = sr.ReadLine();
                line = sr.ReadLine();
                player.points = line.Split((','), StringSplitOptions.RemoveEmptyEntries);
                player.players.Add(player);

            }
        }
    }
}
