using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    public class AutoOpsEntry
    {
        public AutoOpsEntry()
        {

        }

        private string shareName;
        private int shareNum;
        private string ramAt;
        private string tradeInfo;
        private bool calculated;
        private bool overviewed;
        private bool allTabled;

        public string ShareName { get => shareName; set => shareName = value; }
        public int ShareNum { get => shareNum; set => shareNum = value; }
        public string RamAt { get => ramAt; set => ramAt = value; }
        public string TradeInfo { get => tradeInfo; set => tradeInfo = value; }
        public bool Calculated { get => calculated; set => calculated = value; }
        public bool Overviewed { get => overviewed; set => overviewed = value; }
        public bool AllTabled { get => allTabled; set => allTabled = value; }
    }
}
