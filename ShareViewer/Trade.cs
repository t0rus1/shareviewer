using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShareViewer
{
    internal class Trade
    {
        //class which holds share trades, each instance of which is built
        //during a traversal of a day-data file e.g.
        /*
         *  ...
         *  ...
            WERTPAPIER;09.07.2018;B+S BANKSYSTEME AG O.N.;126215.FFM
            08:14:25;4,69;0;0
            09:25:22;4,7;14;14
            12:19:35;4,7;230;244
            WERTPAPIER;09.07.2018;ALTRIA GRP INC.DL-,333;200417.ETR
            09:04:05;49,64;40;40
            ...
            ...
        */

        internal Trade(int shareNum, string tradeDate, string ticker)
        {
            //we compute everything we initially need in the constructor upfront
            this.shareNum = shareNum;
            this.tradeDate = tradeDate;
            Match regMatch = Regex.Match(ticker, @"((\d{2}):(\d{2}):(\d{2}));([\d,]+);(\d+);(\d+)");
            this.good = regMatch.Success;
            if (this.good)
            {
                this.price = Convert.ToDouble(regMatch.Groups[5].Value);
                this.volume = Convert.ToUInt32(regMatch.Groups[6].Value);
                this.cumVolume = Convert.ToUInt32(regMatch.Groups[7].Value);
                int hr, min, sec, totalsecs; ;
                hr = Convert.ToInt16(regMatch.Groups[2].Value);
                min = Convert.ToInt16(regMatch.Groups[3].Value);
                sec = Convert.ToInt16(regMatch.Groups[4].Value);
                totalsecs = 3600 * hr + 60 * min + sec;
                //compute timeband (timband 1 is at 09:00:00 - 09:04:59
                //the timebands in practice run from 1 to 104 each day 
                this.band = (totalsecs / (9 * 3600)) + (totalsecs % (9 * 3600))/300;
                //A Trade object get instantiated with this single ticker, but subsequently this Trade
                //is held in a hash for a single share-date-band combo, and additional tickers may be added 
                //to this Trade object's tickers List as price and volume get adjusted
                tickers = new List<string>();
                tickers.Add(ticker); // for AUDIT purposes
            }
            else
            {
                this.band = 0;
                this.price = 0;
                this.volume = 0;
                this.cumVolume = 0;
            }
        }

        private int shareNum;
        private string tradeDate;
        private bool good;
        private double price;
        private UInt32 volume;
        private UInt32 cumVolume;
        private int band;
        private List<String> tickers;

        internal int ShareNum { get => shareNum; }
        internal string TradeDate { get => tradeDate; }
        internal bool Good { get => good; }
        internal double Price { get => price; set => price = value; }
        internal UInt32 Volume { get => volume; set => volume = value; }
        internal UInt32 CumVolume { get => cumVolume; }
        internal int Band { get => band; set => band = value; }
        internal List<string> Tickers { get => tickers; set => tickers = value; }

        public override String ToString()
        {
            return $"{shareNum},{tradeDate},{band},{price},{volume}";
        }

        internal string AllTickers()
        {
            return String.Join("\r\n", tickers);
            
        }

    }
}
