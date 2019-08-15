using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BowlingPinCalculator.BowlingPinData
{
    public class FileInputToText
    {
        public string reciveATextFile(HttpPostedFileBase scorefile)
        {
            string scoreStringText;
            using (StreamReader reader = new StreamReader(scorefile.InputStream))
            {
                scoreStringText = reader.ReadToEnd();
            }
            return scoreStringText;
        }
    }
}