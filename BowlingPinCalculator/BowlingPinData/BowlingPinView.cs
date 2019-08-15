using BowlingPinCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BowlingPinCalculator.BowlingPinData
{
    public class BowlingPinViewModel
    {
        public IEnumerable<Player> players { get; set; }
        public HttpPostedFileBase scoreFile { get; set; }
    }
}