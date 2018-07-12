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
        internal Trade(int shareNum, string tradeDate, string ticker)
        {
            this.shareNum = shareNum;
            this.tradeDate = tradeDate;
            Match regMatch = Regex.Match(ticker, @"((\d{2}):(\d{2}):(\d{2}));([\d,]+);(\d+);(\d+)");
            this.good = regMatch.Success;
            if (this.good)
            {
                this.price = Convert.ToDouble(regMatch.Groups[5].Value);
                this.volume = Convert.ToUInt32(regMatch.Groups[6].Value);
                this.cumVolume = Convert.ToUInt32(regMatch.Groups[7].Value);
            }
            else
            {
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

        internal int ShareNum { get => shareNum; }
        internal string TradeDate { get => tradeDate; }
        internal bool Good { get => good; }
        internal double Price { get => price; }
        internal UInt32 Volume { get => volume; }
        internal UInt32 CumVolume { get => cumVolume; }

    }
}
